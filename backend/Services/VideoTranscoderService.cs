using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using backend.Data;
using backend.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace backend.Services
{
    public class VideoTranscoderService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<VideoTranscoderService> _logger;

        public VideoTranscoderService(
            IServiceProvider serviceProvider,
            ILogger<VideoTranscoderService> logger
        )
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var storageService = scope.ServiceProvider.GetRequiredService<IStorageService>();

                    var pendingAsset = await dbContext.TaiNguyenSos
                        .Where(a => a.TrangThai == "Processing" && a.LoaiTep == "Video")
                        .FirstOrDefaultAsync(stoppingToken);

                    if (pendingAsset != null)
                    {
                        var assetId = pendingAsset.MaTaiNguyen;

                        // Nếu file gốc không còn tồn tại -> đánh dấu Failed
                        if (string.IsNullOrEmpty(pendingAsset.OriginalFilePath) || !File.Exists(pendingAsset.OriginalFilePath))
                        {
                            _logger.LogWarning($"Original file not found for asset {assetId}. Marking as Failed.");
                            pendingAsset.TrangThai = "Failed";
                            await dbContext.SaveChangesAsync(CancellationToken.None);
                            continue;
                        }

                        _logger.LogInformation($"Starting MP4 upload for: {assetId}");

                        try
                        {
                            var extension = Path.GetExtension(pendingAsset.OriginalFilePath); // e.g., .mp4
                            var fileKey = $"videos/{assetId}/video{extension}";

                            using (var fs = new FileStream(pendingAsset.OriginalFilePath, FileMode.Open, FileAccess.Read))
                            {
                                await storageService.UploadFileAsync(fs, fileKey, pendingAsset.KieuMIME ?? "video/mp4");
                            }

                            // Cleanup local temp file
                            File.Delete(pendingAsset.OriginalFilePath);

                            // Update DB
                            pendingAsset.TrangThai = "Ready";
                            pendingAsset.DuongDanLuuTru = fileKey;
                            await dbContext.SaveChangesAsync(CancellationToken.None);

                            _logger.LogInformation($"Uploaded MP4 to R2 successfully: {assetId}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Failed to upload video {assetId} to R2.");
                            pendingAsset.TrangThai = "Failed";
                            await dbContext.SaveChangesAsync(CancellationToken.None);
                        }
                    }
                    else
                    {
                        // Không có video nào cần xử lý, chờ 10 giây rồi quét lại
                        await Task.Delay(10000, stoppingToken);
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Video Upload Service is stopping...");
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fatal Error in Video Upload Background Service.");
                    try { await Task.Delay(15000, stoppingToken); }
                    catch (OperationCanceledException) { return; }
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repository.Services
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<DanhMuc>> GetGroupsAsync();
        Task<DanhMuc?> GetGroupByIdAsync(int id);
        Task<IEnumerable<DanhMuc>> GetSubCategoriesAsync(int? parentId = null);
        Task<DanhMuc?> GetSubCategoryByIdAsync(int id);
        Task<bool> AddCategoryAsync(DanhMuc category);
        Task<bool> UpdateGroupAsync(DanhMuc group);
        Task<bool> HardDeleteGroupAsync(int id);
        Task<bool> ToggleStatusAsync(int id);
        Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null);
        Task<bool> HasChildrenAsync(int groupId);
        Task<bool> HasCoursesAsync(int groupId);
    }
}

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const goBack = () => {
  // If user came from certificate list, we could use router.back(), else route to courses
  router.push('/certificates');
}

const copyToClipboard = (text) => {
  // Simulate copy effect
  alert('Đã sao chép: ' + text);
}
</script>

<template>
  <div class="min-h-screen bg-slate-50 font-plus-jakarta flex flex-col antialiased">
    <!-- Navbar -->
    <header class="bg-white border-b border-slate-200 px-6 py-4 flex flex-col sm:flex-row justify-between items-start sm:items-center sticky top-0 z-10">
      <div class="flex items-center gap-4 mb-4 sm:mb-0">
        <div class="w-12 h-12 bg-blue-600 rounded-xl flex items-center justify-center text-white shrink-0 shadow-md">
          <span class="material-symbols-outlined text-[24px]">workspace_premium</span>
        </div>
        <div>
          <h1 class="text-xl font-extrabold text-slate-900 tracking-tight">Chứng chỉ của bạn</h1>
          <p class="text-xs text-slate-500 font-medium">Quản lý và chia sẻ thành tựu học tập của bạn tại AET LMS</p>
        </div>
      </div>
      <button @click="goBack" class="text-blue-700 hover:bg-blue-50 font-semibold px-4 py-2 rounded-lg flex items-center gap-2 transition-colors text-sm">
        <span class="material-symbols-outlined text-[18px]">arrow_back</span> Trở lại danh sách
      </button>
    </header>

    <!-- Main Content -->
    <main class="flex-1 max-w-[1400px] w-full mx-auto px-6 py-8 grid grid-cols-1 lg:grid-cols-12 gap-8">
      
      <!-- Left Column: Certificate Preview -->
      <div class="lg:col-span-8 flex flex-col">
        <div class="bg-white p-8 md:p-14 shadow-sm relative w-full aspect-[1.414/1] flex flex-col items-center justify-center border border-slate-100/50">
          
          <!-- Decorative Corners -->
          <!-- Top Left -->
          <div class="absolute top-6 left-6 w-8 h-8 border-t-[3px] border-l-[3px] border-blue-600/60 rounded-tl-sm"></div>
          <!-- Top Right -->
          <div class="absolute top-6 right-6 w-8 h-8 border-t-[3px] border-r-[3px] border-blue-600/60 rounded-tr-sm"></div>
          <!-- Bottom Left -->
          <div class="absolute bottom-6 left-6 w-8 h-8 border-b-[3px] border-l-[3px] border-blue-600/60 rounded-bl-sm"></div>
          <!-- Bottom Right -->
          <div class="absolute bottom-6 right-6 w-8 h-8 border-b-[3px] border-r-[3px] border-blue-600/60 rounded-br-sm"></div>

          <!-- Watermark Background -->
          <div class="absolute inset-0 flex flex-col items-center justify-center opacity-5 pointer-events-none">
             <div class="w-64 h-64 bg-slate-900 rotate-45 transform scale-150 relative"></div>
          </div>

          <!-- Content Wrapper -->
          <div class="relative z-10 w-full flex flex-col items-center text-center px-4">
            <h4 class="text-blue-700 font-bold uppercase tracking-[0.3em] mb-4 md:mb-8 text-sm md:text-base">AET LMS</h4>
            <h2 class="text-3xl md:text-5xl font-black text-slate-900 mb-6 md:mb-10 tracking-tight">Chứng Nhận Hoàn Thành</h2>
            
            <p class="text-slate-500 italic mb-4 md:mb-6 text-sm md:text-lg font-serif">Chứng nhận này được trao cho</p>
            <h1 class="text-4xl md:text-[3.5rem] font-black text-blue-700 mb-6 md:mb-10 leading-none">Nguyễn Văn A</h1>
            
            <div class="w-64 h-px bg-slate-200 mb-6 md:mb-8"></div>
            
            <p class="text-slate-500 text-sm md:text-base mb-2 font-medium">Vì đã hoàn thành xuất sắc khóa học trực tuyến</p>
            <h3 class="text-xl md:text-2xl font-bold text-slate-900 mb-6 md:mb-8 text-balance">V-Core AI: Trí tuệ Việt cho quản trị số</h3>
            
            <p class="text-slate-600 font-medium text-sm md:text-base">Ngày cấp: <span class="font-bold text-slate-900">26/03/2026</span></p>

            <div class="w-full mt-10 md:mt-16 flex justify-between px-4 md:px-12 items-end">
              <div class="text-center font-bold text-slate-700 text-xs md:text-sm uppercase tracking-wider relative pt-8 border-t border-slate-800 w-40">
                Giám đốc học thuật
              </div>
              
              <!-- Badge / Stamp Box -->
              <div class="w-20 h-20 bg-blue-50/50 rounded-full border border-blue-100 flex items-center justify-center absolute left-1/2 -translate-x-1/2 bottom-0 opacity-80 backdrop-blur-sm -mb-6 md:-mb-10">
                <span class="material-symbols-outlined text-blue-200 text-4xl">verified</span>
              </div>

              <div class="text-center font-bold text-slate-700 text-xs md:text-sm uppercase tracking-wider relative pt-8 border-t border-slate-800 w-40">
                Hội đồng khảo thí
              </div>
            </div>
          </div>
        </div>

        <!-- Note below certificate -->
        <div class="mt-4 flex items-start gap-2 text-xs text-slate-500">
          <span class="material-symbols-outlined text-[16px] text-slate-400 shrink-0">info</span>
          <p>Chứng chỉ này được cấp chính thức bởi hệ thống AET LMS và có giá trị xác thực trên toàn hệ thống.</p>
        </div>
      </div>

      <!-- Right Column: Sidebar Widgets -->
      <div class="lg:col-span-4 flex flex-col gap-6">
        
        <!-- Widget: Tools -->
        <div class="bg-white rounded-2xl p-6 shadow-sm border border-slate-100">
          <h3 class="font-extrabold text-slate-900 mb-5 text-lg">Công cụ chứng chỉ</h3>
          <button class="w-full bg-[#0935a4] hover:bg-[#072573] text-white font-bold py-3.5 px-4 rounded-xl flex items-center justify-center gap-2 transition-colors mb-3 shadow-sm">
            <span class="material-symbols-outlined text-[20px]">download</span> Tải chứng chỉ (PDF)
          </button>
          <div class="flex gap-3">
            <button class="flex-1 bg-slate-50 border border-slate-200 hover:bg-slate-100 text-slate-700 font-bold py-3 rounded-xl flex items-center justify-center gap-2 text-sm transition-colors">
              <span class="material-symbols-outlined text-[18px]">share</span> LinkedIn
            </button>
            <button @click="copyToClipboard('https://aetlms.com/cert/123')" class="flex-1 bg-slate-50 border border-slate-200 hover:bg-slate-100 text-slate-700 font-bold py-3 rounded-xl flex items-center justify-center gap-2 text-sm transition-colors">
              <span class="material-symbols-outlined text-[18px]">content_copy</span> Copy Link
            </button>
          </div>
        </div>

        <!-- Widget: Verification -->
        <div class="bg-white rounded-2xl p-6 shadow-sm border border-slate-100">
          <div class="flex items-center gap-2 mb-6">
            <div class="w-6 h-6 rounded-full bg-blue-100 text-blue-700 flex items-center justify-center shrink-0">
               <span class="material-symbols-outlined text-[14px]">verified_user</span>
            </div>
            <h3 class="font-extrabold text-slate-900 text-lg">Xác thực chứng chỉ</h3>
          </div>
          
          <div class="mb-5">
            <p class="text-[10px] font-bold text-slate-500 uppercase tracking-widest mb-2">Mã định danh (UUID)</p>
            <div class="bg-slate-50 border border-slate-200 rounded-lg p-3 flex justify-between items-center group cursor-pointer hover:bg-slate-100 transition" @click="copyToClipboard('8f9b2a1c...4f5a6b7c')">
              <span class="text-blue-700 font-mono text-sm">8f9b2a1c...4f5a6b7c</span>
              <span class="material-symbols-outlined text-[16px] text-slate-400 group-hover:text-slate-700">content_copy</span>
            </div>
          </div>

          <div>
            <p class="text-[10px] font-bold text-slate-500 uppercase tracking-widest mb-2">Đường dẫn xác minh</p>
            <p class="text-[11px] text-slate-500 leading-relaxed font-mono break-all bg-white overflow-hidden">
               verify.aetlms.com/c/8f9b2a1c-4e5d-4f8a-9c1b-2d3e4f5a6b7c
            </p>
          </div>
        </div>

        <!-- Widget: Other Certificates -->
        <div class="bg-white rounded-2xl p-6 shadow-sm border border-slate-100">
          <h3 class="font-extrabold text-slate-900 mb-5 text-lg">Các chứng chỉ khác</h3>
          <div class="flex flex-col gap-4">
             <div class="flex items-start gap-4 p-2 -mx-2 rounded-lg hover:bg-slate-50 transition cursor-pointer">
                <div class="w-10 h-10 bg-slate-100 border border-slate-200 rounded-lg flex items-center justify-center shrink-0 text-slate-600">
                   <span class="material-symbols-outlined text-[18px]">history_edu</span>
                </div>
                <div>
                   <h4 class="font-bold text-slate-900 text-sm leading-snug break-words">Xây dựng văn hóa doanh nghiệp</h4>
                   <p class="text-[10px] uppercase font-bold text-slate-500 tracking-wider mt-1">Cấp ngày: 12/01/2026</p>
                </div>
             </div>

             <div class="flex items-start gap-4 p-2 -mx-2 rounded-lg hover:bg-slate-50 transition cursor-pointer">
                <div class="w-10 h-10 bg-slate-100 border border-slate-200 rounded-lg flex items-center justify-center shrink-0 text-slate-600">
                   <span class="material-symbols-outlined text-[18px]">history_edu</span>
                </div>
                <div>
                   <h4 class="font-bold text-slate-900 text-sm leading-snug break-words">Quản trị nhân sự cơ bản</h4>
                   <p class="text-[10px] uppercase font-bold text-slate-500 tracking-wider mt-1">Cấp ngày: 05/11/2025</p>
                </div>
             </div>
          </div>
        </div>

      </div>
    </main>

    <!-- Footer Simple -->
    <footer class="mt-auto px-6 py-8 border-t border-slate-200 lg:px-12 flex flex-col md:flex-row items-center justify-between text-xs font-medium text-slate-500">
      <p>© 2026 AET LMS Learning Portal. All rights reserved.</p>
      <div class="flex gap-6 mt-4 md:mt-0 bg-transparent">
        <a href="#" class="hover:text-slate-800 transition">Điều khoản dịch vụ</a>
        <a href="#" class="hover:text-slate-800 transition">Chính sách bảo mật</a>
        <a href="#" class="hover:text-slate-800 transition">Hỗ trợ học viên</a>
      </div>
    </footer>
  </div>
</template>

<style scoped>
/* Any custom styles for certificate */
@import url('https://fonts.googleapis.com/css2?family=Playfair+Display:ital,wght@0,400;0,700;1,400&display=swap');

.font-serif {
  font-family: 'Playfair Display', serif;
}
</style>

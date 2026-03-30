<script setup>
import { ref } from 'vue';
import LayoutStudent from '@/layouts/LayoutStudent.vue';

// State for toast
const showToast = ref(true);

const filterYear = ref('Tất cả');
const filterCourseType = ref('Tất cả');

</script>

<template>
  <LayoutStudent>
    <div class="bg-slate-50 min-h-screen font-plus-jakarta antialiased pt-12 pb-24 border-t border-surface-container">
      <div class="max-w-[1200px] mx-auto px-6 relative">
      
        <!-- Header & Filters -->
        <div class="flex flex-col md:flex-row justify-between items-start md:items-end mb-10 gap-6">
          <div>
            <h2 class="text-[10px] uppercase font-bold text-primary tracking-widest mb-3">HỌC TẬP & PHÁT TRIỂN</h2>
            <h1 class="text-4xl font-extrabold text-slate-900 mb-3 tracking-tight">Chứng chỉ của tôi</h1>
            <p class="text-slate-600 max-w-md leading-relaxed text-[13px] font-medium">
              Ghi nhận những nỗ lực và thành tựu xuất sắc của bạn trong hành trình nâng cao kỹ năng tại Aura Learning System.
            </p>
          </div>
          
          <!-- Filters -->
          <div class="flex gap-3">
            <button class="bg-white border border-slate-200 shadow-sm rounded-xl px-4 py-2.5 text-xs font-semibold text-slate-700 flex items-center gap-2 hover:bg-slate-50 transition-colors">
              <span class="material-symbols-outlined text-[16px] text-slate-400">filter_list</span> Năm: Tất cả <span class="material-symbols-outlined text-[16px]">expand_more</span>
            </button>
            <button class="bg-white border border-slate-200 shadow-sm rounded-xl px-4 py-2.5 text-xs font-semibold text-slate-700 flex items-center gap-2 hover:bg-slate-50 transition-colors">
              <span class="material-symbols-outlined text-[16px] text-slate-400">category</span> Loại khóa học <span class="material-symbols-outlined text-[16px]">expand_more</span>
            </button>
          </div>
        </div>

        <!-- Main Grid Layout -->
        <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
          
          <!-- Featured Certificate (V-Core AI) -->
          <div @click="$router.push('/certificate-details')" class="md:col-span-2 bg-white rounded-[20px] shadow-sm border border-slate-100 p-2 flex flex-col sm:flex-row gap-6 cursor-pointer hover:shadow-md transition">
            <div class="sm:w-[50%] bg-[#1a3644] rounded-[15px] p-6 flex justify-center items-center overflow-hidden min-h-[350px]">
              <!-- Mock Certificate Graphics -->
              <div class="w-[85%] aspect-[3/4] bg-[#fdfbf7] border-[10px] border-[#2e4756] shadow-2xl p-4 flex flex-col items-center relative rotate-[-2deg] hover:rotate-0 transition-all duration-300">
                <div class="border border-yellow-600/50 w-full h-full p-3 flex flex-col items-center justify-center text-center opacity-80">
                  <div class="text-[10px] uppercase tracking-widest text-slate-500 mb-2">CERTIFICATE</div>
                  <div class="w-12 h-[1px] bg-yellow-600/50 mb-2"></div>
                  <div class="text-[8px] text-slate-400 mb-4">THÔNG BÁO CHỨNG NHẬN</div>
                  <div class="w-8 h-8 rounded-full border border-yellow-600 mt-auto flex items-center justify-center">
                    <div class="w-6 h-6 rounded-full bg-yellow-600/20"></div>
                  </div>
                </div>
              </div>
            </div>
            
            <div class="sm:w-[50%] p-4 pr-6 flex flex-col justify-center">
              <div class="flex flex-wrap items-center gap-2 text-[9px] font-bold text-slate-500 uppercase tracking-widest mb-4">
                <span class="bg-[#e5ecfb] text-[#1f42a6] px-2.5 py-1 rounded">AI & TECH</span>
                <span class="px-2.5 py-1 bg-slate-50 rounded text-slate-400 border border-slate-100">• ADVANCED LEVEL</span>
              </div>
              <h3 class="text-2xl font-bold text-slate-900 mb-6 leading-tight">V-Core AI: Trí tuệ Việt cho quản trị số</h3>
              <div class="flex gap-10 mb-8 mt-2 text-sm border-t border-slate-100 pt-6">
                <div>
                  <p class="text-slate-400 font-bold mb-1.5 text-[9px] uppercase tracking-widest">Ngày cấp</p>
                  <p class="font-bold text-slate-800 text-[13px]">26/03/2026</p>
                </div>
                <div>
                  <p class="text-slate-400 font-bold mb-1.5 text-[9px] uppercase tracking-widest">Mã ID</p>
                  <p class="font-bold text-slate-800 text-[13px]">AUR-9X2V-001</p>
                </div>
              </div>
              
              <div class="flex gap-2 mt-auto">
                <button class="flex-1 bg-[#0935a4] hover:bg-[#072573] text-white font-bold py-3 px-4 rounded-xl flex items-center justify-center gap-2 text-[13px] transition-colors shadow-md">
                  <span class="material-symbols-outlined text-[18px]">download</span> Tải xuống (PDF)
                </button>
                <button class="w-[50px] h-[46px] bg-slate-50 border border-slate-200 hover:bg-slate-100 text-slate-600 rounded-xl flex items-center justify-center transition-colors">
                  <span class="material-symbols-outlined text-[18px]">share</span>
                </button>
              </div>
            </div>
          </div>

          <!-- Secondary Certificate (Digital Marketing) -->
          <div @click="$router.push('/certificate-details')" class="md:col-span-1 bg-white rounded-[20px] shadow-sm border border-slate-100 p-2 flex flex-col cursor-pointer hover:shadow-md transition">
            <div class="h-44 bg-[#2b5452] rounded-[15px] p-4 flex items-center justify-center mb-5 relative overflow-hidden">
               <div class="w-[60%] h-[120%] bg-[#fdfbf7] border-4 border-[#376865] shadow-2xl p-2 rotate-6 opacity-90 relative top-4">
                  <div class="border border-yellow-600/30 w-full h-full p-2 text-center text-[6px]">
                    <div class="font-bold mt-2">CERTIFICATE</div>
                    <div class="mt-auto w-4 h-4 rounded-full bg-yellow-600/20 absolute bottom-4 left-1/2 -translate-x-1/2"></div>
                  </div>
               </div>
               <div class="absolute top-3 right-3 bg-white/90 backdrop-blur rounded-full w-8 h-8 flex items-center justify-center shadow-sm text-[#0935a4]">
                  <span class="material-symbols-outlined text-[18px]" style="font-variation-settings: 'FILL' 1;">verified</span>
               </div>
            </div>
            <div class="px-4 pb-4 flex flex-col flex-1">
              <div class="mb-3">
                <span class="bg-[#e5ecfb] text-[#1f42a6] text-[9px] uppercase font-bold px-2 py-1 rounded tracking-widest">Digital Marketing</span>
              </div>
              <h3 class="text-base font-bold text-slate-900 mb-4 leading-snug">Tối ưu hóa chuyển đổi đa nền tảng</h3>
              <div class="text-[10px] space-y-2 mb-6 text-slate-500 font-bold tracking-tight">
                <p><span class="uppercase tracking-widest opacity-60">Cấp ngày:</span> <span class="text-slate-700">15/01/2026</span></p>
                <p><span class="uppercase tracking-widest opacity-60">ID:</span> <span class="text-slate-700">AUR-K7L2-552</span></p>
              </div>
              <div class="flex gap-2 mt-auto pt-4 border-t border-slate-50">
                <button class="flex-1 bg-slate-50 border border-slate-100 hover:bg-slate-100 text-slate-700 font-bold py-2.5 rounded-xl flex items-center justify-center gap-2 text-xs transition-colors">
                  <span class="material-symbols-outlined text-[16px]">visibility</span> Xem chi tiết
                </button>
                <button class="w-10 h-10 bg-slate-50 border border-slate-100 hover:bg-slate-100 text-slate-600 rounded-xl flex items-center justify-center transition-colors">
                  <span class="material-symbols-outlined text-[16px]">share</span>
                </button>
              </div>
            </div>
          </div>

          <!-- Secondary Certificate (Agile & Scrum) -->
          <div @click="$router.push('/certificate-details')" class="md:col-span-1 bg-white rounded-[20px] shadow-sm border border-slate-100 p-2 flex flex-col cursor-pointer hover:shadow-md transition">
            <div class="h-44 bg-[#3d5e5e] rounded-[15px] p-4 flex items-center justify-center mb-5 overflow-hidden">
               <div class="w-[60%] h-[120%] bg-[#fdfbf7] border-4 border-[#4a7373] shadow-2xl p-2 -rotate-6 opacity-90 relative top-2">
                  <div class="border border-yellow-600/30 w-full h-full p-2 text-center text-[6px]">
                    <div class="font-bold mt-2">CERTIFICATE</div>
                    <div class="mt-auto w-4 h-4 rounded-full bg-yellow-600/20 absolute bottom-4 left-1/2 -translate-x-1/2"></div>
                  </div>
               </div>
            </div>
            <div class="px-3 pb-3 flex flex-col flex-1 pl-4">
              <div class="mb-3">
                <span class="bg-slate-100 text-slate-600 text-[9px] uppercase font-bold px-2 py-1 rounded tracking-widest">Leadership</span>
              </div>
              <h3 class="text-base font-bold text-slate-900 mb-4 leading-snug text-balance">Quản trị dự án Agile & Scrum</h3>
              <div class="text-[10px] space-y-2 mb-6 text-slate-500 font-bold tracking-tight">
                <p><span class="uppercase tracking-widest opacity-60">Cấp ngày:</span> <span class="text-slate-700">02/12/2025</span></p>
                <p><span class="uppercase tracking-widest opacity-60">ID:</span> <span class="text-slate-700">AUR-M4A9-102</span></p>
              </div>
              <div class="flex gap-2 mt-auto pt-4 border-t border-slate-50">
                <button class="flex-1 bg-slate-50 border border-slate-100 hover:bg-slate-100 text-slate-700 font-bold py-2.5 rounded-xl flex items-center justify-center gap-2 text-xs transition-colors">
                  <span class="material-symbols-outlined text-[16px]">visibility</span> Xem chi tiết
                </button>
                <button class="w-10 h-10 bg-slate-50 border border-slate-100 hover:bg-slate-100 text-slate-600 rounded-xl flex items-center justify-center transition-colors">
                  <span class="material-symbols-outlined text-[16px]">share</span>
                </button>
              </div>
            </div>
          </div>

          <!-- Secondary Certificate (Data Science) -->
          <div @click="$router.push('/certificate-details')" class="md:col-span-1 bg-white rounded-[20px] shadow-sm border border-slate-100 p-2 flex flex-col cursor-pointer hover:shadow-md transition">
            <div class="h-44 bg-[#2f4f4f] rounded-[15px] p-4 flex items-center justify-center mb-5 overflow-hidden">
               <div class="w-[60%] h-[120%] bg-[#fdfbf7] border-4 border-[#3a6363] shadow-2xl p-2 rotate-2 opacity-90 relative top-2">
                  <div class="border border-yellow-600/30 w-full h-full p-2 text-center text-[6px]">
                    <div class="font-bold mt-2">CERTIFICATE</div>
                    <div class="mt-auto w-4 h-4 rounded-full bg-yellow-600/20 absolute bottom-4 left-1/2 -translate-x-1/2"></div>
                  </div>
               </div>
            </div>
             <div class="px-3 pb-3 flex flex-col flex-1 pl-4">
              <div class="mb-3">
                <span class="bg-slate-100 text-slate-600 text-[9px] uppercase font-bold px-2 py-1 rounded tracking-widest">Data Science</span>
              </div>
              <h3 class="text-base font-bold text-slate-900 mb-4 leading-snug">Phân tích dữ liệu kinh doanh</h3>
              <div class="text-[10px] space-y-2 mb-6 text-slate-500 font-bold tracking-tight">
                <p><span class="uppercase tracking-widest opacity-60">Cấp ngày:</span> <span class="text-slate-700">18/11/2025</span></p>
                <p><span class="uppercase tracking-widest opacity-60">ID:</span> <span class="text-slate-700">AUR-D3F1-992</span></p>
              </div>
              <div class="flex gap-2 mt-auto pt-4 border-t border-slate-50">
                <button class="flex-1 bg-slate-50 border border-slate-100 hover:bg-slate-100 text-slate-700 font-bold py-2.5 rounded-xl flex items-center justify-center gap-2 text-xs transition-colors">
                  <span class="material-symbols-outlined text-[16px]">visibility</span> Xem chi tiết
                </button>
                <button class="w-10 h-10 bg-slate-50 border border-slate-100 hover:bg-slate-100 text-slate-600 rounded-xl flex items-center justify-center transition-colors">
                  <span class="material-symbols-outlined text-[16px]">share</span>
                </button>
              </div>
            </div>
          </div>

          <!-- Empty State CTA -->
          <div class="md:col-span-1 rounded-[20px] border-2 border-dashed border-slate-200 bg-white/40 flex justify-center items-center p-8 text-center min-h-[350px]">
            <div>
              <p class="text-slate-600 font-medium text-xs leading-relaxed mb-6 max-w-[200px] mx-auto">
                Bạn còn <span class="font-bold text-slate-900">2 khóa học</span> đang hoàn thành để nhận chứng chỉ tiếp theo.
              </p>
              <button class="bg-[#e4ebf9] hover:bg-[#d4dff5] text-[#1340a6] font-bold px-6 py-2.5 rounded-full text-xs transition-colors shadow-sm tracking-wide">
                Tiếp tục học ngay
              </button>
            </div>
          </div>

        </div>

        <!-- Pagination -->
        <div class="border-t border-slate-200/60 mt-12 pt-6 flex flex-col sm:flex-row justify-between items-center gap-4">
          <p class="text-[11px] font-medium text-slate-500 tracking-wide">
            Đang hiển thị <span class="font-bold text-slate-900 mx-0.5">4</span> trên <span class="font-bold text-slate-900 mx-0.5">4</span> chứng chỉ đạt được
          </p>
          <div class="flex items-center gap-2">
            <button class="w-8 h-8 rounded-lg flex items-center justify-center text-slate-400 hover:bg-slate-200 hover:text-slate-700 transition">
              <span class="material-symbols-outlined text-[16px]">chevron_left</span>
            </button>
            <button class="w-8 h-8 rounded-lg bg-[#0935a4] text-white font-bold text-xs shadow flex items-center justify-center">
              1
            </button>
            <button class="w-8 h-8 rounded-lg flex items-center justify-center text-slate-400 hover:bg-slate-200 hover:text-slate-700 transition">
              <span class="material-symbols-outlined text-[16px]">chevron_right</span>
            </button>
          </div>
        </div>

        <!-- Float Notification -->
        <div v-if="showToast" class="fixed right-6 bottom-16 lg:top-32 lg:bottom-auto z-50 animate-in slide-in-from-right fade-in duration-500">
          <div class="bg-[#2D333B] text-white p-4 rounded-xl shadow-2xl flex gap-4 max-w-[320px] border border-slate-700">
            <div class="w-10 h-10 bg-[#0935a4] rounded-lg flex items-center justify-center shrink-0">
              <span class="material-symbols-outlined text-white text-[20px]" style="transform: rotate(15deg);">campaign</span>
            </div>
            <div class="flex-1 pb-1">
              <h4 class="font-bold text-sm mb-1 text-white">Thành tích mới!</h4>
              <p class="text-slate-300 text-[11px] leading-relaxed mb-3">Bạn vừa nhận được chứng chỉ "V-Core AI". Hãy chia sẻ lên LinkedIn để mọi người cùng biết!</p>
              <div class="flex gap-4 text-xs font-bold">
                <button @click="$router.push('/certificate-details')" class="text-white hover:text-blue-300 transition underline underline-offset-2">Xem ngay</button>
                <button @click="showToast = false" class="text-slate-400 hover:text-white transition">Bỏ qua</button>
              </div>
            </div>
          </div>
        </div>

      </div>
    </div>
  </LayoutStudent>
</template>

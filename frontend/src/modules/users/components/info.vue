<script setup>
import { ref } from 'vue'
import LayoutStudent from '@/layouts/LayoutStudent.vue'

const activeTab = ref('profile')
const name = ref('Nguyễn Minh Khôi')
const language = ref('Tiếng Việt (Vietnam)')
const portfolio = ref('khoinguyen.design')
const bio = ref('Chuyên viên thiết kế sản phẩm với hơn 5 năm kinh nghiệm. Đang theo đuổi các khóa học về quản trị hệ thống và tư duy chiến lược tại AET LMS.')
</script>

<template>
  <LayoutStudent>
    <div class="profile-page py-12 px-6 lg:px-12 bg-gray-50/30 min-h-screen font-plus-jakarta">
      <div class="max-w-[1000px] mx-auto bg-white rounded-[32px] shadow-[0_8px_30px_rgb(0,0,0,0.04)] border border-gray-100 overflow-hidden">
        <!-- Tabs -->
        <div class="flex border-b border-gray-100 px-8">
          <button 
            v-for="tab in [
              { id: 'profile', name: 'Hồ sơ cá nhân' },
              { id: 'security', name: 'Bảo mật' },
              { id: 'sessions', name: 'Phiên đăng nhập' },
              { id: 'notifications', name: 'Thông báo' }
            ]" 
            :key="tab.id"
            @click="activeTab = tab.id"
            :class="[
              'px-6 py-6 text-sm font-bold transition-all relative',
              activeTab === tab.id ? 'text-[#003fb1]' : 'text-gray-400 hover:text-gray-600'
            ]"
          >
            {{ tab.name }}
            <div v-if="activeTab === tab.id" class="absolute bottom-0 left-6 right-6 h-1 bg-[#003fb1] rounded-t-full"></div>
          </button>
        </div>

        <div class="p-10 space-y-12">
          <!-- Avatar Section -->
          <div class="flex items-center gap-8">
            <div class="relative group">
              <div class="w-24 h-24 rounded-2xl overflow-hidden border-4 border-gray-50 shadow-sm">
                <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuDEidDb1VqdaeEskLwwEHnq8KXXg2Q5DCyVyAvmJ8UKDM9WPNHy3LVwV7ixaySFtdHLAeYLEkawECHzLkR37GSe4wYv00suwzkFMQ5rjb61H2nipFZ5pTrvlO3Xk-jiJszR1Bp9daAEYO3fbefIUa1AawZN-0aISC4Nop3F9NZXuK-6GVvdxDetV_SZa1XuLHQFSPCbaTu7mEthClMyRjvoIeqP8Ast-sWbIxckQJEYYePYJ_2l-VE98SVdLNgqVo5jpPw7xpVHaQ4" alt="Avatar" class="w-full h-full object-cover">
              </div>
              <button class="absolute -bottom-2 -right-2 w-8 h-8 bg-[#003fb1] text-white rounded-full flex items-center justify-center shadow-lg hover:scale-110 transition-transform border-4 border-white">
                <span class="material-symbols-outlined text-sm">edit</span>
              </button>
            </div>
            <div>
              <h3 class="text-lg font-black text-gray-900 tracking-tight">Ảnh đại diện</h3>
              <p class="text-xs text-gray-500 font-medium mt-1">Tải lên tệp JPG, GIF hoặc PNG.</p>
              <p class="text-xs text-gray-500 font-medium tracking-tight">Kích thước tối đa 2MB.</p>
            </div>
          </div>

          <!-- Form Grid -->
          <div class="grid grid-cols-2 gap-8">
            <div class="space-y-3">
              <label class="text-[11px] font-black text-gray-400 uppercase tracking-widest pl-1">HỌ VÀ TÊN</label>
              <input v-model="name" type="text" class="w-full bg-gray-50 border border-transparent rounded-xl py-4 px-5 text-sm font-black text-gray-900 focus:bg-white focus:border-[#003fb1]/20 focus:ring-4 focus:ring-blue-50 transition-all outline-none">
            </div>
            <div class="space-y-3">
              <label class="text-[11px] font-black text-gray-400 uppercase tracking-widest pl-1">NGÔN NGỮ</label>
              <div class="relative group">
                <select v-model="language" class="w-full bg-gray-50 border border-transparent rounded-xl py-4 px-5 pr-12 text-sm font-black text-gray-900 focus:bg-white focus:border-[#003fb1]/20 focus:ring-4 focus:ring-blue-50 transition-all outline-none appearance-none cursor-pointer">
                  <option>Tiếng Việt (Vietnam)</option>
                  <option>English (United States)</option>
                </select>
                <span class="material-symbols-outlined absolute right-4 top-1/2 -translate-y-1/2 text-gray-400 group-focus-within:text-[#003fb1] pointer-events-none transition-colors">expand_more</span>
              </div>
            </div>
          </div>

          <div class="space-y-3">
            <label class="text-[11px] font-black text-gray-400 uppercase tracking-widest pl-1">ĐƯỜNG DẪN PORTFOLIO</label>
            <div class="flex gap-0 overflow-hidden rounded-xl border-2 border-transparent group focus-within:border-[#003fb1]/20 focus-within:ring-4 focus-within:ring-blue-50 transition-all">
              <span class="bg-gray-100 px-6 flex items-center text-sm font-black text-gray-500 border-r border-gray-200">https://</span>
              <input v-model="portfolio" type="text" class="flex-1 bg-gray-50 py-4 px-5 text-sm font-black text-gray-900 outline-none group-focus-within:bg-white transition-all">
            </div>
          </div>

          <div class="space-y-3">
            <label class="text-[11px] font-black text-gray-400 uppercase tracking-widest pl-1">GIỚI THIỆU BẢN THÂN</label>
            <textarea v-model="bio" rows="4" class="w-full bg-gray-50 border border-transparent rounded-xl py-4 px-5 text-sm font-bold text-gray-600 focus:bg-white focus:border-[#003fb1]/20 focus:ring-4 focus:ring-blue-50 transition-all outline-none leading-relaxed resize-none"></textarea>
            <p class="text-[11px] text-gray-400 font-bold italic mt-1 pl-1">Mô tả này sẽ xuất hiện trên hồ sơ công khai của bạn cho giảng viên và bạn học.</p>
          </div>

          <!-- Buttons -->
          <div class="flex justify-end items-center gap-6 pt-6 border-t border-gray-50">
            <button class="text-sm font-black text-[#003fb1] hover:underline">Hủy bỏ</button>
            <button class="bg-[#2d4aa5] text-white px-10 py-4 rounded-xl text-sm font-black hover:bg-[#003fb1] hover:-translate-y-1 shadow-lg shadow-blue-500/20 active:scale-95 transition-all flex items-center gap-3">
              <span class="material-symbols-outlined text-xl">save</span>
              Lưu thay đổi
            </button>
          </div>

          <!-- 2FA Section -->
          <div class="bg-gray-50 rounded-3xl p-8 flex items-center justify-between border border-gray-100">
            <div class="flex items-center gap-6">
              <div class="w-14 h-14 rounded-2xl bg-blue-50 flex items-center justify-center">
                <span class="material-symbols-outlined text-[#003fb1] text-2xl font-bold">verified_user</span>
              </div>
              <div>
                <p class="text-sm font-black text-gray-900 tracking-tight">Xác thực hai yếu tố (2FA)</p>
                <p class="text-xs text-gray-500 font-medium mt-1">Tăng cường bảo mật cho tài khoản của bạn bằng mã OTP.</p>
              </div>
            </div>
            <button class="bg-white border border-gray-200 text-gray-900 px-6 py-3 rounded-xl text-xs font-black hover:bg-gray-50 transition-all">Thiết lập ngay</button>
          </div>

          <!-- Danger Zone -->
          <div class="bg-red-50/30 rounded-3xl p-8 border border-red-50 space-y-4">
             <div class="flex items-center gap-3 text-red-600">
               <span class="material-symbols-outlined text-xl font-bold">report</span>
               <p class="text-xs font-black uppercase tracking-widest">KHU VỰC NGUY HIỂM</p>
             </div>
             <div class="flex items-center justify-between">
               <p class="text-xs text-gray-500 font-medium max-w-md">Xóa tài khoản của bạn sẽ dẫn đến việc mất vĩnh viễn quyền truy cập vào các khóa học, chứng chỉ và lịch sử học tập.</p>
               <button class="bg-white border border-red-100 text-red-600 px-6 py-3 rounded-xl text-xs font-black hover:bg-red-50 transition-all">Xóa tài khoản</button>
             </div>
          </div>
        </div>
      </div>
    </div>
  </LayoutStudent>
</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@400;500;600;700;800&display=swap');

.font-plus-jakarta {
  font-family: 'Plus Jakarta Sans', sans-serif;
}
</style>

<script setup>
import { ref } from 'vue'
import LayoutStudent from '@/layouts/LayoutStudent.vue'

const activeTab = ref('Tất cả')
const tabs = ['Tất cả', 'Khóa học', 'Đơn hàng', 'Hệ thống']

const isAlertVisible = ref(true)

const notifications = ref([
  {
    id: 1,
    title: 'Bài giảng mới: Nguyên lý thị giác',
    desc: 'Giảng viên vừa cập nhật tài liệu mới cho chương 4 của khóa học Creative Design 2024.',
    time: '10 phút trước',
    icon: 'menu_book',
    isRead: false,
    colorClass: 'bg-blue-50 text-blue-600'
  },
  {
    id: 2,
    title: 'Thanh toán thành công #ORD-8821',
    desc: 'Cảm ơn bạn đã đăng ký gói Professional. Hóa đơn đã được gửi vào email cá nhân.',
    time: '2 giờ trước',
    icon: 'receipt_long',
    isRead: false,
    colorClass: 'bg-emerald-50 text-emerald-600'
  },
  {
    id: 3,
    title: 'Bảo trì hệ thống định kỳ',
    desc: 'Hệ thống sẽ tạm ngưng hoạt động từ 02:00 đến 04:00 sáng mai để nâng cấp máy chủ.',
    time: 'Hôm qua',
    icon: 'settings',
    isRead: true,
    colorClass: 'bg-gray-100 text-gray-600'
  },
  {
    id: 4,
    title: 'Chứng chỉ hoàn thành khóa học',
    desc: 'Chúc mừng! Bạn đã hoàn thành khóa học "Data Science Foundation". Tải chứng chỉ ngay.',
    time: '3 ngày trước',
    icon: 'school',
    isRead: true,
    colorClass: 'bg-indigo-50 text-indigo-600'
  },
  {
    id: 5,
    title: 'Cảnh báo đăng nhập lạ',
    desc: 'Tài khoản của bạn vừa được đăng nhập từ một thiết bị Chrome trên Windows tại Hà Nội.',
    time: '5 ngày trước',
    icon: 'warning',
    isRead: true,
    colorClass: 'bg-orange-50 text-orange-500'
  }
])

const markAllAsRead = () => {
  notifications.value.forEach(n => n.isRead = true)
}
</script>

<template>
  <LayoutStudent>
    <div class="min-h-screen bg-[#F8F9FA] py-8 px-4 font-plus-jakarta">
    <div class="max-w-4xl mx-auto space-y-6">
      
      <!-- Alert Top -->
      <transition enter-active-class="transition ease-out duration-300 transform"
                  enter-from-class="opacity-0 -translate-y-4"
                  enter-to-class="opacity-100 translate-y-0"
                  leave-active-class="transition ease-in duration-200 transform"
                  leave-from-class="opacity-100 translate-y-0"
                  leave-to-class="opacity-0 -translate-y-4">
        <div v-if="isAlertVisible" class="bg-white rounded-2xl shadow-sm border border-gray-100 p-4 flex items-start gap-4 w-max pr-8 relative">
          <div class="w-10 h-10 rounded-full bg-blue-50 text-blue-600 flex items-center justify-center shrink-0">
            <span class="material-symbols-outlined text-xl">notifications_active</span>
          </div>
          <div class="flex-1">
            <h4 class="text-xs font-bold text-blue-600 uppercase tracking-widest mb-1">Thông báo mới</h4>
            <p class="text-sm text-gray-800 font-medium">Nhắc nhở học tập: Khóa học UI/UX đang chờ bạn</p>
          </div>
          <button @click="isAlertVisible = false" class="absolute top-4 right-4 text-gray-400 hover:text-gray-600 transition-colors">
            <span class="material-symbols-outlined text-lg">close</span>
          </button>
        </div>
      </transition>

      <!-- Main Card -->
      <div class="bg-white rounded-[24px] shadow-sm border border-gray-100 overflow-hidden">
        <!-- Header -->
        <div class="px-8 pt-8 pb-4 flex items-center justify-between">
          <h1 class="text-2xl font-bold text-gray-900">Thông báo của bạn</h1>
          <button @click="markAllAsRead" class="text-blue-600 font-semibold text-sm hover:text-blue-700 transition-colors">
            Đánh dấu tất cả đã đọc
          </button>
        </div>

        <!-- Tabs -->
        <div class="px-8 border-b border-gray-100 flex items-center gap-8">
          <button 
            v-for="tab in tabs" 
            :key="tab"
            @click="activeTab = tab"
            :class="[
              'py-4 text-sm font-semibold transition-all relative',
              activeTab === tab ? 'text-blue-600' : 'text-gray-500 hover:text-gray-700'
            ]"
          >
            {{ tab }}
            <div v-if="activeTab === tab" class="absolute bottom-0 left-0 w-full h-[2px] bg-blue-600 rounded-t-full"></div>
          </button>
        </div>

        <!-- Notification List -->
        <div class="flex flex-col">
          <div 
            v-for="notification in notifications" 
            :key="notification.id"
            class="group p-6 pl-8 flex items-start gap-6 border-b border-gray-50 hover:bg-[#F9FAFB] transition-colors relative"
          >
            <!-- Unread Indicator -->
            <div class="absolute right-8 top-1/2 -translate-y-1/2 w-[6px] h-[6px] rounded-full bg-blue-600" v-if="!notification.isRead"></div>

            <!-- Icon -->
            <div :class="['w-12 h-12 rounded-full flex items-center justify-center shrink-0', notification.colorClass]">
              <span class="material-symbols-outlined">{{ notification.icon }}</span>
            </div>

            <!-- Content -->
            <div class="flex-1 pr-12">
              <div class="flex items-center justify-between mb-1">
                <h3 class="font-bold text-gray-900 text-base" :class="{ 'text-gray-700 font-semibold': notification.isRead }">
                  {{ notification.title }}
                </h3>
                <span class="text-xs text-gray-400 whitespace-nowrap">{{ notification.time }}</span>
              </div>
              <p class="text-[15px] leading-snug" :class="notification.isRead ? 'text-gray-500' : 'text-gray-600'">
                {{ notification.desc }}
              </p>
            </div>
          </div>
        </div>

        <!-- Load More -->
        <button class="w-full py-6 text-sm font-bold text-gray-600 hover:text-gray-900 hover:bg-gray-50 transition-colors flex items-center justify-center gap-2">
          Tải thêm thông báo...
          <span class="material-symbols-outlined text-lg">expand_more</span>
        </button>
      </div>

      <!-- Footer Cards -->
      <div class="grid grid-cols-1 gap-6 pt-4">
        <!-- Card 1 -->
        <div class="bg-[#EEF2FA] rounded-2xl p-8 border border-blue-100 flex flex-col justify-between items-start">
          <div>
            <h3 class="text-blue-800 font-bold text-lg mb-2">Cài đặt thông báo</h3>
            <p class="text-blue-900/70 text-[15px] leading-relaxed mb-6">
              Bạn có thể tùy chỉnh cách nhận thông báo qua Email, Desktop hoặc Mobile trong trang cài đặt tài khoản.
            </p>
          </div>
          <router-link to="/profile" class="text-blue-700 font-extrabold text-sm flex items-center gap-2 hover:gap-3 transition-all uppercase tracking-wide">
            ĐI TỚI CÀI ĐẶT
            <span class="material-symbols-outlined text-sm">arrow_forward</span>
          </router-link>
        </div>
      </div>

    </div>
    </div>
  </LayoutStudent>
</template>

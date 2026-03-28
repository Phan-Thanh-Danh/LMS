<script setup>
import { ref } from 'vue'
import { useAuthStore } from '@/stores/auth'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()
const isProfileOpen = ref(false)

const handleLogout = () => {
  authStore.logout()
  router.push('/login')
}
</script>

<template>
  <header class="h-20 bg-white/80 backdrop-blur-md border-b border-gray-200 sticky top-0 z-40 px-8 flex items-center justify-between shadow-sm">
    <!-- Search Bar -->
    <div class="flex-1 max-w-xl relative group">
      <span class="material-symbols-outlined absolute left-4 top-1/2 -translate-y-1/2 text-gray-400 group-focus-within:text-primary transition-colors">search</span>
      <input type="text" placeholder="Tìm kiếm khóa học, bài học, tài liệu..." 
        class="w-full bg-gray-100 hover:bg-gray-200 focus:bg-white border-2 border-transparent focus:border-primary/20 rounded-xl py-2.5 pl-12 pr-4 text-sm outline-none transition-all duration-200 font-medium">
    </div>

    <!-- Actions -->
    <div class="flex items-center gap-6">
      <router-link to="/cart" class="relative p-2 text-gray-500 hover:text-primary hover:bg-primary/5 rounded-full transition-all">
        <span class="material-symbols-outlined">shopping_cart</span>
        <span class="absolute top-2 right-2 w-4 h-4 bg-primary text-white flex items-center justify-center rounded-full text-[10px] font-bold">2</span>
      </router-link>

      <button class="relative p-2 text-gray-500 hover:text-primary hover:bg-primary/5 rounded-full transition-all">
        <span class="material-symbols-outlined">notifications</span>
        <span class="absolute top-2 right-2 w-2 h-2 bg-red-500 rounded-full border-2 border-white"></span>
      </button>

      <div class="w-px h-8 bg-gray-200"></div>

      <!-- User Profile -->
      <div class="relative">
        <button @click="isProfileOpen = !isProfileOpen" 
          class="flex items-center gap-3 p-1 rounded-full hover:bg-gray-100 transition-all border border-transparent hover:border-gray-200 group">
          <div class="w-9 h-9 rounded-full overflow-hidden border-2 border-primary/20 group-hover:border-primary transition-colors">
            <img :src="authStore.user?.avatar || 'https://i.pravatar.cc/100'" alt="Avatar" class="w-full h-full object-cover">
          </div>
          <div class="hidden lg:block text-left pr-2">
            <p class="text-xs font-black text-gray-900 leading-tight uppercase tracking-tighter">{{ authStore.user?.name || 'Học viên' }}</p>
            <p class="text-[10px] font-bold text-primary uppercase tracking-widest">Premium Member</p>
          </div>
          <span class="material-symbols-outlined text-gray-400 text-sm">expand_more</span>
        </button>

        <!-- Profile Dropdown -->
        <div v-if="isProfileOpen" class="absolute right-0 top-full mt-3 w-56 bg-white rounded-2xl shadow-2xl border border-gray-100 p-2 animate-in fade-in slide-in-from-top-2 duration-200">
          <div class="px-4 py-3 border-b border-gray-100 space-y-1">
            <p class="text-xs font-black text-gray-900 truncate uppercase tracking-tighter">{{ authStore.user?.name }}</p>
            <p class="text-[10px] text-gray-500 truncate font-bold">{{ authStore.user?.email }}</p>
          </div>
          <div class="py-2">
            <router-link to="/profile" class="w-full flex items-center gap-3 px-4 py-2 text-sm font-bold text-gray-600 hover:bg-primary/5 hover:text-primary rounded-lg transition-colors">
              <span class="material-symbols-outlined text-lg">person</span> Hồ sơ của tôi
            </router-link>
            <button class="w-full flex items-center gap-3 px-4 py-2 text-sm font-bold text-gray-600 hover:bg-primary/5 hover:text-primary rounded-lg transition-colors">
              <span class="material-symbols-outlined text-lg">workspace_premium</span> Chứng chỉ
            </button>
          </div>
          <div class="pt-2 border-t border-gray-100">
            <button @click="handleLogout" class="w-full flex items-center gap-3 px-4 py-2 text-sm font-bold text-red-500 hover:bg-red-50 rounded-lg transition-colors">
              <span class="material-symbols-outlined text-lg">logout</span> Đăng xuất
            </button>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

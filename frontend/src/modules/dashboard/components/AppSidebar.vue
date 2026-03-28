<script setup>
import { ref } from 'vue'
import { useRouter, useRoute } from 'vue-router'

const router = useRouter()
const route = useRoute()

const navItems = [
    { name: 'Tổng quan', icon: 'dashboard', path: '/dashboard' },
    { name: 'Khóa học của tôi', icon: 'school', path: '/learning' },
    { name: 'Tiến độ học tập', icon: 'auto_graph', path: '/learning/101/progress' },
    { name: 'Tin nhắn', icon: 'mail', path: '/messages' },
    { name: 'Cài đặt', icon: 'settings', path: '/settings' },
]

const isActive = (path) => route.path === path
</script>

<template>
  <aside class="w-64 bg-slate-900 h-screen flex flex-col border-r border-slate-800 fixed left-0 top-0 z-50 transition-all duration-300">
    <!-- Logo Section -->
    <div class="h-20 flex items-center px-6 border-b border-slate-800">
      <div class="flex items-center gap-3">
        <div class="w-10 h-10 bg-linear-to-br from-primary to-blue-700 rounded-xl flex items-center justify-center shadow-lg shadow-primary/20">
          <span class="material-symbols-outlined text-white text-2xl font-bold">school</span>
        </div>
        <span class="text-xl font-black text-white tracking-tighter uppercase">AET <span class="text-primary">LMS</span></span>
      </div>
    </div>

    <!-- Navigation Items -->
    <nav class="flex-1 px-4 py-8 space-y-2 overflow-y-auto custom-scrollbar">
      <router-link v-for="item in navItems" :key="item.path" :to="item.path"
        :class="['flex items-center gap-4 px-4 py-3.5 rounded-xl transition-all duration-200 group relative',
                 isActive(item.path) ? 'bg-primary text-white shadow-lg shadow-primary/20' : 'text-slate-400 hover:bg-slate-800 hover:text-white']">
        
        <span class="material-symbols-outlined text-[22px] transition-transform group-hover:scale-110">
          {{ item.icon }}
        </span>
        <span class="font-bold text-sm tracking-wide">{{ item.name }}</span>

        <!-- Active Indicator -->
        <div v-if="isActive(item.path)" class="absolute right-3 w-1.5 h-1.5 bg-white rounded-full"></div>
      </router-link>
    </nav>

    <!-- Footer / Help Section -->
    <div class="p-6">
      <div class="bg-slate-800/50 rounded-2xl p-4 border border-slate-700/50">
        <div class="w-8 h-8 rounded-full bg-primary/20 flex items-center justify-center mb-3">
          <span class="material-symbols-outlined text-primary text-lg">contact_support</span>
        </div>
        <p class="text-xs font-bold text-white mb-1">Cần trợ giúp?</p>
        <p class="text-[10px] text-slate-500 mb-3">Liên hệ đội ngũ hỗ trợ 24/7</p>
        <button class="w-full py-2 bg-slate-700 hover:bg-slate-600 text-white text-[10px] font-bold rounded-lg transition-colors">
          Trung tâm hỗ trợ
        </button>
      </div>
    </div>
  </aside>
</template>

<style scoped>
.custom-scrollbar::-webkit-scrollbar { width: 4px; }
.custom-scrollbar::-webkit-scrollbar-track { background: transparent; }
.custom-scrollbar::-webkit-scrollbar-thumb { background: #1e293b; border-radius: 10px; }
.custom-scrollbar::-webkit-scrollbar-thumb:hover { background: #334155; }
</style>

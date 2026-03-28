<script setup>
import AppSidebar from '@/modules/dashboard/components/AppSidebar.vue'
import TopNavbar from '@/modules/dashboard/components/TopNavbar.vue'
</script>

<template>
  <div class="min-h-screen bg-gray-50 flex">
    <!-- Fixed Sidebar -->
    <AppSidebar />

    <!-- Main Content Area -->
    <div class="flex-1 flex flex-col ml-64">
      <TopNavbar />
      
      <main class="flex-1 overflow-y-auto p-8 lg:p-12 custom-scrollbar">
        <div class="max-w-[1440px] mx-auto">
          <slot />
        </div>
      </main>

      <!-- Minimal Footer -->
      <footer class="px-12 py-8 bg-white border-t border-gray-100 flex items-center justify-between text-[10px] font-bold text-gray-400 uppercase tracking-widest">
        <span>© 2026 AET LMS Academy. All rights reserved.</span>
        <div class="flex gap-6">
          <a href="#" class="hover:text-primary transition-colors">Điều khoản</a>
          <a href="#" class="hover:text-primary transition-colors">Bảo mật</a>
        </div>
      </footer>
    </div>
  </div>
</template>

<style scoped>
.custom-scrollbar::-webkit-scrollbar { width: 6px; }
.custom-scrollbar::-webkit-scrollbar-track { background: transparent; }
.custom-scrollbar::-webkit-scrollbar-thumb { background: #e2e8f0; border-radius: 10px; }
.custom-scrollbar::-webkit-scrollbar-thumb:hover { background: #cbd5e1; }
</style>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const router = useRouter()
const authStore = useAuthStore()
const email = ref('')
const isLoading = ref(false)
const errorMsg = ref('')

const handleSubmit = async () => {
  if (!email.value) return
  
  isLoading.value = true
  errorMsg.value = ''
  
  const result = await authStore.forgotPassword(email.value)
  
  isLoading.value = false
  
  if (result.success) {
    router.push('/verify-otp')
  } else {
    errorMsg.value = result.message
  }
}
</script>

<template>
  <div class="forgot-password-page bg-surface text-on-surface min-h-screen flex flex-col font-plus-jakarta antialiased">
    <!-- TopAppBar -->
    <header class="bg-[#f8f9fb] z-50">
      <div class="flex justify-center items-center w-full px-6 py-4">
        <div class="flex items-center gap-2">
          <span class="material-symbols-outlined text-primary text-2xl" style="font-variation-settings: 'FILL' 1;">auto_stories</span>
          <span class="text-xl font-bold text-on-surface tracking-tight">AET LMS</span>
        </div>
      </div>
    </header>

    <main class="grow flex items-center justify-center px-4 py-12">
      <div class="w-full max-w-md">
        <!-- Password Recovery Card -->
        <div class="bg-white rounded-2xl shadow-xl overflow-hidden p-8 md:p-10 border border-surface-container">
          <!-- Hero Image Illustration -->
          <div class="mb-8 flex justify-center">
            <div class="w-20 h-20 rounded-full bg-surface-container-low flex items-center justify-center">
              <span class="material-symbols-outlined text-primary text-4xl">lock_reset</span>
            </div>
          </div>
          <div class="space-y-2 mb-8">
            <h3 class="text-[22px] font-extrabold text-on-surface leading-tight font-headline">Quên mật khẩu?</h3>
            <p class="text-on-surface-variant text-[14px] leading-relaxed font-body">
              Vui lòng nhập địa chỉ email bạn đã dùng để đăng ký. Chúng tôi sẽ gửi mã xác thực gồm 6 chữ số.
            </p>
          </div>
          <form class="space-y-6" @submit.prevent="handleSubmit">
            <!-- Email Input Group -->
            <div class="space-y-2">
              <label class="block text-sm font-semibold text-on-surface-variant font-label" for="email">Địa chỉ Email</label>
              <div class="relative">
                <input 
                  id="email" 
                  v-model="email"
                  class="w-full px-4 py-3 bg-white border border-outline-variant rounded-xl focus:ring-2 focus:ring-primary outline-none transition-all duration-200 text-on-surface placeholder:text-outline" 
                  placeholder="ten@email.com" 
                  required 
                  type="email"
                >
              </div>
              <p v-if="errorMsg" class="text-xs text-red-500 font-medium flex items-center gap-1 mt-1">
                <span class="material-symbols-outlined text-[14px]">error</span>
                {{ errorMsg }}
              </p>
            </div>
            <!-- Action Button -->
            <button 
              class="w-full bg-primary hover:bg-primary-container text-white font-bold py-3.5 px-6 rounded-xl transition-all duration-300 flex items-center justify-center gap-2 group shadow-lg shadow-primary/20 active:scale-95 disabled:opacity-70 disabled:cursor-not-allowed" 
              type="submit"
              :disabled="isLoading"
            >
              <span v-if="isLoading">Đang xử lý...</span>
              <template v-else>
                <span>Gửi mã xác thực</span>
                <span class="material-symbols-outlined text-[20px] group-hover:translate-x-1 transition-transform">arrow_forward</span>
              </template>
            </button>
          </form>
          <div class="mt-8 pt-6 border-t border-surface-container-low text-center">
            <router-link to="/login" class="inline-flex items-center gap-2 text-primary hover:text-primary-container font-bold text-sm transition-colors duration-200">
              <span class="material-symbols-outlined text-[18px]">arrow_back</span>
              Quay lại đăng nhập
            </router-link>
          </div>
        </div>
        <!-- Contextual Help -->
        <div class="mt-8 bg-surface-container-low rounded-xl p-4 flex gap-4 items-start">
          <span class="material-symbols-outlined text-secondary mt-0.5">help_outline</span>
          <div class="space-y-1">
            <p class="text-xs font-bold text-on-surface-variant uppercase tracking-widest">Cần hỗ trợ?</p>
            <p class="text-[13px] text-on-surface-variant leading-tight">Nếu bạn không nhận được email sau 5 phút, hãy kiểm tra thư mục Spam hoặc liên hệ bộ phận hỗ trợ.</p>
          </div>
        </div>
      </div>
    </main>

    <!-- Footer -->
    <footer class="bg-white/50 backdrop-blur-md w-full border-t border-surface-container-low">
      <div class="flex flex-col md:flex-row justify-between items-center px-8 py-6 max-w-7xl mx-auto w-full">
        <div class="text-slate-500 text-[10px] uppercase tracking-widest mb-4 md:mb-0">
          © 2026 AET LMS. All rights reserved.
        </div>
        <div class="flex gap-6">
          <a class="text-slate-500 text-[10px] uppercase tracking-widest hover:text-primary transition-colors" href="#">Privacy Policy</a>
          <a class="text-slate-500 text-[10px] uppercase tracking-widest hover:text-primary transition-colors" href="#">Terms of Service</a>
        </div>
      </div>
    </footer>

    <!-- Background Decoration -->
    <div class="fixed top-0 left-0 -z-10 w-full h-full overflow-hidden pointer-events-none opacity-50">
      <div class="absolute -top-[10%] -left-[10%] w-[40%] h-[40%] rounded-full bg-primary/5 blur-[120px]"></div>
      <div class="absolute -bottom-[10%] -right-[10%] w-[30%] h-[30%] rounded-full bg-secondary-container/10 blur-[100px]"></div>
    </div>
  </div>
</template>

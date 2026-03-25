<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const password = ref('')
const confirmPassword = ref('')
const showPassword1 = ref(false)
const showPassword2 = ref(false)
const errorMsg = ref('')

const handleReset = () => {
  if (password.value !== confirmPassword.value) {
    errorMsg.value = 'Mật khẩu xác nhận không khớp!'
    return
  }
  
  if (password.value.length < 8) {
    errorMsg.value = 'Mật khẩu phải dài ít nhất 8 ký tự.'
    return
  }

  console.log('Resetting password to:', password.value)
  // Mock success and redirect to login
  alert('Đổi mật khẩu thành công! Vui lòng đăng nhập lại.')
  router.push('/login')
}
</script>

<template>
  <div class="reset-password-page bg-surface-container-low min-h-screen flex flex-col font-plus-jakarta antialiased">
    <!-- Header -->
    <header class="fixed top-0 w-full z-50 bg-white/80 backdrop-blur-xl">
      <div class="flex justify-between items-center px-6 py-4 max-w-7xl mx-auto">
        <div class="text-xl font-bold text-on-surface tracking-tighter">
          AET <span class="text-primary">LMS</span>
        </div>
        <div class="text-on-surface-variant text-sm font-medium">Hỗ trợ</div>
      </div>
    </header>

    <main class="grow flex items-center justify-center px-4 py-24">
      <div class="w-full max-w-md bg-white rounded-2xl p-8 md:p-10 shadow-2xl transition-all duration-300">
        <!-- Icon -->
        <div class="mb-8 flex justify-center">
          <div class="w-16 h-16 bg-primary-fixed rounded-full flex items-center justify-center text-primary">
            <span class="material-symbols-outlined text-3xl">lock_reset</span>
          </div>
        </div>
        
        <!-- Header -->
        <div class="text-center mb-8">
          <h3 class="text-2xl font-extrabold text-on-surface tracking-tight mb-3">Tạo mật khẩu mới</h3>
          <p class="text-on-surface-variant text-sm leading-relaxed">
            Mật khẩu mới của bạn phải dài tối thiểu 8 ký tự, bao gồm chữ và số.
          </p>
        </div>

        <!-- Form -->
        <form class="space-y-6" @submit.prevent="handleReset">
          <!-- New Password -->
          <div class="space-y-2">
            <label class="block text-sm font-semibold text-on-surface-variant" for="new_password">Mật khẩu mới</label>
            <div class="relative group">
              <input 
                id="new_password" 
                v-model="password"
                :type="showPassword1 ? 'text' : 'password'"
                class="w-full px-4 py-3 bg-surface-container-high border-none rounded-lg text-on-surface focus:ring-2 focus:ring-primary transition-all duration-200 outline-none" 
                required
              />
              <button 
                type="button" 
                class="absolute right-3 top-1/2 -translate-y-1/2 text-on-surface-variant hover:text-primary transition-colors"
                @click="showPassword1 = !showPassword1"
              >
                <span class="material-symbols-outlined text-[20px]">{{ showPassword1 ? 'visibility_off' : 'visibility' }}</span>
              </button>
            </div>
          </div>

          <!-- Confirm Password -->
          <div class="space-y-2">
            <label class="block text-sm font-semibold text-on-surface-variant" for="confirm_password">Xác nhận mật khẩu mới</label>
            <div class="relative group">
              <input 
                id="confirm_password" 
                v-model="confirmPassword"
                :type="showPassword2 ? 'text' : 'password'"
                class="w-full px-4 py-3 bg-surface-container-high border-none rounded-lg text-on-surface focus:ring-2 focus:ring-primary transition-all duration-200 outline-none" 
                required
              />
              <button 
                type="button" 
                class="absolute right-3 top-1/2 -translate-y-1/2 text-on-surface-variant hover:text-primary transition-colors"
                @click="showPassword2 = !showPassword2"
              >
                <span class="material-symbols-outlined text-[20px]">{{ showPassword2 ? 'visibility_off' : 'visibility' }}</span>
              </button>
            </div>
            <p v-if="errorMsg" class="mt-1 text-xs text-error font-medium flex items-center gap-1">
              <span class="material-symbols-outlined text-[14px]">error</span>
              {{ errorMsg }}
            </p>
          </div>

          <!-- Action Button -->
          <div class="pt-4">
            <button class="w-full bg-primary hover:bg-primary-container text-white font-bold py-3.5 px-6 rounded-xl shadow-lg shadow-primary/20 transition-all duration-200 active:scale-[0.98] flex items-center justify-center gap-2 group" type="submit">
              <span>Lưu thay đổi</span>
              <span class="material-symbols-outlined text-[20px] transition-transform group-hover:translate-x-1">arrow_forward</span>
            </button>
          </div>
        </form>

        <!-- Back to Login -->
        <div class="mt-8 text-center">
          <router-link to="/login" class="text-sm font-bold text-primary hover:underline underline-offset-4 flex items-center justify-center gap-1 transition-all">
            <span class="material-symbols-outlined text-[18px]">keyboard_backspace</span>
            Quay lại đăng nhập
          </router-link>
        </div>
      </div>
    </main>

    <!-- Background Decoration -->
    <div class="fixed inset-0 -z-10 overflow-hidden pointer-events-none opacity-50">
      <div class="absolute -top-[10%] -left-[5%] w-[40%] h-[40%] bg-primary-fixed/30 blur-[120px] rounded-full"></div>
      <div class="absolute bottom-[0%] right-[0%] w-[30%] h-[30%] bg-secondary-fixed/20 blur-[100px] rounded-full"></div>
    </div>
  </div>
</template>

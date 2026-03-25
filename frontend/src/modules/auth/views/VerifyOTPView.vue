<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const digits = ref(['', '', '', '', '', ''])
const inputRefs = ref([])

onMounted(() => {
  // Focus first input
  if (inputRefs.value[0]) inputRefs.value[0].focus()
})

const handleInput = (index, event) => {
  const value = event.target.value
  if (value && index < 5) {
    inputRefs.value[index + 1].focus()
  }
}

const handleKeyDown = (index, event) => {
  if (event.key === 'Backspace' && !digits.value[index] && index > 0) {
    inputRefs.value[index - 1].focus()
  }
}

const handleSubmit = () => {
  const otp = digits.value.join('')
  console.log('Verifying OTP:', otp)
  // Mock navigate to reset password
  router.push('/reset-password')
}

const resendOTP = () => {
  console.log('Resending OTP...')
}
</script>

<template>
  <div class="verify-otp-page bg-surface-container-low min-h-screen flex flex-col items-center justify-center p-6 antialiased font-plus-jakarta">
    <!-- Brand Identity Logo -->
    <div class="mb-10 flex flex-col items-center gap-2">
      <div class="w-12 h-12 bg-primary flex items-center justify-center rounded-xl shadow-lg shadow-primary/20">
        <span class="material-symbols-outlined text-white text-3xl" style="font-variation-settings: 'FILL' 1;">verified_user</span>
      </div>
      <h1 class="text-xl font-bold tracking-tighter text-on-surface">AET LMS</h1>
    </div>

    <!-- Main OTP Verification Card -->
    <main class="w-full max-w-md bg-white rounded-2xl p-8 md:p-10 shadow-xl border border-surface-container">
      <div class="text-center mb-8">
        <h3 class="text-2xl font-extrabold text-on-surface tracking-tight mb-3">Xác thực tài khoản</h3>
        <p class="text-sm text-on-surface-variant leading-relaxed">
          Mã xác thực gồm 6 chữ số đã được gửi đến email <br>
          <span class="font-bold text-on-surface">user@example.com</span>. <br>
          Vui lòng kiểm tra hộp thư.
        </p>
      </div>

      <!-- OTP Input Form -->
      <form class="space-y-8" @submit.prevent="handleSubmit">
        <div class="flex justify-between items-center space-x-3">
          <input 
            v-for="(digit, index) in digits" 
            :key="index"
            ref="inputRefs"
            v-model="digits[index]"
            class="w-full h-14 text-center text-xl font-bold bg-surface-container-high border-none rounded-lg ring-0 focus:ring-2 focus:ring-primary outline-none transition-all" 
            maxlength="1" 
            pattern="\d*" 
            type="text"
            @input="handleInput(index, $event)"
            @keydown="handleKeyDown(index, $event)"
          />
        </div>

        <!-- Timer and Resend -->
        <div class="flex flex-col items-center space-y-4">
          <div class="flex items-center text-sm font-medium text-on-surface-variant">
            <span class="material-symbols-outlined text-sm mr-2">schedule</span>
            Mã hết hạn sau: <span class="font-bold ml-1.5 text-on-surface">02:59</span>
          </div>
          <div class="text-sm">
            <span class="text-on-surface-variant">Không nhận được mã?</span>
            <button class="ml-1 text-primary font-bold hover:underline transition-all" type="button" @click="resendOTP">
              Gửi lại mã
            </button>
          </div>
        </div>

        <!-- Action Button -->
        <button class="w-full py-4 bg-primary hover:bg-primary-container text-white font-bold rounded-xl shadow-lg shadow-primary/20 transition-all active:scale-[0.98]" type="submit">
          Xác nhận
        </button>
      </form>

      <!-- Back to Login Navigation -->
      <div class="mt-8 pt-6 border-t border-surface-container-low flex justify-center">
        <router-link to="/login" class="flex items-center text-sm font-bold text-on-surface-variant hover:text-primary transition-colors">
          <span class="material-symbols-outlined text-sm mr-1.5">arrow_back</span>
          Quay lại đăng nhập
        </router-link>
      </div>
    </main>

    <!-- Footer -->
    <footer class="w-full py-8 mt-12">
      <div class="max-w-7xl mx-auto px-8 flex flex-col md:flex-row justify-between items-center gap-4">
        <div class="text-[10px] font-bold uppercase tracking-widest text-slate-400">
          © 2026 AET LMS. All rights reserved.
        </div>
      </div>
    </footer>
  </div>
</template>

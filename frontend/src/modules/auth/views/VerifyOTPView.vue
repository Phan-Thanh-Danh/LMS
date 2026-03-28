<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const router = useRouter()
const authStore = useAuthStore()
const digits = ref(['', '', '', '', '', ''])
const inputRefs = ref([])
const isVerifying = ref(false)
const isSuccess = ref(false)
const errorMsg = ref('')

onMounted(() => {
  if (inputRefs.value[0]) inputRefs.value[0].focus()
})

const handleInput = (index, event) => {
  const value = event.target.value
  errorMsg.value = ''
  if (value && index < 5) {
    inputRefs.value[index + 1].focus()
  }
}

const handleKeyDown = (index, event) => {
  errorMsg.value = ''
  if (event.key === 'Backspace' && !digits.value[index] && index > 0) {
    inputRefs.value[index - 1].focus()
  }
}

const handleSubmit = async () => {
  const otp = digits.value.join('')
  if (otp.length < 6) return
  
  isVerifying.value = true
  errorMsg.value = ''

  if (authStore.pendingAction === 'forgot-password') {
    authStore.pendingOtpCode = otp
    isVerifying.value = false
    router.push('/reset-password')
  } else {
    const result = await authStore.verifyEmail(otp)
    isVerifying.value = false
    
    if (result.success) {
      isSuccess.value = true
    } else {
      errorMsg.value = result.message
    }
  }
}

const resendOTP = async () => {
  isVerifying.value = true
  errorMsg.value = ''
  const result = await authStore.resendOtp()
  isVerifying.value = false
  if (result.success) {
    alert('Đã gửi lại mã OTP vào email của bạn.')
  } else {
    errorMsg.value = result.message || 'Lỗi khi gửi lại OTP.'
  }
}
</script>

<template>
  <div class="verify-otp-page bg-surface-container-low min-h-screen flex flex-col items-center justify-center p-6 antialiased font-plus-jakarta relative overflow-hidden">
    <!-- Background Decor -->
    <div class="absolute -top-24 -left-24 w-96 h-96 bg-primary/5 rounded-full blur-3xl"></div>
    <div class="absolute -bottom-24 -right-24 w-96 h-96 bg-emerald-500/5 rounded-full blur-3xl"></div>

    <!-- Brand Identity Logo -->
    <div class="mb-10 flex flex-col items-center gap-2 relative z-10">
      <div class="w-12 h-12 bg-primary flex items-center justify-center rounded-xl shadow-lg shadow-primary/20">
        <span class="material-symbols-outlined text-white text-3xl font-bold">verified_user</span>
      </div>
      <h1 class="text-xl font-black tracking-tighter text-on-surface uppercase">AET <span class="text-primary">LMS</span></h1>
    </div>

    <!-- Main OTP Verification Card -->
    <main class="w-full max-w-md bg-white rounded-4xl p-8 md:p-12 shadow-2xl shadow-slate-200/50 border border-surface-container relative z-10 overflow-hidden">
      
      <Transition name="fade-slide" mode="out-in">
        <!-- Success State -->
        <div v-if="isSuccess" class="py-4 text-center space-y-8 flex flex-col items-center">
            <div class="w-24 h-24 bg-emerald-50 rounded-full flex items-center justify-center relative shadow-inner">
                <div class="absolute inset-0 bg-emerald-500/10 rounded-full animate-ping"></div>
                <span class="material-symbols-outlined text-emerald-500 text-5xl font-bold relative z-10 transition-transform hover:scale-110 duration-500">task_alt</span>
            </div>
            
            <div class="space-y-3">
                <h3 class="text-3xl font-black text-on-surface tracking-tighter uppercase leading-tight">Mọi thứ xong xuôi!</h3>
                <p class="text-on-surface-variant font-medium leading-relaxed max-w-[280px] mx-auto">
                    Chúc mừng! Tài khoản của bạn đã được xác thực và tạo thành công.
                </p>
            </div>

            <router-link to="/login" class="w-full py-4 bg-primary hover:bg-blue-700 text-white font-black text-sm rounded-2xl shadow-lg shadow-primary/20 transition-all active:scale-[0.98] flex items-center justify-center gap-2 uppercase tracking-tight">
                <span class="material-symbols-outlined text-lg">login</span>
                Đăng nhập ngay
            </router-link>
        </div>

        <!-- Input State -->
        <div v-else class="space-y-10">
            <div class="text-center">
                <h3 class="text-3xl font-black text-on-surface tracking-tighter uppercase mb-4 leading-tight">Xác thực tài khoản</h3>
                <p class="text-sm text-on-surface-variant font-medium leading-relaxed">
                Mã 6 chữ số vừa được gửi đến email: <br>
                <span class="font-black text-primary uppercase tracking-tighter mt-1 block">{{ authStore.pendingRegistrationEmail || 'user@example.com' }}</span>
                </p>
            </div>

            <!-- OTP Input Form -->
            <form class="space-y-10" @submit.prevent="handleSubmit">
                <div class="flex justify-between items-center space-x-3">
                <input 
                    v-for="(digit, index) in digits" 
                    :key="index"
                    ref="inputRefs"
                    v-model="digits[index]"
                    class="w-full h-16 text-center text-2xl font-black bg-surface-container-low border-2 border-transparent focus:border-primary focus:bg-white rounded-xl outline-none transition-all duration-300 shadow-sm focus:shadow-xl focus:shadow-primary/5" 
                    maxlength="1" 
                    pattern="\d*" 
                    type="text"
                    :disabled="isVerifying"
                    @input="handleInput(index, $event)"
                    @keydown="handleKeyDown(index, $event)"
                />
                </div>
                
                <p v-if="errorMsg" class="text-xs text-red-500 font-medium flex items-center justify-center gap-1 mt-2">
                  <span class="material-symbols-outlined text-[14px]">error</span>
                  {{ errorMsg }}
                </p>

                <!-- Timer and Resend -->
                <div class="flex flex-col items-center space-y-6">
                <div class="flex items-center text-[10px] font-black tracking-widest text-on-surface-variant uppercase bg-surface-container-low px-4 py-2 rounded-full border border-surface-container">
                    <span class="material-symbols-outlined text-sm mr-2 text-primary">schedule</span>
                    Mã hết hạn sau: <span class="font-black ml-1.5 text-on-surface">02:59</span>
                </div>
                <div class="text-[11px] font-bold uppercase tracking-wider">
                    <span class="text-on-surface-variant">Không nhận được mã?</span>
                    <button class="ml-2 text-primary hover:text-blue-700 transition-colors underline-offset-4 hover:underline" type="button" @click="resendOTP">
                    Gửi lại mã
                    </button>
                </div>
                </div>

                <!-- Action Button -->
                <button :disabled="isVerifying" class="w-full py-4 bg-primary hover:bg-blue-700 disabled:bg-slate-300 text-white font-black rounded-2xl shadow-xl shadow-primary/20 transition-all active:scale-[0.98] flex items-center justify-center gap-3 relative overflow-hidden group" type="submit">
                <span v-if="!isVerifying" class="flex items-center gap-2 uppercase text-sm tracking-tight">
                    <span class="material-symbols-outlined text-lg">check_circle</span>
                    Xác nhận kích hoạt
                </span>
                <div v-else class="flex items-center gap-2">
                    <div class="w-5 h-5 border-3 border-white/30 border-t-white rounded-full animate-spin"></div>
                    <span class="uppercase text-sm tracking-tight">Đang xác thực...</span>
                </div>
                </button>
            </form>

            <!-- Back to Login Navigation -->
            <div class="pt-8 border-t border-gray-50 flex justify-center">
                <router-link to="/login" class="flex items-center text-xs font-black uppercase tracking-widest text-on-surface-variant hover:text-primary transition-colors group">
                <span class="material-symbols-outlined text-lg mr-2 transition-transform group-hover:-translate-x-1">arrow_back</span>
                Quay lại đăng nhập
                </router-link>
            </div>
        </div>
      </Transition>
    </main>

    <!-- Footer -->
    <footer class="w-full py-8 mt-4 relative z-10 text-center">
        <p class="text-[10px] font-black uppercase tracking-[0.3em] text-slate-400">
            © 2026 AET LMS ACADEMY. SECURE AUTHENTICATION.
        </p>
    </footer>
  </div>
</template>

<style scoped>
.fade-slide-enter-active, .fade-slide-leave-active {
  transition: all 0.5s cubic-bezier(0.4, 0, 0.2, 1);
}
.fade-slide-enter-from {
  opacity: 0;
  transform: translateY(20px);
}
.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(-20px);
}
</style>

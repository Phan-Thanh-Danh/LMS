<script setup>
import { ref } from 'vue'

const fullName = ref('')
const email = ref('')
const password = ref('')
const confirmPassword = ref('')
const showPassword = ref(false)
const agreeTerms = ref(false)

const handleRegister = () => {
  console.log('Register attempt:', { fullName: fullName.value, email: email.value, password: password.value })
  // Integration logic here
}
</script>

<template>
  <main class="flex min-h-screen bg-surface font-plus-jakarta antialiased">
    <!-- Left Side: Branding/Illustration (Shared) -->
    <section class="hidden lg:flex lg:w-1/2 relative overflow-hidden items-center justify-center bg-primary-container">
      <div class="absolute inset-0 z-0">
        <img 
          alt="Corporate Learning Environment" 
          class="w-full h-full object-cover opacity-30 grayscale mix-blend-overlay" 
          src="https://lh3.googleusercontent.com/aida-public/AB6AXuCbNiRv-_SyTdYpYCdCA5M_RqScv52gB9j1Hve7RORqVpQDyEKqMIFty2zKdfK4Oui7gkyi63iWmzTbc3FiwB7GpO41tVMHWIgOwXiMtgVqa-suYbovsQfVHDzLS7NElCC3Ojyt5CTrtEa6J6u6-GR_jbO1FWTRiSZNaC9gRHR26I2KqZV1jrx83SWCXvzlNbAGJsgE2o5pvR_zAqdJy3X9UOmgT1NORJG9tCYT_z6EY1rTnnxagOBGI3tLn3UbRRBrEMr3i8ngaVQ"
        >
        <div class="absolute inset-0 bg-linear-to-br from-primary-container/80 via-primary/90 to-on-primary-fixed-variant/95"></div>
      </div>
      <div class="relative z-10 px-16 max-w-2xl text-white">
        <div class="mb-12 inline-flex items-center gap-3 px-4 py-2 rounded-full border border-white/10 glass-panel">
          <span class="flex h-2 w-2 rounded-full bg-blue-300 animate-pulse"></span>
          <span class="text-xs font-bold tracking-widest uppercase text-white/90">Enterprise Learning Solution</span>
        </div>
        <h1 class="text-5xl font-extrabold tracking-tighter mb-8 leading-[1.1] text-white">
          Kiến tạo tương lai <br> số của doanh nghiệp.
        </h1>
        <p class="text-xl font-medium text-blue-100 leading-relaxed mb-8">Bắt đầu hành trình học tập chuyên nghiệp của bạn ngay hôm nay.</p>
      </div>
    </section>

    <!-- Right Side: Auth Form Container -->
    <section class="w-full lg:w-1/2 flex flex-col items-center justify-center px-6 md:px-12 py-12 bg-surface">
      <header class="absolute top-0 w-full flex justify-between items-center px-8 py-6 z-50 pointer-events-none lg:w-1/2 lg:right-0">
        <div class="text-xl font-bold tracking-tighter text-slate-900 pointer-events-auto flex items-center gap-2">
          <span class="material-symbols-outlined text-primary-container" style="font-variation-settings: 'FILL' 1;">auto_awesome_motion</span>AET LMS
        </div>
      </header>

      <div class="w-full max-w-md">
        <div class="space-y-8">
          <div class="text-center lg:text-left">
            <h2 class="text-3xl font-bold tracking-tight text-on-surface mb-2">Đăng ký tài khoản</h2>
            <p class="text-on-surface-variant">Tham gia cộng đồng học tập lớn nhất khu vực.</p>
          </div>

          <form class="space-y-5" @submit.prevent="handleRegister">
            <!-- Full Name -->
            <div class="space-y-2">
              <label class="block text-sm font-semibold text-slate-700" for="fullName">Họ và tên</label>
              <div class="relative">
                <span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-outline text-sm">person</span>
                <input 
                  id="fullName" 
                  v-model="fullName"
                  class="w-full pl-10 pr-4 py-3 bg-white border border-outline-variant rounded-md focus:ring-2 focus:ring-primary-container outline-none transition-all" 
                  placeholder="Nguyễn Văn A" 
                  required
                >
              </div>
            </div>

            <!-- Email -->
            <div class="space-y-2">
              <label class="block text-sm font-semibold text-slate-700" for="email">Địa chỉ Email</label>
              <div class="relative">
                <span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-outline text-sm">mail</span>
                <input 
                  id="email" 
                  v-model="email"
                  class="w-full pl-10 pr-4 py-3 bg-white border border-outline-variant rounded-md focus:ring-2 focus:ring-primary-container outline-none transition-all" 
                  placeholder="name@company.com" 
                  required 
                  type="email"
                >
              </div>
            </div>

            <!-- Password -->
            <div class="space-y-2">
              <label class="block text-sm font-semibold text-slate-700" for="password">Mật khẩu</label>
              <div class="relative">
                <span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-outline text-sm">lock</span>
                <input 
                  id="password" 
                  v-model="password"
                  :type="showPassword ? 'text' : 'password'"
                  class="w-full pl-10 pr-10 py-3 bg-white border border-outline-variant rounded-md focus:ring-2 focus:ring-primary-container outline-none transition-all" 
                  placeholder="Tối thiểu 8 ký tự" 
                  required
                >
                <button type="button" class="absolute right-3 top-1/2 -translate-y-1/2 text-outline" @click="showPassword = !showPassword">
                  <span class="material-symbols-outlined text-sm">{{ showPassword ? 'visibility_off' : 'visibility' }}</span>
                </button>
              </div>
            </div>

            <!-- Terms -->
            <div class="flex items-center">
              <input id="terms" v-model="agreeTerms" class="w-4 h-4 text-primary border-outline-variant rounded focus:ring-primary" type="checkbox" required>
              <label class="ml-2 text-sm text-on-surface-variant font-medium cursor-pointer" for="terms">
                Tôi đồng ý với <a href="#" class="text-primary hover:underline">Điều khoản</a> và <a href="#" class="text-primary hover:underline">Chính sách</a>
              </label>
            </div>

            <button class="w-full py-3.5 px-4 bg-primary text-white font-bold rounded-md hover:shadow-lg hover:shadow-primary/20 active:scale-[0.98] transition-all flex items-center justify-center gap-2 group" type="submit">
              <span>Đăng ký tham gia</span>
              <span class="material-symbols-outlined text-sm group-hover:translate-x-1 transition-transform">arrow_forward</span>
            </button>
          </form>

          <p class="text-center text-sm text-on-surface-variant font-medium">
            Đã có tài khoản? 
            <router-link to="/login" class="text-primary font-bold hover:underline underline-offset-4 ml-1">Đăng nhập</router-link>
          </p>
        </div>
      </div>
    </section>
  </main>
</template>

<style scoped>
.glass-panel {
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(16px);
}
</style>

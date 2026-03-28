<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const router = useRouter()
const authStore = useAuthStore()

const emailOrName = ref('')
const password = ref('')
const showPassword = ref(false)
const rememberMe = ref(false)
const errorMsg = ref('')
const isLoading = ref(false)

const handleLogin = async () => {
  if (!emailOrName.value || !password.value) {
    errorMsg.value = 'Vui lòng nhập đầy đủ email và mật khẩu.'
    return
  }

  isLoading.value = true
  errorMsg.value = ''

  const result = await authStore.login(emailOrName.value, password.value)

  isLoading.value = false

  if (result.success) {
    router.push('/dashboard')
  } else {
    errorMsg.value = result.message
  }
}
</script>

<template>
  <main class="flex min-h-screen bg-surface font-plus-jakarta antialiased">
    <!-- Left Side: Branding/Illustration (Desktop Only) -->
    <section class="hidden lg:flex lg:w-1/2 relative overflow-hidden items-center justify-center bg-primary-container">
      <!-- Background Image with Overlay -->
      <div class="absolute inset-0 z-0">
        <img 
          alt="Corporate Learning Environment" 
          class="w-full h-full object-cover opacity-30 grayscale mix-blend-overlay" 
          src="https://lh3.googleusercontent.com/aida-public/AB6AXuCbNiRv-_SyTdYpYCdCA5M_RqScv52gB9j1Hve7RORqVpQDyEKqMIFty2zKdfK4Oui7gkyi63iWmzTbc3FiwB7GpO41tVMHWIgOwXiMtgVqa-suYbovsQfVHDzLS7NElCC3Ojyt5CTrtEa6J6u6-GR_jbO1FWTRiSZNaC9gRHR26I2KqZV1jrx83SWCXvzlNbAGJsgE2o5pvR_zAqdJy3X9UOmgT1NORJG9tCYT_z6EY1rTnnxagOBGI3tLn3UbRRBrEMr3i8ngaVQ"
        >
        <div class="absolute inset-0 bg-linear-to-br from-primary-container/80 via-primary/90 to-on-primary-fixed-variant/95"></div>
      </div>
      
      <!-- Content Over Image -->
      <div class="relative z-10 px-16 max-w-2xl text-white">
        <div class="mb-12 inline-flex items-center gap-3 px-4 py-2 rounded-full border border-white/10 glass-panel">
          <span class="flex h-2 w-2 rounded-full bg-blue-300 animate-pulse"></span>
          <span class="text-xs font-bold tracking-widest uppercase text-white/90">Enterprise Learning Solution</span>
        </div>
        <h1 class="text-5xl font-extrabold tracking-tighter mb-8 leading-[1.1] text-white">
          Kiến tạo tương lai <br> số của doanh nghiệp.
        </h1>
        <div class="flex flex-col gap-6">
          <blockquote class="text-xl font-medium text-blue-100 leading-relaxed italic border-l-4 border-blue-400 pl-6">
            "Khám phá tiềm năng bản thân cùng cộng đồng 2,400+ chuyên gia hàng đầu thế giới."
          </blockquote>
          <div class="flex items-center gap-4 mt-4">
            <div class="flex -space-x-3">
              <img class="h-10 w-10 rounded-full border-2 border-primary ring-2 ring-primary-container object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuD3Xe0q4erLt_tyZS1LFRF3JRk6rsBd5DGKO3CVi8G5j7HAtaQG8NdwghYJsrS9z0gBW8NJ5VIsgRc8njjd604eekart3l0nptbFucxxP_fYxA3v5mai1s2b_3LxVpduCp5f-sZm7bCccBC598L1BajU8ftK1X0ayfW-mE8pVa4x8uFhCJGWz3mCgP3rFcSuS-W93l75oMPYbndg8MdjNRXNURAnRs8gT78rJ3QyQwCIiNblm4GrnU8RLMIBo0L4UKDZnoClv86Oz0" alt="Avatar 1">
              <img class="h-10 w-10 rounded-full border-2 border-primary ring-2 ring-primary-container object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuC6U5nbOEruHEFKvf8cFIXeGknxMGqn9mA1S86hMOMXrFdNLzBv7GW3s7Ei3cwIM_FipDzfV8G6MvL04ZKHyVoV16dqkV7VidvRrJfJVZe0DUg5we_bSbNoLGiicjzgLOxSiB7MgTnS69gDY4NbTm2iKcso_aMuPECGeXTz1RhOFY5c_DWecytH3_tzYw6P6svKh2yY8Fq-0HJTQec6AmT-tKEx8IiBttmDYTHSkyfAAi70ojdT2qdjTjiPJshY6Fo1oTDNgCI3mXY" alt="Avatar 2">
              <img class="h-10 w-10 rounded-full border-2 border-primary ring-2 ring-primary-container object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuBb51p4mjn45w2-OisFPnGXmPkJRAUenOZiO8ZkMQHQf2kA-WQTyx8wIihbss-1ozjKGt6GLhjnJcV9tIEAuuYERxtvJNwPN7yWre5kHX8yIpSGY0rUj65TGYQuZ1-W4qm9zQLwvWmDxCJXXYu6bHHmgdhU7ewpJoWfc42LxVuqPf7lbANI71MIaJSjj8K0hGxroaMBd08T8wvELu0ZVFp4ooHcQDkgD9i8Go7MLfeZQiA_i1UP7MzLS7IvVpJRox17owo1cLa2BMI" alt="Avatar 3">
            </div>
            <p class="text-sm text-blue-100 font-medium">Tham gia cùng đội ngũ <span class="text-white font-bold underline underline-offset-4 decoration-blue-400">tinh hoa</span> toàn cầu.</p>
          </div>
        </div>
      </div>
      <!-- Abstract Shapes for Visual Interest -->
      <div class="absolute -bottom-24 -left-24 w-96 h-96 bg-blue-400/10 rounded-full blur-3xl"></div>
      <div class="absolute -top-24 -right-24 w-96 h-96 bg-indigo-500/20 rounded-full blur-3xl"></div>
    </section>

    <!-- Right Side: Auth Form Container -->
    <section class="w-full lg:w-1/2 flex flex-col items-center justify-center px-6 md:px-12 py-12 bg-surface">
      <header class="absolute top-0 w-full flex justify-between items-center px-8 py-6 z-50 pointer-events-none lg:w-1/2 lg:right-0">
        <div class="text-xl font-bold tracking-tighter text-slate-900 pointer-events-auto flex items-center gap-2">
          <span class="material-symbols-outlined text-primary-container" style="font-variation-settings: 'FILL' 1;">auto_awesome_motion</span>AET LMS
        </div>
        <div class="hidden md:flex gap-8 items-center pointer-events-auto">
          <router-link to="/" class="text-sm font-medium text-slate-500 hover:text-blue-600 transition-colors">Về Home</router-link>
          <button class="text-sm font-semibold text-blue-700 hover:text-blue-600 transition-colors">Support</button>
        </div>
      </header>

      <div class="w-full max-w-md">
        <div class="space-y-8">
          <!-- Header -->
          <div class="text-center lg:text-left">
            <h2 class="text-3xl font-bold tracking-tight text-on-surface mb-2">Chào mừng trở lại</h2>
            <p class="text-on-surface-variant">Đăng nhập để tiếp tục hành trình học tập của bạn.</p>
          </div>

          <!-- Social Logins -->
          <div class="grid grid-cols-2 gap-4">
            <button class="flex items-center justify-center gap-3 px-4 py-3 bg-white border border-outline-variant rounded-lg hover:bg-surface-container-low transition-all active:scale-95 group">
              <img class="w-5 h-5" src="https://lh3.googleusercontent.com/aida-public/AB6AXuCTOL3_vMc35BR6vPbBldwOxZKI4ApPQpge4-aVdWTTgwDBkjzES_hgUgIaNB1xBURg_K3xPOIo-OU1o9F5CC6ZySI9YtV8G8LmqL4gXlcgTrI6XWNcUBOoiAnE1HTFN93AeZUaFnO1cmg7jh1FyaSgrcKyusWnRKFFUxqljO1fiZosjw8X6sF1V7exO-vUJ9_o5tu24N3xp3dGdiaTeFbenXiIsZ8P4E1tc6tBWauS3XqQpAzhpmyW7tANIaHmWa5mFF2sfNer5w0" alt="Google">
              <span class="text-sm font-semibold text-on-surface">Google</span>
            </button>
            <button class="flex items-center justify-center gap-3 px-4 py-3 bg-white border border-outline-variant rounded-lg hover:bg-surface-container-low transition-all active:scale-95 group">
              <img class="w-5 h-5" src="https://lh3.googleusercontent.com/aida-public/AB6AXuAgRhIxEdVBSIrwM9MJfU2_F3jse4yjxAChy1PgyQK7tnEP8Ta69qDSxGcsZq1MCEp7xiJ-Uh2ef5-FPw-HaqJTvEfZwYsvDSckqqpN2bHO_smGtyDbT_7w4xflYqlk-HyP9VFMz5m5uNxA6TOyy3st8zrtoDme7jEY0HZXOeMQ68mQ2etPlDc9oSgDPAZCZr0Lq8p3ra3g9sc5-Zd_VfZxulbkairtLiECt2yM-eNMMdSwMDHhWO-fqxNKasmgR3QjeGncJnpsz3I" alt="Facebook">
              <span class="text-sm font-semibold text-on-surface">Facebook</span>
            </button>
          </div>

          <!-- Divider -->
          <div class="relative py-2">
            <div class="absolute inset-0 flex items-center"><div class="w-full border-t border-outline-variant/50"></div></div>
            <div class="relative flex justify-center text-xs uppercase tracking-widest font-bold">
              <span class="bg-surface px-4 text-on-surface-variant/60">Hoặc tiếp tục với</span>
            </div>
          </div>

          <!-- Form -->
          <form class="space-y-6" @submit.prevent="handleLogin">
            <div class="space-y-2">
              <label class="block text-sm font-semibold text-slate-700" for="email">Họ tên hoặc Email</label>
              <div class="relative">
                <span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-outline text-sm">mail</span>
                <input 
                  id="email" 
                  v-model="emailOrName"
                  class="w-full pl-10 pr-4 py-3 bg-surface-container-high border-outline-variant rounded-md focus:ring-2 focus:ring-primary-container outline-none transition-all" 
                  placeholder="name@company.com" 
                  required 
                >
              </div>
              <p v-if="errorMsg" class="text-xs text-error font-medium flex items-center gap-1 mt-1">
                <span class="material-symbols-outlined text-[14px]">error</span>
                {{ errorMsg }}
              </p>
            </div>

            <!-- Password Input -->
            <div class="space-y-2">
              <div class="flex justify-between items-center">
                <label class="block text-sm font-semibold text-slate-700" for="password">Mật khẩu</label>
                <router-link to="/forgot-password" class="text-xs font-bold text-primary max-w-fit hover:underline">Quên mật khẩu?</router-link>
              </div>
              <div class="relative">
                <span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-outline text-sm">lock</span>
                <input 
                  id="password" 
                  v-model="password"
                  :type="showPassword ? 'text' : 'password'"
                  class="w-full pl-10 pr-10 py-3 bg-surface-container-high border-outline-variant rounded-md focus:ring-2 focus:ring-primary-container outline-none transition-all" 
                  placeholder="••••••••" 
                  required
                >
                <button 
                  type="button"
                  class="absolute right-3 top-1/2 -translate-y-1/2 text-outline hover:text-on-surface transition-colors"
                  @click="showPassword = !showPassword"
                >
                  <span class="material-symbols-outlined text-sm">{{ showPassword ? 'visibility_off' : 'visibility' }}</span>
                </button>
              </div>
            </div>

            <!-- Remember Me -->
            <div class="flex items-center">
              <input id="remember" v-model="rememberMe" class="w-4 h-4 text-primary border-outline-variant rounded focus:ring-primary" type="checkbox">
              <label class="ml-2 text-sm text-on-surface-variant font-medium cursor-pointer" for="remember">Duy trì đăng nhập</label>
            </div>

            <!-- Submit Button -->
            <button 
              class="w-full py-3.5 px-4 bg-primary text-white font-bold rounded-md hover:shadow-lg hover:shadow-primary/20 active:scale-[0.98] transition-all flex items-center justify-center gap-2 group disabled:opacity-70 disabled:cursor-not-allowed" 
              type="submit"
              :disabled="isLoading"
            >
              <span v-if="isLoading">Đang xử lý...</span>
              <template v-else>
                <span>Đăng nhập</span>
                <span class="material-symbols-outlined text-sm group-hover:translate-x-1 transition-transform">arrow_forward</span>
              </template>
            </button>
          </form>

          <!-- Footer Action -->
          <p class="text-center text-sm text-on-surface-variant font-medium">
            Chưa có tài khoản? 
            <router-link to="/register" class="text-primary font-bold hover:underline underline-offset-4 ml-1">Đăng ký ngay</router-link>
          </p>
        </div>
      </div>

      <!-- Footer -->
      <footer class="absolute bottom-0 flex justify-between items-center w-full px-8 py-4 z-10 lg:w-1/2">
        <p class="text-[10px] uppercase tracking-widest text-slate-400">© 2026 AET LMS. All rights reserved.</p>
        <div class="flex gap-4">
          <a class="text-[10px] uppercase tracking-widest text-slate-400 hover:text-slate-600 transition-opacity" href="#">Privacy Policy</a>
          <a class="text-[10px] uppercase tracking-widest text-slate-400 hover:text-slate-600 transition-opacity" href="#">Terms of Service</a>
        </div>
      </footer>
    </section>
  </main>
</template>

<style scoped>
.glass-panel {
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(16px);
}
</style>

<template>
  <LayoutUdemy>
    <div class="min-h-[75vh] flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8 bg-white">
      <div class="max-w-[400px] w-full space-y-8">
        <div>
          <h2 class="mt-2 text-center text-[26px] font-bold text-gray-900 font-serif leading-tight">
            Đăng nhập vào tài khoản AET của bạn
          </h2>
        </div>
        <form class="mt-8 space-y-4" @submit.prevent="handleLogin">
          <!-- Social Login Buttons -->
          <div class="space-y-3 mb-2">
            <button type="button" class="w-full flex items-center justify-center gap-3 bg-white border border-gray-300 rounded-sm px-4 py-3.5 text-[15px] font-bold text-gray-700 hover:bg-gray-50 transition-colors shadow-sm">
              <img src="https://www.svgrepo.com/show/475656/google-color.svg" alt="Google" class="w-5 h-5">
              Tiếp tục với Google
            </button>
            <button type="button" class="w-full flex items-center justify-center gap-3 bg-white border border-gray-300 rounded-sm px-4 py-3.5 text-[15px] font-bold text-gray-700 hover:bg-gray-50 transition-colors shadow-sm">
              <svg class="w-5 h-5 text-[#1877F2]" fill="currentColor" viewBox="0 0 24 24"><path d="M24 12.073c0-6.627-5.373-12-12-12s-12 5.373-12 12c0 5.99 4.388 10.954 10.125 11.854v-8.385H7.078v-3.469h3.047V9.43c0-3.007 1.792-4.669 4.533-4.669 1.312 0 2.686.235 2.686.235v2.953H15.83c-1.491 0-1.956.925-1.956 1.874v2.25h3.328l-.532 3.469h-2.796v8.385C19.612 23.027 24 18.062 24 12.073z"/></svg>
              Tiếp tục với Facebook
            </button>
            <button type="button" class="w-full flex items-center justify-center gap-3 bg-white border border-gray-300 rounded-sm px-4 py-3.5 text-[15px] font-bold text-gray-700 hover:bg-gray-50 transition-colors shadow-sm">
              <svg class="w-5 h-5 text-gray-900" fill="currentColor" viewBox="0 0 24 24"><path d="M12 0C5.373 0 0 5.373 0 12c0 5.302 3.438 9.8 8.207 11.387.6.113.793-.261.793-.577v-2.234c-3.338.726-4.033-1.416-4.033-1.416-.546-1.387-1.333-1.756-1.333-1.756-1.089-.745.083-.729.083-.729 1.205.084 1.839 1.237 1.839 1.237 1.07 1.834 2.807 1.304 3.492.997.107-.775.418-1.305.762-1.604-2.665-.305-5.467-1.334-5.467-5.931 0-1.311.469-2.381 1.236-3.221-.124-.303-.535-1.524.117-3.176 0 0 1.008-.322 3.301 1.23.957-.266 1.983-.399 3.003-.404 1.02.005 2.047.138 3.006.404 2.291-1.552 3.297-1.23 3.297-1.23.653 1.653.242 2.874.118 3.176.77.84 1.235 1.911 1.235 3.221 0 4.609-2.807 5.624-5.479 5.921.43.372.823 1.102.823 2.222v3.293c0 .319.192.694.801.576C20.566 21.797 24 17.3 24 12c0-6.627-5.373-12-12-12z"/></svg>
              Tiếp tục với GitHub
            </button>
          </div>

          <div class="relative py-2">
            <div class="absolute inset-0 flex items-center">
              <div class="w-full border-t border-gray-300"></div>
            </div>
            <div class="relative flex justify-center text-[13px]">
              <span class="px-2 bg-white text-gray-500 font-medium">Hoặc</span>
            </div>
          </div>

          <div class="space-y-4">
            <div>
              <label for="email" class="sr-only">Email</label>
              <input id="email" name="email" type="email" required class="appearance-none relative block w-full px-4 py-4 border border-black placeholder-gray-600 text-gray-900 rounded-sm focus:outline-none focus:ring-1 focus:ring-black focus:border-black focus:z-10 text-[15px]" placeholder="Email">
            </div>
            <div>
              <label for="password" class="sr-only">Mật khẩu</label>
              <input id="password" name="password" type="password" required class="appearance-none relative block w-full px-4 py-4 border border-black placeholder-gray-600 text-gray-900 rounded-sm focus:outline-none focus:ring-1 focus:ring-black focus:border-black focus:z-10 text-[15px]" placeholder="Mật khẩu">
            </div>
          </div>

          <div>
            <button type="submit" class="group relative w-full flex justify-center py-3.5 px-4 border border-transparent text-[16px] font-bold rounded-sm text-white bg-[#8731e8] hover:bg-[#6c27ba] focus:outline-none transition-colors shadow-sm">
              Đăng nhập
            </button>
          </div>
          
          <div class="text-center text-[14px] mt-4">
            <span class="text-gray-600">Hoặc </span>
            <a href="#" class="font-bold text-[#8731e8] hover:text-[#521d8b] hover:underline">Quên mật khẩu</a>
          </div>
        </form>
        
        <div class="text-center text-[14.5px] text-gray-800 pt-6">
          Bạn chưa có tài khoản? 
          <router-link to="/register" class="font-bold text-[#8731e8] hover:text-[#521d8b] hover:underline ml-1">Đăng ký ngay</router-link>
        </div>
      </div>
    </div>
  </LayoutUdemy>
</template>

<script setup>
import LayoutUdemy from '../components/LayoutUdemy.vue'

const handleLogin = () => {
  console.log('Login submitted')
}
</script>

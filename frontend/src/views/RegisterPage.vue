<template>
  <LayoutUdemy>
    <div class="min-h-[75vh] flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8 bg-white">
      <div class="max-w-[400px] w-full space-y-6">
        <div>
          <h2 class="mt-2 text-center text-[26px] font-bold text-gray-900 font-serif leading-tight">
            Đăng ký và bắt đầu học ngay!
          </h2>
        </div>
        <form class="mt-8 space-y-5" @submit.prevent="handleRegister">
          <div class="space-y-4">
            <div>
              <label for="name" class="sr-only">Họ và tên</label>
              <input id="name" name="name" type="text" required class="appearance-none relative block w-full px-4 py-4 border border-black placeholder-gray-600 text-gray-900 rounded-sm focus:outline-none focus:ring-1 focus:ring-black focus:border-black focus:z-10 text-[15px]" placeholder="Họ tên đầy đủ">
            </div>
            <div>
              <label for="email" class="sr-only">Email</label>
              <input id="email" name="email" type="email" required class="appearance-none relative block w-full px-4 py-4 border border-black placeholder-gray-600 text-gray-900 rounded-sm focus:outline-none focus:ring-1 focus:ring-black focus:border-black focus:z-10 text-[15px]" placeholder="Email">
            </div>
            <div>
              <label for="password" class="sr-only">Mật khẩu</label>
              <input id="password" name="password" type="password" required class="appearance-none relative block w-full px-4 py-4 border border-black placeholder-gray-600 text-gray-900 rounded-sm focus:outline-none focus:ring-1 focus:ring-black focus:border-black focus:z-10 text-[15px]" placeholder="Mật khẩu">
            </div>
            <div>
              <label for="role" class="sr-only">Vai trò</label>
              <select id="role" name="role" required class="relative block w-full px-4 py-4 border border-black text-gray-900 rounded-sm focus:outline-none focus:ring-1 focus:ring-black focus:border-black focus:z-10 text-[15px] bg-white cursor-pointer">
                <option value="" disabled selected hidden>Chọn đăng ký với tư cách...</option>
                <option value="student">Học viên - Tìm kiếm khóa học</option>
                <option value="instructor">Giảng viên - Trở thành đối tác AET</option>
              </select>
            </div>
          </div>

          <div class="flex items-start mt-4 mb-4 px-1">
            <div class="flex items-center h-5">
              <input id="offers" name="offers" type="checkbox" checked class="h-4 w-4 bg-white border-black text-[#8731e8] focus:ring-[#8731e8] rounded-sm cursor-pointer accent-[#8731e8]">
            </div>
            <div class="ml-3 text-[14px]">
              <label for="offers" class="text-gray-800 leading-snug cursor-pointer">
                Gửi cho tôi các ưu đãi đặc biệt, đề xuất cá nhân, và bí quyết học tập hiệu quả.
              </label>
            </div>
          </div>

          <div>
            <button type="submit" class="group relative w-full flex justify-center py-3.5 px-4 border border-transparent text-[16px] font-bold rounded-sm text-white bg-[#8731e8] hover:bg-[#6c27ba] focus:outline-none transition-colors shadow-sm">
              Đăng ký
            </button>
          </div>
          
          <div class="text-[12px] text-gray-500 text-center leading-relaxed">
            Bằng việc đăng ký, bạn đồng ý với <a href="#" class="underline hover:text-[#8731e8]">Điều khoản sử dụng</a> và <a href="#" class="underline hover:text-[#8731e8]">Chính sách bảo mật</a> của AET.
          </div>
        </form>
        
        <div class="text-center text-[14.5px] text-gray-800 border-t border-gray-200 pt-6 mt-6">
          Đã có tài khoản? 
          <router-link to="/login" class="font-bold text-[#8731e8] hover:text-[#521d8b] hover:underline ml-1">Đăng nhập</router-link>
        </div>
      </div>
    </div>
  </LayoutUdemy>
</template>

<script setup>
import LayoutUdemy from '../components/LayoutUdemy.vue'

const handleRegister = () => {
  console.log('Register submitted')
}
</script>

<template>
  <LayoutUdemy>
    <main class="max-w-6xl mx-auto px-4 py-8 min-h-[60vh] flex flex-col text-gray-900 mt-6">
      <h1 class="text-[34px] font-bold font-serif mb-8 text-black">Giỏ hàng</h1>
      
      <p class="text-[17px] text-gray-900 font-bold mb-4">{{ cartItems.length }} Khóa học trong giỏ</p>

      <div class="flex flex-col lg:flex-row gap-10">
        <!-- List Items -->
        <div class="flex-1 space-y-4">
          <!-- Empty Cart State -->
          <div v-if="cartItems.length === 0" class="border border-gray-200 p-8 rounded-2xl flex flex-col items-center justify-center bg-white shadow-sm h-72 text-center mt-2">
            <img src="https://cdni.iconscout.com/illustration/premium/thumb/empty-cart-7359557-6024626.png" class="w-32 mb-4 opacity-75" alt="Giỏ hàng trống" />
            <p class="text-gray-600 mb-6 font-medium text-[15px]">Giỏ hàng của bạn đang trống. Hãy tiếp tục mua sắm để tìm một khóa học tuyệt vời!</p>
            <router-link to="/" class="bg-[#8731e8] hover:bg-[#6c27ba] text-white font-bold py-3.5 px-10 rounded-lg transition-colors shadow-md">Mua sắm ngay</router-link>
          </div>

          <!-- Items -->
          <div v-for="item in cartItems" :key="item.id" class="border-t border-gray-200 py-6 flex gap-4 transition-all group relative">
            <button @click="removeItem(item.id)" class="absolute top-4 right-0 text-gray-400 hover:text-red-600 transition-colors p-2 z-10" title="Xóa khỏi giỏ hàng">
              <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2.5" d="M6 18L18 6M6 6l12 12"></path></svg>
            </button>

            <img :src="item.img" alt="Course" class="w-36 aspect-video object-cover border border-gray-200 rounded-[10px]">
            
            <div class="flex-1 flex justify-between">
              <div class="w-3/5 pr-4">
                <h3 class="font-bold text-[16px] leading-[1.4] text-gray-900 mb-1 line-clamp-2 cursor-pointer">{{ item.title }}</h3>
                <p class="text-[13px] text-gray-600 mb-1.5">Kiến tạo bởi <span class="text-blue-700 cursor-pointer">{{ item.author }}</span></p>
                <div class="flex items-center gap-1 mt-0.5">
                  <span class="bg-[#eceb98]/70 text-[#5f5d0e] text-[11px] font-bold px-1.5 py-0.5 rounded-sm">Bán chạy nhất</span>
                  <span class="text-amber-600 text-[13px] font-bold ml-2">4.7</span>
                  <div class="flex text-amber-500">
                    <svg v-for="s in 5" :key="s" class="w-[12px] h-[12px]" fill="currentColor" viewBox="0 0 20 20"><path d="M10 15l-5.878 3.09 1.123-6.545L.489 6.91l6.572-.955L10 0l2.939 5.955 6.572.955-4.756 4.635 1.123 6.545z"></path></svg>
                  </div>
                  <span class="text-gray-500 text-[12px] ml-1">(27,114 đánh giá)</span>
                </div>
              </div>
              
              <div class="text-right w-32 flex flex-col pt-1 pr-8">
                <div class="text-[17px] font-bold text-[#8731e8]">₫{{ item.price }}</div>
                <div class="text-[14px] text-gray-500 line-through mt-0.5">₫{{ item.oldPrice }}</div>
              </div>
            </div>
          </div>
        </div>

        <!-- Checkout / Summary Section -->
        <div class="w-full lg:w-[360px] flex flex-col pt-2">
          <div class="text-gray-500 font-bold text-[16px] mb-1">Tổng cộng:</div>
          <div class="text-[36px] font-extrabold mb-1 leading-tight text-gray-900 tracking-tight">₫{{ cartItems.length > 0 ? '698,000' : '0' }}</div>
          <div class="text-[16px] text-gray-500 line-through mb-4">₫{{ cartItems.length > 0 ? '3,898,000' : '0' }}</div>
          <div v-if="cartItems.length > 0" class="text-[16px] mb-5 text-green-700 font-medium bg-green-50 px-3 py-1.5 inline-block w-max rounded-md">Tiết kiệm được 82%</div>
          
          <button :disabled="cartItems.length === 0" class="w-full bg-[#8731e8] hover:bg-[#6c27ba] disabled:bg-gray-300 disabled:text-gray-500 disabled:cursor-not-allowed text-white font-bold py-4 rounded-lg transition-colors text-[17px] mb-4 shadow-sm relative top-1">
             {{ cartItems.length > 0 ? 'Thanh toán bảo mật kết nối' : 'Đang không có sản phẩm' }}
          </button>

          <div class="border-t border-gray-200 pt-5 mt-4">
            <p class="font-bold text-[15px] mb-3 text-black">Mã ưu đãi (Promotions)</p>
            <div class="flex gap-2 text-[14px]">
              <input type="text" placeholder="Nhập mã Coupons" class="flex-1 border border-gray-400 bg-gray-50 px-3 py-2.5 rounded-l-md focus:bg-white focus:outline-none focus:ring-1 focus:ring-black transition-colors min-w-0">
              <button class="bg-[#2d2f31] hover:bg-black text-white font-bold px-5 py-2.5 rounded-r-md transition-colors whitespace-nowrap">Áp dụng</button>
            </div>
            <!-- Mock active coupon -->
            <div v-if="cartItems.length > 0" class="mt-4 flex items-center justify-between p-3 border border-dashed border-gray-300 rounded-md">
              <div class="flex items-center gap-2 text-gray-700 text-[13px]">
                <svg class="w-5 h-5 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.994 1.994 0 013 12V7a4 4 0 014-4z"></path></svg>
                <span class="font-bold">AET_MEMBER_NEW2026</span> được áp dụng
              </div>
              <!-- Close Icon to pretend removing coupon -->
              <svg class="w-4 h-4 text-gray-400 cursor-pointer hover:text-red-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path></svg>
            </div>
          </div>
        </div>
      </div>
    </main>
  </LayoutUdemy>
</template>

<script setup>
import { ref } from 'vue'
import LayoutUdemy from '../components/LayoutUdemy.vue'

// Giả lập giỏ hàng data
const cartItems = ref([
  { 
    id: 1, 
    title: 'Khóa học Lập trình Python 100 Ngày: Bootcamp Chuyên nghiệp', 
    author: 'Lê Văn Anh', 
    price: '349,000', 
    oldPrice: '1,899,000', 
    img: 'https://images.unsplash.com/photo-1526379095098-d400fd0bf935?w=500&auto=format&fit=crop&q=60' 
  },
  { 
    id: 2, 
    title: 'Nhập môn Machine Learning & Trí tuệ Nhân tạo thực chiến', 
    author: 'Trần Duy Tân', 
    price: '349,000', 
    oldPrice: '1,999,000', 
    img: 'https://images.unsplash.com/photo-1555066931-4365d14bab8c?w=500&auto=format&fit=crop&q=60' 
  }
])

const removeItem = (id) => {
  cartItems.value = cartItems.value.filter(item => item.id !== id)
}
</script>

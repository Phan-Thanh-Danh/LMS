<script setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import LayoutStudent from '@/layouts/LayoutStudent.vue'

const router = useRouter()

// Mock Data
const cartItems = ref([
  {
    id: 1,
    title: 'Thiết kế UI/UX Nâng cao: Kiến tạo Trải nghiệm Người dùng Đẳng cấp',
    instructor: 'Trần Anh Tuấn',
    price: 899000,
    originalPrice: 1250000,
    discount: 28,
    image: 'https://img-c.udemycdn.com/course/480x270/4514210_3943_5.jpg',
    isOwned: false
  },
  {
    id: 2,
    title: 'Lập trình Fullstack với Next.js 14 và Tailwind CSS',
    instructor: 'Alex Rivers',
    price: 550000,
    originalPrice: null,
    discount: null,
    image: 'https://img-c.udemycdn.com/course/480x270/2032544_9e47.jpg',
    isOwned: true
  }
])

const savedItems = ref([
  {
    id: 3,
    title: 'Nhiếp ảnh Thương mại',
    price: 320000,
    image: 'https://img-c.udemycdn.com/course/480x270/1217038_52bb_5.jpg'
  },
  {
    id: 4,
    title: 'Motion Design cơ bản',
    price: 450000,
    image: 'https://img-c.udemycdn.com/course/480x270/2841490_f785_6.jpg'
  }
])

const voucherCode = ref('AETPRO2024')
const isVoucherApplied = ref(true)

// Computed constants to match the image exactly
const subtotal = computed(() => 1449000)
const discountAmount = computed(() => -351000)
const tax = computed(() => 87840)
const total = computed(() => 1185840)

const formatPrice = (value) => {
  return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value).replace('₫', 'đ')
}

const goToCheckout = () => {
  router.push('/cart-details')
}
</script>

<template>
  <LayoutStudent>
    <div class="cart-page min-h-screen py-12 px-6 lg:px-12 font-plus-jakarta bg-gray-50/30">
      <div class="max-w-[1400px] mx-auto">
        <!-- Header -->
        <div class="mb-10">
          <h1 class="text-4xl font-black tracking-tight text-gray-900 mb-2">Giỏ hàng của bạn</h1>
          <p class="text-lg text-gray-500 font-medium">
            Bạn đang có <span class="text-[#003fb1] font-extrabold">3 khóa học</span> trong giỏ hàng
          </p>
        </div>

        <div class="grid grid-cols-1 lg:grid-cols-12 gap-8 items-start">
          <!-- Left: Cart Items -->
          <div class="lg:col-span-8 space-y-8">
            <div class="bg-white rounded-[32px] shadow-[0_8px_30px_rgb(0,0,0,0.04)] overflow-hidden p-8 border border-gray-100">
              <div class="divide-y divide-gray-100">
                <div v-for="item in cartItems" :key="item.id" class="flex flex-col md:flex-row gap-8 py-8 first:pt-0 last:pb-0 group">
                  <div class="relative w-full md:w-64 shrink-0 aspect-video rounded-2xl overflow-hidden shadow-sm group-hover:shadow-xl transition-all duration-500">
                    <img :src="item.image" :alt="item.title" class="w-full h-full object-cover">
                    <div v-if="item.isOwned" class="absolute inset-0 bg-black/40 backdrop-blur-[2px] flex items-center justify-center">
                      <span class="bg-white/95 text-gray-900 text-[11px] font-black px-4 py-1.5 rounded-full uppercase tracking-widest shadow-lg">Đã sở hữu</span>
                    </div>
                  </div>

                  <div class="flex-1 flex flex-col justify-between">
                    <div>
                      <div class="flex justify-between items-start gap-4">
                        <h3 class="text-xl font-extrabold text-gray-900 leading-tight group-hover:text-[#003fb1] transition-colors cursor-pointer">{{ item.title }}</h3>
                        <div class="text-right shrink-0">
                          <p class="text-2xl font-black text-[#003fb1] tracking-tight">{{ formatPrice(item.price) }}</p>
                          <p v-if="item.originalPrice" class="text-sm text-gray-400 font-bold line-through mt-0.5 opacity-60">{{ formatPrice(item.originalPrice) }}</p>
                        </div>
                      </div>
                      <p class="text-sm text-gray-500 mt-2 font-medium">Giảng viên: <span class="text-gray-800 font-bold">{{ item.instructor }}</span></p>
                      <div v-if="item.discount" class="mt-3 inline-flex items-center px-3 py-1 rounded-lg bg-red-50 text-[#EF4444] text-[10px] font-black tracking-wider uppercase border border-red-100">
                        TIẾT KIỆM {{ item.discount }}%
                      </div>
                    </div>

                    <div class="flex items-center gap-8 mt-6">
                      <button class="flex items-center gap-2 text-sm font-bold text-[#EF4444] hover:text-red-700 transition-colors group/btn">
                        <span class="w-1.5 h-1.5 rounded-full bg-current"></span>
                        Bỏ khỏi giỏ
                      </button>
                      <button class="flex items-center gap-2 text-sm font-bold text-[#003fb1] hover:text-blue-800 transition-colors">
                        <span class="material-symbols-outlined text-lg">bookmark</span>
                        Lưu để mua sau
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="space-y-6 pt-4">
              <div class="flex justify-between items-end px-2">
                <h2 class="text-2xl font-black text-gray-900">Danh sách để dành ({{ savedItems.length }})</h2>
                <button class="text-sm font-bold text-[#003fb1] hover:underline">Xem tất cả</button>
              </div>
              
              <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div v-for="item in savedItems" :key="item.id" class="bg-white p-6 rounded-[24px] shadow-sm border border-gray-100 flex gap-5 hover:shadow-xl hover:-translate-y-1 transition-all duration-300 group">
                  <div class="w-32 h-20 shrink-0 rounded-xl overflow-hidden shadow-sm">
                    <img :src="item.image" :alt="item.title" class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500">
                  </div>
                  <div class="flex-1 min-w-0">
                    <h4 class="text-sm font-black text-gray-900 truncate group-hover:text-[#003fb1] transition-colors cursor-pointer">{{ item.title }}</h4>
                    <p class="text-sm font-bold text-gray-500 mt-1">{{ formatPrice(item.price) }}</p>
                    <button class="mt-3 text-[11px] font-black text-[#003fb1] tracking-widest uppercase hover:text-blue-800 transition-colors">Thêm vào giỏ</button>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="lg:col-span-4 space-y-6 sticky top-28">
            <div class="bg-white rounded-[32px] shadow-[0_8px_30px_rgb(0,0,0,0.04)] border border-gray-100 p-8">
              <h2 class="text-2xl font-black text-gray-900 mb-8">Tổng đơn hàng</h2>
              <div class="space-y-5 text-sm mb-10">
                <div class="flex justify-between items-center text-gray-500 font-medium">
                  <span>Tạm tính</span>
                  <span class="font-bold text-gray-900 text-base">{{ formatPrice(subtotal) }}</span>
                </div>
                <div class="flex justify-between items-center text-gray-500 font-medium">
                  <span>Giảm giá khóa học</span>
                  <span class="font-bold text-[#10B981] text-base">{{ formatPrice(discountAmount) }}</span>
                </div>
                <div class="flex justify-between items-center text-gray-500 font-medium">
                  <span>Thuế VAT (8%)</span>
                  <span class="font-bold text-gray-900 text-base">{{ formatPrice(tax) }}</span>
                </div>
              </div>

              <div class="border-t border-gray-100 pt-8 mb-10">
                <div class="flex justify-between items-baseline mb-1">
                  <span class="text-xl font-extrabold text-gray-900">Tổng cộng</span>
                  <span class="text-4xl font-black text-[#003fb1] tracking-tighter">{{ formatPrice(total) }}</span>
                </div>
              </div>

              <div class="space-y-3 mb-10">
                <p class="text-[11px] font-black text-gray-400 uppercase tracking-widest">MÃ GIẢM GIÁ</p>
                <div class="flex gap-2">
                  <div class="relative flex-1">
                    <input v-model="voucherCode" type="text" class="w-full bg-gray-50 border border-gray-200 rounded-2xl py-4 px-5 text-sm font-black text-gray-900 focus:ring-4 focus:ring-blue-50 focus:bg-white focus:border-[#003fb1] transition-all outline-none">
                    <span v-if="isVoucherApplied" class="material-symbols-outlined absolute right-4 top-1/2 -translate-y-1/2 text-[#10B981] text-xl font-bold">check_circle</span>
                  </div>
                  <button class="bg-[#191c1e] text-white px-6 py-4 rounded-2xl text-sm font-black hover:bg-black transition-colors">Áp dụng</button>
                </div>
              </div>

              <button @click="goToCheckout" class="w-full h-16 bg-[#2d4aa5] text-white rounded-2xl font-black text-lg hover:shadow-2xl hover:shadow-blue-500/30 hover:-translate-y-1 transition-all active:scale-95 flex items-center justify-center gap-3 mb-8">
                Tiến hành Thanh toán
              </button>

              <div class="flex items-center justify-center gap-6 mb-8 text-gray-300">
                <span class="material-symbols-outlined text-3xl">credit_card</span>
                <span class="material-symbols-outlined text-3xl">account_balance</span>
                <span class="material-symbols-outlined text-3xl">qr_code_2</span>
                <span class="material-symbols-outlined text-3xl">contactless</span>
              </div>
            </div>

            <div class="bg-white rounded-[24px] shadow-sm border border-gray-100 px-8 py-6 flex items-center gap-5">
              <div class="w-12 h-12 rounded-2xl bg-blue-50 flex items-center justify-center shrink-0">
                 <span class="material-symbols-outlined text-[#003fb1] text-3xl">verified_user</span>
              </div>
              <div>
                <p class="text-xs font-black text-gray-900 uppercase tracking-tight">THANH TOÁN AN TOÀN 100%</p>
                <p class="text-[11px] text-gray-500 font-medium">Dữ liệu của bạn luôn được mã hóa bảo mật.</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </LayoutStudent>
</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@400;500;600;700;800&display=swap');

.font-plus-jakarta {
  font-family: 'Plus Jakarta Sans', sans-serif;
}
</style>

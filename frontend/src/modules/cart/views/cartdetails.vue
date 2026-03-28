<script setup>
import { ref } from 'vue'
import LayoutStudent from '@/layouts/LayoutStudent.vue'

const paymentMethod = ref('credit-card')
const cardName = ref('NGUYEN VAN A')
const cardNumber = ref('')
const expiry = ref('')
const cvc = ref('')
const agreeTerms = ref(false)

const formatPrice = (value) => {
  return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value).replace('₫', 'đ')
}
</script>

<template>
  <LayoutStudent>
    <div class="checkout-page py-16 px-6 lg:px-12 bg-gray-50/50 min-h-screen font-plus-jakarta">
      <div class="max-w-[1400px] mx-auto">
        <!-- Stepper -->
        <div class="flex justify-center mb-16">
          <div class="bg-white px-10 py-5 rounded-3xl shadow-[0_8px_30px_rgb(0,0,0,0.04)] border border-gray-100 flex items-center gap-12">
            <div class="flex items-center gap-4 text-sm font-bold text-gray-400">
              <span class="w-10 h-10 rounded-full bg-gray-50 flex items-center justify-center text-xs border border-gray-100">1</span>
              Xem lại
            </div>
            <div class="w-16 h-[2px] bg-gray-100"></div>
            <div class="flex items-center gap-4 text-sm font-black text-[#003fb1]">
              <span class="w-10 h-10 rounded-full bg-[#003fb1] text-white flex items-center justify-center text-xs shadow-xl shadow-blue-500/30">2</span>
              Thanh toán
            </div>
            <div class="w-16 h-[2px] bg-gray-100"></div>
            <div class="flex items-center gap-4 text-sm font-bold text-gray-400">
              <span class="w-10 h-10 rounded-full bg-gray-50 flex items-center justify-center text-xs border border-gray-100">3</span>
              Xác nhận
            </div>
          </div>
        </div>
        
        <div class="grid grid-cols-1 lg:grid-cols-12 gap-12 items-start">
          <!-- Left: Payment Methods -->
          <div class="lg:col-span-8 space-y-10">
            <h1 class="text-3xl font-black text-gray-900 tracking-tight">Phương thức thanh toán</h1>
            
            <div class="space-y-4">
              <!-- Option 1: Credit Card -->
              <div 
                class="bg-white rounded-[32px] border-2 transition-all cursor-pointer overflow-hidden shadow-sm"
                :class="paymentMethod === 'credit-card' ? 'border-[#003fb1] ring-8 ring-blue-50' : 'border-transparent hover:border-gray-200'"
                @click="paymentMethod = 'credit-card'"
              >
                <div class="p-8 flex items-center justify-between border-b border-gray-50">
                  <div class="flex items-center gap-5">
                    <div class="w-7 h-7 rounded-full border-2 border-[#003fb1] flex items-center justify-center">
                      <div v-if="paymentMethod === 'credit-card'" class="w-3.5 h-3.5 rounded-full bg-[#003fb1] shadow-lg shadow-blue-500/50"></div>
                    </div>
                    <span class="text-lg font-black text-gray-900 tracking-tight">Thẻ tín dụng / Ghi nợ</span>
                  </div>
                  <div class="flex gap-3 text-gray-400 opacity-60">
                     <span class="material-symbols-outlined text-3xl">credit_card</span>
                     <span class="material-symbols-outlined text-3xl">contactless</span>
                  </div>
                </div>

                <!-- Credit Card Form -->
                <div v-show="paymentMethod === 'credit-card'" class="p-10 space-y-8 animate-in fade-in slide-in-from-top-4 duration-500">
                  <div class="space-y-3">
                    <label class="text-[11px] font-black text-gray-400 uppercase tracking-widest pl-1">TÊN TRÊN THẺ</label>
                    <input v-model="cardName" type="text" class="w-full bg-gray-50 border border-transparent rounded-2xl py-5 px-6 text-base font-black text-gray-900 focus:bg-white focus:border-[#003fb1] focus:ring-8 focus:ring-blue-50 transition-all outline-none" placeholder="VD: NGUYEN VAN A">
                  </div>

                  <div class="space-y-3">
                    <label class="text-[11px] font-black text-gray-400 uppercase tracking-widest pl-1">SỐ THẺ</label>
                    <div class="relative group">
                      <span class="material-symbols-outlined absolute left-5 top-1/2 -translate-y-1/2 text-gray-400 group-focus-within:text-[#003fb1] transition-colors">payments</span>
                      <input v-model="cardNumber" type="text" class="w-full bg-gray-50 border border-transparent rounded-2xl py-5 pl-14 pr-6 text-base font-black text-gray-900 focus:bg-white focus:border-[#003fb1] focus:ring-8 focus:ring-blue-50 transition-all outline-none" placeholder="0000 0000 0000 0000">
                    </div>
                  </div>

                  <div class="grid grid-cols-2 gap-8">
                    <div class="space-y-3">
                      <label class="text-[11px] font-black text-gray-400 uppercase tracking-widest pl-1">NGÀY HẾT HẠN</label>
                      <input v-model="expiry" type="text" class="w-full bg-gray-50 border border-transparent rounded-2xl py-5 px-6 text-base font-black text-gray-900 focus:bg-white focus:border-[#003fb1] focus:ring-8 focus:ring-blue-50 transition-all outline-none" placeholder="MM/YY">
                    </div>
                    <div class="space-y-3">
                      <label class="text-[11px] font-black text-gray-400 uppercase tracking-widest pl-1">CVC / CVV</label>
                      <div class="relative">
                        <input v-model="cvc" type="password" class="w-full bg-gray-50 border border-transparent rounded-2xl py-5 px-6 text-base font-black text-gray-900 focus:bg-white focus:border-[#003fb1] focus:ring-8 focus:ring-blue-50 transition-all outline-none" placeholder="***">
                        <span class="material-symbols-outlined absolute right-5 top-1/2 -translate-y-1/2 text-gray-300 cursor-help hover:text-gray-500">help</span>
                      </div>
                    </div>
                  </div>

                  <div class="bg-blue-50/50 p-5 rounded-[20px] flex items-center gap-4 border border-blue-50">
                    <span class="material-symbols-outlined text-[#003fb1] text-2xl font-bold">verified_user</span>
                    <p class="text-[13px] text-gray-600 font-bold">Dữ liệu thẻ của bạn được mã hóa an toàn theo tiêu chuẩn PCI DSS.</p>
                  </div>
                </div>
              </div>

              <!-- Option 2: MoMo -->
              <div 
                class="bg-white rounded-[24px] border-2 p-8 flex items-center justify-between transition-all cursor-pointer shadow-sm group"
                :class="paymentMethod === 'momo' ? 'border-[#003fb1] ring-8 ring-blue-50' : 'border-transparent hover:border-gray-200'"
                @click="paymentMethod = 'momo'"
              >
                <div class="flex items-center gap-5">
                  <div class="w-7 h-7 rounded-full border-2 border-gray-200 flex items-center justify-center">
                    <div v-if="paymentMethod === 'momo'" class="w-3.5 h-3.5 rounded-full bg-[#003fb1] shadow-lg shadow-blue-500/50"></div>
                  </div>
                  <span class="text-lg font-black text-gray-900 tracking-tight">Ví điện tử MoMo</span>
                </div>
                <span class="material-symbols-outlined text-gray-400 text-3xl opacity-60">account_balance_wallet</span>
              </div>

              <!-- Option 3: Bank Transfer -->
              <div 
                class="bg-white rounded-[24px] border-2 p-8 flex items-center justify-between transition-all cursor-pointer shadow-sm group"
                :class="paymentMethod === 'bank' ? 'border-[#003fb1] ring-8 ring-blue-50' : 'border-transparent hover:border-gray-200'"
                @click="paymentMethod = 'bank'"
              >
                <div class="flex items-center gap-5">
                  <div class="w-7 h-7 rounded-full border-2 border-gray-200 flex items-center justify-center">
                    <div v-if="paymentMethod === 'bank'" class="w-3.5 h-3.5 rounded-full bg-[#003fb1] shadow-lg shadow-blue-500/50"></div>
                  </div>
                  <span class="text-lg font-black text-gray-900 tracking-tight">Chuyển khoản ngân hàng (VietQR)</span>
                </div>
                <span class="material-symbols-outlined text-gray-400 text-3xl opacity-60">qr_code_2</span>
              </div>
            </div>

            <!-- Footer Trust Badges -->
            <div class="grid grid-cols-2 gap-8">
              <div class="bg-white p-8 rounded-[32px] shadow-sm border border-gray-100 flex items-center gap-6">
                 <div class="w-14 h-14 rounded-2xl bg-blue-50 flex items-center justify-center shrink-0">
                    <span class="material-symbols-outlined text-[#003fb1] text-3xl font-bold">verified_user</span>
                 </div>
                 <div>
                   <p class="text-[15px] font-black text-gray-900 leading-tight">Mã hóa 256-bit SSL</p>
                   <p class="text-xs text-gray-500 font-medium mt-1">Giao dịch của bạn luôn được bảo vệ tuyệt đối.</p>
                 </div>
              </div>
              <div class="bg-white p-8 rounded-[32px] shadow-sm border border-gray-100 flex items-center gap-6">
                 <div class="w-14 h-14 rounded-2xl bg-blue-50 flex items-center justify-center shrink-0">
                    <span class="material-symbols-outlined text-[#003fb1] text-3xl font-bold">restart_alt</span>
                 </div>
                 <div>
                   <p class="text-[15px] font-black text-gray-900 leading-tight">Hoàn tiền 30 ngày</p>
                   <p class="text-xs text-gray-500 font-medium mt-1">An tâm học tập với chính sách bảo hành học phí.</p>
                 </div>
              </div>
            </div>
          </div>

          <!-- Right: Order Summary Sidebar -->
          <div class="lg:col-span-4 sticky top-28 space-y-8">
            <div class="bg-white rounded-[32px] shadow-[0_8px_30px_rgb(0,0,0,0.04)] border border-gray-100 p-10">
              <h2 class="text-2xl font-black text-gray-900 mb-10">Tóm tắt đơn hàng</h2>
              
              <!-- Items -->
              <div class="space-y-6 mb-10">
                <div class="flex gap-5 p-4 bg-gray-50 rounded-[24px] border border-gray-100 relative group">
                  <div class="w-24 h-16 rounded-xl overflow-hidden shrink-0 shadow-md">
                    <img src="https://img-c.udemycdn.com/course/480x270/4514210_3943_5.jpg" class="w-full h-full object-cover">
                  </div>
                  <div class="min-w-0">
                    <h4 class="text-sm font-black text-gray-900 leading-tight group-hover:text-[#003fb1] transition-colors">Quản trị nhân sự số toàn diện</h4>
                    <p class="text-[11px] text-gray-500 font-bold mt-1 uppercase tracking-tighter">Gói chuyên gia • 12 Modules</p>
                  </div>
                </div>
              </div>

              <!-- Pricing Breakdown -->
              <div class="space-y-5 text-sm mb-10">
                <div class="flex justify-between items-center text-gray-500 font-medium">
                  <span>Tạm tính</span>
                  <span class="font-black text-gray-900 text-base">{{ formatPrice(1499000) }}</span>
                </div>
                <div class="flex justify-between items-center text-gray-500 font-medium">
                  <span>Thuế (VAT 8%)</span>
                  <span class="font-black text-gray-900 text-base">{{ formatPrice(119920) }}</span>
                </div>
              </div>

              <!-- Total -->
              <div class="border-t border-gray-100 pt-8 mb-10">
                 <div class="flex justify-between items-baseline mb-1">
                   <span class="text-xl font-black text-gray-900">Tổng cộng</span>
                   <span class="text-4xl font-black text-[#003fb1] tracking-tighter">{{ formatPrice(1618920) }}</span>
                 </div>
              </div>

              <!-- Terms -->
              <div class="flex gap-4 mb-10">
                <div class="relative">
                  <input v-model="agreeTerms" type="checkbox" class="w-6 h-6 rounded-lg border-2 border-gray-200 text-[#003fb1] focus:ring-8 focus:ring-blue-50 transition-all cursor-pointer">
                </div>
                <p class="text-[11px] text-gray-400 font-bold leading-relaxed">
                  Tôi đã đọc và đồng ý với <a href="#" class="text-[#003fb1] hover:underline">Điều khoản dịch vụ</a> và <a href="#" class="text-[#003fb1] hover:underline">Chính sách bảo mật</a> của nền tảng.
                </p>
              </div>

              <!-- Checkout Button -->
              <button 
                class="w-full h-16 rounded-2xl font-black text-lg flex items-center justify-center gap-3 transition-all active:scale-95 shadow-2xl shadow-blue-500/20"
                :class="agreeTerms ? 'bg-[#2d4aa5] text-white hover:bg-[#003fb1] hover:-translate-y-1' : 'bg-gray-100 text-gray-400 cursor-not-allowed'"
                :disabled="!agreeTerms"
              >
                <span class="material-symbols-outlined text-2xl font-bold">lock</span>
                Thanh toán ngay
              </button>

              <p class="mt-8 text-[11px] text-center font-black text-gray-300 uppercase tracking-[0.2em]">ĐẢM BẢO BỞI TRUSTEDPAY GLOBAL</p>
            </div>

            <!-- Help -->
            <div class="bg-gray-50 rounded-[24px] p-8 text-center border border-gray-100">
              <p class="text-xs text-gray-500 font-bold">Cần hỗ trợ thanh toán?</p>
              <p class="text-base font-black text-gray-900 mt-2">Gọi Hotline: <span class="text-[#003fb1] underline underline-offset-4 decoration-2">1900 6789</span></p>
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

.animate-in {
  animation: slideDown 0.6s cubic-bezier(0.16, 1, 0.3, 1) both;
}

@keyframes slideDown {
  from {
    opacity: 0;
    transform: translateY(-20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>

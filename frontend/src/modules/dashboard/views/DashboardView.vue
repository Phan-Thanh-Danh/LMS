<script setup>
import { useAuthStore } from '@/stores/auth'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()

const handleLogout = () => {
  authStore.logout()
  router.push('/login')
}
</script>

<template>
  <div class="dashboard-page bg-surface font-plus-jakarta antialiased min-h-screen">
    <!-- Section 1: Top Promo Banner -->
    <div class="bg-[#EBF0FF] text-primary py-3 px-6 text-center text-sm font-semibold tracking-tight">
      Sự kiện đặc biệt! Giảm giá các khóa học chỉ từ 339.000đ. Kết thúc sau 2 ngày.
    </div>

    <!-- Section 2: Mega Navbar -->
    <header class="glass-nav sticky top-0 z-50 w-full border-b border-surface-container-high bg-white/85 backdrop-blur-xl">
      <div class="flex items-center justify-between px-6 py-4 max-w-[1920px] mx-auto gap-8">
        <!-- Brand Logo -->
        <div class="flex items-center gap-6">
          <router-link to="/" class="text-2xl font-extrabold tracking-tighter text-primary">AET LMS</router-link>
          <button class="hidden lg:flex items-center gap-1 text-sm font-medium hover:text-primary transition-colors">
            Danh mục
          </button>
        </div>
        <!-- Search Bar -->
        <div class="hidden md:flex flex-1 max-w-[40%] relative">
          <span class="material-symbols-outlined absolute left-4 top-1/2 -translate-y-1/2 text-on-surface-variant text-xl">search</span>
          <input class="w-full bg-surface-container-low border-none rounded-full py-3 pl-12 pr-4 text-sm focus:ring-2 focus:ring-primary/20 transition-all outline-none" placeholder="Tìm kiếm bất cứ thứ gì" type="text"/>
        </div>
        <!-- Navigation Links -->
        <nav class="hidden xl:flex items-center gap-6 text-sm font-medium">
          <a class="hover:text-primary transition-colors" href="#">Chứng chỉ</a>
          <a class="hover:text-primary transition-colors" href="#">AET Business</a>
          <a class="hover:text-primary transition-colors" href="#">Giảng dạy</a>
          <a class="text-primary font-bold border-b-2 border-primary pb-1" href="#">Khóa học của tôi</a>
        </nav>
        <!-- Action Icons & Avatar -->
        <div class="flex items-center gap-5">
          <button class="hover:text-primary transition-all active:scale-95"><span class="material-symbols-outlined">favorite</span></button>
          <button class="hover:text-primary transition-all active:scale-95"><span class="material-symbols-outlined">shopping_cart</span></button>
          <button class="relative hover:text-primary transition-all active:scale-95">
            <span class="material-symbols-outlined">notifications</span>
            <span class="absolute -top-1 -right-1 w-2 h-2 bg-error rounded-full"></span>
          </button>
          <div class="group relative">
            <div class="w-10 h-10 rounded-full overflow-hidden border-2 border-primary-fixed cursor-pointer hover:ring-4 hover:ring-primary/10 transition-all">
              <img alt="User Profile" class="w-full h-full object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuDEidDb1VqdaeEskLwwEHnq8KXXg2Q5DCyVyAvmJ8UKDM9WPNHy3LVwV7ixaySFtdHLAeYLEkawECHzLkR37GSe4wYv00suwzkFMQ5rjb61H2nipFZ5pTrvlO3Xk-jiJszR1Bp9daAEYO3fbefIUa1AawZN-0aISC4Nop3F9NZXuK-6GVvdxDetV_SZa1XuLHQFSPCbaTu7mEthClMyRjvoIeqP8Ast-sWbIxckQJEYYePYJ_2l-VE98SVdLNgqVo5jpPw7xpVHaQ4"/>
            </div>
            <!-- Simple Dropdown -->
            <div class="absolute right-0 top-full mt-2 w-48 bg-white border border-surface-container rounded-lg shadow-xl opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-all">
               <div class="p-4 border-b border-surface-container">
                  <p class="font-bold text-sm">{{ authStore.user?.name || 'User' }}</p>
                  <p class="text-xs text-on-surface-variant">{{ authStore.user?.email || 'user@example.com' }}</p>
               </div>
               <button @click="handleLogout" class="w-full text-left p-3 text-sm text-error hover:bg-error/5 transition-colors">Đăng xuất</button>
            </div>
          </div>
        </div>
      </div>
      <!-- Sub-Navbar -->
      <div class="hidden md:flex items-center justify-center gap-12 py-3 border-t border-surface-container-high text-[13px] font-medium text-on-surface-variant bg-white">
        <a class="hover:text-primary transition-colors" href="#">Phát triển</a>
        <a class="hover:text-primary transition-colors" href="#">Kinh doanh</a>
        <a class="hover:text-primary transition-colors" href="#">IT & Phần mềm</a>
        <a class="hover:text-primary transition-colors" href="#">Thiết kế</a>
        <a class="hover:text-primary transition-colors" href="#">Marketing</a>
        <a class="hover:text-primary transition-colors" href="#">Sức khỏe & Thể hình</a>
        <a class="hover:text-primary transition-colors" href="#">Âm nhạc</a>
      </div>
    </header>

    <main class="max-w-[1440px] mx-auto px-6 lg:px-12 py-10 space-y-16">
      <!-- Section 3: Welcome Area -->
      <section class="flex flex-col md:flex-row items-center gap-8 bg-white p-8 rounded-xl shadow-sm border border-surface-container">
        <div class="w-24 h-24 rounded-full overflow-hidden border-4 border-primary/10 shadow-lg">
          <img alt="User Large Avatar" class="w-full h-full object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuCwhaCqkQX7rEfi-IxxTkeVcFDRZ5R-0yzlQVJzGrtviRF_uwV6_3dD9sq_p4Ax6q39UEAE5yKIlD2zgTiZ0rhR1aval0PRmPNFVve-eFox-fl7fsK9ryMKmgVEMUtjx59IoUCJneD13uI8qP0cIoWtLiIaug0pyvMe3AsFVXvoF2T8bdJ9PrTnnt6T0gKKG4FqRYI1yH-m21pJyXcw2uPW0QiadFMeXwamBgCG14rSA2kHQy5qiHN6HkCsJFDgssazdOl03JaQDis"/>
        </div>
        <div class="text-center md:text-left">
          <h1 class="text-3xl font-extrabold tracking-tight text-on-surface mb-2">Chào mừng {{ authStore.user?.name || 'Alex' }} trở lại!</h1>
          <a class="text-primary font-bold hover:underline flex items-center justify-center md:justify-start gap-1" href="#">
            Thêm mục tiêu và sở thích <span class="material-symbols-outlined text-sm">arrow_forward</span>
          </a>
        </div>
      </section>

      <!-- Section 4: Hero Promo Banner -->
      <section class="relative h-[400px] rounded-2xl overflow-hidden shadow-2xl">
        <div class="absolute inset-0 bg-linear-to-r from-primary to-slate-900/80">
          <img alt="Hero Background" class="w-full h-full object-cover opacity-40 mix-blend-overlay" src="https://lh3.googleusercontent.com/aida-public/AB6AXuAdjW8mgzA8kMwkYKd6vz3pV5b5Pv5v0VK4zukg5mAwl20enSKKrH5WAHdBABKbjQqe8s5Pd_2XQ4dyiGY3iyHCOX35dIOtyBQ6Joj_1Ok0sN5Auo81iUGxWqBSjEA_-c_QUeGRQGeBKmia5LKBgOE3yv1cls03paJrnOV8e2-zcDdH5799PItn9YkBE8bKgSRzw2Wd4wkOfJ_gktxqphMVWhmbBnOF0sZpaJmnrUnCMClyIi9YAEuOCjJ0NBv5hpV_NFbBuhK5jyU"/>
        </div>
        <div class="relative z-10 h-full flex items-center px-12">
          <div class="bg-white p-10 rounded-xl shadow-2xl max-w-md border border-white/20">
            <span class="inline-block bg-error/10 text-error text-[10px] font-bold px-2 py-1 rounded mb-4 tracking-widest uppercase">Ưu đãi hôm nay</span>
            <h2 class="text-2xl font-extrabold text-on-surface mb-4 leading-tight">Mở khóa tương lai của bạn với Masterclass AI mới nhất</h2>
            <p class="text-on-surface-variant text-sm mb-6 leading-relaxed">Học từ các chuyên gia hàng đầu về Deep Learning và Neural Networks.</p>
            <div class="flex items-center gap-4">
              <span class="text-2xl font-black text-on-surface">339.000đ</span>
              <span class="text-on-surface-variant line-through text-sm">2.499.000đ</span>
            </div>
            <button class="mt-6 w-full bg-primary hover:bg-primary-container text-white font-bold py-4 rounded-lg transition-all active:scale-95 shadow-lg shadow-primary/20">
              Khám phá ngay
            </button>
          </div>
        </div>
      </section>

      <!-- Section 5: Learning Path Carousel -->
      <section class="space-y-8">
        <div class="flex items-center justify-between">
          <h2 class="text-[28px] font-extrabold tracking-tight text-on-surface">Lĩnh vực sẽ học tiếp theo</h2>
          <a class="text-primary font-bold text-sm hover:underline transition-all" href="#">Xem tất cả</a>
        </div>
        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
          <!-- Course Card In-Progress -->
          <div class="group bg-white rounded-xl overflow-hidden shadow-sm border border-transparent hover:border-primary/20 transition-all cursor-pointer">
            <div class="aspect-video relative overflow-hidden">
              <img alt="Vue 3 Course" class="w-full h-full object-cover group-hover:scale-105 transition-transform duration-500" src="https://lh3.googleusercontent.com/aida-public/AB6AXuA2SL5Pt8DDCBTDtkFGmaTu4t1Eu4MfPzBcVSPeS-Lgcz8Z8fx3te5zKLb3J5XfDL-k2rmshReIJNBi6wWtKFnwU0Gvpq9hfXAURZHiFr1YAPALYWqeVzcB44gEYMcoWKFz1ybF2jbF8r7At8MhR0tCVhIK8WkngMfyF741GxhllYWjTjgFvFKMyV5ITCdlHSnj7ZCBZivEnu2u7lPNkecq49q8CQ_eDtQbRiu_dmWr4AmaKof93qIqBnTcch5Uf-W8_KfrlKjW6mk"/>
              <div class="absolute inset-0 bg-black/20 group-hover:bg-transparent transition-all"></div>
              <div class="absolute bottom-3 right-3 bg-white/90 backdrop-blur-md px-2 py-1 rounded text-[10px] font-bold">12:45:10</div>
            </div>
            <div class="p-4 space-y-3">
              <h3 class="font-bold text-on-surface line-clamp-2 leading-tight h-10 group-hover:text-primary transition-colors">Mastering Vue 3: The Complete Composition API Guide</h3>
              <p class="text-xs text-on-surface-variant">Bởi Maximillian Schwarzmüller</p>
              <div class="space-y-1">
                <div class="flex justify-between text-[10px] font-bold text-primary uppercase tracking-tighter">
                  <span>65% Hoàn thành</span>
                </div>
                <div class="h-1.5 w-full bg-surface-container rounded-full overflow-hidden">
                  <div class="h-full bg-primary" style="width: 65%"></div>
                </div>
              </div>
            </div>
          </div>
          <!-- Course Card Recommended -->
          <div class="group bg-white rounded-xl overflow-hidden shadow-sm border border-transparent hover:border-primary/20 transition-all cursor-pointer">
            <div class="aspect-video relative overflow-hidden">
              <img alt="Business Analytics" class="w-full h-full object-cover group-hover:scale-105 transition-transform duration-500" src="https://lh3.googleusercontent.com/aida-public/AB6AXuAHOwQMV74FaEdnRHMe-cVQkQ4D1CGOD6VnpiiKTe8SeBbh37dNrjR2ac8aKrUjUvxwoxvK1LpaIp7RPYBjgCvyaBUBCGZVkEQrj41joYUZ7IieFbzQOJsJaBKmp5I966GshJUY1X1crpm3xkGJu8Pq1uQEeQHEfCBX2Wm08yXM8VVZ161s2Yd606AJ5aPe-EPsfVSB6AQt5dMHPJaZj8MhZnnt_fyDuZHY9M14s4X1eDwmZpZ_nT6NSbK3fwDuA4SKj58gNgzUX2w"/>
              <span class="absolute top-2 left-2 bg-secondary text-white text-[10px] font-bold px-2 py-1 rounded">Bán chạy nhất</span>
            </div>
            <div class="p-4 space-y-2">
              <h3 class="font-bold text-on-surface line-clamp-2 leading-tight h-10 group-hover:text-primary transition-colors">Business Analytics: From Data to Insights with SQL</h3>
              <p class="text-xs text-on-surface-variant">Bởi Dr. Jane Doe</p>
              <div class="flex items-center gap-1 text-[#E3A008]">
                <span class="text-sm font-bold">4.8</span>
                <div class="flex"><span class="material-symbols-outlined text-xs" style="font-variation-settings: 'FILL' 1;">star</span><span class="material-symbols-outlined text-xs" style="font-variation-settings: 'FILL' 1;">star</span><span class="material-symbols-outlined text-xs" style="font-variation-settings: 'FILL' 1;">star</span><span class="material-symbols-outlined text-xs" style="font-variation-settings: 'FILL' 1;">star</span><span class="material-symbols-outlined text-xs" style="font-variation-settings: 'FILL' 1;">star_half</span></div>
                <span class="text-on-surface-variant text-[10px] font-normal">(12,450)</span>
              </div>
              <div class="pt-2">
                <span class="text-lg font-black text-on-surface">339.000đ</span>
              </div>
            </div>
          </div>
          <!-- More Cards... -->
          <div v-for="i in 2" :key="i" class="group bg-white rounded-xl overflow-hidden shadow-sm border border-transparent hover:border-primary/20 transition-all cursor-pointer">
            <div class="aspect-video relative">
               <div class="w-full h-full bg-slate-100 flex items-center justify-center">
                  <span class="material-symbols-outlined text-slate-300 transform scale-[3]">school</span>
               </div>
            </div>
            <div class="p-4 space-y-2">
              <div class="h-4 w-3/4 bg-slate-100 rounded"></div>
              <div class="h-3 w-1/2 bg-slate-100 rounded"></div>
              <div class="pt-4 h-6 w-1/4 bg-slate-100 rounded"></div>
            </div>
          </div>
        </div>
      </section>

      <!-- Section 6: Dark Navy Section -->
      <section class="grid grid-cols-1 lg:grid-cols-2 gap-8">
        <div class="bg-indigo-950 text-white p-12 rounded-2xl flex flex-col items-center text-center justify-center space-y-6 relative overflow-hidden group">
          <div class="absolute inset-0 bg-primary/10 group-hover:scale-150 transition-transform duration-1000"></div>
          <h2 class="text-3xl font-extrabold relative z-10 leading-tight">Giảng dạy online cho cả thế giới trên AET LMS</h2>
          <p class="text-blue-100 relative z-10 max-w-sm">Tham gia cùng hàng ngàn giảng viên và chia sẻ kiến thức của bạn với hàng triệu sinh viên.</p>
          <button class="relative z-10 border-2 border-white/40 hover:border-white text-white font-bold px-8 py-3 rounded transition-all active:scale-95">
            Giảng dạy trên AET LMS
          </button>
        </div>
        <div class="bg-slate-900 text-white p-12 rounded-2xl flex flex-col items-center justify-center space-y-10">
          <div class="text-center space-y-2">
            <h2 class="text-2xl font-bold">Các công ty hàng đầu chọn AET Business</h2>
            <p class="text-slate-400 text-sm">Hơn 15,000 công ty sử dụng AET để phát triển nhân sự.</p>
          </div>
          <div class="grid grid-cols-3 gap-8 opacity-60 grayscale hover:grayscale-0 transition-all duration-500">
            <div class="flex items-center justify-center font-black text-2xl tracking-tighter">SAMSUNG</div>
            <div class="flex items-center justify-center font-black text-2xl tracking-tighter">CISCO</div>
            <div class="flex items-center justify-center font-black text-2xl tracking-tighter">AT&T</div>
          </div>
        </div>
      </section>
    </main>

    <!-- Footer -->
    <footer class="bg-slate-900 text-[#D1D5DB] border-t border-slate-800">
      <div class="max-w-[1920px] mx-auto px-12 py-16 grid grid-cols-2 md:grid-cols-4 lg:grid-cols-6 gap-12 text-sm">
        <div class="col-span-2 space-y-6">
          <a class="text-2xl font-black text-white" href="#">AET LMS</a>
          <p class="leading-relaxed max-w-xs">Nền tảng học trực tuyến hàng đầu tại Việt Nam.</p>
        </div>
        <div>
           <h4 class="text-white font-bold mb-4">Chứng chỉ</h4>
           <ul class="space-y-2">
              <li><a href="#" class="hover:text-white">IT & Phần mềm</a></li>
              <li><a href="#" class="hover:text-white">Marketing</a></li>
           </ul>
        </div>
        <div>
           <h4 class="text-white font-bold mb-4">Hợp tác</h4>
           <ul class="space-y-2">
              <li><a href="#" class="hover:text-white">Giảng dạy</a></li>
              <li><a href="#" class="hover:text-white">Doanh nghiệp</a></li>
           </ul>
        </div>
      </div>
    </footer>
  </div>
</template>

<style scoped>
.glass-nav {
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
}
</style>

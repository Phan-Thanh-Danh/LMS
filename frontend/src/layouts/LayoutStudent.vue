<script setup>
import { ref, onMounted } from 'vue'
import { useAuthStore } from '@/stores/auth'
import { useRouter } from 'vue-router'
import api from '@/services/axios'

const authStore = useAuthStore()
const router = useRouter()

onMounted(async () => {
  if (authStore.isAuthenticated) {
    try {
      const res = await api.get('/Auth/profile')
      if (res.data) {
        authStore.updateProfile({
          name: res.data.hoTen,
          avatar: res.data.duongDanAnhDaiDien
        })
      }
    } catch (e) {
      console.error('Failed to sync profile', e)
    }
  }
})

// STU-07: Mega Menu State & Data
const isMenuOpen = ref(false)
const activeCategory = ref(null)
const activeSubCategory = ref(null)

const categories = ref([
    {
        id: 1,
        name: 'Phát triển',
        icon: 'code',
        subCategories: [
            {
                id: 11,
                name: 'Phát triển Web',
                topics: ['Vue.js', 'React', 'Angular', 'Node.js', 'PHP', 'JavaScript', 'HTML & CSS']
            },
            {
                id: 12,
                name: 'Khoa học Dữ liệu',
                topics: ['Python', 'Machine Learning', 'Deep Learning', 'Data Analysis', 'Statistics', 'R']
            },
            {
                id: 13,
                name: 'Phát triển Di động',
                topics: ['Google Flutter', 'Android Development', 'iOS Development', 'Swift', 'React Native', 'Kotlin']
            },
            {
                id: 14,
                name: 'Ngôn ngữ lập trình',
                topics: ['Python', 'Java', 'C#', 'C++', 'Go', 'Rust', 'TypeScript']
            }
        ]
    },
    {
        id: 2,
        name: 'Kinh doanh',
        icon: 'business',
        subCategories: [
            {
                id: 21,
                name: 'Quản trị công ty',
                topics: ['Lãnh đạo', 'Quản lý dự án', 'Kỹ năng giao tiếp', 'Chế độ đãi ngộ', 'Chiến lược kinh doanh']
            },
            {
                id: 22,
                name: 'Tài chính',
                topics: ['Đầu tư chứng khoán', 'Phân tích tài chính', 'Kế toán', 'Tiền điện tử', 'Tài chính cá nhân']
            },
            {
                id: 23,
                name: 'Kỹ năng mềm',
                topics: ['Thuyết trình', 'Làm việc nhóm', 'Quản lý thời gian', 'Tư duy phản biện', 'Sáng tạo']
            }
        ]
    },
    {
        id: 3,
        name: 'IT & Phần mềm',
        icon: 'terminal',
        subCategories: [
            {
                id: 31,
                name: 'Chứng chỉ IT',
                topics: ['AWS Certified', 'CompTIA A+', 'Cisco CCNA', 'Google Cloud', 'Microsoft Azure']
            },
            {
                id: 32,
                name: 'An ninh mạng',
                topics: ['Ethical Hacking', 'Network Security', 'CompTIA Security+', 'CISSP', 'Phòng chống mã độc']
            }
        ]
    },
    {
        id: 4,
        name: 'Thiết kế',
        icon: 'palette',
        subCategories: [
            {
                id: 41,
                name: 'Thiết kế đồ họa',
                topics: ['Photoshop', 'Illustrator', 'Canva', 'InDesign', 'Lý thuyết màu sắc']
            },
            {
                id: 42,
                name: 'Thiết kế UX/UI',
                topics: ['Figma', 'User Experience Design', 'Mobile Design', 'Web Design', 'UI Prototyping']
            }
        ]
    }
])

// Tự động set mục đầu tiên khi mở menu
const handleMenuEnter = () => {
    isMenuOpen.value = true
    if (!activeCategory.value) {
        activeCategory.value = categories.value[0]
        activeSubCategory.value = categories.value[0].subCategories[0]
    }
}

const handleLogout = () => {
  authStore.logout()
  router.push('/login')
}
</script>

<template>
  <div class="dashboard-layout bg-surface font-plus-jakarta antialiased min-h-screen">
    <!-- Section 1: Top Promo Banner -->
    <div class="bg-[#EBF0FF] text-primary py-3 px-6 text-center text-sm font-semibold tracking-tight">
      Sự kiện đặc biệt! Giảm giá các khóa học chỉ từ 339.000đ. Kết thúc sau 2 ngày.
    </div>

    <!-- Section 2: Mega Navbar -->
    <header class="glass-nav sticky top-0 z-50 w-full border-b border-surface-container-high bg-white/85 backdrop-blur-xl">
      <div class="flex items-center justify-between px-6 py-4 max-w-[1920px] mx-auto gap-8">
        <!-- Brand Logo -->
        <div class="flex items-center gap-6">
          <router-link 
            :to="authStore.isAuthenticated ? '/student/dashboard' : '/'" 
            class="text-2xl font-extrabold tracking-tighter text-primary"
          >
            AET LMS
          </router-link>
          
          <!-- STU-07: Mega Menu Category -->
          <div class="relative group" @mouseenter="handleMenuEnter" @mouseleave="isMenuOpen = false">
            <button class="hidden lg:flex items-center gap-1 text-sm font-medium hover:text-primary transition-colors py-2">
              Danh mục
            </button>

            <!-- Mega Menu Dropdown Container -->
            <div v-if="isMenuOpen" class="absolute top-full -left-4 w-screen max-w-[1000px] bg-white shadow-2xl border border-surface-container flex h-[500px] z-100 animate-in fade-in slide-in-from-top-2 duration-200">
                <!-- Column 1: Main Categories -->
                <div class="w-64 bg-white border-r border-surface-container py-4 overflow-y-auto">
                    <div v-for="cat in categories" :key="cat.id" 
                         @mouseenter="activeCategory = cat; activeSubCategory = cat.subCategories[0]"
                         :class="['flex items-center justify-between px-4 py-3 cursor-pointer transition-colors', 
                                  activeCategory?.id === cat.id ? 'bg-surface-container-low text-primary font-bold' : 'text-on-surface hover:bg-surface-container-lowest']">
                        <div class="flex items-center gap-3 text-sm">
                            <span class="material-symbols-outlined text-lg">{{ cat.icon }}</span>
                            {{ cat.name }}
                        </div>
                        <span class="material-symbols-outlined text-sm opacity-50">chevron_right</span>
                    </div>
                </div>

                <!-- Column 2: Sub Categories -->
                <div v-if="activeCategory" class="w-64 bg-white border-r border-surface-container py-4 overflow-y-auto">
                    <div v-for="sub in activeCategory.subCategories" :key="sub.id"
                         @mouseenter="activeSubCategory = sub"
                         :class="['flex items-center justify-between px-4 py-3 cursor-pointer transition-colors',
                                  activeSubCategory?.id === sub.id ? 'bg-surface-container-low text-primary font-bold' : 'text-on-surface hover:bg-surface-container-lowest']">
                        <span class="text-sm">{{ sub.name }}</span>
                        <span class="material-symbols-outlined text-sm opacity-50">chevron_right</span>
                    </div>
                </div>

                <!-- Column 3: Topics -->
                <div v-if="activeSubCategory" class="flex-1 bg-white py-6 px-8 overflow-y-auto">
                    <h3 class="text-sm font-bold text-gray-900 mb-4 uppercase tracking-wider">Chủ đề phổ biến</h3>
                    <div class="grid grid-cols-1 gap-1">
                        <router-link v-for="topic in activeSubCategory.topics" :key="topic" 
                           :to="{ path: '/explore', query: { category: topic } }" 
                           class="text-sm text-on-surface-variant hover:text-primary py-2 transition-colors">
                            {{ topic }}
                        </router-link>
                    </div>
                </div>
            </div>
          </div>
        </div>
        <!-- Search Bar -->
        <div class="hidden md:flex flex-1 max-w-[40%] relative">
          <span class="material-symbols-outlined absolute left-4 top-1/2 -translate-y-1/2 text-on-surface-variant text-xl">search</span>
          <input class="w-full bg-surface-container-low border-none rounded-full py-3 pl-12 pr-4 text-sm focus:ring-2 focus:ring-primary/20 transition-all outline-none" placeholder="Tìm kiếm bất cứ thứ gì" type="text"/>
        </div>

        <!-- Action Icons & Avatar -->
        <div class="flex items-center gap-6">
          <router-link to="/certificates" class="hidden xl:block hover:text-primary transition-colors text-sm font-medium" active-class="text-primary font-bold border-b-2 border-primary pb-1">Chứng chỉ</router-link>
          <router-link to="/learning" class="hidden xl:block hover:text-primary transition-colors text-sm font-medium" active-class="text-primary font-bold border-b-2 border-primary pb-1">Khóa học của tôi</router-link>
          
          <div class="flex items-center gap-5 ml-2">
            <button class="hover:text-primary transition-all active:scale-95"><span class="material-symbols-outlined">favorite</span></button>
            <router-link to="/cart" class="hover:text-primary transition-all active:scale-95">
              <span class="material-symbols-outlined font-bold text-[#003fb1]">shopping_cart</span>
            </router-link>
            <router-link to="/notifications" class="relative hover:text-primary transition-all active:scale-95">
              <span class="material-symbols-outlined">notifications</span>
              <span class="absolute -top-1 -right-1 w-2 h-2 bg-error rounded-full"></span>
            </router-link>
            <div class="group relative">
              <div class="w-10 h-10 rounded-full overflow-hidden border-2 border-primary-fixed cursor-pointer hover:ring-4 hover:ring-primary/10 transition-all bg-gray-100 flex items-center justify-center">
                <img v-if="authStore.user?.avatar" alt="User Profile" class="w-full h-full object-cover" :src="authStore.user?.avatar"/>
                <span v-else class="material-symbols-outlined text-gray-400">person</span>
              </div>
              <!-- Simple Dropdown -->
              <div class="absolute right-0 top-full mt-2 w-48 bg-white border border-surface-container rounded-lg shadow-xl opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-all">
                <div class="p-4 border-b border-surface-container">
                  <router-link to="/profile" class="block hover:text-primary transition-colors">
                    <p class="font-bold text-sm">{{ authStore.user?.name || 'User' }}</p>
                    <p class="text-xs text-on-surface-variant">{{ authStore.user?.email || 'user@example.com' }}</p>
                  </router-link>
                </div>
                <router-link to="/profile" class="w-full text-left p-3 text-sm text-on-surface hover:bg-surface-container-low transition-colors block border-b border-surface-container">Hồ sơ cá nhân</router-link>
                <button @click="handleLogout" class="w-full text-left p-3 text-sm text-error hover:bg-error/5 transition-colors">Đăng xuất</button>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- Sub-Navbar -->
      <div class="hidden md:flex items-center justify-center gap-12 py-3 border-t border-surface-container-high text-[13px] font-medium text-on-surface-variant bg-white">
        <router-link :to="{ path: '/explore', query: { category: 'Phát triển' } }" class="hover:text-primary transition-colors">Phát triển</router-link>
        <router-link :to="{ path: '/explore', query: { category: 'Kinh doanh' } }" class="hover:text-primary transition-colors">Kinh doanh</router-link>
        <router-link :to="{ path: '/explore', query: { category: 'IT & Phần mềm' } }" class="hover:text-primary transition-colors">IT & Phần mềm</router-link>
        <router-link :to="{ path: '/explore', query: { category: 'Thiết kế' } }" class="hover:text-primary transition-colors">Thiết kế</router-link>
        <router-link :to="{ path: '/explore', query: { category: 'Marketing' } }" class="hover:text-primary transition-colors">Marketing</router-link>
        <router-link :to="{ path: '/explore', query: { category: 'Sức khỏe & Thể hình' } }" class="hover:text-primary transition-colors">Sức khỏe & Thể hình</router-link>
        <router-link :to="{ path: '/explore', query: { category: 'Âm nhạc' } }" class="hover:text-primary transition-colors">Âm nhạc</router-link>
      </div>
    </header>

    <slot />

    <!-- Footer -->
    <footer class="bg-slate-900 text-[#D1D5DB] border-t border-slate-800 mt-16">
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

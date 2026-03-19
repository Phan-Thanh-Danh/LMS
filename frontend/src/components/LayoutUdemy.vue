<template>
  <div class="min-h-screen flex flex-col font-sans text-gray-800">
    <!-- AET Top Navbar -->
    <header class="bg-white border-b border-gray-200 h-[72px] text-sm px-6 flex items-center justify-between gap-4 sticky top-0 z-50">
      <!-- Logo -->
      <router-link to="/" class="flex-shrink-0 relative top-1">
        <span class="text-[32px] font-bold font-serif tracking-tight text-blue-700">AET LMS</span>
      </router-link>

      <!-- Categories (Mega Menu) -->
      <div class="relative hidden md:block">
        <!-- Backdrop -->
        <div v-show="showCategoryMenu" @click="showCategoryMenu = false" class="fixed inset-0 w-screen h-screen z-40 bg-transparent"></div>
        
        <button @click="showCategoryMenu = !showCategoryMenu" class="relative z-50 flex items-center gap-1 hover:text-blue-600 transition-colors ml-2 py-2 focus:outline-none">
          <span class="text-sm font-bold" :class="showCategoryMenu ? 'text-blue-700' : 'text-gray-800 hover:text-blue-700'">Danh mục</span>
        </button>

        <!-- Mega Menu Popup (Cellphones Style) -->
        <div v-show="showCategoryMenu" class="absolute top-[45px] left-0 w-[800px] bg-white rounded-2xl shadow-[0_15px_60px_-15px_rgba(0,0,0,0.3)] border border-gray-100 z-50 flex overflow-hidden min-h-[420px] transition-opacity duration-200">
          <!-- Left side: Main Categories -->
          <div class="w-[35%] bg-gray-50 py-3 border-r border-gray-200">
            <div 
              v-for="(cat, key) in categoryTree" 
              :key="key"
              @mouseenter="activeSubMenu = key"
              :class="[
                'px-5 py-3.5 flex justify-between items-center cursor-pointer transition-colors', 
                activeSubMenu === key ? 'bg-white font-bold text-blue-700 border-l-4 border-blue-700 shadow-sm' : 'hover:bg-gray-100 text-gray-700 font-medium border-l-4 border-transparent'
              ]"
            >
              <div class="flex items-center gap-3">
                <span v-html="cat.icon" class="w-5 h-5 flex-shrink-0 text-current"></span>
                <span class="text-[14px]">{{ cat.name }}</span>
              </div>
              <svg class="w-4 h-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7"></path></svg>
            </div>
          </div>

          <!-- Right side: Sub Categories -->
          <div class="w-[65%] p-8 bg-white overflow-y-auto max-h-[500px]">
            <h3 class="text-[20px] font-bold text-gray-900 mb-6 border-b border-gray-100 pb-3 flex items-center gap-2">
              <span v-html="categoryTree[activeSubMenu].icon" class="w-6 h-6 text-blue-700"></span>
              {{ categoryTree[activeSubMenu].name }}
            </h3>
            <div class="grid grid-cols-2 gap-x-8 gap-y-8">
              <div v-for="(col, index) in categoryTree[activeSubMenu].subs" :key="index">
                <h4 class="font-bold text-[15px] text-gray-800 mb-3">{{ col.title }}</h4>
                <ul class="space-y-3">
                  <li v-for="link in col.links" :key="link">
                    <a href="#" class="text-[14px] text-gray-600 hover:text-blue-700 hover:underline transition-colors font-medium">{{ link }}</a>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Search Bar -->
      <div class="flex-1 max-w-3xl hidden md:block pl-2">
        <div class="relative group">
          <div class="absolute inset-y-0 left-0 flex items-center pl-4 pointer-events-none text-gray-500 group-focus-within:text-blue-600">
            <svg class="w-5 h-5 opacity-70" fill="none" stroke="currentColor" viewBox="0 0 24 24">
               <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path>
            </svg>
          </div>
          <input type="text" placeholder="Tìm kiếm khóa học..." class="block w-full pl-12 pr-4 py-3 bg-gray-50 border border-black rounded-full text-sm placeholder-gray-500 focus:bg-white focus:outline-none focus:ring-1 focus:ring-black transition-all">
        </div>
      </div>

      <!-- Links (AET Business, Teach) -->
      <div class="hidden lg:flex items-center gap-5 text-[15px] font-medium text-gray-700 ml-4">
        <a href="#" class="hover:text-blue-700 transition-colors">AET Doanh nghiệp</a>
        <a href="#" class="hover:text-blue-700 transition-colors">Trở thành Giảng viên</a>
      </div>

      <!-- Icons (Cart) -->
      <div class="flex items-center">
        <router-link to="/cart" class="p-3 hover:text-blue-700 transition-colors ml-2 rounded-full hover:bg-gray-50 relative">
          <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z"></path>
          </svg>
          <span class="absolute top-1.5 right-1 w-4 h-4 bg-[#8731e8] text-white flex items-center justify-center rounded-full text-[10px] font-bold">2</span>
        </router-link>

        <!-- Auth Buttons -->
        <div class="hidden md:flex items-center gap-2 ml-4">
          <router-link to="/login" class="px-5 py-2.5 border border-gray-300 bg-white text-gray-800 font-bold text-[15px] hover:bg-gray-50 transition-colors rounded-full shadow-sm">Đăng nhập</router-link>
          <router-link to="/register" class="px-5 py-2.5 border border-transparent bg-gray-900 text-white font-bold text-[15px] hover:bg-gray-800 transition-colors rounded-full shadow-sm">Đăng ký</router-link>
          <button class="px-3 py-2.5 border border-gray-300 bg-white text-gray-800 hover:bg-gray-50 transition-colors ml-1 rounded-full shadow-sm">
            <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 12a9 9 0 01-9 9m9-9a9 9 0 00-9-9m9 9H3m9 9a9 9 0 01-9-9m9 9c1.657 0 3-4.03 3-9s-1.343-9-3-9m0 18c-1.657 0-3-4.03-3-9s1.343-9 3-9m-9 9a9 9 0 019-9"></path></svg>
          </button>
        </div>
      </div>
    </header>

    <!-- Main Content Slot -->
    <main class="flex-1">
      <slot></slot>
    </main>

    <!-- Simple Footer -->
    <footer class="bg-gray-900 text-white py-12 px-6 mt-16 text-[15px]">
      <div class="max-w-7xl mx-auto flex flex-col md:flex-row justify-between items-start gap-8">
        <div class="grid grid-cols-2 md:grid-cols-3 gap-8 w-full md:w-3/4">
          <div class="flex flex-col gap-2.5">
            <a href="#" class="hover:underline">AET Doanh nghiệp</a>
            <a href="#" class="hover:underline">Giảng dạy trên AET</a>
            <a href="#" class="hover:underline">Tải ứng dụng</a>
            <a href="#" class="hover:underline">Về chúng tôi</a>
            <a href="#" class="hover:underline">Liên hệ</a>
          </div>
          <div class="flex flex-col gap-2.5">
            <a href="#" class="hover:underline">Tuyển dụng</a>
            <a href="#" class="hover:underline">Blog</a>
            <a href="#" class="hover:underline">Trợ giúp và Hỗ trợ</a>
            <a href="#" class="hover:underline">Tiếp thị liên kết</a>
            <a href="#" class="hover:underline">Nhà đầu tư</a>
          </div>
          <div class="flex flex-col gap-2.5">
            <a href="#" class="hover:underline">Điều khoản</a>
            <a href="#" class="hover:underline">Chính sách bảo mật</a>
            <a href="#" class="hover:underline">Cài đặt Cookie</a>
            <a href="#" class="hover:underline">Sitemap</a>
            <a href="#" class="hover:underline">Tuyên bố khả năng tiếp cận</a>
          </div>
        </div>
        <div>
          <button class="border border-white px-5 py-2 flex items-center gap-2 hover:bg-gray-800 transition-colors font-bold">
            <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 12a9 9 0 01-9 9m9-9a9 9 0 00-9-9m9 9H3m9 9a9 9 0 01-9-9m9 9c1.657 0 3-4.03 3-9s-1.343-9-3-9m0 18c-1.657 0-3-4.03-3-9s1.343-9 3-9m-9 9a9 9 0 019-9"></path></svg>
            Tiếng Việt
          </button>
        </div>
      </div>
      <div class="max-w-7xl mx-auto flex flex-col md:flex-row justify-between items-center mt-14 text-sm text-gray-400 gap-4">
        <span class="text-3xl font-bold font-serif text-white tracking-tight">AET LMS</span>
        <p>© 2026 Học viện AET. Đã dăng ký bản quyền.</p>
      </div>
    </footer>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const showCategoryMenu = ref(false)
const activeSubMenu = ref('lTrinh')

const categoryTree = {
  'lTrinh': {
    name: 'Lập trình',
    icon: '<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 20l4-16m4 4l4 4-4 4M6 16l-4-4 4-4" /></svg>',
    subs: [
      { title: 'Ngôn ngữ Phổ biến', links: ['Python', 'JavaScript', 'C++', 'Java', 'C#'] },
      { title: 'Phát triển Web', links: ['VueJS', 'ReactJS', 'NodeJS', 'PHP', 'Laravel'] },
      { title: 'Phát triển Di động', links: ['Flutter', 'React Native', 'iOS Swift', 'Android Kotlin'] }
    ]
  },
  'data': {
    name: 'Khoa học Dữ liệu',
    icon: '<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z" /></svg>',
    subs: [
      { title: 'Phân tích Dữ liệu', links: ['Excel', 'SQL', 'Power BI', 'Tableau'] },
      { title: 'AI & Machine Learning', links: ['Machine Learning cơ bản', 'Deep Learning', 'Computer Vision'] },
      { title: 'Quản trị Cơ sở dữ liệu', links: ['MySQL', 'PostgreSQL', 'MongoDB', 'Oracle'] }
    ]
  },
  'design': {
    name: 'Thiết kế Đồ họa',
    icon: '<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 21a4 4 0 01-4-4V5a2 2 0 012-2h4a2 2 0 012 2v12a4 4 0 01-4 4zm0 0h12a2 2 0 002-2v-4a2 2 0 00-2-2h-2.343M11 7.343l1.657-1.657a2 2 0 012.828 0l2.829 2.829a2 2 0 010 2.828l-8.486 8.485M7 17h.01" /></svg>',
    subs: [
      { title: 'Phần mềm 2D', links: ['Photoshop', 'Illustrator', 'InDesign', 'CorelDRAW'] },
      { title: 'UI/UX Design', links: ['Figma', 'Adobe XD', 'Thiết kế Giao diện Web', 'Wireframing'] },
      { title: 'Đồ họa 3D', links: ['Blender', 'Maya', 'AutoCAD'] }
    ]
  },
  'marketing': {
    name: 'Digital Marketing',
    icon: '<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 3.055A9.001 9.001 0 1020.945 13H11V3.055z" /><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M20.488 9H15V3.512A9.025 9.025 0 0120.488 9z" /></svg>',
    subs: [
      { title: 'Quảng cáo Trực tuyến', links: ['Facebook Ads', 'Google Ads', 'SEO Căn bản', 'Tối ưu Conversion'] },
      { title: 'Nội dung Số', links: ['Copywriting', 'Xây kênh Tiktok', 'Sáng tạo Youtube', 'Podcast'] }
    ]
  }
}
</script>

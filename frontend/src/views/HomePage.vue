<template>
  <LayoutUdemy>
    <!-- Hero Section -->
    <section class="max-w-7xl mx-auto mt-6 px-4 relative mb-12">
      <div class="w-full h-[350px] md:h-[450px] overflow-hidden relative shadow-sm rounded-2xl">
        <img src="https://images.unsplash.com/photo-1516321318423-f06f85e504b3?ixlib=rb-4.0.3&auto=format&fit=crop&w=2070&q=80" alt="Hero Banner" class="w-full h-full object-cover">
        
        <!-- Hero Box -->
        <div class="absolute top-10 left-10 md:top-16 md:left-16 bg-white/95 backdrop-blur-sm p-8 shadow-xl max-w-[420px] rounded-2xl border border-white/40">
          <h1 class="text-[34px] leading-tight font-bold font-serif mb-4 text-gray-900">Học tập dẫn lối thành công</h1>
          <p class="text-[17px] text-gray-800">Kỹ năng cho hiện tại (và tương lai của bạn). Bắt đầu hành trình cùng AET ngay hôm nay!</p>
        </div>
      </div>
    </section>

    <!-- Trusted By -->
    <section class="bg-gray-50/50 py-12 mb-16 mx-4 md:mx-auto max-w-7xl justify-center flex flex-col items-center px-6 rounded-3xl border border-gray-100">
      <p class="text-lg font-normal text-gray-500 mb-8 text-center">Được tin dùng bởi hơn 15,000 công ty và hàng triệu học viên trên toàn cầu</p>
      <div class="flex gap-10 md:gap-16 items-center flex-wrap justify-center opacity-70">
        <span class="text-2xl font-bold font-serif text-gray-500">Volkswagen</span>
        <span class="text-2xl font-bold font-serif text-gray-500">Samsung</span>
        <span class="text-2xl font-bold font-serif text-gray-500">Cisco</span>
        <span class="text-2xl font-bold font-serif text-gray-500">Vimeo</span>
        <span class="text-2xl font-bold font-serif text-gray-500">P&G</span>
        <span class="text-2xl font-bold font-serif text-gray-500 italic">Hewlett Packard</span>
      </div>
    </section>

    <!-- Courses List Section -->
    <section class="max-w-7xl mx-auto px-4 mb-16">
      <h2 class="text-3xl font-bold mb-4 font-serif text-gray-900">Khám phá đa dạng các khóa học</h2>
      <p class="text-xl text-gray-700 mb-6">Lựa chọn từ hơn 210,000 khóa học video trực tuyến với những cập nhật mới nhất mỗi tháng</p>
      
      <!-- Tabs -->
      <div class="flex gap-6 border-b border-gray-200 mb-6 overflow-x-auto whitespace-nowrap">
        <button 
          v-for="cat in categories" 
          :key="cat"
          @click="activeCategory = cat"
          :class="[
            'pb-3 text-[16px] transition-all border-b-2', 
            activeCategory === cat ? 'text-blue-700 font-bold border-blue-700' : 'text-gray-500 font-medium border-transparent hover:text-black hover:border-gray-300'
          ]"
        >
          {{ cat }}
        </button>
      </div>

      <!-- Course Category Descriptor Container box -->
      <div class="border border-gray-200 bg-white shadow-sm p-8 pt-10 rounded-3xl animate-fade-in" :key="activeCategory">
        <h3 class="text-[25px] font-bold mb-3 text-black">{{ currentInfo.title }}</h3>
        <p class="text-[15px] text-gray-800 mb-6 max-w-4xl line-height-[1.5]">{{ currentInfo.desc }}</p>
        <button class="border border-gray-300 bg-white px-5 py-2.5 rounded-lg font-bold text-sm hover:bg-gray-50 transition-colors mb-10 shadow-sm text-gray-800">
          Khám phá {{ activeCategory }} ngay
        </button>
        
        <!-- Courses Grid Dynamically rendered -->
        <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 lg:grid-cols-5 gap-6">
          <div v-for="(course, i) in currentCourses" :key="i" class="relative group/card cursor-pointer bg-white rounded-2xl transition-all duration-300 flex flex-col h-full border border-gray-100 hover:shadow-xl hover:border-gray-200 hover:-translate-y-1 z-0 hover:z-50">
            <div class="w-full aspect-video overflow-hidden relative rounded-t-2xl">
              <img :src="course.img" :alt="course.title" class="w-full h-full object-cover group-hover/card:scale-105 transition-transform duration-500">
            </div>
            <div class="p-4 flex flex-col flex-1">
              <h4 class="font-bold text-[16px] leading-[1.3] text-gray-900 line-clamp-2">{{ course.title }}</h4>
              <p class="text-xs text-gray-500 mt-2">{{ course.author }}</p>
              <div class="flex items-center gap-1 mt-auto pt-2">
                <span class="text-sm font-bold text-amber-600">{{ course.rating }}</span>
                <div class="flex text-amber-400">
                  <svg v-for="s in 5" :key="s" class="w-[14px] h-[14px]" fill="currentColor" viewBox="0 0 20 20"><path d="M10 15l-5.878 3.09 1.123-6.545L.489 6.91l6.572-.955L10 0l2.939 5.955 6.572.955-4.756 4.635 1.123 6.545z"></path></svg>
                </div>
                <span class="text-xs text-gray-500 ml-1">({{ course.reviews }})</span>
              </div>
              <div class="font-bold text-[17px] mt-1 text-black">₫{{ course.price }} <span class="text-[13px] text-gray-500 line-through font-normal ml-1">₫{{ course.oldPrice }}</span></div>
              <div class="mt-2" v-if="course.bestseller"><span class="bg-[#eceb98]/60 text-[#5f5d0e] text-xs font-bold px-2 py-1 rounded-md">Bán chạy nhất</span></div>
            </div>

            <!-- Hover Popover (giống Udemy) -->
            <div :class="[
              'absolute top-0 w-[340px] bg-white rounded-xl shadow-[0_10px_40px_-10px_rgba(0,0,0,0.3)] border border-gray-200 p-6 opacity-0 translate-y-2 invisible group-hover/card:visible group-hover/card:opacity-100 group-hover/card:translate-y-0 transition-all duration-300 pointer-events-none group-hover/card:pointer-events-auto cursor-default z-[100]',
              i % 5 >= 3 ? 'right-[102%] mr-2' : 'left-[102%] ml-2'
            ]">
              <!-- Triangle Arrow -->
              <div :class="[
                'absolute top-10 w-4 h-4 bg-white border-gray-200 transform pointer-events-none',
                i % 5 >= 3 ? 'right-[-9px] border-r border-b rotate-[-45deg]' : 'left-[-9px] border-l border-b rotate-[45deg]'
              ]"></div>
              
              <h4 class="text-[19px] font-bold text-gray-900 leading-[1.3] mb-3">{{ course.title }}</h4>
              
              <!-- Badges -->
              <div class="flex items-center gap-2 mb-3">
                <span v-if="course.bestseller" class="bg-[#eceb98] text-[#3d3c0a] text-[11px] font-bold px-2 py-0.5 rounded-sm">Bán chạy nhất</span>
                <span class="text-[13px] font-bold text-green-700">Cập nhật Tháng 3/2026</span>
              </div>
              
              <!-- Specs -->
              <div class="text-[12px] text-gray-500 mb-4 flex items-center gap-1.5 flex-wrap">
                <span>17 tổng số giờ</span> &middot; <span>Trung cấp</span> &middot; <span>Phụ đề</span>
              </div>
              
              <p class="text-[13px] text-gray-700 leading-relaxed mb-4">Làm chủ lộ trình trong 30 ngày: xây dựng các dự án thực tế cùng {{ course.author }}, nắm vững LangGraph, AutoGen và SDK mới nhất.</p>
              
              <!-- Checklist -->
              <ul class="space-y-3 mb-6 text-left">
                <li class="flex items-start gap-2.5">
                  <svg class="w-4 h-4 text-gray-800 flex-shrink-0 relative top-[2px]" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7"></path></svg>
                  <span class="text-[13px] text-gray-700 leading-[1.4]">Dự án 1: Career Digital Twin. Xây dựng và deploy mô hình đa lớp độc lập cho nhà tuyển dụng tương lai.</span>
                </li>
                <li class="flex items-start gap-2.5">
                  <svg class="w-4 h-4 text-gray-800 flex-shrink-0 relative top-[2px]" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7"></path></svg>
                  <span class="text-[13px] text-gray-700 leading-[1.4]">Dự án 2: Lập trình hệ thống Business giúp tiếp cận hàng ngàn khách hàng qua Email doanh nghiệp chuyên nghiệp.</span>
                </li>
                <li class="flex items-start gap-2.5">
                  <svg class="w-4 h-4 text-gray-800 flex-shrink-0 relative top-[2px]" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7"></path></svg>
                  <span class="text-[13px] text-gray-700 leading-[1.4]">Dự án 3: Khám phá sâu (Deep Research). Nhóm Agent tự động hóa tra cứu 24/7 với chủ đề tùy chọn.</span>
                </li>
              </ul>
              
              <!-- Actions -->
              <div class="flex gap-3 items-center">
                <button class="flex-1 bg-[#8731e8] hover:bg-[#6c27ba] text-white font-bold py-[14px] rounded-lg transition-colors cursor-pointer text-[15px]">
                  Thêm vào giỏ
                </button>
                <button class="p-3 border border-black rounded-full hover:bg-gray-100 transition-colors group/btn cursor-pointer">
                  <svg class="w-6 h-6 text-black group-hover/btn:fill-red-500 group-hover/btn:text-red-500 transition-colors" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z"></path></svg>
                </button>
              </div>
            </div>

          </div>
        </div>
      </div>
    </section>
  </LayoutUdemy>
</template>

<script setup>
import { ref, computed } from 'vue'
import LayoutUdemy from '../components/LayoutUdemy.vue'

const categories = [
  'Python', 
  'Excel', 
  'Phát triển Web', 
  'JavaScript', 
  'Khoa học Máy tính', 
  'Kỹ năng mềm'
]

const activeCategory = ref('Python')

const categoryInfo = {
  'Python': {
    title: 'Mở rộng cơ hội nghề nghiệp cùng lập trình Python',
    desc: 'Tham gia các khóa học Python hàng đầu của AET Academy để làm chủ ngôn ngữ lập trình mạnh mẽ này. Hoàn hảo cho các dự án Web, Khoa học dữ liệu, AI và Machine Learning.',
  },
  'Excel': {
    title: 'Phân tích dữ liệu đỉnh cao cùng Microsoft Excel',
    desc: 'Nâng cao cấp độ tin học văn phòng của bạn. Excel không chỉ là bảng tính, nó là công cụ sống còn của phân tích dữ liệu, kế toán và tự động hóa công việc cho bất kỳ doanh nghiệp nào.',
  },
  'Phát triển Web': {
    title: 'Trở thành Lập trình viên Web Fullstack',
    desc: 'Nắm vững phân tầng website qua HTML, CSS, JavaScript và framework hiện đại (React/Vue). Xây dựng những Website hoàn chỉnh từ con số 0 để vững bước vào thị trường IT.',
  },
  'JavaScript': {
    title: 'Làm chủ ngôn ngữ của Web: JavaScript',
    desc: 'Từ việc tạo ra các tương tác cơ bản đến xây dựng frontend phức tạp. JavaScript là ngôn ngữ phổ biến nhất toàn cầu và bắt buộc phải biết với bất kỳ ai làm nghề Web Dev.',
  },
  'Khoa học Máy tính': {
    title: 'Nền tảng vững chắc với Khoa học Máy tính (CS)',
    desc: 'Tìm hiểu hệ nhị phân, cấu trúc dữ liệu và thuật toán ở mức chuyên sâu. Việc hiểu biết cốt lõi này sẽ giải phóng giới hạn và giúp bạn tư duy sắc bén hơn bao giờ hết.',
  },
  'Kỹ năng mềm': {
    title: 'Bứt phá sự nghiệp nhờ Kỹ năng Giao tiếp & Mềm',
    desc: 'Phát triển năng lực giao tiếp, quản lý thời gian, tư duy phản biện và lãnh đạo. Sở hữu các kỹ năng này giúp bạn dễ dàng thăng tiến lên các vị trí quản lý Manager cấp cao.',
  }
}

// Data Array Mock-up
const pythonCourses = [
  { title: 'Khóa học Lập trình Python 100 Ngày: Bootcamp Chuyên nghiệp', author: 'Lê Văn Anh', rating: 4.7, reviews: '29,123', price: '349,000', oldPrice: '1,899,000', img: 'https://images.unsplash.com/photo-1526379095098-d400fd0bf935?w=500&auto=format&fit=crop&q=60', bestseller: true },
  { title: 'Lập trình Python từ Cơ bản đến Nâng cao', author: 'Nguyễn Văn Biên', rating: 4.6, reviews: '48,901', price: '349,000', oldPrice: '2,199,000', img: 'https://images.unsplash.com/photo-1587620962725-abab7fe55159?w=500&auto=format&fit=crop&q=60', bestseller: false },
  { title: 'Nhập môn Machine Learning & Trí tuệ Nhân tạo thực chiến', author: 'Trần Duy Tân', rating: 4.5, reviews: '15,800', price: '349,000', oldPrice: '2,499,000', img: 'https://images.unsplash.com/photo-1555066931-4365d14bab8c?w=500&auto=format&fit=crop&q=60', bestseller: true },
  { title: 'Phân tích & Trực quan hóa dữ liệu Doanh nghiệp', author: 'Lê Đức Thọ', rating: 4.6, reviews: '12,560', price: '549,000', oldPrice: '1,999,000', img: 'https://images.unsplash.com/photo-1542831371-29b0f74f9713?w=500&auto=format&fit=crop&q=60', bestseller: false },
  { title: 'Lập trình Game 2D với thư viện Pygame', author: 'Đội ngũ AET', rating: 4.8, reviews: '9,104', price: '349,000', oldPrice: '1,999,000', img: 'https://images.unsplash.com/photo-1618477388954-7852f32655ec?w=500&auto=format&fit=crop&q=60', bestseller: false }
]

const webCourses = [
  { title: 'Khóa học VueJS 3 & Vue Router Toàn Tập Thực Chiến', author: 'Vũ Nguyễn Coder', rating: 4.9, reviews: '11,200', price: '499,000', oldPrice: '2,500,000', img: 'https://images.unsplash.com/photo-1555099962-4199c345e5dd?w=500&auto=format&fit=crop&q=60', bestseller: true },
  { title: 'HTML, CSS, JavaScript cho người mới bắt đầu lập trình', author: 'F8 Team', rating: 4.8, reviews: '150,000', price: '199,000', oldPrice: '999,000', img: 'https://images.unsplash.com/photo-1627398225081-24c89544eb1a?w=500&auto=format&fit=crop&q=60', bestseller: true },
  { title: 'Cẩm nang toàn diện ReactJS Thực chiến & Clone UI web', author: 'EvonDev', rating: 4.7, reviews: '22,400', price: '549,000', oldPrice: '1,999,000', img: 'https://images.unsplash.com/photo-1633356122544-f134324a6cee?w=500&auto=format&fit=crop&q=60', bestseller: false }
]

const excelCourses = [
  { title: 'Microsoft Excel Nâng Cao Cho Dân Kế Toán và Kiểm toán', author: 'Phạm Thị Cúc', rating: 4.9, reviews: '55,102', price: '299,000', oldPrice: '1,200,000', img: 'https://images.unsplash.com/photo-1460925895917-afdab827c52f?w=500&auto=format&fit=crop&q=60', bestseller: true },
  { title: 'VBA và Tự động hóa công việc quản trị với Excel nâng cao', author: 'Đào Tuấn', rating: 4.5, reviews: '12,020', price: '450,000', oldPrice: '2,000,000', img: 'https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=500&auto=format&fit=crop&q=60', bestseller: false },
  { title: 'Thống kê cơ bản và báo cáo Dashboard với Excel', author: 'Nguyễn Văn Data', rating: 4.8, reviews: '7,400', price: '399,000', oldPrice: '1,850,000', img: 'https://images.unsplash.com/photo-1603796846097-bee99e4a601f?w=500&auto=format&fit=crop&q=60', bestseller: true }
]

const allCourses = {
  'Python': pythonCourses,
  'Excel': excelCourses,
  'Phát triển Web': webCourses,
  'JavaScript': webCourses, // mix web courses for simplicity
  'Khoa học Máy tính': pythonCourses, 
  'Kỹ năng mềm': excelCourses 
}

const currentInfo = computed(() => categoryInfo[activeCategory.value])
const currentCourses = computed(() => allCourses[activeCategory.value])

</script>

<style scoped>
.animate-fade-in {
  animation: fadeIn 0.4s ease-out;
}
@keyframes fadeIn {
  from { opacity: 0.5; transform: translateY(5px); }
  to { opacity: 1; transform: translateY(0); }
}
</style>

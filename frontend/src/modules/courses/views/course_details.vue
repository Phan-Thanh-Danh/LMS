<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import LayoutStudent from '@/layouts/LayoutStudent.vue';

// Lấy thông tin router để có thể xử lý các action
const router = useRouter();

// Dữ liệu mock cho khóa học
const course = {
  title: 'V-Core AI: Trí tuệ Việt cho quản trị số',
  category: 'TRÍ TUỆ NHÂN TẠO > QUẢN TRỊ DOANH NGHIỆP',
  description: 'Khám phá sức mạnh của AI trong việc tối ưu hóa quy trình quản trị doanh nghiệp tại Việt Nam. Được thiết kế bởi các chuyên gia đầu ngành cho bối cảnh kinh tế số.',
  rating: 4.9,
  students: 1240,
  instructor: 'Tiến sĩ Nguyễn Văn A',
  language: 'TIẾNG VIỆT',
  lastUpdated: '10/2024',
  price: '1.499.000 đ',
  originalPrice: '2.500.000 đ',
  discount: '-40%',
  thumbnail: 'https://images.unsplash.com/photo-1550751827-4bd374c3f58b?auto=format&fit=crop&q=80&w=800',
  learnings: [
    'Ứng dụng AI vào tự động hóa báo cáo tài chính',
    'Xây dựng hệ thống quản trị nhân sự thông minh',
    'Tối ưu hóa chuỗi cung ứng bằng thuật toán dự báo',
    'Phân tích dữ liệu khách hàng Việt Nam đa kênh',
    'Quy trình bảo mật dữ liệu doanh nghiệp trong kỷ nguyên số'
  ],
  curriculum: [
    {
      id: 1,
      title: 'Chương 1: Tổng quan về AI trong bối cảnh Việt Nam',
      stats: '4 bài giảng • 45 phút',
      expanded: true,
      lessons: [
        { id: 11, title: '1.1 Định nghĩa AI và tác động đến doanh nghiệp nội địa', duration: '12:30', media: 'play_circle' },
        { id: 12, title: '1.2 Các xu hướng quản trị số tại Việt Nam 2024', duration: '15:10', media: 'play_circle' },
        { id: 13, title: '1.3 Case study: Thành công từ các Startup Việt', duration: '10:45', media: 'lock' }
      ]
    },
    { id: 2, title: 'Chương 2: Xây dựng nền tảng dữ liệu số', stats: '6 bài giảng • 1h 20m', expanded: false, lessons: [] },
    { id: 3, title: 'Chương 3: AI trong quản trị tài chính và dòng tiền', stats: '8 bài giảng • 2h 45m', expanded: false, lessons: [] }
  ],
  instructorInfo: {
    name: 'Tiến sĩ Nguyễn Văn A',
    title: 'Chuyên gia cấp cao về AI & Chiến lược chuyển đổi số',
    rating: '4.8 Rating',
    totalStudents: '15.400 Học viên',
    totalCourses: '12 Khóa học',
    avatar: 'https://images.unsplash.com/photo-1560250097-0b93528c311a?auto=format&fit=crop&w=150&q=80',
    bio: 'Tiến sĩ Nguyễn Văn A có hơn 15 năm kinh nghiệm trong lĩnh vực trí tuệ nhân tạo và quản trị hệ thống. Ông từng là cố vấn chiến lược cho nhiều tập đoàn công nghệ lớn tại Việt Nam và khu vực. Với phong cách giảng dạy thực tiễn, ông giúp học viên chuyển hóa lý thuyết AI phức tạp thành các giải pháp quản trị cụ thể, tối ưu.'
  },
  reviews: {
    average: 4.9,
    total: 1240,
    distribution: [
      { stars: 5, percentage: 92 },
      { stars: 4, percentage: 6 },
      { stars: 3, percentage: 2 },
      { stars: 2, percentage: 0 },
      { stars: 1, percentage: 0 }
    ],
    items: [
      {
        name: 'Hoàng Long',
        initial: 'HL',
        rating: 5,
        content: 'Khóa học cực kỳ sát với thực tế doanh nghiệp Việt Nam. Tôi đã áp dụng ngay module quản trị tài chính vào công ty mình và thấy hiệu quả rõ rệt trong việc theo dõi dòng tiền.'
      },
      {
        name: 'Minh Thư',
        initial: 'MT',
        rating: 5,
        content: 'Giảng viên giải thích rất dễ hiểu ngay cả với những người không chuyên về tech như mình. Rất đáng đồng tiền bát gạo!'
      }
    ]
  }
};

const handleAddToCart = () => {
  router.push('/cart');
};
</script>

<template>
  <LayoutStudent>
    <div class="bg-white font-plus-jakarta antialiased">
      <!-- Hero Section (Blue Background area) -->
      <section class="bg-surface-container-lowest border-b border-surface-container-highest">
          <div class="max-w-[1240px] mx-auto px-6 py-12">
              <div class="grid grid-cols-1 lg:grid-cols-12 gap-10 items-center">
                  <div class="lg:col-span-8">
                      <div class="text-[10px] font-bold text-primary tracking-widest uppercase mb-4">{{ course.category }}</div>
                      <h1 class="text-4xl lg:text-5xl font-extrabold text-gray-900 leading-[1.15] mb-4">
                          {{ course.title }}
                      </h1>
                      <p class="text-base text-gray-700 leading-relaxed mb-6 max-w-2xl">
                          {{ course.description }}
                      </p>
                      
                      <div class="flex flex-wrap items-center gap-4 text-sm font-medium text-gray-700 mb-6">
                          <div class="flex items-center gap-1.5">
                              <span class="text-amber-500 font-bold">{{ course.rating }}</span>
                              <div class="flex text-amber-500 text-sm">
                                  <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1; font-size: 16px;">star</span>
                                  <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1; font-size: 16px;">star</span>
                                  <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1; font-size: 16px;">star</span>
                                  <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1; font-size: 16px;">star</span>
                                  <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1; font-size: 16px;">star_half</span>
                              </div>
                              <span class="text-gray-500">({{ course.students }} học viên)</span>
                          </div>
                          <div class="flex items-center gap-1">
                              <span class="material-symbols-outlined text-[16px] text-primary">person</span>
                              Giảng viên: <span class="font-bold text-gray-900">{{ course.instructor }}</span>
                          </div>
                      </div>

                      <div class="flex flex-wrap gap-3">
                          <div class="flex items-center gap-1.5 bg-white border border-surface-container px-3 py-1.5 rounded-full text-xs font-bold text-gray-700">
                              <span class="material-symbols-outlined text-[16px]">language</span> {{ course.language }}
                          </div>
                          <div class="flex items-center gap-1.5 bg-white border border-surface-container px-3 py-1.5 rounded-full text-xs font-bold text-gray-700">
                              <span class="material-symbols-outlined text-[16px]">update</span> CẬP NHẬT {{ course.lastUpdated }}
                          </div>
                      </div>
                  </div>

                  <div class="lg:col-span-4 lg:hidden">
                      <!-- Mobile View of Cover Image -->
                      <div class="rounded-xl overflow-hidden relative shadow-lg aspect-video mb-6">
                          <img :src="course.thumbnail" alt="Cover" class="w-full h-full object-cover">
                          <div class="absolute inset-0 bg-black/20 flex flex-col items-center justify-center">
                              <button class="w-16 h-12 bg-primary/90 hover:bg-primary rounded-xl flex items-center justify-center transition-transform hover:scale-105 backdrop-blur-sm shadow-xl">
                                  <span class="material-symbols-outlined text-white text-3xl" style="font-variation-settings: 'FILL' 1;">play_arrow</span>
                              </button>
                          </div>
                          <div class="absolute bottom-0 w-full bg-black/80 px-4 py-2 text-center text-xs font-bold text-white tracking-widest uppercase">
                              Xem trước khóa học
                          </div>
                      </div>
                  </div>
              </div>
          </div>
      </section>

      <!-- Main Content Area -->
      <section class="max-w-[1240px] mx-auto px-6 py-12">
          <div class="grid grid-cols-1 lg:grid-cols-12 gap-12 relative">
              
              <!-- Left Column Content -->
              <div class="lg:col-span-8 space-y-12">
                  <!-- Những gì bạn sẽ học -->
                  <div class="bg-surface-container-lowest rounded-xl border border-surface-container p-8">
                      <h2 class="text-xl font-bold text-gray-900 mb-6">Những gì bạn sẽ học</h2>
                      <div class="grid grid-cols-1 md:grid-cols-2 gap-x-8 gap-y-4">
                          <div v-for="(item, index) in course.learnings" :key="index" class="flex items-start gap-3">
                              <span class="material-symbols-outlined text-primary mt-0.5" style="font-variation-settings: 'FILL' 1;">check_circle</span>
                              <span class="text-sm text-gray-700 leading-relaxed">{{ item }}</span>
                          </div>
                      </div>
                  </div>

                  <!-- Lộ trình học tập -->
                  <div>
                      <div class="flex flex-col sm:flex-row sm:items-end justify-between mb-6 gap-4">
                          <div>
                              <h2 class="text-xl font-bold text-gray-900 mb-1">Lộ trình học tập</h2>
                              <p class="text-sm text-gray-600">12 Chương • 48 Bài giảng • 18h 30m tổng thời lượng</p>
                          </div>
                          <button class="text-xs font-bold text-primary tracking-widest uppercase hover:underline">
                              MỞ RỘNG TẤT CẢ
                          </button>
                      </div>

                      <div class="border border-surface-container rounded-xl overflow-hidden bg-white">
                          <div v-for="(chapter, idx) in course.curriculum" :key="chapter.id" 
                               class="border-b border-surface-container last:border-0">
                              <!-- Header -->
                              <button @click="chapter.expanded = !chapter.expanded"
                                  class="w-full flex items-center justify-between p-5 bg-surface-container-lowest hover:bg-surface-container-low transition-colors text-left focus:outline-none focus:bg-surface-container-low">
                                  <div class="flex items-center gap-3">
                                      <span class="material-symbols-outlined text-gray-500 transition-transform duration-300" :class="chapter.expanded ? 'rotate-180' : ''">
                                          expand_more
                                      </span>
                                      <span class="font-bold text-sm text-gray-900">{{ chapter.title }}</span>
                                  </div>
                                  <span class="text-xs text-gray-500">{{ chapter.stats }}</span>
                              </button>
                              
                              <!-- Items -->
                              <div v-show="chapter.expanded" class="bg-white">
                                  <div v-for="lesson in chapter.lessons" :key="lesson.id" 
                                       class="flex items-center justify-between p-4 pl-12 hover:bg-surface-container-lowest transition-colors group cursor-pointer border-t border-surface-container-highest border-dashed first:border-t-0">
                                      <div class="flex items-center gap-3">
                                          <span class="material-symbols-outlined text-primary text-[18px]" style="font-variation-settings: 'FILL' 1;" v-if="lesson.media === 'play_circle'">play_circle</span>
                                          <span class="material-symbols-outlined text-gray-400 text-[18px]" v-else>lock</span>
                                          <span class="text-sm text-gray-800 group-hover:text-primary transition-colors line-clamp-1">{{ lesson.title }}</span>
                                      </div>
                                      <span class="text-xs text-gray-500 whitespace-nowrap">{{ lesson.duration }}</span>
                                  </div>
                              </div>
                          </div>
                      </div>
                  </div>

                  <!-- Giảng viên hướng dẫn -->
                  <div>
                      <h2 class="text-xl font-bold text-gray-900 mb-6">Giảng viên hướng dẫn</h2>
                      <div class="flex flex-col sm:flex-row gap-6 items-start">
                          <img :src="course.instructorInfo.avatar" alt="Instructor" class="w-24 h-24 rounded-full object-cover shadow-sm shrink-0">
                          <div>
                              <h3 class="font-bold text-lg text-gray-900">{{ course.instructorInfo.name }}</h3>
                              <p class="text-sm text-primary font-medium mb-3">{{ course.instructorInfo.title }}</p>
                              <div class="flex flex-wrap items-center gap-4 text-xs font-bold text-gray-700 mb-4">
                                  <span class="flex items-center gap-1"><span class="material-symbols-outlined text-[16px]">star</span> {{ course.instructorInfo.rating }}</span>
                                  <span class="flex items-center gap-1"><span class="material-symbols-outlined text-[16px]">person</span> {{ course.instructorInfo.totalStudents }}</span>
                                  <span class="flex items-center gap-1"><span class="material-symbols-outlined text-[16px]">play_circle</span> {{ course.instructorInfo.totalCourses }}</span>
                              </div>
                              <p class="text-sm text-gray-700 leading-relaxed">{{ course.instructorInfo.bio }}</p>
                          </div>
                      </div>
                  </div>

                  <!-- Đánh giá từ học viên -->
                  <div>
                      <h2 class="text-xl font-bold text-gray-900 mb-6">Đánh giá từ học viên</h2>
                      <div class="flex flex-col md:flex-row gap-8 items-center bg-surface-container-lowest rounded-xl p-8 border border-surface-container mb-6">
                          <div class="text-center w-full md:w-auto shrink-0 space-y-1">
                              <div class="text-6xl font-black text-primary">{{ course.reviews.average }}</div>
                              <div class="flex justify-center text-amber-500">
                                  <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">star</span>
                                  <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">star</span>
                                  <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">star</span>
                                  <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">star</span>
                                  <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">star</span>
                              </div>
                              <div class="text-xs font-bold text-gray-500">XẾP HẠNG KHÓA HỌC</div>
                          </div>
                          
                          <div class="flex-1 w-full space-y-2">
                              <div v-for="dist in course.reviews.distribution" :key="dist.stars" class="flex items-center gap-3">
                                  <span class="text-xs font-medium text-amber-500 w-16 flex justify-end">
                                      <span v-for="i in dist.stars" :key="i" class="material-symbols-outlined text-[12px]" style="font-variation-settings: 'FILL' 1;">star</span>
                                  </span>
                                  <div class="flex-1 h-3 bg-surface-container rounded-full overflow-hidden">
                                      <div class="h-full bg-primary rounded-full transition-all" :style="{ width: dist.percentage + '%' }"></div>
                                  </div>
                                  <span class="w-8 text-xs font-medium text-gray-500">{{ dist.percentage }}%</span>
                              </div>
                          </div>
                      </div>

                      <!-- Reviews List -->
                      <div class="space-y-4">
                          <div v-for="(review, index) in course.reviews.items" :key="index" class="bg-white border border-surface-container rounded-lg p-6">
                              <div class="flex gap-4">
                                  <div class="w-12 h-12 rounded-full bg-primary-container text-primary font-bold flex items-center justify-center shrink-0">
                                      {{ review.initial }}
                                  </div>
                                  <div>
                                      <div class="font-bold text-sm text-gray-900">{{ review.name }}</div>
                                      <div class="flex text-amber-500 mb-3 mt-1">
                                          <span v-for="i in review.rating" :key="i" class="material-symbols-outlined text-[14px]" style="font-variation-settings: 'FILL' 1;">star</span>
                                      </div>
                                      <p class="text-sm text-gray-700 italic border-l-2 border-surface-container pl-4">
                                          "{{ review.content }}"
                                      </p>
                                  </div>
                              </div>
                          </div>
                      </div>
                  </div>

              </div>
              
              <!-- Right Column Sticky Overlay (Desktop) -->
              <div class="hidden lg:block lg:col-span-4 relative -mt-64">
                  <div class="sticky top-24 space-y-6">
                      <!-- Video Hero Image Card -->
                      <div class="rounded-xl overflow-hidden shadow-2xl relative bg-black aspect-video">
                          <img :src="course.thumbnail" alt="Cover" class="w-full h-full object-cover">
                          <div class="absolute inset-0 bg-black/20 flex flex-col items-center justify-center cursor-pointer hover:bg-black/30 transition-colors">
                              <button class="w-16 h-12 bg-primary/90 hover:bg-primary rounded-xl flex items-center justify-center transition-transform hover:scale-105 backdrop-blur-sm shadow-xl">
                                  <span class="material-symbols-outlined text-white text-3xl" style="font-variation-settings: 'FILL' 1;">play_arrow</span>
                              </button>
                          </div>
                          <div class="absolute bottom-0 w-full bg-black/80 px-4 py-3 text-center text-xs font-bold text-white tracking-widest uppercase cursor-pointer hover:bg-black transition-colors">
                              Xem trước khóa học
                          </div>
                      </div>

                      <!-- Pricing Card -->
                      <div class="bg-white rounded-xl border border-surface-container-highest shadow-xl p-8">
                          <div class="flex items-center gap-3 mb-6">
                              <span class="text-3xl font-extrabold text-gray-900">{{ course.price }}</span>
                              <span class="text-lg text-gray-400 line-through">{{ course.originalPrice }}</span>
                              <span class="bg-error/10 text-error font-bold text-xs px-2 py-1 rounded">{{ course.discount }}</span>
                          </div>
                          
                          <div class="space-y-3 mb-4">
                              <button @click="handleAddToCart" class="w-full bg-primary hover:bg-blue-700 text-white font-bold py-3.5 px-6 rounded-md transition-shadow shadow-md hover:shadow-lg">
                                  Mua ngay
                              </button>
                              <button @click="handleAddToCart" class="w-full border border-primary text-primary hover:bg-primary-container font-bold py-3.5 px-6 rounded-md transition-colors">
                                  Thêm vào giỏ hàng
                              </button>
                          </div>
                          
                          <p class="text-center text-[10px] text-gray-500 font-medium mb-6">Hoàn tiền trong 30 ngày nếu không hài lòng</p>
                          
                          <div>
                              <h4 class="font-bold text-sm text-gray-900 mb-4">Khóa học này bao gồm:</h4>
                              <ul class="space-y-3">
                                  <li class="flex items-center gap-3 text-sm text-gray-700">
                                      <span class="material-symbols-outlined text-gray-400 text-[18px]">all_inclusive</span> Truy cập trọn đời
                                  </li>
                                  <li class="flex items-center gap-3 text-sm text-gray-700">
                                      <span class="material-symbols-outlined text-gray-400 text-[18px]">workspace_premium</span> Chứng chỉ hoàn thành
                                  </li>
                                  <li class="flex items-center gap-3 text-sm text-gray-700">
                                      <span class="material-symbols-outlined text-gray-400 text-[18px]">devices</span> Học trên mọi thiết bị
                                  </li>
                                  <li class="flex items-center gap-3 text-sm text-gray-700">
                                      <span class="material-symbols-outlined text-gray-400 text-[18px]">download</span> Tài liệu thực hành đính kèm
                                  </li>
                              </ul>
                          </div>
                      </div>

                      <!-- Doanh nghiệp Card -->
                      <div class="bg-surface-container-lowest rounded-xl border border-surface-container p-6">
                          <h4 class="font-bold text-sm text-gray-900 mb-2">Đăng ký doanh nghiệp?</h4>
                          <p class="text-xs text-gray-600 mb-4 leading-relaxed">Đào tạo đội ngũ của bạn với gói ưu đãi đặc biệt dành cho tổ chức.</p>
                          <a href="#" class="text-xs font-bold text-primary hover:underline flex items-center gap-1">
                              Tìm hiểu thêm <span class="material-symbols-outlined text-[14px]">arrow_forward</span>
                          </a>
                      </div>
                  </div>
              </div>

              <!-- Mobile Sticky Buy Button -->
              <div class="lg:hidden fixed bottom-0 left-0 w-full bg-white border-t border-surface-container p-4 shadow-2xl z-50">
                  <div class="flex items-center gap-4 max-w-[1240px] mx-auto">
                      <div class="flex-1">
                          <div class="text-lg font-bold text-gray-900 leading-none mb-1">{{ course.price }}</div>
                          <div class="text-[10px] text-gray-500 line-through">{{ course.originalPrice }}</div>
                      </div>
                      <button @click="handleAddToCart" class="w-2/3 bg-primary text-white font-bold py-3 px-4 rounded-md shadow-lg">
                          Thêm vào giỏ
                      </button>
                  </div>
              </div>

          </div>
      </section>

      <div class="h-16"></div> <!-- Spacer for mobile bar -->
    </div>
  </LayoutStudent>
</template>

<style scoped>
/* Optional specific overrides */
</style>

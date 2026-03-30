<script setup>
import { ref, watch, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { useCategoryStore } from '@/stores/category';
import LayoutStudent from '@/layouts/LayoutStudent.vue';

const route = useRoute();
const categoryStore = useCategoryStore();
const currentCategory = ref('');
const displayedCourses = ref([]);

// Extended Mock data for various categories
const allCourses = [
  {
    id: 1,
    title: 'Mastering React & Next.js: Từ Zero đến Pro',
    category: 'Phát triển Web',
    parentCategory: 'Phát triển',
    image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDFfuV6DKABPU7qHvGhU_i94GasnZ83ZyWKqiUDArnqA2C45m2F200AFljw9mIb_VwxGiLxgoY1dIPTMF7KQ3oXL-bxXMOdolMTJM3ww1aTJDUZkuI0lCzsT7nmWdd3_B42JGlm89kRgll92vVkpatt7q74zA_xpQ-_VvPn0p--4rDZDvXzl1XYAxDfJr-zQjZuzgJltnWYEMoecQ9WTrveZL9l-5VmvKuq-KRmmDwR8HIGttkeLYmAnjuyZxcMajuGkXPW-UUu0QQ',
    rating: 4.9,
    reviews: 1240,
    instructor: 'Lê Minh Tuấn',
    price: '899.000đ',
    tag: 'Best Seller'
  },
  {
    id: 2,
    title: 'Lập trình Vue 3 Composition API từ cơ bản đến nâng cao',
    category: 'Vue.js',
    parentCategory: 'Phát triển',
    image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDcvHPkjcyMNVJ9jEsW3vHIuHZw58zbwqwk4HOsnZ2rh1cL7YTLnxPrg_WiVv_AJlIzfGqymU2Ubq1n9K1A9kIuzlfJZtJoXjEAhJypHAAtjuXkZVDixfq3Rr107VQGiAoeKZpII4xirHnwPeACWgxF4d8XZoGAptBiaLIeFl-nWz1hHV5l_CqctsshYQb2aDQJ6OASEzjL6S5gYJhUIvUcQfBBfrDrsfgkj1vu3QVqG_MvN0oV8OOhqxUJj0AOQkJ6Pc-IxBUVg_Q',
    rating: 4.8,
    reviews: 850,
    instructor: 'Phan Thanh Danh',
    price: '599.000đ'
  },
  {
    id: 3,
    title: 'Thiết kế UI/UX Chuyên nghiệp với Figma 2024',
    category: 'Figma',
    parentCategory: 'Thiết kế',
    image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuCCFO-pqpvxXKjPj2IVCTmESby-d8e_HLNpgXqE2_vmwSe342VSg_eHBieXxT5-c_qMJzoGuKBCdM8ow4tHR_tK-kzOK8NztlruBAQ7PDbI0_MMgcDaBvdPBGin5_MSvOYCw0lgqbz2R0aqceihPd5R43jRa2-A5E18mTDrnB5RVLKnp3CWHlh9CXYIkrIoAu-8KCOeZskb3WwA5aA8-xXc55KU0qQiYi5Z-wRjtdc8GO4_U2MH2iVWuaftsn_ECs9aihBRnA3N8qM',
    rating: 4.8,
    reviews: 850,
    instructor: 'Nguyễn Thảo Vy',
    price: '599.000đ',
    tag: 'Hot'
  },
  {
    id: 4,
    title: 'Chiến lược Digital Marketing Thực chiến cho SMEs',
    category: 'Marketing',
    parentCategory: 'Marketing',
    image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDwv1XUsgDxDBXTzfLGqw2t2us2dKd7LknpPMNiRWvxYZGEmA733VrrllMfM7G_wUyBX7x0LwmTsQRo2gMUYWGQxmX7g7aaUg8WdJU9xVZuYn1Ie2AQXJ3H_Jdu1iwoxMNnTf-mZ1pDsuxZtowqaT2zsxpEKf3B00c7Ip7LjOWVyQRNe1WG2USh8PD4erL3lEZ_lMOxMXUDC2OxazNaY4zhRoUXif1tnSoiUN5QGSB3Alz6zNVT0tm9TuNZkHnAp0KqrZ3-aFKXlfU',
    rating: 5.0,
    reviews: 420,
    instructor: 'Trần Hoàng Nam',
    price: '1.299.000đ'
  },
  {
    id: 5,
    title: 'Quản trị Tài chính Doanh nghiệp vừa và nhỏ',
    category: 'Tài chính',
    parentCategory: 'Kinh doanh',
    image: 'https://images.unsplash.com/photo-1460925895917-afdab827c52f?auto=format&fit=crop&q=80&w=800',
    rating: 4.7,
    reviews: 310,
    instructor: 'Lê Vĩnh Trường',
    price: '950.000đ'
  },
  {
    id: 6,
    title: 'AWS Certified Solutions Architect',
    category: 'AWS Certified',
    parentCategory: 'IT & Phần mềm',
    image: 'https://images.unsplash.com/photo-1451187580459-43490279c0fa?auto=format&fit=crop&q=80&w=800',
    rating: 4.9,
    reviews: 2150,
    instructor: 'Amazon Web Services',
    price: '1.500.000đ',
    tag: 'Best Seller'
  },
  {
    id: 7,
    title: 'Yoga và Thiền định Cơ bản',
    category: 'Sức khỏe',
    parentCategory: 'Sức khỏe & Thể hình',
    image: 'https://images.unsplash.com/photo-1544367567-0f2fcb009e0b?auto=format&fit=crop&q=80&w=800',
    rating: 4.8,
    reviews: 580,
    instructor: 'Master Trí Kiên',
    price: '499.000đ'
  },
  {
    id: 8,
    title: 'Học Piano cơ bản trong 30 ngày',
    category: 'Nhạc cụ',
    parentCategory: 'Âm nhạc',
    image: 'https://images.unsplash.com/photo-1552422535-c45813c61732?auto=format&fit=crop&q=80&w=800',
    rating: 4.6,
    reviews: 120,
    instructor: 'Trang Phạm',
    price: '799.000đ'
  },
  {
    id: 9,
    title: 'Python for Data Science và AI',
    category: 'Khoa học Dữ liệu',
    parentCategory: 'Phát triển',
    image: 'https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?auto=format&fit=crop&q=80&w=800',
    rating: 4.9,
    reviews: 1340,
    instructor: 'Trần Minh Đạo',
    price: '1.250.000đ'
  },
  {
    id: 10,
    title: 'Quản trị nhân sự trong kỷ nguyên số',
    category: 'Kỹ năng mềm',
    parentCategory: 'Kinh doanh',
    image: 'https://images.unsplash.com/photo-1552664730-d307ca884978?auto=format&fit=crop&q=80&w=800',
    rating: 4.5,
    reviews: 200,
    instructor: 'Hà Nguyễn',
    price: '850.000đ'
  }
];

const filterCourses = () => {
    currentCategory.value = route.query.category || 'Tất cả khóa học';
    if(currentCategory.value && currentCategory.value !== 'Tất cả khóa học') {
        const queryCategory = currentCategory.value.trim().toLowerCase();
        displayedCourses.value = allCourses.filter(c => 
            c.parentCategory.toLowerCase() === queryCategory || 
            c.category.toLowerCase() === queryCategory
        );
    } else {
        displayedCourses.value = allCourses;
    }
};

onMounted(async () => {
    if (categoryStore.categories.length === 0) {
        await categoryStore.fetchCategories();
    }
    filterCourses();
});

watch(() => route.query.category, () => {
    filterCourses();
});

</script>

<template>
  <LayoutStudent>
    <div class="bg-surface font-plus-jakarta antialiased min-h-screen border-t border-surface-container">
        <!-- Hero section for Explore -->
        <div class="bg-gray-900 text-white py-16 px-6">
            <div class="max-w-7xl mx-auto">
                <h1 class="text-4xl lg:text-5xl font-black tracking-tight mb-4 uppercase">
                    Chủ đề: {{ currentCategory }}
                </h1>
                <p class="text-gray-300 text-lg max-w-2xl leading-relaxed">
                    Nâng cao kỹ năng của bạn với các khóa học chất lượng cao về <span class="text-primary font-bold">{{ currentCategory }}</span>. Học mọi lúc, mọi nơi cùng các chuyên gia hàng đầu.
                </p>
            </div>
        </div>

        <div class="max-w-7xl mx-auto px-6 py-12">
            <!-- Results count -->
            <div class="mb-8 flex flex-col sm:flex-row sm:items-center justify-between gap-4">
                <div class="font-bold text-gray-900 text-xl">{{ displayedCourses.length }} khóa học về <span class="text-primary">{{ currentCategory }}</span></div>
                
                <div class="flex items-center gap-4">
                    <span class="text-sm font-bold text-gray-500">Sắp xếp theo:</span>
                    <select class="border border-surface-container bg-white rounded-lg px-4 py-2 text-sm font-medium focus:ring-2 focus:ring-primary outline-none">
                        <option>Được đánh giá cao nhất</option>
                        <option>Mới nhất</option>
                        <option>Phổ biến nhất</option>
                    </select>
                </div>
            </div>

            <!-- Empty State -->
            <div v-if="displayedCourses.length === 0" class="py-20 text-center border border-dashed border-gray-300 rounded-2xl bg-gray-50">
                <span class="material-symbols-outlined text-6xl text-gray-300 mb-4">search_off</span>
                <h3 class="text-2xl font-black text-gray-900 mb-2">Không tìm thấy khóa học nào</h3>
                <p class="text-gray-500 text-sm">Chúng tôi đang cập nhật các khóa học mới cho chủ đề này. Vui lòng quay lại sau!</p>
            </div>

            <!-- Grid Courses -->
            <div v-else class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
                <router-link :to="`/course/${course.id}`" v-for="course in displayedCourses" :key="course.id" 
                    class="group bg-white rounded-2xl overflow-hidden shadow-sm border border-gray-100 hover:shadow-2xl hover:-translate-y-1 transition-all duration-500 cursor-pointer flex flex-col h-full">
                    
                    <div class="aspect-video relative overflow-hidden shrink-0">
                        <img :src="course.image" :alt="course.title" class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-700">
                        <div class="absolute inset-x-0 bottom-0 h-1/2 bg-linear-to-t from-black/60 to-transparent"></div>
                        <span v-if="course.tag" class="absolute top-3 left-3 bg-amber-400 text-white text-[10px] font-black px-2 py-1 rounded-md shadow-lg shadow-amber-400/20 uppercase tracking-widest">{{ course.tag }}</span>
                        
                        <div class="absolute bottom-3 left-3 flex items-center gap-1.5 text-white">
                            <div class="flex text-amber-400">
                                <span v-for="i in 5" :key="i" class="material-symbols-outlined text-[14px]" style="font-variation-settings: 'FILL' 1;">star</span>
                            </div>
                            <span class="text-[10px] font-bold opacity-90">({{ course.rating || '4.9' }})</span>
                        </div>
                    </div>

                    <div class="p-5 flex-1 flex flex-col space-y-3">
                        <h3 class="font-black text-gray-900 leading-tight tracking-tight h-10 line-clamp-2 group-hover:text-primary transition-colors text-sm uppercase">
                            {{ course.title }}
                        </h3>
                        <p class="text-[10px] font-bold text-gray-400 uppercase tracking-widest leading-none">Bởi {{ course.instructor }}</p>
                        
                        <div class="pt-4 mt-auto border-t border-gray-50 flex items-center justify-between">
                            <div class="flex items-center gap-2">
                                <span class="text-lg font-black text-gray-900 tracking-tighter">{{ course.price || '399.000đ' }}</span>
                                <span class="text-[10px] text-gray-400 line-through font-bold">1.200.000đ</span>
                            </div>
                            <div class="w-8 h-8 rounded-full border-2 border-primary/20 flex items-center justify-center group-hover:bg-primary group-hover:border-primary transition-all">
                                <span class="material-symbols-outlined text-sm font-bold text-primary group-hover:text-white">arrow_forward</span>
                            </div>
                        </div>
                    </div>
                </router-link>
            </div>
            
        </div>
    </div>
  </LayoutStudent>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { apiService } from '@/services/api'
import LayoutStudent from '@/layouts/LayoutStudent.vue'
import ResumeLearningCard from '../components/widgets/ResumeLearningCard.vue'
import StreakWidget from '../components/widgets/StreakWidget.vue'
import StatsOverview from '../components/widgets/StatsOverview.vue'
import RecommendedCarousel from '../components/RecommendedCarousel.vue'

const dashboardData = ref(null)
const isLoading = ref(true)

onMounted(async () => {
    try {
        isLoading.value = true
        dashboardData.value = await apiService.getDashboardData()
    } catch (error) {
        console.error("Dashboard data fetch failed:", error)
    } finally {
        isLoading.value = false
    }
})
</script>

<template>
  <LayoutStudent>
    <main class="max-w-[1440px] mx-auto px-6 lg:px-12 py-10 space-y-16">
        <!-- Welcome Header -->
        <div class="mb-4">
            <h1 class="text-4xl font-black text-gray-900 tracking-tighter uppercase mb-2">
                Chào mừng lại, {{ dashboardData?.user?.name || 'Học viên' }}
            </h1>
            <p class="text-sm font-bold text-gray-400 uppercase tracking-[0.2em]">Sẵn sàng cho những kiến thức mới hôm nay chưa?</p>
        </div>

        <!-- Top Grid: Resume & Streak -->
        <div class="grid grid-cols-1 lg:grid-cols-3 gap-8 mb-16">
            <div class="lg:col-span-2">
                <ResumeLearningCard v-if="dashboardData?.recommendations?.[0]" 
                    :course="dashboardData.recommendations[0]" />
                <div v-else-if="isLoading" class="h-48 bg-gray-100 animate-pulse rounded-2xl border border-gray-100"></div>
            </div>
            <div>
                <StreakWidget v-if="dashboardData" :days="dashboardData.user?.stats?.streak || 0" />
                <div v-else-if="isLoading" class="h-48 bg-gray-900 animate-pulse rounded-2xl"></div>
            </div>
        </div>

        <!-- Stats Section -->
        <div class="mb-16">
            <div class="flex items-center gap-4 mb-6">
                <div class="h-px bg-gray-200 flex-1"></div>
                <span class="text-[10px] font-black text-gray-400 uppercase tracking-[0.3em]">Thống kê học tập</span>
                <div class="h-px bg-gray-200 flex-1"></div>
            </div>
            <StatsOverview :stats="dashboardData?.user?.stats" />
        </div>

        <!-- Recommendations Section -->
        <div class="mb-16">
            <RecommendedCarousel :courses="dashboardData?.recommendations || []" />
        </div>

        <!-- Promotional Banner -->
        <section class="relative h-[300px] rounded-3xl overflow-hidden shadow-2xl group border-4 border-white">
            <div class="absolute inset-0 bg-linear-to-r from-blue-900 via-indigo-900 to-transparent z-10"></div>
            <img alt="Banner" class="absolute inset-0 w-full h-full object-cover group-hover:scale-110 transition-transform duration-[2s]" 
                src="https://images.unsplash.com/photo-1516321318423-f06f85e504b3?ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80"/>
            
            <div class="relative z-20 h-full flex flex-col justify-center px-12 max-w-xl space-y-4">
                <span class="inline-block bg-amber-400 text-white text-[10px] font-black px-3 py-1 rounded-full uppercase tracking-widest self-start">Ưu đãi độc quyền</span>
                <h2 class="text-3xl font-black text-white leading-tight uppercase tracking-tighter">Nâng tầm sự nghiệp với khóa học AI Masterclass</h2>
                <p class="text-blue-100 text-sm font-medium leading-relaxed">Giảm ngay 80% cho học viên mới. Chỉ áp dụng trong 24 giờ tới.</p>
                <button class="bg-white hover:bg-primary hover:text-white text-primary font-black py-3 px-8 rounded-xl transition-all self-start uppercase text-xs tracking-widest shadow-xl">
                    Khám phá ngay
                </button>
            </div>
        </section>
    </main>
  </LayoutStudent>
</template>

<script setup>
import { ref, watch } from 'vue'
import { useEnrolledCoursesStore } from '@/stores/enrolledCourses'
import LayoutStudent from '@/layouts/LayoutStudent.vue'

const store = useEnrolledCoursesStore()

const searchInput = ref('')
let debounceTimer = null

// Debounced search
watch(searchInput, (val) => {
    clearTimeout(debounceTimer)
    debounceTimer = setTimeout(() => {
        store.searchQuery = val
    }, 300)
})

const tabs = [
    { key: 'learning', label: 'Đang học' },
    { key: 'completed', label: 'Đã hoàn thành' },
    { key: 'wishlist', label: 'Wishlist' },
]

const sortOptions = [
    { value: 'recent', label: 'Truy cập gần đây' },
    { value: 'progress', label: 'Tiến độ học tập' },
    { value: 'name', label: 'Tên khóa học (A-Z)' },
]
</script>

<template>
  <LayoutStudent>
    <main class="max-w-[1440px] mx-auto px-6 lg:px-12 py-10 min-h-[70vh]">
        <!-- Header & Tabs -->
        <div class="mb-8 mt-4">
            <h1 class="text-3xl font-extrabold text-on-surface mb-6">Khóa học của tôi</h1>
            
            <div class="flex border-b border-surface-container-highest">
                <button v-for="tab in tabs" :key="tab.key"
                    @click="store.activeTab = tab.key"
                    :class="['flex items-center gap-2 px-6 py-3 font-medium text-sm transition-colors border-b-2',
                             store.activeTab === tab.key 
                                ? 'border-primary text-primary font-bold' 
                                : 'border-transparent text-on-surface-variant hover:text-primary']">
                    {{ tab.label }}
                    <span v-if="store.tabCounts[tab.key] > 0"
                        :class="['text-[10px] font-bold px-1.5 py-0.5 rounded-full leading-tight',
                                 store.activeTab === tab.key ? 'bg-primary text-white' : 'bg-surface-container text-on-surface-variant']">
                        {{ store.tabCounts[tab.key] }}
                    </span>
                </button>
            </div>
        </div>

        <!-- Filters & Search -->
        <div class="flex flex-col md:flex-row justify-between items-center gap-4 mb-8">
            <div class="relative w-full md:w-80">
                <span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-on-surface-variant text-lg">search</span>
                <input v-model="searchInput" type="text" placeholder="Tìm trong khóa học của bạn..." 
                    class="w-full bg-white border border-surface-container-highest rounded-md py-2.5 pl-10 pr-4 text-sm focus:outline-none focus:ring-2 focus:ring-primary focus:border-transparent transition-all">
            </div>
            
            <div class="flex items-center gap-2 w-full md:w-auto">
                <label class="text-sm font-medium text-on-surface-variant whitespace-nowrap">Sắp xếp theo:</label>
                <select v-model="store.sortBy" class="w-full md:w-48 bg-white border border-surface-container-highest rounded-md py-2.5 px-3 text-sm focus:outline-none focus:ring-2 focus:ring-primary focus:border-transparent font-medium cursor-pointer">
                    <option v-for="opt in sortOptions" :key="opt.value" :value="opt.value">{{ opt.label }}</option>
                </select>
            </div>
        </div>

        <!-- Empty State -->
        <div v-if="store.filteredCourses.length === 0" class="flex flex-col items-center justify-center py-24 text-center">
            <div class="w-20 h-20 rounded-full bg-surface-container flex items-center justify-center mb-4">
                <span class="material-symbols-outlined text-4xl text-on-surface-variant">
                    {{ store.activeTab === 'wishlist' ? 'bookmark_border' : store.activeTab === 'completed' ? 'workspace_premium' : 'school' }}
                </span>
            </div>
            <h3 class="text-lg font-bold text-on-surface mb-2">
                {{ store.searchQuery ? 'Không tìm thấy kết quả' : 
                   store.activeTab === 'wishlist' ? 'Chưa có khóa học nào trong wishlist' :
                   store.activeTab === 'completed' ? 'Bạn chưa hoàn thành khóa học nào' :
                   'Bạn chưa đăng ký khóa học nào' }}
            </h3>
            <p class="text-sm text-on-surface-variant mb-6">
                {{ store.searchQuery ? 'Thử tìm kiếm với từ khóa khác' : 'Khám phá hàng nghìn khóa học chất lượng cao' }}
            </p>
            <router-link v-if="!store.searchQuery" to="/" class="bg-primary text-white font-bold px-6 py-3 rounded-lg hover:bg-blue-700 transition-colors">
                Khám phá khóa học
            </router-link>
        </div>

        <!-- Course Grid -->
        <div v-else class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
            <div v-for="course in store.filteredCourses" :key="course.id"
                class="bg-white rounded-xl overflow-hidden shadow-sm border border-surface-container-highest hover:shadow-md hover:border-primary/30 transition-all flex flex-col h-full">
                
                <!-- Thumbnail -->
                <div class="aspect-video relative overflow-hidden bg-surface-container">
                    <img :src="course.thumbnail" :alt="course.title" class="w-full h-full object-cover">
                    <div v-if="course.remainingLessons > 0" class="absolute bottom-2 right-2 bg-black/70 text-white text-[10px] font-bold px-2 py-1 rounded backdrop-blur-sm">
                        Còn {{ course.remainingLessons }} bài học
                    </div>
                    <div v-if="course.progress === 100 " class="absolute top-2 right-2 bg-emerald-500 text-white text-[10px] font-bold px-2 py-1 rounded flex items-center gap-1">
                        <span class="material-symbols-outlined text-[12px]" style="font-variation-settings: 'FILL' 1;">check_circle</span> Hoàn thành
                    </div>
                </div>

                <!-- Card Body -->
                <div class="p-5 flex flex-col flex-1">
                    <h3 class="font-bold text-base text-on-surface line-clamp-2 leading-tight mb-1">{{ course.title }}</h3>
                    <p class="text-sm text-on-surface-variant mb-4">Bởi {{ course.instructor }}</p>
                    
                    <div class="mt-auto space-y-3">
                        <!-- Progress Bar -->
                        <div class="space-y-1.5">
                            <div class="flex justify-between text-xs font-bold text-on-surface">
                                <span>{{ course.progress === 0 ? 'Chưa bắt đầu' : 'Tiến độ' }}</span>
                                <span :class="course.progress === 100 ? 'text-emerald-500' : 'text-primary'">{{ course.progress }}%</span>
                            </div>
                            <div class="h-2 w-full bg-surface-container rounded-full overflow-hidden">
                                <div class="h-full rounded-full transition-all duration-500" 
                                    :class="course.progress === 100 ? 'bg-emerald-500' : 'bg-primary'"
                                    :style="{ width: course.progress + '%' }"></div>
                            </div>
                        </div>
                        
                        <!-- Next Lesson Info -->
                        <div v-if="course.nextLesson" class="text-xs font-medium text-on-surface-variant bg-surface p-2 rounded-md truncate">
                            <span class="text-primary font-bold">{{ course.progress === 0 ? 'Bắt đầu:' : 'Tiếp theo:' }}</span> {{ course.nextLesson }}
                        </div>

                        <!-- Action Button -->
                        <router-link v-if="store.activeTab !== 'wishlist'"
                            :to="course.progress > 0 ? `/learning/${course.id}/progress` : `/learn/${course.id}`"
                            :class="['w-full font-bold py-2.5 rounded-md transition-colors text-sm flex items-center justify-center gap-1',
                                     course.progress === 100 
                                        ? 'bg-emerald-50 text-emerald-700 hover:bg-emerald-100 border border-emerald-200'
                                        : course.progress === 0
                                            ? 'bg-primary-container text-primary hover:bg-primary hover:text-white'
                                            : 'bg-primary hover:bg-blue-700 text-white']">
                            <span class="material-symbols-outlined text-[16px]">
                                {{ course.progress === 100 ? 'workspace_premium' : course.progress === 0 ? 'play_circle' : 'play_arrow' }}
                            </span>
                            {{ course.progress === 100 ? 'Xem chứng chỉ' : course.progress === 0 ? 'Bắt đầu học ngay' : 'Tiếp tục học' }}
                        </router-link>

                        <!-- Wishlist Remove -->
                        <div v-else class="flex gap-2">
                            <router-link :to="`/learn/${course.id}`"
                                class="flex-1 bg-primary text-white font-bold py-2.5 rounded-md text-sm flex items-center justify-center hover:bg-blue-700 transition-colors">
                                Đăng ký học
                            </router-link>
                            <button @click="store.removeFromWishlist(course.id)"
                                class="w-10 h-10 flex items-center justify-center rounded-md border border-surface-container-highest text-on-surface-variant hover:text-error hover:border-error transition-colors" title="Xóa khỏi wishlist">
                                <span class="material-symbols-outlined text-lg">bookmark_remove</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Pagination (static, connect to API later) -->
        <div v-if="store.filteredCourses.length > 0" class="flex justify-center items-center mt-12 gap-2">
            <button class="w-10 h-10 flex items-center justify-center rounded-md border border-surface-container-highest text-on-surface-variant hover:bg-surface hover:text-primary transition-colors disabled:opacity-40" disabled>
                <span class="material-symbols-outlined text-sm">arrow_back_ios_new</span>
            </button>
            <button class="w-10 h-10 flex items-center justify-center rounded-md bg-primary text-white font-bold text-sm">1</button>
            <button class="w-10 h-10 flex items-center justify-center rounded-md border border-surface-container-highest text-on-surface-variant hover:bg-surface hover:text-primary transition-colors font-medium text-sm">2</button>
            <button class="w-10 h-10 flex items-center justify-center rounded-md border border-surface-container-highest text-on-surface-variant hover:bg-surface hover:text-primary transition-colors">
                <span class="material-symbols-outlined text-sm">arrow_forward_ios</span>
            </button>
        </div>
    </main>
  </LayoutStudent>
</template>

<style scoped>
.line-clamp-2 {
    display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    overflow: hidden;
}
</style>

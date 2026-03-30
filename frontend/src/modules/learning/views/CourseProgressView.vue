<script setup>
import { computed, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useEnrolledCoursesStore } from '@/stores/enrolledCourses'
import CourseProgressHero from '../components/progress/CourseProgressHero.vue'
import ProgressStatRow from '../components/progress/ProgressStatRow.vue'
import ChapterAccordion from '../components/shared/ChapterAccordion.vue'
import CertificateUnlockCard from '../components/progress/CertificateUnlockCard.vue'

const route = useRoute()
const store = useEnrolledCoursesStore()

// Fetch course by route param :id, fall back to default
const course = computed(() => store.getCourseById(route.params.id) || {
    title: "Mastering Vue 3: The Complete Composition API",
    author: "Maximilian Schwarzmüller",
    thumbnail: "https://images.unsplash.com/photo-1555066931-4365d14bab8c?q=80&w=300&auto=format&fit=crop",
    progress: 65,
    completedLessons: 45,
    totalLessons: 70
})

const progressNeeded = computed(() => Math.max(0, 100 - (course.value?.progress ?? 0)))

const statsData = computed(() => [
    { id: 1, label: 'Thời gian đã học', value: '12h 45m', icon: 'schedule' },
    { id: 2, label: 'Điểm Quiz (TB)', value: '8.5', unit: '/10', icon: 'grading' },
    { id: 3, label: 'Bài đã hoàn thành', value: String(course.value?.completedLessons ?? 0), unit: `/${course.value?.totalLessons ?? 0}`, icon: 'task_alt' }
])

const chaptersData = [
    {
        id: 1,
        title: 'Chương 1: Giới thiệu & Cài đặt',
        status: 'Đã hoàn thành (3/3)',
        progress: 100,
        statusColor: 'text-success',
        progressColor: 'bg-emerald-500',
        expanded: false,
        lessons: [
            { id: 101, title: '11. Thiết lập Project với Vite', duration: '15:20', completed: true },
            { id: 102, title: '12. Reactivity với ref() và reactive()', duration: '28:30', active: true },
            { id: 103, title: '13. Bài kiểm tra Reactivity', quiz: '10 câu', quizLabel: 'Cần làm quiz' },
            { id: 104, title: '14. Computed Properties chuyên sâu', duration: '22:15', locked: true }
        ]
    },
    {
        id: 2,
        title: 'Chương 2: Reactivity Cốt lõi',
        status: 'Đang tiến hành (1/4)',
        progress: 25,
        statusColor: 'text-primary',
        progressColor: 'bg-primary',
        expanded: true,
        lessons: [
            { id: 201, title: '11. Thiết lập Project với Vite', duration: '15:20', completed: true },
            { id: 202, title: '12. Reactivity với ref() và reactive()', duration: '28:20', active: true },
            { id: 203, title: '13. Bài kiểm tra Reactivity', quiz: '10 câu', quizLabel: 'Cần làm bài' },
            { id: 204, title: '14. Computed Properties chuyên sâu', duration: '22:15', locked: true }
        ]
    },
    {
        id: 3,
        title: 'Chương 3: Lifecycle Hooks',
        status: '0/5 bài học',
        progress: 0,
        statusColor: 'text-on-surface-variant',
        progressColor: 'bg-surface-container',
        expanded: false,
        locked: true,
        lessons: []
    }
]

const activeTab = ref('hoidap')
</script>

<template>
  <div class="min-h-screen bg-[#F8F9FA] font-plus-jakarta antialiased pb-20">
    <!-- Clean Header -->
    <header class="bg-white border-b border-surface-container-highest flex items-center justify-between px-6 py-4 sticky top-0 z-50 shadow-sm">
        <div class="flex items-center gap-4">
            <router-link to="/student/dashboard" class="flex items-center gap-2 text-sm font-bold text-gray-900 hover:text-primary transition-colors">
                <span class="material-symbols-outlined text-base">arrow_back</span> Dashboard
            </router-link>
            <div class="w-px h-5 bg-surface-container-highest"></div>
            <h1 class="text-base font-bold text-gray-900 hidden md:block">
                Bảng tiến độ: {{ course?.title || 'Mastering Vue 3: The Complete Composition API' }}
            </h1>
        </div>
        <div class="flex items-center gap-4">
            <div class="w-9 h-9 rounded-full overflow-hidden border border-surface-container shadow-sm">
                <img src="https://i.pravatar.cc/100?img=33" alt="Avatar" class="w-full h-full object-cover">
            </div>
        </div>
    </header>

    <main class="max-w-[1240px] mx-auto px-4 md:px-6 py-8">
        <div class="grid grid-cols-1 lg:grid-cols-12 gap-8">
            <!-- Left Column Content -->
            <div class="lg:col-span-8 space-y-6">
                <!-- Top Overview -->
                <CourseProgressHero :course="course" />
                <ProgressStatRow :stats="statsData" />
                <ChapterAccordion :chapters="chaptersData" />

                <!-- Tabs Card -->
                <div class="bg-white rounded-xl border border-surface-container-highest shadow-sm overflow-hidden mt-8">
                    <div class="flex items-center border-b border-surface-container-highest px-6 pt-2 gap-4">
                        <button 
                            @click="activeTab = 'hoidap'"
                            :class="['py-3 px-2 font-bold text-sm border-b-2 transition-colors -mb-[1px]', activeTab === 'hoidap' ? 'border-primary text-primary' : 'border-transparent text-on-surface-variant hover:text-gray-900']"
                        >
                            Hỏi đáp
                        </button>
                        <button 
                            @click="activeTab = 'danhgia'"
                            :class="['py-3 px-2 font-bold text-sm border-b-2 transition-colors -mb-[1px]', activeTab === 'danhgia' ? 'border-primary text-primary' : 'border-transparent text-on-surface-variant hover:text-gray-900']"
                        >
                            Đánh giá
                        </button>
                    </div>

                    <!-- Hỏi đáp Content -->
                    <div v-show="activeTab === 'hoidap'" class="p-6">
                        <div class="flex items-center justify-between mb-6">
                            <h3 class="font-bold text-lg text-gray-900">Cộng đồng hỏi đáp</h3>
                            <div class="flex items-center gap-3">
                                <div class="relative w-64">
                                    <span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-on-surface-variant text-sm">search</span>
                                    <input type="text" placeholder="Tìm kiếm câu hỏi..." class="w-full text-sm bg-surface-container-low border border-surface-container-highest rounded-md py-2 pl-9 pr-3 focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary">
                                </div>
                                <select class="text-sm border border-surface-container-highest bg-surface-container-low rounded-md py-2 px-3 focus:outline-none focus:border-primary">
                                    <option>Tất cả bài giảng</option>
                                    <option>Bài 12</option>
                                </select>
                            </div>
                        </div>

                        <!-- Input Area -->
                        <div class="flex gap-4 bg-surface-container-lowest p-5 rounded-lg border border-surface-container mb-8">
                            <img src="https://i.pravatar.cc/100?img=33" alt="Avatar" class="w-10 h-10 rounded-full object-cover shrink-0">
                            <div class="flex-1">
                                <textarea class="w-full text-sm border border-surface-container-highest rounded-md p-3 mb-3 min-h-[80px] focus:outline-none focus:ring-1 focus:ring-primary focus:border-primary" placeholder="Bạn có thắc mắc gì về bài học này không?"></textarea>
                                <div class="flex items-center justify-between">
                                    <select class="text-sm border border-surface-container-highest rounded-md py-2 px-3 focus:outline-none focus:border-primary">
                                        <option>Chọn bài giảng đính kèm</option>
                                    </select>
                                    <button class="bg-primary hover:bg-blue-700 text-white font-bold py-2 px-6 rounded-md text-sm transition-colors flex items-center gap-2">
                                        <span class="material-symbols-outlined text-sm">send</span> Gửi câu hỏi
                                    </button>
                                </div>
                            </div>
                        </div>

                        <!-- Comment List -->
                        <div class="space-y-6">
                            <!-- Comment Item -->
                            <div class="flex gap-4">
                                <img src="https://i.pravatar.cc/100?img=12" alt="Avatar" class="w-10 h-10 rounded-full object-cover shrink-0">
                                <div class="flex-1">
                                    <div class="flex items-center justify-between mb-1">
                                        <div class="flex items-center gap-2">
                                            <span class="font-bold text-sm text-gray-900">Nguyễn Văn A</span>
                                            <span class="text-xs text-on-surface-variant">5 phút trước</span>
                                        </div>
                                        <button class="text-on-surface-variant hover:text-gray-900">
                                            <span class="material-symbols-outlined text-sm">more_vert</span>
                                        </button>
                                    </div>
                                    <span class="inline-block bg-primary/10 text-primary text-[10px] font-bold px-2 py-1 rounded mb-2 uppercase tracking-wide">
                                        BÀI 12. REACTIVITY VỚI REF() VÀ REACTIVE()
                                    </span>
                                    <p class="text-sm text-gray-800 leading-relaxed mb-3">
                                        Thầy cho em hỏi... trong mô hình này nếu áp dụng vào doanh nghiệp nhỏ thì có bị cồng kềnh quá không ạ?
                                    </p>
                                    <div class="flex items-center gap-4 mb-4">
                                        <button class="flex items-center gap-1 text-xs font-bold text-gray-600 hover:text-primary transition-colors">
                                            <span class="material-symbols-outlined text-[15px] filled">thumb_up</span> 12 THÍCH
                                        </button>
                                        <button class="flex items-center gap-1 text-xs font-bold text-gray-600 hover:text-primary transition-colors">
                                            <span class="material-symbols-outlined text-[15px]">chat_bubble</span> 2 TRẢ LỜI
                                        </button>
                                    </div>

                                    <!-- Replies -->
                                    <div class="bg-surface-container-low rounded-lg p-4 space-y-4 border border-surface-container-highest/50">
                                        <!-- Reply 1 -->
                                        <div class="flex gap-3">
                                            <img src="https://i.pravatar.cc/100?img=59" alt="Avatar" class="w-8 h-8 rounded-full object-cover shrink-0 ring-2 ring-primary/20">
                                            <div class="flex-1">
                                                <div class="flex items-center justify-between mb-1">
                                                    <div class="flex items-center gap-2">
                                                        <span class="font-bold text-sm text-gray-900">Trần Bình</span>
                                                        <span class="bg-primary text-white text-[10px] font-bold px-1.5 py-0.5 rounded">GIẢNG VIÊN</span>
                                                    </div>
                                                    <div class="flex gap-1 items-center text-[10px] text-error font-bold">
                                                        <span class="material-symbols-outlined text-sm">push_pin</span> ĐÃ GHIM
                                                    </div>
                                                </div>
                                                <p class="text-sm text-gray-800 leading-relaxed mb-2">
                                                    Câu hỏi rất hay! Với doanh nghiệp nhỏ, em có thể lược bỏ bước 2 và 3 trong quy trình để tối ưu nhé.
                                                </p>
                                                <div class="flex items-center gap-3 text-xs text-on-surface-variant">
                                                    <span>1 GIỜ TRƯỚC</span>
                                                    <button class="font-bold hover:text-gray-900">Thích</button>
                                                    <button class="font-bold hover:text-gray-900">Phản hồi</button>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Reply 2 -->
                                        <div class="flex gap-3">
                                            <img src="https://i.pravatar.cc/100?img=5" alt="Avatar" class="w-8 h-8 rounded-full object-cover shrink-0">
                                            <div class="flex-1">
                                                <div class="flex items-center gap-2 mb-1">
                                                    <span class="font-bold text-sm text-gray-900">Lê Thị C</span>
                                                </div>
                                                <p class="text-sm text-gray-800 leading-relaxed mb-2">
                                                    Mình cũng thắc mắc đoạn này, cảm ơn thầy đã giải đáp.
                                                </p>
                                                <div class="flex items-center gap-3 text-xs text-on-surface-variant">
                                                    <span>30 PHÚT TRƯỚC</span>
                                                    <button class="font-bold hover:text-gray-900">Thích</button>
                                                    <button class="font-bold hover:text-gray-900">Phản hồi</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Pagination block -->
                                    <div class="flex justify-center mt-6">
                                        <div class="flex items-center gap-1 border border-surface-container-highest rounded-md overflow-hidden bg-white">
                                            <button class="px-3 py-1.5 text-sm text-on-surface-variant hover:bg-surface-container-low transition-colors"><span class="material-symbols-outlined text-sm">chevron_left</span></button>
                                            <button class="px-3 py-1.5 text-sm font-bold bg-primary text-white">1</button>
                                            <button class="px-3 py-1.5 text-sm font-bold text-gray-700 hover:bg-surface-container-low transition-colors">2</button>
                                            <button class="px-3 py-1.5 text-sm font-bold text-gray-700 hover:bg-surface-container-low transition-colors">3</button>
                                            <button class="px-3 py-1.5 text-sm text-on-surface-variant hover:bg-surface-container-low transition-colors"><span class="material-symbols-outlined text-sm">chevron_right</span></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Đánh giá khóa học Card -->
                <div class="bg-white rounded-xl border border-surface-container-highest shadow-sm overflow-hidden p-6 mt-6">
                    <div class="flex items-center justify-between mb-6 border-b border-surface-container-highest pb-4">
                        <h3 class="font-bold text-lg text-gray-900">Đánh giá khóa học</h3>
                        <button class="flex items-center gap-1 text-xs font-bold text-primary bg-primary/10 px-3 py-1.5 rounded-full">
                            <span class="material-symbols-outlined text-sm">info</span> ĐÃ ĐÁNH GIÁ
                        </button>
                    </div>

                    <div class="bg-surface-container-lowest rounded-lg border border-surface-container-highest p-6 mb-6 text-center">
                        <span class="text-xs font-bold text-on-surface-variant tracking-wider uppercase block mb-3">Chất lượng khóa học</span>
                        <div class="flex items-center justify-center gap-2">
                            <span class="material-symbols-outlined text-3xl text-amber-400" style="font-variation-settings: 'FILL' 1;">star</span>
                            <span class="material-symbols-outlined text-3xl text-amber-400" style="font-variation-settings: 'FILL' 1;">star</span>
                            <span class="material-symbols-outlined text-3xl text-amber-400" style="font-variation-settings: 'FILL' 1;">star</span>
                            <span class="material-symbols-outlined text-3xl text-amber-400" style="font-variation-settings: 'FILL' 1;">star</span>
                            <span class="material-symbols-outlined text-3xl text-surface-container-highest" style="font-variation-settings: 'FILL' 1;">star</span>
                        </div>
                    </div>

                    <div class="border border-surface-container-highest rounded-lg overflow-hidden relative">
                        <textarea class="w-full text-sm p-4 min-h-[120px] focus:outline-none resize-none" placeholder="Chia sẻ cảm nhận của bạn về nội dung và giảng viên..."></textarea>
                        <div class="flex items-center justify-between bg-surface-container-low px-4 py-3 border-t border-surface-container-highest">
                            <button class="flex items-center gap-1 text-xs font-medium text-on-surface-variant hover:text-gray-900 transition-colors">
                                <span class="material-symbols-outlined text-sm">image</span> Đính kèm ảnh
                            </button>
                            <button class="bg-primary hover:bg-blue-700 text-white font-bold py-2 px-6 rounded-md text-sm transition-colors">
                                Gửi đánh giá
                            </button>
                        </div>
                    </div>
                </div>

                <!-- Đánh giá từ cộng đồng Card -->
                <div class="mt-8">
                    <div class="flex items-center justify-between mb-6">
                        <h3 class="font-bold text-lg text-gray-900">Đánh giá từ cộng đồng</h3>
                        <div class="flex items-center gap-2 text-sm">
                            <span class="material-symbols-outlined text-amber-400" style="font-variation-settings: 'FILL' 1;">star</span>
                            <span class="font-bold text-gray-900">4.8</span>
                            <span class="text-on-surface-variant">(124 đánh giá)</span>
                        </div>
                    </div>

                    <!-- Review Item -->
                    <div class="bg-white rounded-xl border border-surface-container-highest shadow-sm p-6 mb-4 flex gap-4">
                        <img src="https://i.pravatar.cc/100?img=47" alt="Avatar" class="w-12 h-12 rounded-full object-cover shrink-0">
                        <div>
                            <div class="flex items-start justify-between mb-1">
                                <div>
                                    <h4 class="font-bold text-sm text-gray-900">Nguyễn Thu Hà</h4>
                                    <div class="flex items-center gap-0.5 mt-1">
                                        <span class="material-symbols-outlined text-xs text-amber-400" style="font-variation-settings: 'FILL' 1;">star</span>
                                        <span class="material-symbols-outlined text-xs text-amber-400" style="font-variation-settings: 'FILL' 1;">star</span>
                                        <span class="material-symbols-outlined text-xs text-amber-400" style="font-variation-settings: 'FILL' 1;">star</span>
                                        <span class="material-symbols-outlined text-xs text-amber-400" style="font-variation-settings: 'FILL' 1;">star</span>
                                        <span class="material-symbols-outlined text-xs text-amber-400" style="font-variation-settings: 'FILL' 1;">star</span>
                                    </div>
                                </div>
                                <span class="text-xs text-on-surface-variant">2 NGÀY TRƯỚC</span>
                            </div>
                            <p class="text-sm text-gray-800 leading-relaxed mt-3 mb-3">
                                Khóa học rất chi tiết và dễ hiểu. Giảng viên giải thích cặn kẽ các khái niệm khó. Tôi đã áp dụng được ngay vào công việc thực tế của mình. Cảm ơn hệ thống rất nhiều!
                            </p>
                            <!-- Attached image -->
                            <div class="w-16 h-16 rounded-md overflow-hidden border border-surface-container shadow-sm cursor-pointer hover:opacity-90">
                                <img src="https://images.unsplash.com/photo-1555066931-4365d14bab8c?q=80&w=150&auto=format&fit=crop" class="w-full h-full object-cover">
                            </div>
                        </div>
                    </div>

                    <div class="flex justify-center mt-6">
                        <button class="bg-white border border-surface-container-highest hover:bg-surface-container-low text-gray-900 font-bold py-2.5 px-8 rounded-full text-sm transition-colors shadow-sm">
                            XEM THÊM ĐÁNH GIÁ
                        </button>
                    </div>
                </div>

            </div>
            
            <!-- Right Column Sticky -->
            <div class="lg:col-span-4">
                <CertificateUnlockCard :progressNeeded="progressNeeded" :courseId="route.params.id" class="sticky top-24" />
            </div>
        </div>
    </main>

    <!-- Simple Footer (optional, but good for completeness as spacing at bottom is visible) -->
    <footer class="border-t border-surface-container-highest mt-20 bg-white">
        <div class="max-w-[1240px] mx-auto px-6 py-6 flex flex-col md:flex-row items-center justify-between text-xs font-bold text-on-surface-variant gap-4">
            <p>© 2026 AURA LEARNING SYSTEM. ALL RIGHTS RESERVED.</p>
            <div class="flex items-center gap-6">
                <a href="#" class="hover:text-primary transition-colors">PRIVACY POLICY</a>
                <a href="#" class="hover:text-primary transition-colors">TERMS OF SERVICE</a>
                <a href="#" class="hover:text-primary transition-colors">HELP CENTER</a>
            </div>
        </div>
    </footer>
  </div>
</template>

<style scoped>
/* Scoped styles if needed */
.material-symbols-outlined.filled {
  font-variation-settings: 'FILL' 1;
}
</style>

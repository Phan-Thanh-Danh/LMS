<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { useEnrolledCoursesStore } from '@/stores/enrolledCourses'
import LayoutStudent from '@/layouts/LayoutStudent.vue'
import CourseProgressHero from '../components/progress/CourseProgressHero.vue'
import ProgressStatRow from '../components/progress/ProgressStatRow.vue'
import ChapterAccordion from '../components/shared/ChapterAccordion.vue'
import CertificateUnlockCard from '../components/progress/CertificateUnlockCard.vue'

const route = useRoute()
const store = useEnrolledCoursesStore()

// Fetch course by route param :id, fall back to first course
const course = computed(() => store.getCourseById(route.params.id) || store.allCourses[0])

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
            { id: 101, title: '1.1 Lời mở đầu khóa học', duration: '02:15', completed: true },
            { id: 102, title: '1.2 Cài đặt môi trường Node.js', duration: '05:40', completed: true },
            { id: 103, title: '1.3 Tạo dự án đầu tiên với Vue CLI', duration: '12:20', completed: true }
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
            { id: 201, title: '2.1 Thiết lập Project với Vite', duration: '15:20', completed: true },
            { id: 202, title: '2.2 Reactivity với ref() và reactive()', duration: '28:30', active: true },
            { id: 203, title: '2.3 Bài kiểm tra: Reactivity', quiz: '10 câu', quizLabel: 'Cần làm quiz' },
            { id: 204, title: '2.4 Computed Properties chuyên sâu', duration: '22:15', locked: true }
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
</script>

<template>
  <LayoutStudent>
    <main class="max-w-[1440px] mx-auto px-4 md:px-6 py-8">
        <div class="flex items-center gap-4 mb-8">
            <router-link to="/learning" class="text-on-surface-variant hover:text-primary transition-colors flex items-center gap-1 text-sm font-bold">
                <span class="material-symbols-outlined text-base">arrow_back</span> Khóa học của tôi
            </router-link>
            <div class="w-px h-4 bg-surface-container-highest"></div>
            <h1 class="text-base font-bold text-gray-900 truncate hidden md:block">
                Bảng tiến độ: {{ course?.title }}
            </h1>
        </div>

        <div class="grid grid-cols-1 lg:grid-cols-12 gap-8">
            <div class="lg:col-span-8 space-y-6">
                <CourseProgressHero :course="course" />
                <ProgressStatRow :stats="statsData" />
                <ChapterAccordion :chapters="chaptersData" />
            </div>
            <div class="lg:col-span-4">
                <CertificateUnlockCard :progressNeeded="progressNeeded" :courseId="route.params.id" />
            </div>
        </div>
    </main>
  </LayoutStudent>
</template>

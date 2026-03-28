<script setup>
import { ref, computed, watch, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { apiService } from '@/services/api'
import { useHeartbeat } from '@/composables/useHeartbeat'
import LearningLayout from '../layouts/LearningLayout.vue'
import VideoPlayer from '../components/learn/VideoPlayer.vue'
import LessonSidebar from '../components/learn/LessonSidebar.vue'
import QuizLesson from '../components/learn/QuizLesson.vue'

const router = useRouter()
const route = useRoute()

const courseId = computed(() => Number(route.params.id) || 101)
const lessonId = computed(() => Number(route.params.lessonId) || Number(route.params.quizId) || 12)

const courseData = ref(null)
const activeLesson = ref(null)
const isLoading = ref(true)

const fetchLessonData = async () => {
    try {
        isLoading.value = true
        const data = await apiService.getLessonDetail(courseId.value, lessonId.value)
        courseData.value = data
        activeLesson.value = data.currentLesson
    } catch (err) {
        console.error("Failed to fetch lesson data", err)
    } finally {
        isLoading.value = false
    }
}

// Flat list for navigation
const allLessons = computed(() => {
    if (!courseData.value) return []
    return courseData.value.chapters.flatMap(ch => ch.lessons)
})

const currentIndex = computed(() => allLessons.value.findIndex(l => l.id === activeLesson.value?.id))

onMounted(fetchLessonData)

// Sync with route changes
watch(() => [route.params.lessonId, route.params.quizId], () => {
    fetchLessonData()
})

// Heartbeat for persistence
useHeartbeat({ 
    courseId: courseId.value, 
    lessonId: computed(() => activeLesson.value?.id),
    type: 'learning' 
})

const handleSwitchLesson = (lesson) => {
    if (lesson.type === 'quiz') {
        router.push(`/learn/${courseId.value}/quiz/${lesson.id}`)
    } else {
        router.push(`/learn/${courseId.value}/${lesson.id}`)
    }
}

const handlePrevLesson = () => {
    if (currentIndex.value > 0) {
        handleSwitchLesson(allLessons.value[currentIndex.value - 1])
    }
}

const handleNextLesson = () => {
    if (currentIndex.value < allLessons.value.length - 1) {
        handleSwitchLesson(allLessons.value[currentIndex.value + 1])
    }
}

const handleMarkComplete = (isCompleted) => {
    console.log(`[STU-03] Lesson ${activeLesson.value?.id} complete: ${isCompleted}`)
}
</script>

<template>
  <LearningLayout>
    <template #title>
        <div v-if="isLoading" class="h-6 w-64 bg-white/20 animate-pulse rounded"></div>
        <span v-else>{{ courseData?.courseTitle }}</span>
    </template>
    <template #progress>85%</template> <!-- Mock for now -->

    <template #content>
        <div v-if="isLoading" class="flex items-center justify-center h-full">
            <span class="material-symbols-outlined text-4xl animate-spin text-primary">progress_activity</span>
        </div>
        <Transition v-else name="fade" mode="out-in">
            <VideoPlayer v-if="activeLesson?.type === 'video'" :key="'video-' + activeLesson.id"
                :lesson="activeLesson"
                @prev-lesson="handlePrevLesson"
                @next-lesson="handleNextLesson"
                @mark-complete="handleMarkComplete" />
            <QuizLesson v-else-if="activeLesson?.type === 'quiz'" :key="'quiz-' + activeLesson.id" 
                :course-id="courseId"
                :lesson-id="activeLesson.quizId"
                @next-lesson="handleNextLesson" />
        </Transition>
    </template>

    <template #sidebar>
        <LessonSidebar 
            :chapters="courseData?.chapters || []" 
            :active-lesson-id="activeLesson?.id"
            @switch-lesson="handleSwitchLesson" />
    </template>
  </LearningLayout>
</template>

<style scoped>
.fade-enter-active, .fade-leave-active { transition: opacity 0.25s ease; }
.fade-enter-from, .fade-leave-to { opacity: 0; }
</style>

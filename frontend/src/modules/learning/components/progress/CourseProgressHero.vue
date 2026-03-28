<script setup>
defineProps({
    course: {
        type: Object,
        default: () => ({
            title: "Mastering Vue 3: The Complete Composition API",
            author: "Maximillian Schwarzmüller",
            thumbnail: "https://images.unsplash.com/photo-1555066931-4365d14bab8c?q=80&w=300&auto=format&fit=crop",
            progress: 65
        })
    }
})
</script>

<template>
    <div class="bg-white rounded-xl p-6 border border-surface-container-highest shadow-sm flex flex-col sm:flex-row items-center gap-6 justify-between">
        <div class="flex items-center gap-5">
            <div class="w-24 h-16 md:w-32 md:h-20 rounded-md overflow-hidden shrink-0 bg-surface-container">
                <img :src="course.thumbnail" alt="Course Thumbnail" class="w-full h-full object-cover">
            </div>
            <div>
                <span class="text-xs font-bold text-primary uppercase tracking-wider mb-1 block">Khóa học đang theo học</span>
                <h2 class="text-xl font-extrabold text-gray-900 leading-tight">{{ course.title }}</h2>
                <p class="text-sm text-on-surface-variant mt-1">Bởi {{ course.author }}</p>
            </div>
        </div>
        
        <div class="shrink-0 flex items-center justify-center relative w-24 h-24">
            <svg class="w-full h-full" viewBox="0 0 100 100">
                <circle class="text-surface-container stroke-current" stroke-width="8" cx="50" cy="50" r="40" fill="transparent"></circle>
                <circle class="text-primary stroke-current progress-ring__circle" stroke-width="8" stroke-linecap="round" cx="50" cy="50" r="40" fill="transparent" stroke-dasharray="251.2" :stroke-dashoffset="251.2 - (251.2 * course.progress) / 100"></circle>
            </svg>
            <div class="absolute inset-0 flex flex-col items-center justify-center">
                <span class="text-xl font-mono font-bold text-gray-900">{{ course.progress }}%</span>
            </div>
        </div>
    </div>
</template>

<style scoped>
.progress-ring__circle {
    transition: stroke-dashoffset 1s ease-in-out;
    transform: rotate(-90deg);
    transform-origin: 50% 50%;
}
</style>

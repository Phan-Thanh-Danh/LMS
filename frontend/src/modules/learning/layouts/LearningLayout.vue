<script setup>
import { useRouter } from 'vue-router'

const router = useRouter()

const goBack = () => {
    // If we have a course ID, we might want to go to course progress, 
    // but for now, we'll just go back or to the learning dashboard.
    router.back()
}
</script>

<template>
  <div class="learning-layout bg-surface text-on-surface h-screen flex flex-col overflow-hidden font-plus-jakarta">
    <!-- Header (TopbarMini) -->
    <header class="h-16 shrink-0 bg-white border-b border-surface-container-highest flex items-center justify-between px-4 lg:px-6 z-20">
        <div class="flex items-center gap-4 w-full md:w-auto overflow-hidden">
            <button @click="goBack" class="flex items-center justify-center text-on-surface-variant hover:text-primary transition-colors hover:bg-surface-container p-2 rounded-full" title="Quay lại">
                <span class="material-symbols-outlined">arrow_back</span>
            </button>
            <div class="font-extrabold text-xl tracking-tighter text-primary shrink-0 hidden md:block">AET LMS</div>
            <div class="w-px h-6 bg-surface-container-highest hidden md:block"></div>
            <h1 class="font-bold text-gray-900 text-sm md:text-base truncate">
                <slot name="title">Mastering Vue 3: The Complete Composition API</slot>
            </h1>
        </div>
        
        <div class="hidden lg:flex items-center gap-4 shrink-0">
            <button class="flex items-center gap-2 text-sm font-medium text-on-surface-variant hover:text-primary transition-colors px-3 py-1.5 rounded-md hover:bg-surface-container-low">
                <span class="material-symbols-outlined text-lg">star</span>
                Đánh giá khóa học
            </button>
            <div class="flex items-center gap-2 text-sm font-bold bg-surface-container-low px-3 py-1.5 rounded-full">
                <span class="text-primary"><slot name="progress">65%</slot></span>
                <span class="text-on-surface-variant font-medium">hoàn thành</span>
            </div>
        </div>
    </header>

    <!-- Main Content Area (Flex layout chia cột) -->
    <main class="flex-1 flex flex-col lg:flex-row overflow-hidden">
        <div class="flex-1 flex flex-col bg-gray-900 relative">
            <slot name="content" />
        </div>
        <aside class="w-full lg:w-[400px] shrink-0 bg-white border-l border-surface-container-highest flex flex-col h-[50vh] lg:h-auto z-10">
            <slot name="sidebar" />
        </aside>
    </main>
  </div>
</template>

<style scoped>
/* Scrollbar custom for the layout */
::-webkit-scrollbar { width: 6px; }
::-webkit-scrollbar-track { background: transparent; }
::-webkit-scrollbar-thumb { background: #d1d5db; border-radius: 10px; }
::-webkit-scrollbar-thumb:hover { background: #9ca3af; }
</style>

<script setup>
import { ref } from 'vue'

const props = defineProps(['lesson'])
const emit = defineEmits(['mark-complete', 'prev-lesson', 'next-lesson'])

const isCompleted = ref(false)
const isPlaying = ref(false)
const progress = ref(45) // percent through video

const togglePlay = () => {
    isPlaying.value = !isPlaying.value
}

const markComplete = () => {
    isCompleted.value = !isCompleted.value
    emit('mark-complete', isCompleted.value)
}
</script>

<template>
  <div class="flex-1 flex flex-col bg-gray-900">
    <!-- Video Viewport -->
    <div class="flex-1 relative bg-black flex items-center justify-center overflow-hidden group cursor-pointer" @click="togglePlay">
        <img src="https://images.unsplash.com/photo-1555066931-4365d14bab8c?q=80&w=1200&auto=format&fit=crop" alt="Video Frame" class="w-full h-full object-contain opacity-80">
        
        <!-- Play/Pause Center Button -->
        <Transition name="pop">
            <button v-show="!isPlaying"
                class="absolute inset-0 m-auto w-20 h-20 bg-primary/90 text-white rounded-full flex items-center justify-center hover:bg-primary transition-all shadow-lg hover:scale-105 pointer-events-none">
                <span class="material-symbols-outlined text-4xl" style="font-variation-settings: 'FILL' 1;">play_arrow</span>
            </button>
        </Transition>

        <!-- Overlay Controls (hover) -->
        <div class="absolute bottom-0 left-0 right-0 bg-gradient-to-t from-black/90 via-black/50 to-transparent pt-12 pb-4 px-4 opacity-0 group-hover:opacity-100 transition-opacity duration-300" @click.stop>
            <!-- Seekbar -->
            <div class="w-full flex items-center gap-3 mb-3">
                <span class="text-white text-xs font-mono">12:45</span>
                <div class="flex-1 h-1.5 bg-white/30 rounded-full relative hover:h-2.5 transition-all cursor-pointer group/seek">
                    <div class="absolute top-0 left-0 h-full bg-primary rounded-full" :style="{ width: progress + '%' }"></div>
                    <div class="absolute top-1/2 -translate-y-1/2 w-3.5 h-3.5 bg-white rounded-full shadow opacity-0 group-hover/seek:opacity-100 -translate-x-1/2" :style="{ left: progress + '%' }"></div>
                </div>
                <span class="text-white text-xs font-mono">28:30</span>
            </div>

            <!-- Playback Controls -->
            <div class="flex items-center justify-between text-white">
                <div class="flex items-center gap-4">
                    <button @click="togglePlay" class="hover:text-primary transition-colors">
                        <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">{{ isPlaying ? 'pause' : 'play_arrow' }}</span>
                    </button>
                    <button class="hover:text-primary transition-colors">
                        <span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">volume_up</span>
                    </button>
                    <span class="text-sm font-medium border-l border-white/20 pl-4 hidden md:block truncate max-w-[280px]">
                        {{ lesson?.title }}
                    </span>
                </div>
                <div class="flex items-center gap-3">
                    <button class="text-xs font-bold hover:text-primary transition-colors px-1.5 py-0.5 rounded bg-white/10 hover:bg-white/20">1x</button>
                    <button class="hover:text-primary transition-colors"><span class="material-symbols-outlined text-[20px]">closed_caption</span></button>
                    <button class="hover:text-primary transition-colors"><span class="material-symbols-outlined text-[20px]">settings</span></button>
                    <button class="hover:text-primary transition-colors"><span class="material-symbols-outlined text-[20px]">fullscreen</span></button>
                </div>
            </div>
        </div>
    </div>

    <!-- Bottom Action Bar -->
    <div class="h-[72px] shrink-0 bg-white border-t border-surface-container-highest px-4 md:px-6 flex items-center justify-between z-10">
        <!-- Mark Complete Checkbox -->
        <label @click="markComplete" class="flex items-center gap-3 cursor-pointer group select-none">
            <div :class="['w-6 h-6 rounded border-2 flex items-center justify-center transition-all',
                          isCompleted ? 'bg-success border-success' : 'border-surface-container-highest group-hover:border-primary']">
                <span v-if="isCompleted" class="material-symbols-outlined text-white text-[14px]" style="font-variation-settings: 'FILL' 1;">check</span>
            </div>
            <span :class="['text-sm font-bold transition-colors hidden sm:block',
                           isCompleted ? 'text-success' : 'text-on-surface-variant group-hover:text-on-surface']">
                {{ isCompleted ? 'Đã hoàn thành' : 'Đánh dấu hoàn thành' }}
            </span>
        </label>

        <!-- Prev / Next Navigation -->
        <div class="flex items-center gap-2 md:gap-3">
            <button @click="emit('prev-lesson')" class="flex items-center gap-1 md:gap-2 px-3 md:px-4 py-2 text-sm font-bold text-on-surface hover:bg-surface-container rounded-md transition-colors border border-surface-container-highest">
                <span class="material-symbols-outlined text-lg">chevron_left</span>
                <span class="hidden sm:inline">Bài trước</span>
            </button>
            <button @click="emit('next-lesson')" class="flex items-center gap-1 md:gap-2 px-3 md:px-4 py-2 text-sm font-bold text-white bg-primary hover:bg-blue-700 rounded-md transition-colors shadow-sm shadow-primary/20">
                <span class="hidden sm:inline">Bài tiếp theo</span>
                <span class="material-symbols-outlined text-lg">chevron_right</span>
            </button>
        </div>
    </div>
  </div>
</template>

<style scoped>
.pop-enter-active, .pop-leave-active { transition: all 0.2s ease; }
.pop-enter-from, .pop-leave-to { opacity: 0; transform: translate(-50%, -50%) scale(0.8); }
</style>

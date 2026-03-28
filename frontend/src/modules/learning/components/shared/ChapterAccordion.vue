<script setup>
import { ref } from 'vue'

const props = defineProps({
    chapters: {
        type: Array,
        default: () => [
            {
                id: 1,
                title: 'Chương 1: Giới thiệu & Cài đặt',
                status: 'Đã hoàn thành (3/3)',
                progress: 100,
                statusColor: 'text-success',
                progressColor: 'bg-success',
                expanded: false,
                lessons: [
                    { id: 11, title: '11. Thiết lập Project với Vite', duration: '15:20', completed: true },
                    { id: 12, title: '12. Reactivity với ref() và reactive()', duration: '28:30', active: true },
                    { id: 13, title: '13. Bài kiểm tra: Reactivity', quiz: '10 câu', quizLabel: 'Cần làm quiz' }
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
                    { id: 11, title: '11. Thiết lập Project với Vite', duration: '15:20', completed: true },
                    { id: 12, title: '12. Reactivity với ref() và reactive()', duration: '28:30', active: true },
                    { id: 13, title: '13. Bài kiểm tra: Reactivity', quiz: '10 câu', quizLabel: 'Cần làm quiz' },
                    { id: 14, title: '14. Computed Properties chuyên sâu', duration: '22:15', locked: true }
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
    }
})

const chaptersRef = ref([...props.chapters])

const toggleChapter = (chapterId) => {
    const chapter = chaptersRef.value.find(c => c.id === chapterId)
    if (chapter && !chapter.locked) {
        chapter.expanded = !chapter.expanded
    }
}
</script>

<template>
    <div class="bg-white rounded-xl border border-surface-container-highest shadow-sm overflow-hidden">
        <div class="p-5 border-b border-surface-container-highest bg-surface-container-low flex items-center justify-between">
            <h3 class="font-bold text-lg text-gray-900">Chi tiết lộ trình</h3>
        </div>
        
        <div v-for="chapter in chaptersRef" :key="chapter.id"
            :class="['border-b border-surface-container-highest last:border-0', 
                     chapter.progress > 0 && chapter.progress < 100 ? 'bg-primary-container/20' : '',
                     chapter.locked ? 'opacity-60' : '']">
            
            <!-- Chapter Header -->
            <button @click="toggleChapter(chapter.id)" 
                class="w-full flex items-center justify-between p-5 hover:bg-surface transition-colors focus:outline-none focus:ring-inset focus:ring-2 focus:ring-primary/10">
                <div class="flex-1 text-left">
                    <h4 :class="['font-bold text-gray-900', chapter.progress > 0 && chapter.progress < 100 ? 'text-primary' : '']">
                        {{ chapter.title }}
                    </h4>
                    <div class="flex items-center gap-4 mt-2">
                        <span :class="['text-xs font-bold', chapter.statusColor || '']">{{ chapter.status }}</span>
                        <div v-if="chapter.progress > 0" class="flex-1 max-w-[200px] h-1.5 bg-surface-container rounded-full overflow-hidden hidden sm:block">
                            <div :class="['h-full', chapter.progressColor || 'bg-primary']" :style="{ width: chapter.progress + '%' }"></div>
                        </div>
                    </div>
                </div>
                <span class="material-symbols-outlined transition-transform duration-300"
                    :class="[chapter.progress > 0 && chapter.progress < 100 ? 'text-primary' : 'text-on-surface-variant',
                             chapter.expanded ? 'rotate-180' : '']">
                    {{ chapter.locked ? 'lock' : 'expand_more' }}
                </span>
            </button>

            <!-- Lesson List (Accordion Content) -->
            <div v-if="chapter.expanded && !chapter.locked" 
                class="bg-white border-t border-surface-container-highest px-2 py-2">
                <div v-for="lesson in chapter.lessons" :key="lesson.id"
                    :class="['flex items-center justify-between p-3 rounded-lg transition-colors', 
                             lesson.active ? 'bg-surface-container-low border-l-2 border-primary' : 'hover:bg-surface',
                             lesson.locked ? 'opacity-50' : '']">
                    <div class="flex items-center gap-3">
                        <span v-if="lesson.completed" class="material-symbols-outlined text-success text-xl" style="font-variation-settings: 'FILL' 1;">check_circle</span>
                        <span v-else-if="lesson.active" class="material-symbols-outlined text-primary text-xl">play_circle</span>
                        <span v-else-if="lesson.quiz" class="material-symbols-outlined text-on-surface-variant text-xl">assignment</span>
                        <span v-else-if="lesson.locked" class="material-symbols-outlined text-on-surface-variant text-xl">lock</span>
                        <span v-else class="material-symbols-outlined text-on-surface-variant text-xl">radio_button_unchecked</span>

                        <span :class="['text-sm', lesson.active ? 'font-bold text-primary' : 'font-medium text-gray-900']">
                            {{ lesson.title }}
                        </span>
                        
                        <span v-if="lesson.quizLabel" class="px-2 py-0.5 rounded text-[10px] font-bold bg-warning/20 text-yellow-800">
                            {{ lesson.quizLabel }}
                        </span>
                    </div>
                    
                    <span class="text-xs font-mono" :class="lesson.active ? 'text-primary font-bold' : 'text-on-surface-variant'">
                        {{ lesson.duration || lesson.quiz }}
                    </span>
                </div>
            </div>
        </div>
    </div>
</template>

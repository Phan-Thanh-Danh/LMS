<script setup>
import { ref, watch } from 'vue'

const props = defineProps(['chapters', 'activeLessonId'])
const emit = defineEmits(['switch-lesson'])

const activeTab = ref('Lesson')
const tabs = ['Lesson', 'Q&A', 'Ghi chú', 'Tài nguyên']
const localActiveLessonId = ref(props.activeLessonId)

watch(() => props.activeLessonId, (newId) => {
    localActiveLessonId.value = newId
}, { immediate: true })

const toggleChapter = (chapter) => {
    if (chapter.locked) return
    chapter.expanded = !chapter.expanded
}

const selectLesson = (lesson) => {
    if (lesson.locked) return
    localActiveLessonId.value = lesson.id
    emit('switch-lesson', { id: lesson.id, type: lesson.type, title: lesson.title })
}
</script>

<template>
  <div class="flex-1 flex flex-col h-full overflow-hidden bg-white">
    <!-- Tabs Header -->
    <div class="flex border-b border-surface-container-highest shrink-0 overflow-x-auto">
        <button v-for="tab in tabs" :key="tab"
            @click="activeTab = tab"
            :class="['flex-1 py-3 px-2 text-sm font-bold border-b-2 transition-all whitespace-nowrap min-w-[80px]',
                     activeTab === tab ? 'text-primary border-primary' : 'text-on-surface-variant border-transparent hover:text-primary']">
            {{ tab }}
        </button>
    </div>

    <!-- Lesson Tab -->
    <div v-show="activeTab === 'Lesson'" class="flex-1 overflow-y-auto bg-surface custom-scrollbar">
        <div v-for="chapter in chapters" :key="chapter.id"
            :class="['border-b border-surface-container-highest', chapter.locked ? 'opacity-55' : '']">
            
            <!-- Chapter Header -->
            <button @click="toggleChapter(chapter)"
                :class="['w-full flex items-center justify-between p-4 transition-colors', 
                         chapter.expanded ? 'bg-surface-container-low' : 'bg-white hover:bg-surface',
                         chapter.locked ? 'cursor-not-allowed' : 'cursor-pointer']">
                <div class="flex flex-col items-start text-left gap-0.5">
                    <span class="text-[11px] font-bold text-on-surface-variant uppercase tracking-wide">Chương {{ chapter.id }}</span>
                    <h3 class="text-sm font-bold text-on-surface">{{ chapter.title }}</h3>
                    <span class="text-xs text-on-surface-variant">{{ chapter.lessons.filter(l => l.completed).length }}/{{ chapter.lessons.length }} bài học</span>
                </div>
                <span class="material-symbols-outlined text-on-surface-variant shrink-0 transition-transform"
                    :style="{ transform: chapter.expanded ? 'rotate(180deg)' : 'rotate(0deg)' }">
                    {{ chapter.locked ? 'lock' : 'expand_more' }}
                </span>
            </button>

            <!-- Lesson List -->
            <div v-if="chapter.expanded && !chapter.locked" class="flex flex-col">
                <button v-for="lesson in chapter.lessons" :key="lesson.id"
                    @click="selectLesson(lesson)"
                    :class="['w-full flex items-start gap-3 p-4 border-t border-surface-container text-left transition-colors',
                             lesson.id === activeLessonId ? 'bg-primary-container/40' : 'hover:bg-surface',
                             lesson.locked ? 'opacity-50 cursor-not-allowed' : 'cursor-pointer']">
                    
                    <span class="shrink-0 mt-0.5 material-symbols-outlined text-lg"
                        :class="lesson.completed ? 'text-success' : lesson.id === activeLessonId ? 'text-primary' : 'text-on-surface-variant'"
                        :style="lesson.completed ? 'font-variation-settings: \'FILL\' 1;' : ''">
                        {{ lesson.completed ? 'check_circle' : lesson.locked ? 'lock' : lesson.type === 'quiz' ? 'assignment' : lesson.id === activeLessonId ? 'play_circle' : 'radio_button_unchecked' }}
                    </span>

                    <div class="flex-1 min-w-0">
                        <p :class="['text-sm transition-colors truncate', lesson.id === activeLessonId ? 'text-primary font-bold' : 'text-on-surface font-medium']">
                            {{ lesson.title }}
                        </p>
                        <div class="flex items-center gap-2 mt-1">
                            <div class="flex items-center gap-1 text-xs" :class="lesson.id === activeLessonId ? 'text-primary/80' : 'text-on-surface-variant'">
                                <span class="material-symbols-outlined text-[13px]">{{ lesson.type === 'quiz' ? 'assignment' : 'ondemand_video' }}</span>
                                <span>{{ lesson.duration }}</span>
                            </div>
                            <span v-if="lesson.quizLabel" class="px-1.5 py-0.5 rounded text-[10px] font-bold bg-warning/20 text-yellow-800">
                                {{ lesson.quizLabel }}
                            </span>
                        </div>
                    </div>
                </button>
            </div>
        </div>
    </div>

    <!-- Placeholder Tabs -->
    <div v-show="activeTab !== 'Lesson'" class="flex-1 flex flex-col items-center justify-center p-8 text-center text-on-surface-variant gap-3 bg-surface">
        <span class="material-symbols-outlined text-4xl opacity-40">{{ activeTab === 'Q&A' ? 'forum' : activeTab === 'Ghi chú' ? 'edit_note' : 'folder' }}</span>
        <p class="text-sm italic">Tab <strong>{{ activeTab }}</strong> đang được phát triển...</p>
    </div>
  </div>
</template>

<style scoped>
.custom-scrollbar::-webkit-scrollbar { width: 4px; }
.custom-scrollbar::-webkit-scrollbar-track { background: transparent; }
.custom-scrollbar::-webkit-scrollbar-thumb { background: #d1d5db; border-radius: 10px; }
.custom-scrollbar::-webkit-scrollbar-thumb:hover { background: #9ca3af; }
</style>

<script setup>
defineProps({
    stats: {
        type: Array,
        default: () => [
            { id: 1, label: 'Thời gian đã học', value: '12h 45m', icon: 'schedule' },
            { id: 2, label: 'Điểm Quiz (TB)', value: '8.5', unit: '/10', icon: 'grading' },
            { id: 3, label: 'Bài đã hoàn thành', value: '45', unit: '/70', icon: 'task_alt' }
        ]
    }
})
</script>

<template>
    <div class="grid grid-cols-2 md:grid-cols-3 gap-4">
        <div v-for="stat in stats" :key="stat.id" 
            class="bg-white rounded-xl p-4 border border-surface-container-highest shadow-sm"
            :class="stat.id === 3 ? 'col-span-2 md:col-span-1' : ''">
            <div class="flex items-center gap-2 text-on-surface-variant mb-2">
                <span class="material-symbols-outlined text-lg">{{ stat.icon }}</span>
                <span class="text-sm font-medium">{{ stat.label }}</span>
            </div>
            <div class="text-2xl font-mono font-bold text-gray-900">
                {{ stat.value }}<span v-if="stat.unit" class="text-sm text-on-surface-variant">{{ stat.unit }}</span>
            </div>
        </div>
    </div>
</template>

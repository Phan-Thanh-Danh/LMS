<script setup>
defineProps({
    stats: {
        type: Object,
        default: () => ({
            totalHours: 0,
            completedLessons: 0,
            certificates: 0
        })
    }
})

const statItems = [
    { label: 'Giờ học', key: 'totalHours', icon: 'schedule', color: 'text-blue-500', bg: 'bg-blue-50' },
    { label: 'Bài học đã xong', key: 'completedLessons', icon: 'verified', color: 'text-emerald-500', bg: 'bg-emerald-50' },
    { label: 'Chứng chỉ', key: 'certificates', icon: 'workspace_premium', color: 'text-amber-500', bg: 'bg-amber-50' },
]
</script>

<template>
  <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
    <div v-for="item in statItems" :key="item.key" 
      class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 flex items-center gap-5 hover:shadow-md transition-shadow">
      <div :class="['w-14 h-14 rounded-xl flex items-center justify-center', item.bg]">
        <span :class="['material-symbols-outlined text-2xl font-bold', item.color]">{{ item.icon }}</span>
      </div>
      <div>
        <p class="text-[10px] font-black text-gray-400 uppercase tracking-widest mb-1">{{ item.label }}</p>
        <p class="text-2xl font-black text-gray-900 tracking-tighter">{{ stats[item.key] || 0 }}</p>
      </div>
    </div>
  </div>
</template>

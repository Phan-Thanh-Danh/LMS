<script setup>
defineProps({
    course: {
        type: Object,
        required: true
    }
})
</script>

<template>
  <div class="bg-white rounded-2xl p-6 shadow-sm border border-gray-100 flex flex-col md:flex-row items-center gap-6 group hover:shadow-xl hover:border-primary/20 transition-all duration-300">
    <div class="w-full md:w-48 aspect-video rounded-xl overflow-hidden relative shadow-lg">
      <img :src="course.thumbnail" :alt="course.title" class="w-full h-full object-cover group-hover:scale-105 transition-transform duration-700">
      <div class="absolute inset-0 bg-black/20 group-hover:bg-transparent transition-colors"></div>
      <div class="absolute inset-0 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity">
        <div class="w-12 h-12 bg-white rounded-full flex items-center justify-center shadow-2xl scale-75 group-hover:scale-100 transition-transform">
          <span class="material-symbols-outlined text-primary text-3xl font-bold">play_arrow</span>
        </div>
      </div>
    </div>

    <div class="flex-1 space-y-4">
      <div class="space-y-1">
        <span class="text-[10px] font-black text-primary uppercase tracking-[0.2em]">Học tiếp thôi!</span>
        <h3 class="text-xl font-black text-gray-900 leading-tight tracking-tighter uppercase group-hover:text-primary transition-colors">
          {{ course.title }}
        </h3>
        <p class="text-xs font-bold text-gray-500 uppercase tracking-widest">Giảng viên: {{ course.instructor }}</p>
      </div>

      <div class="space-y-2">
        <div class="flex justify-between items-end">
          <span class="text-[10px] font-black text-gray-400 uppercase tracking-widest">Tiến độ khóa học</span>
          <span class="text-sm font-black text-primary">{{ course.progress }}%</span>
        </div>
        <div class="h-2 w-full bg-gray-100 rounded-full overflow-hidden border border-gray-50">
          <div class="h-full bg-linear-to-r from-primary to-blue-400 rounded-full transition-all duration-1000" :style="{ width: course.progress + '%' }"></div>
        </div>
      </div>
    </div>

    <router-link :to="`/learn/${course.id}`" 
      class="w-full md:w-auto px-8 py-4 bg-primary hover:bg-blue-700 text-white font-black text-sm rounded-xl shadow-lg shadow-primary/20 transition-all active:scale-95 flex items-center justify-center gap-2 uppercase tracking-tighter">
      <span class="material-symbols-outlined text-lg">play_circle</span>
      Vào học ngay
    </router-link>
  </div>
</template>

import { defineStore } from 'pinia'
import { categoryService } from '@/services/categoryService'

export const useCategoryStore = defineStore('category', {
  state: () => ({
    categories: [],
    loading: false,
    error: null
  }),

  actions: {
    async fetchCategories() {
      this.loading = true
      this.error = null
      try {
        // Step 1: Fetch groups (top-level)
        const groups = await categoryService.getCategoryGroups()
        
        // Step 2: Fetch sub-categories for each group
        const categoryTree = await Promise.all(
          groups.map(async (group) => {
            const subs = await categoryService.getSubCategories(group.maDanhMuc)
            return {
              id: group.maDanhMuc,
              name: group.tenDanhMuc,
              icon: group.duongDanIcon || 'folder',
              slug: group.duongDanURL,
              subCategories: await Promise.all(
                subs.map(async (sub) => {
                  const topics = await categoryService.getSubCategories(sub.maDanhMuc)
                  return {
                    id: sub.maDanhMuc,
                    name: sub.tenDanhMuc,
                    slug: sub.duongDanURL,
                    topics: topics.map(t => t.tenDanhMuc)
                  }
                })
              )
            }
          })
        )

        this.categories = categoryTree
      } catch (err) {
        this.error = err.message || 'Failed to fetch categories'
        console.error('Error fetching categories:', err)
      } finally {
        this.loading = false
      }
    }
  }
})

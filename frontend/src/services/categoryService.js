import api from './axios'

export const categoryService = {
  /**
   * Get all top-level category groups
   */
  async getCategoryGroups() {
    const response = await api.get('/category-groups')
    return response.data
  },

  /**
   * Get subcategories for a given parent category group
   * @param {number} parentId (optional)
   */
  async getSubCategories(parentId = null) {
    const url = parentId ? `/sub-categories?parentId=${parentId}` : '/sub-categories'
    const response = await api.get(url)
    return response.data
  }
}

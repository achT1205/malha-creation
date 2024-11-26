import { defineStore } from 'pinia';
import { ref } from 'vue';
import { catalogApiClient } from '../api/axios';

export const useCategoryStore = defineStore('categoryStore', () => {
    // State
    const categories = ref([]);
    const isLoading = ref(false);
    const error = ref(null);

    // Actions
    const fetchCategories = async () => {
        isLoading.value = true;
        error.value = null;
        try {
            const response = await catalogApiClient.get('/api/categories');
            categories.value = response.data.categories;
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const addCategory = async (item) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.post('/api/categories', item);
            await fetchCategories()
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const deleteCategory = async (id) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.delete("/api/categories/" + id);
            await fetchCategories()
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    return {
        categories,
        isLoading,
        error,
        fetchCategories,
        addCategory,
        deleteCategory
    };
});

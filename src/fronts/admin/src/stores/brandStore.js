import { defineStore } from 'pinia';
import { ref } from 'vue';
import { catalogApiClient } from '../api/axios';

export const useBrandStore = defineStore('brandStore', () => {
    // State
    const brands = ref([]);
    const isLoading = ref(false);
    const error = ref(null);

    // Actions
    const fetchBrands = async () => {
        isLoading.value = true;
        error.value = null;
        try {
            const response = await catalogApiClient.get('/api/brands');
            brands.value = response.data.brands;
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const addBrand = async (item) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.post('/api/brands', item);
            await fetchBrands()
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const deleteBrand = async (id) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.delete("/api/brands/" + id);
            await fetchBrands()
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    return {
        brands,
        isLoading,
        error,
        fetchBrands,
        addBrand,
        deleteBrand
    };
});

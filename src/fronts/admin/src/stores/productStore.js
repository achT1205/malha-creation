import { defineStore } from 'pinia';
import { ref } from 'vue';
import { catalogApiClient } from '../api/axios';

export const useProductStore = defineStore('productStore', () => {
    // State
    const products = ref([]);
    const isLoading = ref(false);
    const error = ref(null);

    // Actions
    const fetchProducts = async () => {
        isLoading.value = true;
        error.value = null;
        try {
            const response = await catalogApiClient.get('/api/products');
            products.value = response.data.products; // Adjust based on API response structure
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const addProduct = async (product) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.post('/api/products', product);
            await fetchProducts();
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const deleteProduct = async (id) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.delete(`/api/products/${id}`);
            await fetchProducts();
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    return {
        products,
        isLoading,
        error,
        fetchProducts,
        addProduct,
        deleteProduct,
    };
});

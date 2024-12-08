import { defineStore } from 'pinia';
import { ref } from 'vue';
import { catalogApiClient } from '../api/axios';

export const useOccasionStore = defineStore('occasionStore', () => {
    // State
    const occasions = ref([]);
    const isLoading = ref(false);
    const error = ref(null);

    // Actions
    const fetchOccasions = async () => {
        isLoading.value = true;
        error.value = null;
        try {
            const response = await catalogApiClient.get('/api/occasions');
            occasions.value = response.data.occasions; // Adjust based on API response structure
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const addOccasion = async (occasion) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.post('/api/occasions', occasion);
            await fetchOccasions();
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const deleteOccasion = async (id) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.delete(`/api/occasions/${id}`);
            await fetchOccasions();
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    return {
        occasions,
        isLoading,
        error,
        fetchOccasions,
        addOccasion,
        deleteOccasion,
    };
});

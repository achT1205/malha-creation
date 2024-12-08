import { defineStore } from 'pinia';
import { ref } from 'vue';
import { catalogApiClient } from '../api/axios';

export const useCollectionStore = defineStore('collectionStore', () => {
    // State
    const collections = ref([]);
    const isLoading = ref(false);
    const error = ref(null);

    // Actions
    const fetchCollections = async () => {
        isLoading.value = true;
        error.value = null;
        try {
            const response = await catalogApiClient.get('/api/collections');
            collections.value = response.data.collections; // Adjust based on API response structure
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const addCollection = async (collection) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.post('/api/collections', collection);
            await fetchCollections();
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const deleteCollection = async (id) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.delete(`/api/collections/${id}`);
            await fetchCollections();
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    return {
        collections,
        isLoading,
        error,
        fetchCollections,
        addCollection,
        deleteCollection,
    };
});

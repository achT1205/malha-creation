import { defineStore } from 'pinia';
import { ref } from 'vue';
import { catalogApiClient } from '../api/axios';

export const useMaterialStore = defineStore('materialStore', () => {
    // State
    const materials = ref([]);
    const isLoading = ref(false);
    const error = ref(null);

    // Actions
    const fetchMaterials = async () => {
        isLoading.value = true;
        error.value = null;
        try {
            const response = await catalogApiClient.get('/api/materials');
            materials.value = response.data.materials; // Adjust based on API response structure
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const addMaterial = async (material) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.post('/api/materials', material);
            await fetchMaterials();
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    const deleteMaterial = async (id) => {
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.delete(`/api/materials/${id}`);
            await fetchMaterials();
        } catch (err) {
            error.value = err.message;
        } finally {
            isLoading.value = false;
        }
    };

    return {
        materials,
        isLoading,
        error,
        fetchMaterials,
        addMaterial,
        deleteMaterial,
    };
});

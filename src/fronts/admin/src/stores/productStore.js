import { defineStore } from 'pinia';
import { ref } from 'vue';
import { catalogApiClient } from '../api/axios';

export const useProductStore = defineStore('productStore', () => {
    // State
    const products = ref([]);
    const colorVariants = ref([]);
    const product = ref(null);
    const isLoading = ref(false);
    const successed = ref(false);
    const error = ref(null);

    // Actions
    const fetchProducts = async () => {
        isLoading.value = true;
        error.value = null;
        colorVariants.value = []
        try {
            const response = await catalogApiClient.get('/api/products');
            products.value = response.data.products;
            if (response.data.products && response.data.products.count > 0) {
                response.data.products.data.forEach(p => {
                    p.colorVariants.forEach(c => {
                        colorVariants.value.push({
                            id: c.id,
                            slug: c.slug,
                            imageSrc: c.images[0].imageSrc,
                            altText: c.images[0].altText
                        })

                    });
                });
            }
        } catch (err) {
            error.value = err.response.data.detail;
        } finally {
            isLoading.value = false;
        }
    };

    const addProduct = async (product) => {
        successed.value = false
        isLoading.value = true;
        error.value = null;
        try {
            await catalogApiClient.post('/api/products', product);
            window.localStorage.removeItem('newProduct');
            successed.value = true
        } catch (err) {
            error.value = err.response.data.detail;
            successed.value = false
        } finally {
            isLoading.value = false;
        }
    };

    const updateProduct = async (product) => {
        isLoading.value = true;
        successed.value = false
        error.value = null;
        try {
            await catalogApiClient.put(`/api/products/${product.id}`, product);
            window.localStorage.removeItem('editProduct');
            successed.value = true
        } catch (err) {
            error.value = err.response.data.detail;
            successed.value = false
        } finally {
            isLoading.value = false;
        }
    };

    const deleteProduct = async (id) => {
        isLoading.value = true;
        successed.value = false
        error.value = null;
        try {
            await catalogApiClient.delete(`/api/products/${id}`);
            successed.value = true
            await fetchProducts();
        } catch (err) {
            error.value = err.response.data.detail;
            successed.value = false
        } finally {
            isLoading.value = false;
        }
    };

    const getProduct = async (id) => {
        isLoading.value = true;
        error.value = null;
        try {
            const response = await catalogApiClient.get(`/api/products/by-id/${id}`);
            product.value = response.data.product;
            await fetchProducts();
        } catch (err) {
            error.value = err.response.data.detail;
        } finally {
            isLoading.value = false;
        }
    };

    return {
        products,
        product,
        isLoading,
        successed,
        error,
        colorVariants,
        fetchProducts,
        addProduct,
        deleteProduct,
        getProduct,
        updateProduct
    };
});

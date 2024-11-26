<script setup>
import { computed, onMounted } from 'vue';
import { useBrandStore } from '@/stores/brandStore';
import ParameterManager from '@/components/ParameterManager.vue';

const brandStore = useBrandStore();

const items = computed(() => brandStore.brands);
const isLoading = computed(() => brandStore.isLoading);
const error = computed(() => brandStore.error);

const loadItems = () => {
  brandStore.fetchBrands();
};

const addItem = (item) => {
  brandStore.addBrand(item);
};

const deleteItem = (itemId) => {
  brandStore.deleteBrand(itemId);
};

const headers = [
    {
        fieldName: 'name',
        headerName: 'Name',
        sortable: true,
        extarea: false,
        fieldType:"textinput",
        required: true,
        headerStyle: 'min-width: 16rem'
    }
];


onMounted(() => {
  loadItems();
});
</script>

<template>
  <ParameterManager 
    :items="items"
    :isLoading="isLoading"
    :error="error"
    :headers="headers"
    itemLabel="Brand" 
    @fetchItems="loadItems"
    @addItem="addItem"
    @deleteItem="deleteItem"
  />
</template>

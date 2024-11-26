<script setup>
import { computed, onMounted } from 'vue';
import { useCategoryStore } from '@/stores/categoryStore';
import ParameterManager from '@/components/ParameterManager.vue';

const categoryStore = useCategoryStore();

const items = computed(() => categoryStore.categories);
const isLoading = computed(() => categoryStore.isLoading);
const error = computed(() => categoryStore.error);

const loadItems = () => {
  categoryStore.fetchCategories();
};

const addItem = (item) => {
  categoryStore.addCategory(item);
};

const deleteItem = (itemId) => {
  categoryStore.deleteCategory(itemId);
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
    itemLabel="Category" 
    @fetchItems="loadItems"
    @addItem="addItem"
    @deleteItem="deleteItem"
  />
</template>

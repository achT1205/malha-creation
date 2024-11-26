<script setup>
import { computed, onMounted } from 'vue';
import { useOccasionStore } from '@/stores/occasionStore';
import ParameterManager from '@/components/ParameterManager.vue';

const occasionStore = useOccasionStore();

const items = computed(() => occasionStore.occasions);
const isLoading = computed(() => occasionStore.isLoading);
const error = computed(() => occasionStore.error);

const loadItems = () => {
  occasionStore.fetchOccasions();
};

const addItem = (item) => {
  occasionStore.addOccasion(item);
};

const deleteItem = (itemId) => {
  occasionStore.deleteOccasion(itemId);
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
    itemLabel="Occasion" 
    @fetchItems="loadItems"
    @addItem="addItem"
    @deleteItem="deleteItem"
  />
</template>

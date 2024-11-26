<script setup>
import { computed, onMounted } from 'vue';
import { useCollectionStore } from '@/stores/collectionStore';
import ParameterManager from '@/components/ParameterManager.vue';

const collectionStore = useCollectionStore();

const items = computed(() => collectionStore.collections);
const isLoading = computed(() => collectionStore.isLoading);
const error = computed(() => collectionStore.error);

const loadItems = () => {
    collectionStore.fetchCollections();
};

const addItem = (item) => {
    collectionStore.addCollection(item);
};

const deleteItem = (itemId) => {
    collectionStore.deleteCollection(itemId);
};

const headers = [
    {
        fieldName: 'imageSrc',
        headerName: 'Image',
        sortable: false,
        extarea: false,
        fieldType: "textinput",
        required: true,
        headerStyle: 'min-width: 16rem'
    },
    {
        fieldName: 'altText',
        headerName: 'AltText',
        sortable: false,
        extarea: false,
        fieldType: "textinput",
        required: true,
        headerStyle: 'min-width: 16rem'
    },
    {
        fieldName: 'name',
        headerName: 'Name',
        sortable: true,
        extarea: false,
        fieldType: "textinput",
        required: true,
        headerStyle: 'min-width: 16rem'
    }
];

onMounted(() => {
    loadItems();
});
</script>

<template>
    <ParameterManager :items="items" :isLoading="isLoading" :error="error" :headers="headers" itemLabel="Collection"
        @fetchItems="loadItems" @addItem="addItem" @deleteItem="deleteItem" />
</template>

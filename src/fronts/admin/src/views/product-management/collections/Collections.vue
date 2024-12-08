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
        fieldName: 'name',
        headerName: 'Name',
        sortable: true,
        extarea: false,
        fieldType: "textinput",
        required: true,
        headerStyle: 'min-width: 16rem'
    },
  {
    fieldName: 'description',
    headerName: 'Description',
    sortable: false,
    fieldType: "textarea",
    required: true,
    headerStyle: 'min-width: 16rem'
  },
  {
    fieldName: 'coverImage',
    headerName: 'LoverImage',
    sortable: false,
    fieldType: "image",
    required: true,
    headerStyle: 'min-width: 16rem'
  }
];

onMounted(() => {
    loadItems();
});
</script>

<template>
    <ParameterManager :items="items" :isLoading="isLoading" :error="error" :headers="headers"  imageFieldName="coverImage" itemLabel="Collection"
        @fetchItems="loadItems" @addItem="addItem" @deleteItem="deleteItem" />
</template>

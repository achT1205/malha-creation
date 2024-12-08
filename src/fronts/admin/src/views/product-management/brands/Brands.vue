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
    fieldName: 'websiteUrl',
    headerName: 'Website Url',
    sortable: false,
    fieldType: "textinput",
    required: true,
    headerStyle: 'min-width: 16rem'
  },
  {
    fieldName: 'logo',
    headerName: 'Logo',
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
  <ParameterManager :items="items" :isLoading="isLoading" :error="error" :headers="headers" itemLabel="Brand"
    imageFieldName="logo" @fetchItems="loadItems" @addItem="addItem" @deleteItem="deleteItem" />
</template>

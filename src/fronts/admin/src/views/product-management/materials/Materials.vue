<script setup>
import { computed, onMounted } from 'vue';
import { useMaterialStore } from '@/stores/materialStore';
import ParameterManager from '@/components/ParameterManager.vue';

const materialStore = useMaterialStore();

const items = computed(() => materialStore.materials);
const isLoading = computed(() => materialStore.isLoading);
const error = computed(() => materialStore.error);

const loadItems = () => {
  materialStore.fetchMaterials();
};

const addItem = (item) => {
  materialStore.addMaterial(item);
};

const deleteItem = (itemId) => {
  materialStore.deleteMaterial(itemId);
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
  }
];



onMounted(() => {
  loadItems();
});
</script>

<template>
  <ParameterManager :items="items" :isLoading="isLoading" :error="error" :headers="headers" itemLabel="Material"
    @fetchItems="loadItems" @addItem="addItem" @deleteItem="deleteItem" />
</template>

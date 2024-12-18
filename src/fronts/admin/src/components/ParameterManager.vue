<script setup>
import { ref, computed } from 'vue';
import { useToast } from 'primevue/usetoast';
import { FilterMatchMode } from '@primevue/core/api';

const props = defineProps({
  items: {
    type: Array,
    required: true,
  },
  headers: {
    type: Array,
    required: true,
  },
  isLoading: {
    type: Boolean,
    default: false,
  },
  imageFieldName: {
    type: String
  },
  error: {
    type: String,
    default: null,
  },
  itemLabel: {
    type: String,
    default: 'Item', // Default label for item
  },
});

const emit = defineEmits(['fetchItems', 'addItem', 'deleteItem']);

const toast = useToast();
const dt = ref();
const itemDialog = ref(false);
const deleteItemDialog = ref(false);
const item = ref({});
const image = ref({});
const filters = ref({
  global: { value: null, matchMode: FilterMatchMode.CONTAINS },
});
const submitted = ref(false);

function openNew() {
  item.value = {};
  submitted.value = false;
  itemDialog.value = true;
}

function hideDialog() {
  itemDialog.value = false;
  submitted.value = false;
}

function saveItem() {
  submitted.value = true;
  if (item?.value.name?.trim()) {
    if (props.imageFieldName) {
      item.value[props.imageFieldName] = image.value
    }
    emit('addItem', item.value);
    itemDialog.value = false;
    item.value = {};
  }
}

function confirmDeleteItem(prod) {
  item.value = prod;
  deleteItemDialog.value = true;
}

function deleteItem() {
  emit('deleteItem', item.value.id);
  deleteItemDialog.value = false;
  item.value = {};
  toast.add({ severity: 'success', summary: 'Successful', detail: `${props.itemLabel} Deleted`, life: 3000 });
}
</script>

<template>
  <div>
    <div class="card">
      <Toolbar class="mb-6">
        <template #start>
          <Button :label="'New ' + itemLabel" icon="pi pi-plus" severity="secondary" class="mr-2" @click="openNew" />
        </template>
      </Toolbar>

      <DataTable ref="dt" :value="items" dataKey="id" :paginator="true" :rows="10" :filters="filters"
        paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
        :rowsPerPageOptions="[5, 10, 25]" currentPageReportTemplate="Showing {first} to {last} of {totalRecords} ">
        <template #header>
          <div class="flex flex-wrap gap-2 items-center justify-between">
            <h4 class="m-0">Manage {{ itemLabel }}s</h4>
            <IconField>
              <InputIcon>
                <i class="pi pi-search" />
              </InputIcon>
              <InputText v-model="filters['global'].value" :placeholder="'Search ' + itemLabel + '...'" />
            </IconField>
          </div>
        </template>

        <Column v-for="header in headers" :key="header.fieldName" :field="header.fieldName" :header="header.headerName"
          :sortable="header.sortable" :headerStyle="header.headerStyle">
          <template #body="slotProps">
            <img v-if="header.fieldType === 'image'" :src="slotProps.data[header.fieldName].imageSrc"
              :alt="slotProps.data[header.fieldName].altText" class="shadow-2" width="100" />
            <span v-else> {{ slotProps.data[header.fieldName] }}</span>
          </template>
        </Column>


        <Column>
          <template #body="slotProps">
            <Button icon="pi pi-trash" outlined rounded severity="danger" @click="confirmDeleteItem(slotProps.data)" />
          </template>
        </Column>
      </DataTable>
    </div>

    <Dialog v-model:visible="itemDialog" :style="{ width: '450px' }" :header="'New ' + itemLabel" :modal="true">
      <div class="flex flex-col gap-6">
        <div v-for="header in headers" :key="header.fieldName">
          <label v-if="header.fieldType !== 'image'" for="name" class="block font-bold mb-3">{{ header.headerName
            }}</label>
          <InputText v-if="header.fieldType == 'textinput'" :id="header.fieldName" v-model.trim="item[header.fieldName]"
            :required="item.required" :invalid="submitted && !item[header.fieldName]" fluid />


          <Textarea v-else-if="header.fieldType == 'textarea'" :id="header.fieldName"
            :invalid="submitted && !item[header.fieldName]" v-model="item[header.fieldName]" :required="item.required"
            rows="3" cols="53" />

          <div v-else-if="header.fieldType == 'image'">
            <label for="name" class="block font-bold mb-3">imageSrc</label>
            <InputText :id="header.fieldName" v-model.trim="image.imageSrc" :required="item.required"
              :invalid="submitted && !image.imageSrc" fluid />

            <label for="name" class="block font-bold mb-3 mt-5">altText</label>
            <InputText :id="header.fieldName" v-model.trim="image.altText" :required="item.required"
              :invalid="submitted && !image.altText" fluid />
          </div>
        </div>
      </div>

      <template #footer>
        <Button label="Cancel" icon="pi pi-times" text @click="hideDialog" />
        <Button label="Save" icon="pi pi-check" @click="saveItem" />
      </template>
    </Dialog>

    <Dialog v-model:visible="deleteItemDialog" :style="{ width: '450px' }" :header="'Confirm Delete ' + itemLabel"
      :modal="true">
      <div class="flex items-center gap-4">
        <i class="pi pi-exclamation-triangle !text-3xl" />
        <span v-if="item">Are you sure you want to delete <b>{{ item.name }}</b>?</span>
      </div>
      <template #footer>
        <Button label="No" icon="pi pi-times" text @click="deleteItemDialog = false" />
        <Button label="Yes" icon="pi pi-check" @click="deleteItem" />
      </template>
    </Dialog>
  </div>
</template>

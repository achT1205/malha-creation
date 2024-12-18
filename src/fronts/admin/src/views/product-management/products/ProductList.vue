<script setup>
import { FilterMatchMode } from '@primevue/core/api';
import { useToast } from 'primevue/usetoast';
import { onMounted, ref, computed, watch } from 'vue';
import { useRouter } from 'vue-router';
import { useProductStore } from '@/stores/productStore';

const toast = useToast();
const router = useRouter();
const productStore = useProductStore();


const reps = computed(() => productStore.products);
const isLoading = computed(() => productStore.isLoading);
const error = computed(() => productStore.error);
const successed = computed(() => productStore.successed);


const deleteProductDialog = ref(false);
const product = ref({});
const dt = ref();
const products = ref();
const expandedRows = ref();
const filters = ref({
    global: { value: null, matchMode: FilterMatchMode.CONTAINS }
});

watch(reps, (newValue, oldValue) => {
    if (newValue !== null && newValue.data) {
        products.value = newValue.data
    }
});
watch(successed, (newValue, oldValue) => {
    if (newValue === true && oldValue === false) {
        deleteProductDialog.value = false;
        toast.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Product Updated',
            life: 3000,
        });
    }
});

const editProduct = (p) => {
    router.push(`/product-anagement/products/${p.id}/edit`)
}
const deleteProduct = () => {
    productStore.deleteProduct(product.value.id)
}
const exportCSV = () => {
}
const createProduct = (p) => {
    router.push("/product-anagement/products/new-product")
}
const confirmDeleteProduct =(prod) =>{
    product.value = prod;
    deleteProductDialog.value = true;
}


onMounted(() => {
    productStore.fetchProducts()
});

</script>

<template>
    <div v-if="products && products.length">
        <div class="card">
            <Toolbar class="mb-6">
                <template #start>
                    <Button label="New" icon="pi pi-plus" severity="secondary" class="mr-2" @click="createProduct" />
                </template>

                <template #end>
                    <Button label="Export" icon="pi pi-upload" severity="secondary" @click="exportCSV()" />
                </template>
            </Toolbar>
            <DataTable ref="dt" v-model:expandedRows="expandedRows" showGridlines :value="products" dataKey="id"
                :paginator="true" :rows="10" :filters="filters"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25]"
                currentPageReportTemplate="Showing {first} to {last} of {totalRecords} products">
                <template #header>
                    <div class="flex flex-wrap gap-2 items-center justify-between">
                        <h4 class="m-0">Manage Products</h4>
                        <IconField>
                            <InputIcon>
                                <i class="pi pi-search" />
                            </InputIcon>
                            <InputText v-model="filters['global'].value" placeholder="Search..." />
                        </IconField>
                    </div>
                </template>

                <Column expander style="width: 5rem" />

                <Column field="code" header="Code" sortable style="min-width: 12rem" />
                <Column field="name" header="Name" sortable style="min-width: 16rem" />
                <Column field="productTypeString" header="Type" sortable style="min-width: 16rem" />
                <Column field="coverImage" header="Cover Image">
                    <template #body="slotProps">
                        <img :src="slotProps.data.coverImage.imageSrc" :alt="slotProps.data.coverImage.altText"
                            class="rounded" style="width: 64px" />
                    </template>
                </Column>
                <Column :exportable="false" style="min-width: 12rem">
                    <template #body="slotProps">
                        <Button icon="pi pi-pencil" outlined rounded class="mr-2"
                            @click="editProduct(slotProps.data)" />
                        <Button icon="pi pi-trash" outlined rounded severity="danger"
                            @click="confirmDeleteProduct(slotProps.data)" />
                    </template>
                </Column>

                <template #expansion="slotProps">
                    <div class="p-4">
                        <DataTable :value="slotProps.data.colorVariants">
                            <Column field="color" header="Color" sortable>
                                <template #body="slotProps">
                                    <div class="flex items-center gap-2">
                                        <div
                                            :class="['w-8 h-8 mr-2 border border-surface-200 dark:border-surface-700 rounded-full cursor-pointer flex justify-center items-center', slotProps.data.background]">
                                        </div>
                                        <span>{{ slotProps.data.color }}</span>
                                    </div>
                                </template>
                            </Column>
                            <Column field="slug" header="Slug" sortable></Column>
                            <Column v-if="slotProps.data.productType !== 0" field="price" header="Price" sortable>
                            </Column>
                            <Column v-if="slotProps.data.productType !== 0" field="quantity" header="Quantity" sortable>
                            </Column>
                            <Column v-if="slotProps.data.productType !== 0" field="restockThreshold"
                                header="Restock Threshold" sortable>
                            </Column>
                            <Column v-if="slotProps.data.productType === 0" field="sizeVariants" header="Size Variants"
                                sortable>
                                <template #body="slotProps">

                                    <DataTable :value="slotProps.data.sizeVariants">
                                        <Column field="price" header="Price" sortable>
                                        </Column>
                                        <Column field="quantity" header="Quantity" sortable>
                                        </Column>
                                        <Column field="restockThreshold" header="Restock Threshold" sortable>
                                        </Column>
                                    </DataTable>
                                </template>
                            </Column>
                            <Column header="Image">
                                <template #body="slotProps">
                                    <img :src="slotProps.data.images[0].imageSrc"
                                        :alt="slotProps.data.images[0].altText" class="rounded" style="width: 64px" />
                                </template>
                            </Column>

                        </DataTable>
                    </div>
                </template>
            </DataTable>
        </div>

        <Dialog v-model:visible="deleteProductDialog" :style="{ width: '450px' }" header="Confirm" :modal="true">
            <div class="flex items-center gap-4">
                <i class="pi pi-exclamation-triangle !text-3xl" />
                <span v-if="product">Are you sure you want to delete <b>{{ product.name }}</b>?</span>
            </div>
            <template #footer>
                <Button label="No" icon="pi pi-times" text @click="deleteProductDialog = false" />
                <Button label="Yes" icon="pi pi-check" @click="deleteProduct" />
            </template>
        </Dialog>
    </div>
</template>

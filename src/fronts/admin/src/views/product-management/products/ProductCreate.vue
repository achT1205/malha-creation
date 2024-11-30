<script setup>
import { ref, computed, onMounted, nextTick, watch } from 'vue';
import { useToast } from 'primevue/usetoast';

import { useBrandStore } from '@/stores/brandStore';
import { useCategoryStore } from '@/stores/categoryStore';
import { useCollectionStore } from '@/stores/collectionStore';
import { useMaterialStore } from '@/stores/materialStore';
import { useOccasionStore } from '@/stores/occasionStore';
import { useProductStore } from '@/stores/productStore';

const toast = useToast();
const brandStore = useBrandStore();
const categoryStore = useCategoryStore();
const collectionStore = useCollectionStore();
const materialStore = useMaterialStore();
const occasionStore = useOccasionStore();
const productStore = useProductStore();



const brands = computed(() => brandStore.brands);
const categories = computed(() => categoryStore.categories);
const collections = computed(() => collectionStore.collections);
const materials = computed(() => materialStore.materials);
const occasions = computed(() => occasionStore.occasions);
const isLoading = computed(() => productStore.isLoading);
const error = computed(() => productStore.error);

onMounted(() => {
    brandStore.fetchBrands()
    categoryStore.fetchCategories()
    collectionStore.fetchCollections()
    materialStore.fetchMaterials()
    occasionStore.fetchOccasions()
});

const productTypes = ref([{
    id: 0, name: "Clothing"
}, {
    id: 1, name: "Accessory"
}])

const colors = ref([{
    id: 'red', name: "Red", background: 'bg-red-900'
},
{
    id: 'orange', name: "Orange", background: 'bg-orange-500'
},
{
    id: 'blue', name: "Blue", background: 'bg-blue-500'
}])
const colorOverlay = ref();
const selectedColor = ref(null);
const colorVariantTabRefs = ref([]);
const product = ref({
    "name": '',
    "urlFriendlyName": '',
    "description": '',
    "isHandmade": false,
    "coverImage": {
        "imageSrc": '',
        "altText": ''
    },
    "productType": 0,
    "materialId": '',
    "brandId": '',
    "collectionId": '',
    "occasionIds": [],
    "categoryIds": [],
    "colorVariants": []
});

const toggleColorOverlay = (event) => {
    colorOverlay.value.toggle(event);
};

function onRemoveTags(tag) {
    product.value.tags = product.value.tags.filter((t) => t !== tag);
}

const colorExist = (color) => {
    return product.value.colorVariants.some((_) => _.color === color.name);
};

const onColorSelect = async (col) => {
    selectedColor.value = col;
    if (!colorExist(selectedColor.value)) {
        const newColor = {
            color: selectedColor.value.name,
            images: [{
                "imageSrc": "",
                "altText": ""
            }],
            background: selectedColor.value.background,
        };
        if (product.value.productType == 0) {
            newColor.sizeVariants = [{
                "size": "",
                "price": 0,
                "quantity": 0,
                "restockThreshold": 0
            }]
        } else {
            newColor.price = 0
            newColor.quantity = 0
            newColor.currency = "$"
            newColor.restockThreshold = 0
        }
        product.value.colorVariants.push(newColor);
        await nextTick();

        if (colorVariantTabRefs.value) {
            if (colorVariantTabRefs.value && colorVariantTabRefs.value.length) {
                colorVariantTabRefs.value[colorVariantTabRefs.value.length - 1].click()
            }
        }
    }
};

const addColorVariantImage = async (colorVariantIndex) => {
    product.value.colorVariants[colorVariantIndex].images.push({
        "imageSrc": "",
        "altText": ""
    })
    await nextTick();
}
const addSizeVariant = async (colorVariantIndex) => {
    product.value.colorVariants[colorVariantIndex].sizeVariants.push({
        "size": "",
        "price": 0,
        "quantity": 0,
        "restockThreshold": 0
    })
    await nextTick();
}

const removeSizeVariant = async (colorVariantIndex, sizeVariantIndex) => {
    product.value.colorVariants[colorVariantIndex].sizeVariants.splice(sizeVariantIndex, 1)
    await nextTick();
}

const removeColorVariantImage = async (colorVariantIndex, imageIndex) => {
    product.value.colorVariants[colorVariantIndex].images.splice(imageIndex, 1)
    await nextTick();
}

// Watch for changes in isLoading
watch(isLoading, (newValue, oldValue) => {
    if (newValue === false && oldValue === true) {
        toast.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Product Updated',
            life: 3000,
        });
    }
});

// Watch for changes in error
watch(error, (newValue, oldValue) => {
    if (newValue !== null && oldValue === null) {
        toast.add({
            severity: 'error',
            summary: 'Something Went Wrong',
            detail: newValue,
            life: 3000,
        });
    }
});
const saveProduct = () => {
    productStore.addProduct(product.value)
}

</script>

<template>
    <div class="card">
        <span class="block text-surface-900 dark:text-surface-0 font-bold text-xl mb-6">Create Product</span>
        <Fluid class="grid grid-cols-12 gap-4 flex-wrap">
            <div class="col-span-12 lg:col-span-8">
                <div class="grid grid-cols-12 gap-4">
                    <div class="col-span-12">
                        <InputText type="text" placeholder="Product Name" v-model="product.name" />
                    </div>
                    <div class="col-span-12">
                        <InputText type="text" placeholder="Product Url Friendly Name"
                            v-model="product.urlFriendlyName" />
                    </div>
                    <div class="col-span-12 lg:col-span-6">
                        <Select :options="productTypes" v-model="product.productType" optionLabel="name"
                            optionValue="id" placeholder="Select a Type" />
                    </div>
                    <div class="col-span-12 lg:col-span-6">
                        <InputText type="text" placeholder="Product Code" label="Product Code" v-model="product.code" />
                    </div>
                    <div class="col-span-12 lg:col-span-6">
                        <InputText type="text" placeholder="CoverImage Src" label="CoverImage Src"
                            v-model="product.coverImage.imageSrc" />
                    </div>
                    <div class="col-span-12 lg:col-span-6">
                        <InputText type="text" placeholder="CoverImage AltText" label="CoverImage AltText"
                            v-model="product.coverImage.altText" />
                    </div>

                    <div class="col-span-12">
                        <Toolbar>
                            <template v-slot:start>
                                <div>
                                    <Button type="button" icon="pi pi-plus" label="Add a color"
                                        @click="toggleColorOverlay" class="min-w-48" />
                                    <Popover ref="colorOverlay">
                                        <div class="flex flex-col gap-4">
                                            <div>
                                                <span class="font-medium block mb-2">Available colors</span>
                                                <ul class="list-none p-0 m-0 flex flex-col">
                                                    <li v-for="color in colors" :key="color.name"
                                                        class="flex items-center gap-2 px-2 py-3 hover:bg-emphasis cursor-pointer rounded-border"
                                                        @click="onColorSelect(color)">
                                                        <div
                                                            :class="['w-8 h-8 mr-2 border border-surface-200 dark:border-surface-700 rounded-full cursor-pointer flex justify-center items-center', color.background]">
                                                            <i class="pi pi-check text-sm text-white z-50"
                                                                v-if="colorExist(color)" />
                                                        </div>
                                                        <div>
                                                            <span class="font-medium">{{ color.name }}</span>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </Popover>
                                </div>
                            </template>
                            <template v-slot:end> <Button icon="pi pi-trash" class="mr-2" severity="warning" />
                            </template>
                        </Toolbar>
                    </div>

                    <div class="col-span-12" v-if="product.colorVariants.length">
                        <div class="card">
                            <Tabs value="0">
                                <TabList>
                                    <Tab v-for="(colorVariant, colorVariantIndex) in product.colorVariants"
                                        :key="colorVariantIndex" v-slot="slotProps" :value="colorVariantIndex" asChild>
                                        <div ref="colorVariantTabRefs"
                                            :class="['flex items-center gap-2', slotProps.class]"
                                            @click="slotProps.onClick" v-bind="slotProps.a11yAttrs">
                                            <div
                                                :class="['w-8 h-8 mr-2 border border-surface-200 dark:border-surface-700 rounded-full cursor-pointer flex justify-center items-center', colorVariant.background]">
                                                <i class="pi pi-check text-sm text-white z-50"
                                                    v-if="slotProps.active" />
                                            </div>
                                            <span class="font-bold whitespace-nowrap">{{ colorVariant.color }}</span>
                                        </div>
                                    </Tab>
                                </TabList>
                                <TabPanels>
                                    <TabPanel v-slot="slotProps"
                                        v-for="(colorVariant, colorVariantIndex) in product.colorVariants"
                                        :key="colorVariantIndex" :value="colorVariantIndex" asChild>
                                        <div v-show="slotProps.active" :class="slotProps.class"
                                            v-bind="slotProps.a11yAttrs">
                                            <div class="grid grid-cols-12 gap-4" v-if="product.productType !== 0">
                                                <div class="col-span-12">
                                                    {{ colorVariant.color }}
                                                </div>

                                                <div class="col-span-12 lg:col-span-4">
                                                    <InputText type="number" placeholder="Price"
                                                        v-model="colorVariant.price" />
                                                </div>
                                                <div class="col-span-12 lg:col-span-4">
                                                    <InputText type="number" placeholder="Quantity"
                                                        v-model="colorVariant.quantity" />
                                                </div>
                                                <div class="col-span-12 lg:col-span-4">
                                                    <InputText type="number" placeholder="Restock Threshold"
                                                        v-model="colorVariant.restockThreshold" />
                                                </div>
                                            </div>
                                            <Fieldset v-else>
                                                <template #legend>
                                                    <div class="flex items-center pl-2"
                                                        @click="addSizeVariant(colorVariantIndex)">
                                                        <div
                                                            :class="['w-8 h-8 mr-2 border border-surface-200 dark:border-surface-700 rounded-full cursor-pointer flex justify-center items-center bg-gray-500']">
                                                            <i class="pi pi-plus text-sm text-white z-50" />
                                                        </div>
                                                        <span class="font-bold p-2">Add a size</span>
                                                    </div>
                                                </template>
                                                <div v-for="(sizeVariant, index) in colorVariant.sizeVariants"
                                                    :key="index">
                                                    <Divider align="right"
                                                        v-if="index > 0 && index < colorVariant.sizeVariants.length"
                                                        type="dashed">

                                                        <div class="flex items-center pl-2"
                                                            @click="removeSizeVariant(colorVariantIndex, index)">
                                                            <div
                                                                :class="['w-6 h-6 mr-2 border border-surface-200 dark:border-surface-700 rounded-full cursor-pointer flex justify-center items-center bg-gray-500']">
                                                                <i class="pi pi-times text-sm text-white z-50" />
                                                            </div>
                                                        </div>
                                                    </Divider>


                                                    <div class="grid grid-cols-12 gap-3">
                                                        <div class="col-span-12 lg:col-span-6">
                                                            <InputText type="text" placeholder="Size"
                                                                v-model="sizeVariant.size" />
                                                        </div>
                                                        <div class="col-span-12 lg:col-span-6">
                                                            <InputText type="text" placeholder="Price"
                                                                v-model="sizeVariant.price" />
                                                        </div>
                                                        <div class="col-span-12 lg:col-span-6">
                                                            <InputText type="text" placeholder="Quantity"
                                                                v-model="sizeVariant.quantity" />
                                                        </div>
                                                        <div class="col-span-12 lg:col-span-6">
                                                            <InputText type="text" placeholder="Restock Threshold"
                                                                v-model="sizeVariant.restockThreshold" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </Fieldset>
                                            <Fieldset>
                                                <template #legend>
                                                    <div class="flex items-center pl-2"
                                                        @click="addColorVariantImage(colorVariantIndex)">
                                                        <div
                                                            :class="['w-8 h-8 mr-2 border border-surface-200 dark:border-surface-700 rounded-full cursor-pointer flex justify-center items-center bg-gray-500']">
                                                            <i class="pi pi-plus text-sm text-white z-50" />
                                                        </div>
                                                        <span class="font-bold p-2">Add an image</span>
                                                    </div>
                                                </template>
                                                <div v-for="(image, index) in colorVariant.images" :key="index">
                                                    <Divider align="right"
                                                        v-if="index > 0 && index < colorVariant.images.length"
                                                        type="dashed">

                                                        <div class="flex items-center pl-2"
                                                            @click="removeColorVariantImage(colorVariantIndex, index)">
                                                            <div
                                                                :class="['w-6 h-6 mr-2 border border-surface-200 dark:border-surface-700 rounded-full cursor-pointer flex justify-center items-center bg-gray-500']">
                                                                <i class="pi pi-times text-sm text-white z-50" />
                                                            </div>
                                                        </div>
                                                    </Divider>
                                                    <div class="grid grid-cols-12 gap-4 m-0">
                                                        <div class="col-span-12 lg:col-span-6">
                                                            <InputText type="text" placeholder="Image Src"
                                                                v-model="image.imageSrc" />
                                                        </div>
                                                        <div class="col-span-12 lg:col-span-6 m-0">
                                                            <InputText type="text" placeholder="Alt Text"
                                                                v-model="image.altText" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </Fieldset>
                                        </div>
                                    </TabPanel>
                                </TabPanels>
                            </Tabs>
                        </div>
                    </div>

                    <div class="col-span-12">
                        <Editor v-model="product.description" editorStyle="height: 320px" />
                    </div>

                </div>
                {{ product }}
            </div>
            <div class="col-span-12 lg:col-span-4 flex flex-col gap-y-4">
                <div class="border border-surface-200 dark:border-surface-700 rounded">
                    <span
                        class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Publish</span>
                    <div class="p-4">
                        <div class="bg-surface-100 dark:bg-surface-700 py-2 px-4 flex items-center rounded">
                            <span class="text-black/90 font-bold mr-4">Status:</span>
                            <span class="text-black/60 font-semibold">Draft</span>
                            <Button type="button" icon="pi pi-fw pi-pencil" class="ml-auto" text rounded />
                        </div>
                    </div>
                </div>

                <div class="border border-surface-200 dark:border-surface-700 rounded">
                    <span
                        class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Occasions</span>
                    <div class="p-4 flex flex-wrap gap-1">
                        <MultiSelect v-model="product.occasionIds" display="chip" :options="occasions"
                            optionLabel="name" placeholder="Select many" optionValue="id" :maxSelectedLabels="3"
                            class="w-full md:w-30rem" />
                    </div>
                </div>

                <div class="border border-surface-200 dark:border-surface-700 rounded">
                    <span
                        class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Categories</span>
                    <div class="p-4 flex flex-wrap gap-1">
                        <MultiSelect v-model="product.categoryIds" display="chip" :options="categories"
                            optionLabel="name" placeholder="Select many" :maxSelectedLabels="3"
                            class="w-full md:w-30rem" optionValue="id" />
                    </div>
                </div>
                <div class="border border-surface-200 dark:border-surface-700 rounded">
                    <span
                        class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Brand</span>
                    <div class="p-4">
                        <Select :options="brands" v-model="product.brandId" optionLabel="name" optionValue="id"
                            placeholder="Select a Brand" />
                    </div>
                </div>
                <div class="border border-surface-200 dark:border-surface-700 rounded">
                    <span
                        class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Collection</span>
                    <div class="p-4">
                        <Select :options="collections" v-model="product.collectionId" optionLabel="name"
                            optionValue="id" placeholder="Select a Collection" />
                    </div>
                </div>
                <div class="border border-surface-200 dark:border-surface-700 rounded">
                    <span
                        class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Material</span>
                    <div class="p-4">
                        <Select :options="materials" v-model="product.materialId" optionLabel="name" optionValue="id"
                            placeholder="Select a Material" />
                    </div>
                </div>



                <div class="border border-surface-200 dark:border-surface-700 rounded">
                    <span
                        class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Tags</span>
                    <div class="p-4 flex flex-wrap gap-1">
                        <Chip v-for="(tag, i) in product.tags" :key="i" :label="tag"
                            class="mr-2 py-2 px-4 text-surface-900 dark:text-surface-0 font-bold bg-surface-0 dark:bg-surface-900 border border-surface-200 dark:border-surface-700"
                            style="border-radius: 20px">
                            <span class="mr-4">{{ tag }}</span>
                            <span
                                class="flex items-center justify-center border border-surface-200 dark:border-surface-700 bg-gray-100 rounded-full cursor-pointer"
                                :style="{ width: '1.5rem', height: '1.5rem' }" @click="onRemoveTags(tag)">
                                <i class="pi pi-fw pi-times text-black/60" :style="{ fontSize: '9px' }" /> </span>
                        </Chip>
                    </div>
                </div>

                <div class="flex justify-between gap-4">
                    <Button class="flex-1" severity="danger" outlined label="Discard" icon="pi pi-fw pi-trash" />
                    <Button :disabled="isLoading" @click="saveProduct" class="flex-1" label="Publish"
                        icon="pi pi-fw pi-check" />
                </div>
            </div>
        </Fluid>
    </div>
</template>

<style scoped lang="scss">
.remove-file-wrapper:hover {
    .remove-button {
        display: flex !important;
    }
}
</style>

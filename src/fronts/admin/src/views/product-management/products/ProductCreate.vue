<script setup>
import { ref, computed, onMounted } from 'vue';
import { useBrandStore } from '@/stores/brandStore';
import { useCategoryStore } from '@/stores/categoryStore';
import { useCollectionStore } from '@/stores/collectionStore';
import { useMaterialStore } from '@/stores/materialStore';
import { useOccasionStore } from '@/stores/occasionStore';


const brandStore = useBrandStore();
const categoryStore = useCategoryStore();
const collectionStore = useCollectionStore();
const materialStore = useMaterialStore();
const occasionStore = useOccasionStore();


const brands = computed(() => brandStore.brands);
const categories = computed(() => categoryStore.categories);
const collections = computed(() => collectionStore.collections);
const materials = computed(() => materialStore.materials);
const occasions = computed(() => occasionStore.occasions);


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
    id: 'red', name: "Red", background: 'bg-gray-900'
}, {
    id: 'orange', name: "Orange", background: 'bg-orange-500'
}])

const colorOverlay = ref();
const selectedColor = ref(null);
const currentPanelColorIndex = ref(0);
const value = ref('0');
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

const onColorSelect = (col) => {
    selectedColor.value = col;
    if (!colorExist(selectedColor.value)) {
        const newColor = {
            color: selectedColor.value.name,
            background: selectedColor.value.background,
            images: [],
        };
        if (product.value.productType == 0) {
            newColor.sizeVariants = []
        } else {
            newColor.price = 0
            newColor.quantity = 0
            newColor.currency = "$",
                newColor.restockThreshold = 0
        }
        product.value.colorVariants.push(newColor);
    }
};

const onSeletTabPanel = (index) => {
    currentPanelColorIndex.value = index;
};



const selectMember = (member) => {
    selectedMember.value = member;
    op.value.hide();
}

const color1 = ref('bluegray');

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
                                        <div :class="['flex items-center gap-2', slotProps.class]"
                                            @click="slotProps.onClick" v-bind="slotProps.a11yAttrs">
                                            <div
                                                :class="['w-8 h-8 mr-2 border border-surface-200 dark:border-surface-700 rounded-full cursor-pointer flex justify-center items-center', colorVariant.background]">
                                                <i class="pi pi-check text-sm text-white z-50"
                                                    v-if="colorExist(colorVariant.color)" />
                                            </div>
                                            <span class="font-bold whitespace-nowrap">{{ colorVariant.name }}</span>
                                            <Badge value="2" />
                                        </div>
                                    </Tab>
                                </TabList>
                                <TabPanels>
                                    <TabPanel v-slot="slotProps"
                                        v-for="(colorVariant, colorVariantIndex) in product.colorVariants"
                                        :key="colorVariantIndex" :value="colorVariantIndex" asChild>
                                        <div v-show="slotProps.active" :class="slotProps.class"
                                            v-bind="slotProps.a11yAttrs">
                                            <p class="m-0">
                                                {{ colorVariant.color }}
                                            </p>
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
                            optionLabel="name" placeholder="Select many" :maxSelectedLabels="3"
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
                    <Button class="flex-1" label="Publish" icon="pi pi-fw pi-check" />
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

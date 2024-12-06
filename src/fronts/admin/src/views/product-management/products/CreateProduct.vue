<script setup>
import { ref, computed, onMounted, nextTick, watch } from 'vue';
import { useToast } from 'primevue/usetoast';
import { useConfirm } from "primevue/useconfirm";
import { useRouter } from 'vue-router';
import { useBrandStore } from '@/stores/brandStore';
import { useCategoryStore } from '@/stores/categoryStore';
import { useCollectionStore } from '@/stores/collectionStore';
import { useMaterialStore } from '@/stores/materialStore';
import { useOccasionStore } from '@/stores/occasionStore';
import { useProductStore } from '@/stores/productStore';
import { useLocalStorage } from '@/composables/useLocalStorage';

const confirm = useConfirm();
const toast = useToast();
const brandStore = useBrandStore();
const categoryStore = useCategoryStore();
const collectionStore = useCollectionStore();
const materialStore = useMaterialStore();
const occasionStore = useOccasionStore();
const productStore = useProductStore();
const router = useRouter();



const brands = computed(() => brandStore.brands);
const categories = computed(() => categoryStore.categories);
const collections = computed(() => collectionStore.collections);
const materials = computed(() => materialStore.materials);
const occasions = computed(() => occasionStore.occasions);
const isLoading = computed(() => productStore.isLoading);
const successed = computed(() => productStore.successed);
const error = computed(() => productStore.error);
const colorVariants = computed(() => productStore.colorVariants);

onMounted(() => {
    brandStore.fetchBrands()
    categoryStore.fetchCategories()
    collectionStore.fetchCollections()
    materialStore.fetchMaterials()
    occasionStore.fetchOccasions()
    productStore.fetchProducts()
    nextTick(() => {
        if (colorVariantTabRefs.value) {
            if (colorVariantTabRefs.value && colorVariantTabRefs.value.length) {
                colorVariantTabRefs.value[colorVariantTabRefs.value.length - 1].click()
            }
        }
    });
});

const productTypes = ref([{
    id: 0, name: "Clothing"
}, {
    id: 1, name: "Accessory"
}])

const status = ref([{
    id: 0, name: "Draft"
},
{
    id: 1, name: "Published"
},
{
    id: 1, name: "Deleted"
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
const removeColorVariantDialog = ref(false)
const colorOverlay = ref();
const selectedColor = ref(null);
const colorVariantTabRefs = ref([]);
const colorVariantIndex = ref(null)

const product = useLocalStorage(
    {
        "name": "Slim Fit Chino Pants",
        "urlFriendlyName": "slim-fit-chino-pants",
        "description": "Chino pants with a modern slim fit, versatile and stylish.",
        "shippingAndReturns": "Chino pants with a modern slim fit, versatile and stylish.",
        "code": "CODE1",
        "status": 0,
        "isHandmade": false,
        "onReorder": true,
        "coverImage": {
            "imageSrc": "https://placehold.co/300",
            "altText": "Slim Fit Chino Pants"
        },
        "productType": 0,
        "materialId": "a3addcb2-ce9c-4ca3-9068-6a8b8eccf711",
        "brandId": "b7bcda18-05cc-450b-b99a-3c5eafe111a4",
        "collectionId": "e6ef95a7-29f7-4ec4-8984-0d8602c94b27",
        "occasionIds": [
            "b3c6c410-d05a-4426-a6a6-2f086901d411"
        ],
        "categoryIds": [
            "6cbe22ca-900f-4b38-9030-368e0f89bc74"
        ],
        "colorVariants": [
            {
                "color": "Red",
                background: 'bg-red-500',
                "images": [
                    {
                        "imageSrc": "https://placehold.co/300",
                        "altText": "Red Chino Pants Front"
                    }
                ],
                "sizeVariants": [
                    {
                        "size": "S",
                        "price": 35,
                        "currency": "$",
                        "quantity": 10,
                        "restockThreshold": 5
                    },
                    {
                        "size": "M",
                        "price": 35,
                        "currency": "$",
                        "quantity": 7,
                        "restockThreshold": 5
                    },
                    {
                        "size": "L",
                        "price": 35,
                        "currency": "$",
                        "quantity": 5,
                        "restockThreshold": 5
                    }
                ]
            }
        ]
    },
    'newProduct'
);

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
            background: 'bg-' + selectedColor.value.id + '-500' //selectedColor.value.background,
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

const confirmRemoveColorVariant = async (index) => {
    colorVariantIndex.value = index
    removeColorVariantDialog.value = true
};

const removeColorVariant = async () => {

    const index = colorVariants.value.findIndex(_ => _.id === product.value.colorVariants[colorVariantIndex.value])
    product.value.colorVariants.splice(colorVariantIndex.value, 1)
    if (index > -1)
        colorVariants.value.splice(index, 1)

    removeColorVariantDialog.value = false
    await nextTick();
}


watch(successed, (newValue, oldValue) => {
    if (newValue === true && oldValue === false) {
        toast.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Product Updated',
            life: 3000,
        });
        router.push('/product-anagement/products/product-list')
    }
});

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
    <div class="card" v-if="product">
        <span class="block text-surface-900 dark:text-surface-0 font-bold text-xl mb-6">Create Product</span>
        <Fluid class="grid grid-cols-12 gap-4 flex-wrap">
            <div class="col-span-12 lg:col-span-8">
                <div class="grid grid-cols-12 gap-4">
                    <div class="col-span-12">
                        <label for="productName">Product Name</label>
                        <InputText id="productName" type="text" placeholder="Product Name" v-model="product.name" />
                    </div>
                    <div class="col-span-12">
                        <label for="urlFriendlyName">Product Url Friendly Name</label>
                        <InputText id="urlFriendlyName" type="text" placeholder="Product Url Friendly Name"
                            v-model="product.urlFriendlyName" />
                    </div>
                    <div class="col-span-12 lg:col-span-6">
                        <label for="productType">Product type</label>
                        <Select id="productType" :options="productTypes" v-model="product.productType"
                            optionLabel="name" optionValue="id" placeholder="Select a Type" />
                    </div>
                    <div class="col-span-12 lg:col-span-6">
                        <label for="productCode">Product Code</label>
                        <InputText id="productCode" type="text" placeholder="Product Code" label="Product Code"
                            v-model="product.code" />
                    </div>
                    <div class="col-span-12 lg:col-span-6" v-if="product.coverImage">
                        <label for="coverImageSrc">CoverImage Src</label>
                        <InputText id="coverImageSrc" type="text" placeholder="CoverImage Src" label="CoverImage Src"
                            v-model="product.coverImage.imageSrc" />
                    </div>
                    <div class="col-span-12 lg:col-span-6" v-if="product.coverImage">
                        <label for="coverImageAltText">CoverImage AltText</label>
                        <InputText id="coverImageAltText" type="text" placeholder="CoverImage AltText"
                            label="CoverImage AltText" v-model="product.coverImage.altText" />
                    </div>

                    <div class="col-span-12">
                        <Button type="button" icon="pi pi-plus" label="Add a color" @click="toggleColorOverlay"
                            class="min-w-48" />
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

                    <div class="col-span-12" v-if="product.colorVariants && product.colorVariants.length">
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

                                            <Toolbar>
                                                <template v-slot:start>
                                                    <h2> {{ colorVariant.color }}</h2>
                                                </template>

                                                <template v-slot:end>
                                                    <Button icon="pi pi-trash" class="mr-2" severity="danger" outlined
                                                        @click="confirmRemoveColorVariant(colorVariantIndex)" />
                                                </template>
                                            </Toolbar>
                                            <div class="grid grid-cols-12 gap-4" v-if="product.productType !== 0">
                                                <div class="col-span-12 lg:col-span-4">
                                                    <label :for="colorVariant.color + 'price'">Price</label>
                                                    <InputText :id="colorVariant.color + 'price'" type="number"
                                                        placeholder="Price" v-model="colorVariant.price" />
                                                </div>
                                                <div class="col-span-12 lg:col-span-4">
                                                    <label :for="colorVariant.color + 'quantity'">Quantity</label>
                                                    <InputText :id="colorVariant.color + 'quantity'" type="number"
                                                        placeholder="Quantity" v-model="colorVariant.quantity" />
                                                </div>
                                                <div class="col-span-12 lg:col-span-4">
                                                    <label :for="colorVariant.color + 'restockThreshold'">Restock
                                                        Threshold</label>
                                                    <InputText :id="colorVariant.color + 'restockThreshold'"
                                                        type="number" placeholder="Restock Threshold"
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
                                                            <label
                                                                :for="colorVariant.color + index + 'size'">Size</label>
                                                            <InputText :id="colorVariant.color + index + 'size'"
                                                                type="text" placeholder="Size"
                                                                v-model="sizeVariant.size" />
                                                        </div>
                                                        <div class="col-span-12 lg:col-span-6">
                                                            <label
                                                                :for="colorVariant.color + index + 'price'">Price</label>
                                                            <InputText :id="colorVariant.color + index + 'price'"
                                                                type="text" placeholder="Price"
                                                                v-model="sizeVariant.price" />
                                                        </div>
                                                        <div class="col-span-12 lg:col-span-6">
                                                            <label
                                                                :for="colorVariant.color + index + 'price'">Quantity</label>
                                                            <InputText :id="colorVariant.color + index + 'price'"
                                                                type="text" placeholder="Quantity"
                                                                v-model="sizeVariant.quantity" />
                                                        </div>
                                                        <div class="col-span-12 lg:col-span-6">
                                                            <label
                                                                :for="colorVariant.color + index + 'restockThreshold'">Restock
                                                                Threshold</label>
                                                            <InputText
                                                                :id="colorVariant.color + index + 'restockThreshold'"
                                                                type="text" placeholder="Restock Threshold"
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
                                                                :class="['w-6 h-6 border border-surface-200 dark:border-surface-700 rounded-full cursor-pointer flex justify-center items-center bg-gray-500']">
                                                                <i class="pi pi-times text-sm text-white z-50" />
                                                            </div>
                                                        </div>
                                                    </Divider>
                                                    <div class="grid grid-cols-12 gap-4 m-0">
                                                        <div class="col-span-12 lg:col-span-6">
                                                            <label :for="colorVariant.color + index + 'imageSrc'">Image
                                                                Src</label>
                                                            <InputText :id="colorVariant.color + index + 'imageSrc'"
                                                                type="text" placeholder="Image Src"
                                                                v-model="image.imageSrc" />
                                                        </div>
                                                        <div class="col-span-12 lg:col-span-6 m-0">
                                                            <label :for="colorVariant.color + index + 'altText'">Alt
                                                                Text</label>
                                                            <InputText :id="colorVariant.color + index + 'altText'"
                                                                type="text" placeholder="Alt Text"
                                                                v-model="image.altText" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </Fieldset>
                                            <Fieldset>
                                                <template #legend>
                                                    <div class="flex items-center pl-2">
                                                        <span class="font-bold p-2">Outfits</span>
                                                    </div>
                                                </template>
                                                <div class="col-span-12">
                                                    <MultiSelect :disabled="colorVariants.length === 0" id="outfit"
                                                        :options="colorVariants" filter v-model="colorVariant.outfitIds"
                                                        optionLabel="slug" optionValue="id"
                                                        placeholder="Select an Outfit" display="chip"
                                                        :maxSelectedLabels="3" class="w-full md:w-30rem">
                                                        <template #option="slotProps">
                                                            <div class="flex items-center">
                                                                <img :alt="slotProps.option.altText"
                                                                    :src="slotProps.option.imageSrc" class="mr-2"
                                                                    style="width: 18px" />
                                                                <div>{{ slotProps.option.slug }}</div>
                                                            </div>
                                                        </template>
                                                    </MultiSelect>
                                                </div>
                                            </Fieldset>
                                        </div>
                                    </TabPanel>
                                </TabPanels>
                            </Tabs>
                            <Dialog v-model:visible="removeColorVariantDialog" :style="{ width: '450px' }"
                                header="Confirm" :modal="true">
                                <div class="flex items-center gap-4">
                                    <i class="pi pi-exclamation-triangle !text-3xl" />
                                    <span v-if="colorVariantIndex != null">Are you sure you want to delete <b>{{
                                        product.colorVariants[colorVariantIndex].color
                                            }}</b>?</span>
                                </div>
                                <template #footer>
                                    <Button label="No" icon="pi pi-times" text
                                        @click="removeColorVariantDialog = false" />
                                    <Button label="Yes" icon="pi pi-check" @click="removeColorVariant" />
                                </template>
                            </Dialog>
                        </div>
                    </div>

                    <div class="col-span-12">
                        <label for="description">Description</label>
                        <Editor id="description" v-model="product.description" editorStyle="height: 320px" />
                    </div>

                    <div class="col-span-12">
                        <label for="shippingAndReturns">Shipping & Returns</label>
                        <Editor id="shippingAndReturns" v-model="product.shippingAndReturns"
                            editorStyle="height: 200px" />
                    </div>

                </div>
            </div>
            <div class="col-span-12 lg:col-span-4 flex flex-col gap-y-4">
                <div class="border border-surface-200 dark:border-surface-700 rounded">
                    <span
                        class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Publish</span>
                    <div class="p-4">
                        <div class="bg-surface-100 dark:bg-surface-700 py-2 px-4 flex items-center rounded">
                            <Select id="status" :options="status" v-model="product.status" optionLabel="name"
                                optionValue="id" placeholder="Select a Status" />
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

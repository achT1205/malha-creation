<script setup>
import { ref, computed, onMounted, nextTick, watch, reactive } from 'vue';
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
import { clothingProductSchema, accessoryProductSchema } from "@/validations/productValidation";


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
const errors = reactive({});
const product = useLocalStorage(
    {
        "name": "",
        "urlFriendlyName": "",
        "description": "",
        "shippingAndReturns": "",
        "code": "",
        "status": 0,
        "isHandmade": false,
        "coverImage": {
            "imageSrc": "",
            "altText": ""
        },
        "productType": 0,
        "materialId": null,
        "brandId": null,
        "collectionId": null,
        "occasionIds": [],
        "categoryIds": [],
        "colorVariants": []
    },
    'newProduct'
);

const toggleColorOverlay = (event) => {
    colorOverlay.value.toggle(event);
};

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
                "restockThreshold": 0,
                "currency": "$"
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
        "restockThreshold": 0,
        "currency": "$"
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

    if (colorVariantTabRefs.value && colorVariantTabRefs.value.length) {
        colorVariantTabRefs.value[colorVariantTabRefs.value.length - 1].click()
    }
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
            life: 5000,
        });
    }
});

const submitForm = () => {

    for (const key in errors) {
        delete errors[key];
    }

    try {
        const productSchema = product.value.productType === 0 ? clothingProductSchema : accessoryProductSchema
        productSchema.parse(product.value);
        productStore.addProduct(product.value)
    } catch (e) {
        if (e.errors) {

            e.errors.forEach((err) => {
                errors[err.path.join(".")] = err.message;
            });

        }
        console.log(errors);

    }
};

const onProductTypeChange = () => {
    product.value.colorVariants = [];
}

</script>

<template>
    <div class="card" v-if="product">
        <span class="block text-surface-900 dark:text-surface-0 font-bold text-xl mb-6">Create Product</span>
        <form @submit.prevent="submitForm">
            <Fluid class="grid grid-cols-12 gap-4 flex-wrap">

                <div class="col-span-12 lg:col-span-8">
                    <div class="grid grid-cols-12 gap-4">
                        <div class="col-span-12">
                            <label for="productName">Product Name</label>
                            <InputText maxlength="200" id="productName" type="text" placeholder="Product Name"
                                v-model="product.name" />
                            <Message v-if="errors.name" severity="error" size="small" variant="simple" class="mt-1">{{
                                errors.name }}</Message>
                        </div>
                        <div class="col-span-12">
                            <label for="urlFriendlyName">Product Url Friendly Name</label>
                            <InputText maxlength="200" id="urlFriendlyName" type="text"
                                placeholder="Product Url Friendly Name" v-model="product.urlFriendlyName" />
                            <Message v-if="errors.urlFriendlyName" severity="error" size="small" variant="simple"
                                class="mt-1">{{
                                    errors.urlFriendlyName }}</Message>
                        </div>
                        <div class="col-span-12 lg:col-span-6">
                            <label for="productType">Product type</label>
                            <Select id="productType" :options="productTypes" v-model="product.productType"
                                optionLabel="name" optionValue="id" placeholder="Select a Type"
                                @update:modelValue="onProductTypeChange" />
                            <Message v-if="errors.productType" severity="error" size="small" variant="simple"
                                class="mt-1">{{
                                    errors.productType }}</Message>
                        </div>
                        <div class="col-span-12 lg:col-span-6">
                            <label for="productCode">Product Code</label>
                            <InputText maxlength="20" id="productCode" type="text" placeholder="Product Code"
                                label="Product Code" v-model="product.code" />
                            <Message v-if="errors.code" severity="error" size="small" variant="simple" class="mt-1">{{
                                errors.code }}</Message>
                        </div>
                        <div class="col-span-12 lg:col-span-6" v-if="product.coverImage">
                            <label for="coverImageSrc">CoverImage Src</label>
                            <InputText maxlength="300" id="coverImageSrc" type="text" placeholder="CoverImage Src"
                                label="CoverImage Src" v-model="product.coverImage.imageSrc" />
                            <Message v-if="errors['coverImage.imageSrc']" severity="error" size="small" variant="simple"
                                class="mt-1">{{
                                    errors['coverImage.imageSrc'] }}</Message>
                        </div>
                        <div class="col-span-12 lg:col-span-6" v-if="product.coverImage">
                            <label for="coverImageAltText">CoverImage AltText</label>
                            <InputText maxlength="100" id="coverImageAltText" type="text"
                                placeholder="CoverImage AltText" label="CoverImage AltText"
                                v-model="product.coverImage.altText" />
                            <Message v-if="errors['coverImage.altText']" severity="error" size="small" variant="simple"
                                class="mt-1">{{
                                    errors['coverImage.altText'] }}</Message>
                        </div>
                        <div class="col-span-12">
                            <Button type="button" icon="pi pi-plus" label="Add a color" @click="toggleColorOverlay"
                                class="min-w-48" />
                            <Message v-if="errors.colorVariants" severity="error" size="small" variant="simple"
                                class="mt-1">{{
                                    errors.colorVariants }}</Message>
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
                                            :key="colorVariantIndex" v-slot="slotProps" :value="colorVariantIndex"
                                            asChild>
                                            <div ref="colorVariantTabRefs"
                                                :class="['flex items-center gap-2', slotProps.class]"
                                                @click="slotProps.onClick" v-bind="slotProps.a11yAttrs">
                                                <div
                                                    :class="['w-8 h-8 mr-2 border border-surface-200 dark:border-surface-700 rounded-full cursor-pointer flex justify-center items-center', colorVariant.background]">
                                                    <i class="pi pi-check text-sm text-white z-50"
                                                        v-if="slotProps.active" />
                                                </div>
                                                <span class="font-bold whitespace-nowrap">{{ colorVariant.color
                                                    }}</span>
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
                                                        <Button icon="pi pi-trash" class="mr-2" severity="danger"
                                                            outlined
                                                            @click="confirmRemoveColorVariant(colorVariantIndex)" />
                                                    </template>
                                                </Toolbar>
                                                <div class="grid grid-cols-12 gap-4" v-if="product.productType !== 0">
                                                    <div class="col-span-12 lg:col-span-4">
                                                        <label :for="colorVariant.color + 'price'">Price</label>
                                                        <InputNumber :id="colorVariant.color + 'price'" type="number"
                                                            placeholder="Price" v-model="colorVariant.price" />
                                                        <Message
                                                            v-if="errors[`colorVariants.${colorVariantIndex}.price`]"
                                                            severity="error" size="small" variant="simple" class="mt-1">
                                                            {{
                                                                errors[`colorVariants.${colorVariantIndex}.price`] }}
                                                        </Message>
                                                    </div>
                                                    <div class="col-span-12 lg:col-span-4">
                                                        <label :for="colorVariant.color + 'quantity'">Quantity</label>
                                                        <InputNumber :id="colorVariant.color + 'quantity'" type="number"
                                                            placeholder="Quantity" v-model="colorVariant.quantity" />
                                                        <Message
                                                            v-if="errors[`colorVariants.${colorVariantIndex}.quantity`]"
                                                            severity="error" size="small" variant="simple" class="mt-1">
                                                            {{
                                                                errors[`colorVariants.${colorVariantIndex}.quantity`] }}
                                                        </Message>
                                                    </div>
                                                    <div class="col-span-12 lg:col-span-4">
                                                        <label :for="colorVariant.color + 'restockThreshold'">Restock
                                                            Threshold</label>
                                                        <InputNumber :id="colorVariant.color + 'restockThreshold'"
                                                            type="number" placeholder="Restock Threshold"
                                                            v-model="colorVariant.restockThreshold" />
                                                        <Message
                                                            v-if="errors[`colorVariants.${colorVariantIndex}.restockThreshold`]"
                                                            severity="error" size="small" variant="simple" class="mt-1">
                                                            {{
                                                                errors[`colorVariants.${colorVariantIndex}.restockThreshold`]
                                                            }}
                                                        </Message>
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
                                                                <InputText maxlength="5"
                                                                    :id="colorVariant.color + index + 'size'"
                                                                    type="text" placeholder="Size"
                                                                    v-model="sizeVariant.size" />
                                                                <Message
                                                                    v-if="errors[`colorVariants.${colorVariantIndex}.sizeVariants.${index}.size`]"
                                                                    severity="error" size="small" variant="simple"
                                                                    class="mt-1">
                                                                    {{
                                                                        errors[`colorVariants.${colorVariantIndex}.sizeVariants.${index}.size`]
                                                                    }}
                                                                </Message>
                                                            </div>
                                                            <div class="col-span-12 lg:col-span-6">
                                                                <label
                                                                    :for="colorVariant.color + index + 'price'">Price</label>
                                                                <InputNumber :id="colorVariant.color + index + 'price'"
                                                                    placeholder="Price" v-model="sizeVariant.price" />
                                                                <Message
                                                                    v-if="errors[`colorVariants.${colorVariantIndex}.sizeVariants.${index}.price`]"
                                                                    severity="error" size="small" variant="simple"
                                                                    class="mt-1">
                                                                    {{
                                                                        errors[`colorVariants.${colorVariantIndex}.sizeVariants.${index}.price`]
                                                                    }}
                                                                </Message>
                                                            </div>
                                                            <div class="col-span-12 lg:col-span-6">
                                                                <label
                                                                    :for="colorVariant.color + index + 'price'">Quantity</label>
                                                                <InputNumber :id="colorVariant.color + index + 'price'"
                                                                    type="text" placeholder="Quantity"
                                                                    v-model="sizeVariant.quantity" />
                                                                <Message
                                                                    v-if="errors[`colorVariants.${colorVariantIndex}.sizeVariants.${index}.quantity`]"
                                                                    severity="error" size="small" variant="simple"
                                                                    class="mt-1">
                                                                    {{
                                                                        errors[`colorVariants.${colorVariantIndex}.sizeVariants.${index}.quantity`]
                                                                    }}
                                                                </Message>
                                                            </div>
                                                            <div class="col-span-12 lg:col-span-6">
                                                                <label
                                                                    :for="colorVariant.color + index + 'restockThreshold'">Restock
                                                                    Threshold</label>
                                                                <InputNumber
                                                                    :id="colorVariant.color + index + 'restockThreshold'"
                                                                    type="text" placeholder="Restock Threshold"
                                                                    v-model="sizeVariant.restockThreshold" />
                                                                <Message
                                                                    v-if="errors[`colorVariants.${colorVariantIndex}.sizeVariants.${index}.restockThreshold`]"
                                                                    severity="error" size="small" variant="simple"
                                                                    class="mt-1">
                                                                    {{
                                                                        errors[`colorVariants.${colorVariantIndex}.sizeVariants.${index}.restockThreshold`]
                                                                    }}
                                                                </Message>
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
                                                                <label
                                                                    :for="colorVariant.color + index + 'imageSrc'">Image
                                                                    Src</label>
                                                                <InputText maxlength="300"
                                                                    :id="colorVariant.color + index + 'imageSrc'"
                                                                    type="text" placeholder="Image Src"
                                                                    v-model="image.imageSrc" />
                                                                <Message
                                                                    v-if="errors[`colorVariants.${colorVariantIndex}.images.${index}.imageSrc`]"
                                                                    severity="error" size="small" variant="simple"
                                                                    class="mt-1">
                                                                    {{
                                                                        errors[`colorVariants.${colorVariantIndex}.images.${index}.imageSrc`]
                                                                    }}
                                                                </Message>
                                                            </div>
                                                            <div class="col-span-12 lg:col-span-6 m-0">
                                                                <label :for="colorVariant.color + index + 'altText'">Alt
                                                                    Text</label>
                                                                <InputText maxlength="50"
                                                                    :id="colorVariant.color + index + 'altText'"
                                                                    type="text" placeholder="Alt Text"
                                                                    v-model="image.altText" />
                                                                <Message
                                                                    v-if="errors[`colorVariants.${colorVariantIndex}.images.${index}.altText`]"
                                                                    severity="error" size="small" variant="simple"
                                                                    class="mt-1">
                                                                    {{
                                                                        errors[`colorVariants.${colorVariantIndex}.images.${index}.altText`]
                                                                    }}
                                                                </Message>
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
                                                            :options="colorVariants" filter
                                                            v-model="colorVariant.outfitIds" optionLabel="slug"
                                                            optionValue="id" placeholder="Select an Outfit"
                                                            display="chip" :maxSelectedLabels="3"
                                                            class="w-full md:w-30rem">
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
                            <Message v-if="errors.description" severity="error" size="small" variant="simple"
                                class="mt-1">{{
                                    errors.description }}</Message>
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
                            <Message v-if="errors.occasionIds" severity="error" size="small" variant="simple"
                                class="w-full md:w-30rem mt-1">{{
                                    errors.occasionIds }}</Message>
                        </div>
                    </div>

                    <div class="border border-surface-200 dark:border-surface-700 rounded">
                        <span
                            class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Categories</span>
                        <div class="p-4 flex flex-wrap gap-1">
                            <MultiSelect v-model="product.categoryIds" display="chip" :options="categories"
                                optionLabel="name" placeholder="Select many" :maxSelectedLabels="3"
                                class="w-full md:w-30rem" optionValue="id" />
                            <Message v-if="errors.categoryIds" severity="error" size="small" variant="simple"
                                class="w-full md:w-30rem mt-1">{{
                                    errors.categoryIds }}</Message>
                        </div>
                    </div>
                    <div class="border border-surface-200 dark:border-surface-700 rounded">
                        <span
                            class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Brand</span>
                        <div class="p-4">
                            <Select :options="brands" v-model="product.brandId" optionLabel="name" optionValue="id"
                                placeholder="Select a Brand" />
                            <Message v-if="errors.brandId" severity="error" size="small" variant="simple"
                                class="w-full md:w-30rem mt-1">{{
                                    errors.brandId }}</Message>
                        </div>
                    </div>
                    <div class="border border-surface-200 dark:border-surface-700 rounded">
                        <span
                            class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Collection</span>
                        <div class="p-4">
                            <Select :options="collections" v-model="product.collectionId" optionLabel="name"
                                optionValue="id" placeholder="Select a Collection" />
                            <Message v-if="errors.collectionId" severity="error" size="small" variant="simple"
                                class="w-full md:w-30rem mt-1">{{
                                    errors.collectionId }}</Message>
                        </div>
                    </div>
                    <div class="border border-surface-200 dark:border-surface-700 rounded">
                        <span
                            class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Material</span>
                        <div class="p-4">
                            <Select :options="materials" v-model="product.materialId" optionLabel="name"
                                optionValue="id" placeholder="Select a Material" />
                            <Message v-if="errors.materialId" severity="error" size="small" variant="simple"
                                class="w-full md:w-30rem mt-1">{{
                                    errors.materialId }}</Message>
                        </div>
                    </div>

                    <div class="border border-surface-200 dark:border-surface-700 rounded">
                        <span
                            class="text-surface-900 dark:text-surface-0 font-bold block border-b border-surface-200 dark:border-surface-700 p-4">Is
                            Handmade</span>
                        <div class="p-4 flex flex-wrap gap-1">
                            <div class="flex items-center gap-2">
                                <Checkbox v-model="product.isHandmade" inputId="isHandmade" name="isHandmade" binary />
                                <label for="isHandmade"> Is Handmade </label>
                            </div>
                        </div>
                    </div>

                    <div class="flex justify-between gap-4">
                        <Button class="flex-1" severity="danger" outlined label="Discard" icon="pi pi-fw pi-trash" />
                        <Button :disabled="isLoading" type="submit" class="flex-1" label="Publish"
                            icon="pi pi-fw pi-check" />
                    </div>
                </div>

            </Fluid>
        </form>
    </div>
</template>

<style scoped lang="scss">
.remove-file-wrapper:hover {
    .remove-button {
        display: flex !important;
    }
}
</style>

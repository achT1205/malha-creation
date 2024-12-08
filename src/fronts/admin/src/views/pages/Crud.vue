<template>
  <form @submit.prevent="submitForm">
    <div class="col-span-12">
      <label for="productName">Product Name</label>
      <InputText id="productName" v-model="product.name" />
      <p v-if="errors.name" class="error">{{ errors.name }}</p>
    </div>
    <div class="col-span-12">
      <label for="urlFriendlyName">Product Url Friendly Name</label>
      <InputText id="urlFriendlyName" v-model="product.urlFriendlyName" />
      <p v-if="errors.urlFriendlyName" class="error">{{ errors.urlFriendlyName }}</p>
    </div>
    <!-- Ajoutez les champs nécessaires ici -->
    <button type="submit">Submit</button>
  </form>
</template>

<script>
import { ref, reactive } from "vue";
import { z } from "zod";

// Schéma pour les images
const imageSchema = z.object({
  imageSrc: z.string().url("Image source must be a valid URL"),
  altText: z.string().min(1, "Alt text is required"),
});

// Schéma pour les variantes de taille
const sizeVariantSchema = z.object({
  size: z.string().min(1, "Size is required"),
  price: z.number().positive("Price must be greater than 0"),
  currency: z.string().min(1, "Currency is required"),
  quantity: z.number().int().nonnegative("Quantity must be a non-negative integer"),
  restockThreshold: z.number().int().nonnegative("Restock threshold must be a non-negative integer"),
});

// Schéma pour les variantes de couleur
const colorVariantSchema = z.object({
  color: z.string().min(1, "Color is required"),
  background: z.string().min(1, "Background is required"),
  images: z.array(imageSchema).min(1, "At least one image is required"),
  sizeVariants: z.array(sizeVariantSchema).min(1, "At least one size variant is required"),
});

// Schéma principal pour le produit
const productSchema = z.object({
  name: z.string().min(1, "Product name is required"),
  urlFriendlyName: z.string().min(1, "URL-friendly name is required"),
  description: z.string().optional(),
  shippingAndReturns: z.string().optional(),
  code: z.string().min(1, "Product code is required"),
  status: z.number().int(),
  isHandmade: z.boolean(),
  onReorder: z.boolean(),
  coverImage: imageSchema.optional(),
  productType: z.number().int(),
  materialId: z.string().uuid("Material ID must be a valid UUID"),
  brandId: z.string().uuid("Brand ID must be a valid UUID"),
  collectionId: z.string().uuid("Collection ID must be a valid UUID"),
  occasionIds: z.array(z.string().uuid("Occasion ID must be a valid UUID")),
  categoryIds: z.array(z.string().uuid("Category ID must be a valid UUID")),
  colorVariants: z.array(colorVariantSchema).min(1, "At least one color variant is required"),
});



export default {
  setup() {
    const product = ref({
      name: "Slim Fit Chino Pants",
      urlFriendlyName: "slim-fit-chino-pants",
      description: "Chino pants with a modern slim fit, versatile and stylish.",
      shippingAndReturns: "Chino pants with a modern slim fit, versatile and stylish.",
      code: "CODE1",
      status: 0,
      isHandmade: false,
      onReorder: true,
      coverImage: {
        imageSrc: "https://placehold.co/300",
        altText: "Slim Fit Chino Pants",
      },
      productType: 0,
      materialId: "a3addcb2-ce9c-4ca3-9068-6a8b8eccf711",
      brandId: "b7bcda18-05cc-450b-b99a-3c5eafe111a4",
      collectionId: "e6ef95a7-29f7-4ec4-8984-0d8602c94b27",
      occasionIds: ["b3c6c410-d05a-4426-a6a6-2f086901d411"],
      categoryIds: ["6cbe22ca-900f-4b38-9030-368e0f89bc74"],
      colorVariants: [
        {
          color: "Red",
          background: "bg-red-500",
          images: [
            {
              imageSrc: "https://placehold.co/300",
              altText: "Red Chino Pants Front",
            },
          ],
          sizeVariants: [
            {
              size: "S",
              price: 35,
              currency: "$",
              quantity: 10,
              restockThreshold: 5,
            },
          ],
        },
      ],
    });

    const errors = reactive({});

    const submitForm = () => {

      for (const key in errors) {
        delete errors[key];
      }


      try {
        productSchema.parse(product.value);
        alert("Product is valid!");
      } catch (e) {
        if (e.errors) {
          e.errors.forEach((err) => {
            errors[err.path.join(".")] = err.message;
          });
        }
      }
    };

    return { product, errors, submitForm };
  },
};
</script>

<style>
.error {
  color: red;
  font-size: 0.8rem;
}
</style>

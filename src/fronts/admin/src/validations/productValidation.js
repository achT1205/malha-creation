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
    //currency: z.string().min(1, "Currency is required"),
    quantity: z.number().int().nonnegative("Quantity must be a non-negative integer"),
    restockThreshold: z.number().int().nonnegative("Restock threshold must be a non-negative integer"),
});

// Schéma pour les variantes de couleur (Clothing)
const clothingColorVariantSchema = z.object({
    color: z.string().min(1, "Color is required"),
    background: z.string().min(1, "Background is required"),
    images: z.array(imageSchema).min(1, "At least one image is required"),
    sizeVariants: z.array(sizeVariantSchema).min(1, "At least one size variant is required"),
});

// Schéma pour les variantes de couleur (Accessory)
const accessoryColorVariantSchema = z.object({
    color: z.string().min(1, "Color is required"),
    background: z.string().min(1, "Background is required"),
    images: z.array(imageSchema).min(1, "At least one image is required"),
    price: z.number().positive("Price must be greater than 0"),
    //currency: z.string().min(1, "Currency is required"),
    quantity: z.number().int().nonnegative("Quantity must be a non-negative integer"),
    restockThreshold: z.number().int().nonnegative("Restock threshold must be a non-negative integer"),
});

// Schéma principal pour le produit
const clothingProductSchema = z.object({
    name: z.string().min(1, "Product name is required"),
    urlFriendlyName: z
        .string()
        .min(1, "URL-friendly name is required")
        .regex(/^[a-zA-Z0-9\-]+$/, "URL-friendly name must only contain alphanumeric characters and dashes."),
    description: z.string().min(1, "Product description is required"),
    shippingAndReturns: z.string().optional(),
    code: z.string().min(1, "Product code is required"),
    status: z.number().int(),
    isHandmade: z.boolean(),
    coverImage: imageSchema.required("Cover image is required"),
    productType: z.number().int(),
    materialId: z.string().uuid("Material ID must be a valid UUID").min(1, "Material is required"),
    brandId: z.string().uuid("Brand ID must be a valid UUID").min(1, "Brand is required"),
    collectionId: z.string().uuid("Collection ID must be a valid UUID").min(1, "Collection is required"),
    occasionIds: z
      .array(z.string().uuid("Occasion ID must be a valid UUID"))
      .min(1, "At least one occasion is required"),
    categoryIds: z
      .array(z.string().uuid("Category ID must be a valid UUID"))
      .min(1, "At least one category is required"),
    colorVariants: z.array(clothingColorVariantSchema).min(1, "At least one color variant is required"),
});

// Schéma principal pour le produit
const accessoryProductSchema = z.object({
    name: z.string().min(1, "Product name is required"),
    urlFriendlyName: z
        .string()
        .min(1, "URL-friendly name is required")
        .regex(/^[a-zA-Z0-9\-]+$/, "URL-friendly name must only contain alphanumeric characters and dashes."),
    description: z.string().min(1, "Product description is required"),
    shippingAndReturns: z.string().optional(),
    code: z.string().min(1, "Product code is required"),
    status: z.number().int(),
    isHandmade: z.boolean(),
    coverImage: imageSchema.required("Cover image is required"),
    productType: z.number().int(), // 0 = Clothing, 1 = Accessory
    materialId: z.string().uuid("Material ID must be a valid UUID").min(1, "Material is required"),
    brandId: z.string().uuid("Brand ID must be a valid UUID").min(1, "Brand is required"),
    collectionId: z.string().uuid("Collection ID must be a valid UUID").min(1, "Collection is required"),
    occasionIds: z
      .array(z.string().uuid("Occasion ID must be a valid UUID"))
      .min(1, "At least one occasion is required"),
    categoryIds: z
      .array(z.string().uuid("Category ID must be a valid UUID"))
      .min(1, "At least one category is required"),
    colorVariants: z.array(accessoryColorVariantSchema).min(1, "At least one color variant is required"),
});

export { clothingProductSchema, accessoryProductSchema };
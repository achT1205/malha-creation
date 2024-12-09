import { useForm } from 'vee-validate'
import { object, string } from 'yup'

export function useValidator() {
  const { errors, defineField, handleSubmit } = useForm({
    validationSchema: object({
      email: string().email('Le format du email est incorrect').max(200, `Le email ne peut pas dépaccer 200 caractères`).required(`L'email est requis`),
      passwordSimple: string().max(200, 'Le mot de passe ne peut pas dépaccer 200 caractères').required('Le mot de passe est requis'),
      password: string().min(8, 'Le mot de passe doit être au moins de 8 caractères').max(200, 'Le mot de passe ne peut pas dépaccer 200 caractères').required('Le mot de passe est requis'),
      firstName: string().min(2, 'Le Prénom doit être au moins de 2 caractères').max(200, 'Le Prénom ne peut pas dépaccer 200 caractères').required('Le Prénom est requis'),
      lastName: string().min(2, 'Le Nom doit être au moins de 2 caractères').max(200, 'Le Nom ne peut pas dépaccer 200 caractères').required('Le Nom est requis'),
      address : string().max(200, `L'adresse ne peut pas dépaccer 200 caractères`).required(`L'adresse est requise`),
      apartment: string().max(200, `Le détails de l'adresse ne peut pas dépaccer 200 caractères`),
      city: string().max(100, `La ville ne peut pas dépaccer 100 caractères`).required(`La Ville est requise`),
      country: string().max(100, `Le pays ne peut pas dépaccer 100 caractères`).required(`Le Pays est requis`),
      region: string().max(100, `La region ne peut pas dépaccer 100 caractères`).required(`La Region est requise`),
      postalCode: string().max(10, `Le code postal ne peut pas dépaccer 10 caractères`).required(`Le code postal est requis`),
      phone: string().max(15, `Le numéro de téléphone ne peut pas dépaccer 15 caractères`).required(`Le numéro de téléphone est requis`),
    })
  })
  
  const [email, emailAttrs] = defineField('email', {
    validateOnModelUpdate: false
  })
  
  const [password, passwordAttrs] = defineField('password', {
    validateOnModelUpdate: false
  })

  const [passwordSimple, passwordSimpleAttrs] = defineField('passwordSimple', {
    validateOnModelUpdate: false
  })
  
  const [firstName, firstNameAttrs] = defineField('firstName', {
    validateOnModelUpdate: false
  })

  const [lastName, lastNameAttrs] = defineField('lastName', {
    validateOnModelUpdate: false
  })

  const [address, addressAttrs] = defineField('address', {
    validateOnModelUpdate: false
  })

  const [apartment, apartmentAttrs] = defineField('apartment', {
    validateOnModelUpdate: false
  })

  const [city, cityAttrs] = defineField('city', {
    validateOnModelUpdate: false
  })

  const [country, countryAttrs] = defineField('country', {
    validateOnModelUpdate: false
  })

  const [region, regionAttrs] = defineField('region', {
    validateOnModelUpdate: false
  })

  const [postalCode, postalCodeAttrs] = defineField('postalCode', {
    validateOnModelUpdate: false
  })

  const [phone, phoneAttrs] = defineField('phone', {
    validateOnModelUpdate: false
  })
  

  return { 
    errors,
    emailAttrs, 
    passwordAttrs, 
    passwordSimpleAttrs, 
    password: password, 
    email, 
    handleSubmit, 
    passwordSimple, 
    firstName, 
    firstNameAttrs, 
    lastName, 
    lastNameAttrs, 
    address, 
    addressAttrs,
    apartment,
    apartmentAttrs,
    city, 
    cityAttrs,
    country, 
    countryAttrs,
    region, 
    regionAttrs,
    postalCode, 
    postalCodeAttrs,
    phone, 
    phoneAttrs
  };
}

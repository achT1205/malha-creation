
<script setup>
import { ref } from 'vue'
import BaseInput from '@/components/BaseInput.vue'
import { useForm } from 'vee-validate'
import { object, string } from 'yup'

const { errors, defineField, handleSubmit } = useForm({
  validationSchema: object({
    email: string()
      .email('Le format du email est incorrect')
      .max(200, `Le email ne peut pas dépaccer 200 caractères`)
      .required(`L'email est requis`)
  })
})

const [email, emailAttrs] = defineField('email', {
  validateOnModelUpdate: false
})

const user = ref({
  email: email
})

const onSubmit = handleSubmit((values) => {
  console.log(JSON.stringify(values, null, 2))
})
</script>

<template>
  <div class="flex min-h-full flex-1 flex-col justify-center py-12 sm:px-6 lg:px-8">
    <div class="sm:mx-auto sm:w-full sm:max-w-md">
      <h2 class="mt-6 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
        RÉINITIALISER LE MOT DE PASSE
      </h2>
    </div>

    <div class="mt-10 sm:mx-auto sm:w-full sm:max-w-[480px]">
      <div class="bg-white px-6 py-12 shadow sm:rounded-lg sm:px-12">
        <form class="space-y-6" @submit="onSubmit">
          <div>
            <label for="email" class="block text-sm font-medium leading-6 text-gray-900"
              >Adresse e-mail</label
            >
            <div class="mt-2">
              <BaseInput
                v-model="user.email"
                type="email"
                id="email"
                name="email-address"
                autocomplete="email"
                v-bind="emailAttrs"
                :invalid="errors.email ? true : false"
                class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-gray-600 sm:text-sm sm:leading-6"
              />
              <small class="mt-2 text-sm text-red-600" v-show="errors.email">{{
                errors.email
              }}</small>
            </div>
          </div>

          <div>
            <button
              type="submit"
              class="flex w-full justify-center rounded-md bg-gray-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-gray-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-gray-600"
            >
              Envoyer
            </button>
          </div>
        </form>
      </div>

      <p class="mt-10 text-center text-sm text-gray-500">
        <router-link to="/login" class="font-semibold leading-6 text-gray-600 hover:text-gray-500"
          >Retour à la page de connexion</router-link
        >
      </p>
    </div>
  </div>
</template>

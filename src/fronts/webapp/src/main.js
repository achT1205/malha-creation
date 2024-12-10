import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

import Toast from './components/Toast.vue';

const app = createApp(App)
app.component('Toast', Toast);
app.use(createPinia())
app.use(router)

app.mount('#app')

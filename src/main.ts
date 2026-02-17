import './assets/main.css'
import PrimeVue from 'primevue/config'
import ToastService from 'primevue/toastservice'
import 'primeicons/primeicons.css'
import Aura from '@primeuix/themes/aura'
import { usePreset, updatePrimaryPalette } from '@primeuix/themes'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(ToastService)

app.use(PrimeVue, {
  theme: {
    preset: usePreset(
      Aura,
      updatePrimaryPalette({
        500: 'var(--app-gradient)',
        600: 'var(--app-gradient)',
        700: 'var(--app-gradient)',
      }),
    ),
  },
})

app.mount('#app')

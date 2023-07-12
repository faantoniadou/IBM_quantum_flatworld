import { createApp } from 'vue'
import App from './App.vue'
import './assets/tailwind.css'
import PrimeVue from 'primevue/config';
import './styles/custom.css';
import { router } from "./router";

import "primeflex/primeflex.css";
import "primevue/resources/themes/lara-light-blue/theme.css";
import "primevue/resources/primevue.min.css"; /* Deprecated */
import "primeicons/primeicons.css";

const app = createApp(App);

import Dropdown from 'primevue/dropdown';

app.use(router);
app.component('Dropdown', Dropdown);


createApp(App).mount('#app')
app.use(PrimeVue);

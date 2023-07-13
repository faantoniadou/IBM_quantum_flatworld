import { createApp } from 'vue';
import App from './App.vue';
import './assets/tailwind.css';
import './styles/custom.css';
import PrimeVue from 'primevue/config';

// import { router } from "./router";
// import { app } from 'electron';

import 'primeflex/primeflex.css';
import 'primevue/resources/themes/lara-light-blue/theme.css';
import 'primevue/resources/primevue.min.css'; /* Deprecated */
import 'primeicons/primeicons.css';

import Dropdown from 'primevue/dropdown';

const app = createApp(App);
app.use(PrimeVue);


app.component('Dropdown', Dropdown);
app.mount('#app');
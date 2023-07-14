import { createApp } from 'vue';
import App from './App.vue';
import './assets/tailwind.css';
import './styles/custom.css';
import PrimeVue from 'primevue/config';

import { router } from './router/index.js';
// import routes from './router/index.js';


// primevue
import 'primeicons/primeicons.css';
import './styles/_theme.scss';

import Menubar from 'primevue/menubar';
import Button from 'primevue/button';
import Card from 'primevue/card';

const app = createApp(App);

app.use(PrimeVue);
app.use(router);

app.component('Menubar', Menubar);
app.component('Button', Button);
app.component('Card', Card);

app.mount('#app');


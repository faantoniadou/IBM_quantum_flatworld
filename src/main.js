import { createApp } from 'vue';
import App from './App.vue';
import './assets/tailwind.css';
import './styles/custom.css';
import PrimeVue from 'primevue/config';
import './styles/_theme.scss';

import { router } from './router';
// import routes from './router/index.js';
import 'primevue/resources/themes/saga-blue/theme.css';
import 'primevue/resources/primevue.min.css';
import 'primeicons/primeicons.css';



// primevue

import Menubar from 'primevue/menubar';
import Button from 'primevue/button';
import Card from 'primevue/card';
import RadioButton from 'primevue/radiobutton';
import Carousel from 'primevue/carousel';


const app = createApp(App);

app.use(PrimeVue);
app.use(router);

app.component('Menubar', Menubar);
app.component('Carousel', Carousel);
app.component('Button', Button);
app.component('Card', Card);
app.component('RadioButton', RadioButton);

app.mount('#app');


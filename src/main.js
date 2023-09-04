import { createApp } from 'vue';
import App from './App.vue';
import './assets/tailwind.css';
import './styles/custom.css';
import PrimeVue from 'primevue/config';
import './styles/_theme.scss';

import { router } from './router';
// import { config } from 'vue/compiler-dom'

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
import Divider from 'primevue/divider';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import ColumnGroup from 'primevue/columngroup';
import Row from 'primevue/row';

const app = createApp(App);

app.use(PrimeVue);
app.use(router);

app.component('Menubar', Menubar);
app.component('Carousel', Carousel);
app.component('Button', Button);
app.component('Card', Card);
app.component('RadioButton', RadioButton);
app.component('Divider', Divider);
app.component('DataTable', DataTable);
app.component('Column', Column);
app.component('ColumnGroup', ColumnGroup);
app.component('Row', Row);

app.config.isCustomElement = (tag) => tag.startsWith('Unity-');

app.config.allowedNonProps = ['UnityWebgl', 'UnityLoader'];

app.config.warnHandler = function(msg, vm, trace) {
  // `trace` is the component hierarchy trace
  if (msg.includes('Missing required prop: "category"' || 'Failed to resolve component: h:outputStylesheet')) {
    return;
  }
  console.warn(msg + trace);
};

app.mount('#app');

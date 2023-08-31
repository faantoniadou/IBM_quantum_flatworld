import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Learning from '../views/Learning.vue';
import QiskitSchematicsTable from '../views/QiskitSchematicsTable.vue';

const routes = [
  { path: '/', name: 'Home', component: Home },
  { path: '/learning', name: 'Learning', component: Learning },
  {
    path: '/qiskit-schematics-table',
    name: 'QiskitSchematicsTable',
    component: QiskitSchematicsTable,
  },
];

export default routes;

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export { router };

import { createRouter, createWebHistory } from 'vue-router';
import Learning from '../views/Learning.vue';


const routes = [
  {
    path: '/learning',
    name: 'Learning',
    component: Learning,
  },
];

export default routes;

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
}); 

export { router };
import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Learning from '../views/Learning.vue';


// const Learning = () => import('../views/Learning.vue');

const routes = [
  {path: '/', name: 'Home', component: Home},
  {path: '/learning', name: 'Learning', component: Learning},

];

export default routes;

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
}); 

export { router };
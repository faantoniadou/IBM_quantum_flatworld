import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Learning from '../views/Learning.vue';
// import QuantumComputerGame from '../views/QuantumComputerGame.vue';

// const Learning = () => import('../views/Learning.vue');

const routes = [
  {path: '/', name: 'Home', component: Home},
  {path: '/learning', name: 'Learning', component: Learning},
  // {path: '/quantum-computer', name: 'QuantumComputerGame', component: QuantumComputerGame},
];

export default routes;

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
}); 

export { router };
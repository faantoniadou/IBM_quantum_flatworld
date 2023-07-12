// import Vue from 'vue';
// import Router from 'vue-router';
// import Home from './components/Home.vue';


// Vue.use(Router);
// // undefined property 'use' of undefined object 'Vue'
// // fix: npm install vue-router and if that doesn't work, npm install @vue/cli-service

// export default new Router({
//   routes: [
//     {
//       path: '/',
//       name: 'home',
//       component: Home
//     },
//     {
//       path: '/about',
//       name: 'about',
//       // route level code-splitting
//       // this generates a separate chunk (about.[hash].js) for this route
//       // which is lazy-loaded when the route is visited.
//       component: () =>
//         import(/* webpackChunkName: "about" */ './components/About.vue')
//     }
//   ]
// });

import { createRouter, createWebHistory } from "vue-router";
import App from "./App.vue";

export const router = createRouter({
  history: createWebHistory(),
  routes: [{ path: "/", component: App }]
});
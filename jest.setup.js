import axios from 'axios';

const BASE_URL = `process.env.VUE_APP_API_BASE_URL` || 'http://localhost:8081';
axios.defaults.baseURL = 'http://127.0.0.1:8080';

<template>
  <h:outputStylesheet name="primeicons/primeicons.css" library="primefaces"/>
  
  <div class="back-button-container" style="position: fixed; bottom: 0; left: 0; padding: 30px;">
    <Button @click="goBack" class="p-button-lg" icon="pi pi-chevron-left" rounded aria-label="Back"></Button>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';

const router = useRouter();

const goBack = () => {
  router.go(-1);
}
</script>

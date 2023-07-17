<!-- BackButton.vue -->
<template>
  <h:outputStylesheet name="primeicons/primeicons.css" library="primefaces"/>

  <div class="back-button-container" style="position: fixed; bottom: 0; left: 0; padding: 30px;">
    <Button @click="goBack" class="p-button-lg" icon="pi pi-chevron-left" rounded aria-label="Back"></Button>
  </div>
</template>

<script>
export default {
  name: 'BackButton',
  methods: {
    goBack() {
      this.$router.go(-1)
    },
  },
  data() {
    return {
      items: [
        {
          label: 'File',
          items: [
            // { label: 'New', icon: PrimeIcons.PLUS },
            // { label: 'Open', icon: PrimeIcons.DOWNLOAD }
          ]
        }
      ]
    };
  }
}
</script>

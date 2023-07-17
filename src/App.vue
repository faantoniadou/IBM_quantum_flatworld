<template>
  <div class="container">
    <MenubarComponent/>
    <router-view>
      
    </router-view>
  </div>
</template>



<script>
import MenubarComponent from './components/MenubarComponent.vue'

export default {
  name: 'App',
  components: {
    MenubarComponent,
  }
}
</script>



<style lang="scss">
@import "./styles/custom.css";
@import "~primevue/resources/primevue.min.css";
@import "~primevue/resources/themes/lara-dark-purple/theme.css";
@import "./styles/_theme.scss";


</style>
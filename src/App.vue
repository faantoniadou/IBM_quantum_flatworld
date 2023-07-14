<template>
  <MenubarComponent/>
  <div class="container" style="display: flex; flex-direction: column; align-items: center; justify-content: center; margin: 1-px auto; width: 100%;">
    <div class="card" style="display: flex; justify-content: center; width: 650px; height: 100px;">
      <Card style="width: 20em; padding: 0px; font-size: 32px; margin-top: 10px;">
        <template #header>
          I am interested in...
        </template>
      </Card>
    </div>
  </div>
  <div class="button-container">
      <p style="padding: 20px; text-align: center;">
        <router-link :to="'/learning'">
        <Button label="Learning" :loading="loading" :to="'/learning'" class="p-button-lg" style="font-size: 32px; width: 600px; height: 120px;" outlined/>
      </router-link>
      </p>
    <div class="container-lg" style="padding: 10px; width: 100%;">
      <p style="padding: 20px; text-align: center;">
        <Button label="Teaching" :loading="loading" @click="load" class="p-button-lg" style="font-size: 32px; width: 600px; height: 120px;" disabled outlined/>
      </p>
      <p style="padding: 20px; text-align: center;">
        <Button label="Demo" :loading="loading" @click="load" class="p-button-lg" style="font-size: 32px; width: 600px; height: 120px;" disabled outlined/>
      </p>
    </div>
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
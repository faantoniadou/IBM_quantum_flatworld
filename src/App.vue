<template>
  <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
  <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
  <link rel="stylesheet" href="path_to_your_stylesheet/primeicons.css">
    <MenubarComponent/>
    <router-view>

    </router-view>
</template>



<script>
import MenubarComponent from './components/MenubarComponent.vue'

export default {
  name: 'App',
  components: {
    MenubarComponent
  },
}
</script>



<style lang="scss">

</style>
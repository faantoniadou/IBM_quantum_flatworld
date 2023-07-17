<template>
  <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
  <h:outputStylesheet name="primeicons/primeicons.css" library="primefaces"/>
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
  }
}
</script>



<style lang="scss">

</style>
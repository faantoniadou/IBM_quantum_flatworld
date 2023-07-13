

<template>
  <HelloWorld msg="Welcome to Your Vue.js App"/>
  <MenubarComponent/>
</template>

<script>
import HelloWorld from './components/Home.vue'
import MenubarComponent from './components/MenubarComponent.vue'


export default {
  name: 'App',
  components: {
    HelloWorld,
    MenubarComponent
  }
}
</script>


<style lang="scss">
  @import "./styles/custom.css"
</style>
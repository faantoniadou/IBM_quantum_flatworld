

<template>
  <HelloWorld msg="Welcome to Your Vue.js App"/>
  <DropdownComponent/>
</template>

<script>
import HelloWorld from './components/Home.vue'
import DropdownComponent from './components/DropdownComponent.vue'

export default {
  name: 'App',
  components: {
    HelloWorld,
    DropdownComponent
  }
}
</script>


<style lang="scss">


</style>
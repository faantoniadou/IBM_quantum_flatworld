<!-- This is the learning page  -->
<template>
  <div class="container">
    <div class="Learning">
    </div>
  </div>
  <BackButton/>
</template>

<script>
import BackButton from '../components/BackButton.vue'

export default {
  name: 'Learning',
  data() {
    return {
      loading: false,
    };
  },
  methods: {
    load() {
      this.loading = true;
      // do something here, then set loading back to false when finished
    },
  },
  components: {
    BackButton,
  }
}
</script>
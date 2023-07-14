<!-- This is the learning page  -->
<template>
  <div class="Learning">
    <h1>Learning Page</h1>
  </div>
</template>

<script>
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
}
</script>
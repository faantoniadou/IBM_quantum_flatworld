<!-- footer component -->

<template>
<!-- create the footer -->
  <div class="footer">
    <div class="footer-content">
      <div class="footer-section about">
        <h1 class="logo-text"><span></span></h1>
        <p>
        </p>
        <div class="contact">
          <span><i class="fas fa-phone"></i> &nbsp; [Phone number]</span>
          <span><i class="fas fa-envelope"></i> &nbsp;</span>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: 'Footer',
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

<template>
  <div class="container" style="display: flex; flex-direction: column; align-items: center; justify-content: center; margin: 1px auto; width: 100%;">
    <div class="card" style="display: flex; justify-content: center; width: 650px; height: 100px;">
      <Card style="width: 20em; padding: 0px; font-size: 32px; margin-top: 10px;">
        <template #header>
          I am interested in...
        </template>
      </Card>
    </div>
    <div class="button-container" style="display: flex; flex-direction: column; align-items: center; justify-content: center;">
      <p style="padding: 20px; text-align: center;">
        <router-link to="/learning">
          <Button label="Learning" :loading="loading" class="p-button-lg" style="font-size: 32px; width: 600px; height: 120px;" outlined/>
        </router-link>
      </p>
      <p style="padding: 20px; text-align: center;">
        <router-link to="/teaching">
          <Button label="Teaching" :loading="loading" class="p-button-lg" style="font-size: 32px; width: 600px; height: 120px;" outlined/>
        </router-link>
      </p>
      <p style="padding: 20px; text-align: center;">
        <Button label="Demo" :loading="loading" @click="load" class="p-button-lg" style="font-size: 32px; width: 600px; height: 120px;" disabled outlined/>
      </p>
    </div>
  </div>


</template>


<script>
export default {
  name: 'Home',
  data() {
    return {
      loading: false,
    };
  },
  methods: {
    load() {
      this.loading = true;
    },
  },
}
</script>
<template>
  <div class="card flex align-items-center justify-content-center">
    <Card style="width: 25em">
      <template #title>{{ title }}</template>
      <template #content>
        <p>
          {{ description }}
        </p>
      </template>
    </Card>
  </div>
</template>

<script>
export default {
  name: 'CourseCard',
  props: {
    title: {
      type: String,
      required: true
    },
    description: {
      type: String,
      required: true
    },
    // image: {
    //   type: String,
    //   required: true
    // },
    // add more props as necessary
  }
}
</script>

<template>
  <div class="card flex align-items-center justify-content-center">
    <Card :pt="{ body: { class: 'bg-primary border-round-lg' } }" style="width: 25em;">
      <template #title>
        <div style="display: flex; align-items: center;">
          <Button icon="material-icons" rounded style="margin-right: 1em; width: 60px; height: 40px;"><i class="material-icons">school</i></Button>
          <span>{{ title }}</span>
        </div>
      </template>
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
    creator: {
      type: String,
      required: false
    },
    image: {
      type: String,
      required: false
    },
    category: {
      type: String,
      required: false
    },
    // add more props as necessary
  }
}
</script>

<style scoped>
.material-symbols-outlined {
  font-variation-settings:
  'FILL' 0,
  'wght' 400,
  'GRAD' 0,
  'opsz' 48
}

::v-deep .p-card-body {
  background-color: #4E307A;
  color: white;
  /*  change the curvature of the corners */
  border-radius: 2rem;
  padding: 2rem;
  /* change the transparency */
  opacity: 0.7;
}

.material-symbols-outlined {
  font-family: 'Material Symbols Outlined';
  font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 48;
}

.icon-title-container {
  display: flex;
  align-items: center;
}

.mr-2 {
  margin-right: 0.5rem; /* Adjust as needed */
}
</style>

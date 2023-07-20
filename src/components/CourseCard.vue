<template>
  <Card :pt="{ body: { class: 'bg-primary border-round-lg' } }" class="card">
    <template #title>
        <div class="container" style="justify-content: space-between;">
          <Button icon="material-icons" class="course-icon" style="width: 44px; margin-right: 10px;" rounded>
            <i class="material-icons course-icon-image">school</i>
          </Button>
          <!-- align the title to the right -->
          <span class="course-title">{{ title }}</span>
        </div>
      </template>
    <template #content>
      <p>
        {{ description }}
      </p>
    </template>
  </Card>
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

.card {
  width: calc(100% - 2em); /* Adjust the width to fit 3 cards in a row */
  height: 100%;
  margin: 0.5em;
}

/* Media query for larger screens: 2 cards per row and 1 card for smaller screens */

@media (min-width: 576px) { 
  .card {
    width: calc(50% - 2em); 
  }
}

@media (min-width: 1092px) { 
  .card {
    width: calc(33.333% - 2em); 
  }
}


.course-icon {
  width: 59px;
}

.course-title {
  text-align: right;
}

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
  width: 100%;
}

.mr-2 {
  margin-right: 0.5rem; /* Adjust as needed */
}
</style>

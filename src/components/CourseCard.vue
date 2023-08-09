<template>
  <Card :pt="{ body: { class: 'bg-primary border-round-lg' } }" class="card">
    <template #title>
        <div class="title-container">
          <!-- <Button icon="material-icons" class="course-icon" style="width: 44px; margin-right: 10px;" rounded>
            <i class="material-icons course-icon-image">school</i>
          </Button> -->
          <!-- align the title to the right -->
          <span class="course-title">{{ title }}</span>
        </div>
      </template>
    <template #content>
      <p>
        {{ description }}
      </p>
    </template>
    <template #footer>
      <Button label="Start" class="start-button" style="position: relative;" outlined @click="startCourse" />
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
  },

  methods: {
    startCourse() {
    // console.log("Emitting start-course with title:", this.title);
    this.$emit('start-course', this.title);
    }
  }
}
</script>



<style scoped>

.card {
  width: calc(100% - 2em);
  margin: 0.5em;
  position: relative; /* Set the card to be the reference for absolute positioning */
  padding-bottom: 50px; /* Space for the footer button, adjust as needed */
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
  border-radius: 2rem;
  padding: 2rem;
  opacity: 0.7;
  height: 100%;
}

.mr-2 {
  margin-right: 0.5rem;
}

.card-footer {
  position: absolute; /* Position the footer absolutely */
  bottom: 10px; /* Position it 10px from the bottom of the card */
  right: 10px; /* Position it 10px from the right of the card */
}
</style>

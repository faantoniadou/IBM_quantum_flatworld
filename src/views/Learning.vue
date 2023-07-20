<template>
  <div class="container">
    <div class="Learning">
      <div class="card" style="display: flex; justify-content: center; width: 750px; height: 100px;">
        <Card style="width: 14em; padding: 0px; font-size: 32px; margin-top: 10px;">
          <template #header>
            Course Catalogue
          </template>
        </Card>
      </div>
    </div>
  </div>
  <div class="container course-container" style="justify-content: flex-start; display: flex; padding: 30px;">
    <CourseCard v-for="course in courses" :key="course.id" :title="course.title" :description="course.description" :image="course.image" />
  </div>

  <BackButton/>

</template>


<script>
// import { ref } from "vue";
import BackButton from '../components/BackButton.vue'
import CourseCard from '../components/CourseCard.vue'

export default {
  name: 'Learning',
  setup() {
    return {
      loading: false,
      courses: [
        {
          id: 1,
          title: "Introduction to Quantum Computing",
          description: "Learn the basics of computer science, including programming languages, algorithms, data structures, and more.s",
          image: "https://example.com/images/computer-science.jpg"
        },
        {
          id: 2,
          title: "The Quantum Computer",
          description: "Learn the basics of computer science, including programming languages, algorithms, data structures, and more.s",
          image: "https://example.com/images/computer-science.jpg"
        },
        {
          id: 3,
          title: "Quantum Algorithms",
          description: "Learn the basics of computer science, including programming languages, algorithms, data structures, and more.s",
          image: "https://example.com/images/computer-science.jpg"
        },
        {
          id: 4,
          title: "Quantum Cryptography",
          description: "Learn the basics of computer science, including programming languages, algorithms, data structures, and more.s",
          image: "https://example.com/images/computer-science.jpg"
        }
      ]
    };
  },
  methods: {
    load() {
      this.loading = true;
    },
  },
  components: {
    BackButton,
    CourseCard,
  }
}
</script>

<style scoped>

.course-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: flex-start; /* Adjust this */
}


/* Each course card will take up roughly 1/3 of the container's width, minus a little for spacing */
.CourseCard {
  flex: 0 0 calc(33.333% - 20px); /* The "20px" is for spacing between the cards */
  margin: 10px; /* Adds a little space around each card */
}


</style>
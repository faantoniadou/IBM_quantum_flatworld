<template>
  <div class="container">
    <div class="Learning">
      <div class="card" style="display: flex; justify-content: center; width: 650px; height: 100px;">
        <Card style="width: 14em; padding: 0px; font-size: 32px; margin-top: 10px;">
          <template #header>
            Course Catalogue
          </template>
        </Card>
      </div>
    </div>
  </div>

  <div>
    <CourseCard
      v-for="course in courses"
      :key="course.id"
      :title="course.title"
      :description="course.description"
    />  
  </div>

  <BackButton/>

</template>


<script>
import BackButton from '../components/BackButton.vue'
import CourseCard from '../components/CourseCard.vue'


export default {
  name: 'Learning',
  data() {
    return {
      loading: false,
      courses: [
        {
          id: 1,
          title: "Introduction to Computer Science",
          description: "Learn the basics of computer science, including programming languages, algorithms, data structures, and more.",
          image: "https://example.com/images/computer-science.jpg"
        }
      ]
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
    CourseCard
  }
}
</script>
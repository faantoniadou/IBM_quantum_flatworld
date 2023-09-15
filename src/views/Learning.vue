<template>
  <div class="container">
    <div class="Learning">
      <div class="card" style="display: flex; justify-content: center; width: 750px; height: 100px;">
        <Card style="width: 14em; padding: 0px; font-size: 32px; margin-top: 10px;">
          <template #header>
            <span class="course-catalogue-header">Course Catalogue</span>
          </template>
        </Card>


      </div>
    </div>
  </div>
  <div class="course-container" style="margin-left: 100px;">
    <div v-for="(courseGroup, category) in groupedCourses" :key="category" class="category-container">
      <Divider align="left" type="solid">
        <b>{{ category }}</b>
      </Divider>
    <div class="courses">
      <!-- <div class="course-container" style="margin-left: 100px;"> -->
        <CourseCard 
          v-for="course in courseGroup" 
          :key="course.id" 
          :title="course.title" 
          :description="course.description" 
          @start-course="checkCourse"
        />
      </div>
    </div>
  </div>

  <BackButton/>


</template>


<script>
import { ref, computed, onMounted } from "vue";
import BackButton from '../components/BackButton.vue';
import CourseCard from '../components/CourseCard.vue';
import { useRouter } from 'vue-router';
import axios from 'axios';


axios.defaults.baseURL = process.env.VUE_APP_API_BASE_URL;

export default {
  name: 'Learning',
  props: {
    initialCourses: {
      type: Array,
      default: () => []
    }
  },

  components: {
    BackButton,
    CourseCard,
  },

  setup(props) {

    const courses = ref(props.initialCourses);

    async function fetchCourses() {
      try {
        const response = await axios.get('/courses');
        courses.value = response.data;
      } catch (error) {
        console.error('Error fetching courses:', error);
      }
    }

    onMounted(() => {
      fetchCourses();
    });

    const groupedCourses = computed(() => {
      return courses.value.reduce((acc, course) => {
        if (!acc[course.category]) {
          acc[course.category] = [];
        }
        acc[course.category].push(course);
        return acc;
      }, {});
    });

    const router = useRouter();

    // Method to check the course title and show the game
    const checkCourse = (title) => {
      // if title is in the course titles array, open the game )
      // uncomment the below when no placeholder courses are needed
      // if(courses.value.some(course => course.title === title)) {
      //   if (window.ipcRenderer) {
      //       window.ipcRenderer.send('open-unity-window', title);
      //       // console.log(`sent message to open unity window to open ${title}`)
      //   } else {
      //     console.log('ipcRenderer does not exist');
      //   }
      // } else if (title === "Qiskit Schematics") {
      //   router.push('/qiskit-schematics-table');
      // };
      if(title === "The Quantum Computer" || title === "The Bloch Sphere") {
        if (window.ipcRenderer) {
            window.ipcRenderer.send('open-unity-window', title);   // sends message to preload.js to open unity window through ipcRenderer which is listening for this message using ipcMain 
            // console.log(`sent message to open unity window to open ${title}`)
        } else {
          console.log('ipcRenderer does not exist');
        }
      } else if (title === "Qiskit Schematics") {
        router.push('/qiskit-schematics-table');
      };
    };

    // Return reactive properties and methods to the template
    return {
      courses,
      checkCourse,
      groupedCourses
    };
  },
};

</script>

<style scoped>

.category-container {
  margin-bottom: 20px; /* Adjust as needed */
  width: 100%;
}

.courses {
  display: flex;
  flex: wrap;
}

.course-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}


.CourseCard {
  max-width: 500px; /* or whatever max width you prefer */
  width: 100%;
  margin: 0.5em 0; /* Adjust as needed */
}

/* Media query for larger screens: 2 cards per row */
@media (min-width: 600px) { /* Adjust the breakpoint as needed */
  .CourseCard {
    width: calc(50% - 1em); /* The "1em" is for spacing between the cards */
  }
}


</style>
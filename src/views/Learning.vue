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
          :image="course.image"
          @start-course="checkCourse"
        />
      </div>
    </div>
  </div>

  <BackButton/>


</template>


<script>
import { ref, computed } from "vue";
import BackButton from '../components/BackButton.vue';
import CourseCard from '../components/CourseCard.vue';
import { useRouter } from 'vue-router';


export default {
  name: 'Learning',
  components: {
    BackButton,
    CourseCard,
  },

  setup() {
    
    const courses = ref([
        {
          id: 1,
          title: "Hello Quantum!",
          description: "Learn the basics of quantum computing, including qubits, superposition, quantum teleportation, and more.",
          image: "https://example.com/images/computer-science.jpg",
          category: "Basic"
        },
        {
          id: 2,
          title: "Quantum Algorithms",
          description: "Find out how quantum computers can be used to solve complex problems, including Shor's algorithm and Grover's algorithm.",
          image: "https://example.com/images/computer-science.jpg",
          category: "Intermediate"
        },
        {
          id: 3,
          title: "The Quantum Computer",
          description: "Explore the inner workings of a quantum computer, including the quantum gates and circuits that make it work.",
          image: "https://example.com/images/computer-science.jpg",
          category: "Advanced"
        },
        {
          id: 4,
          title: "Quantum Circuits",
          description: "Learn how quantum computers can be used to create unbreakable encryption schemes.",
          image: "https://example.com/images/computer-science.jpg",
          category: "Intermediate"
        },
        {
          id: 5,
          title: "Look Into the Future",
          description: "Learn how quantum computers can be used to create unbreakable encryption schemes.",
          image: "https://example.com/images/computer-science.jpg",
          category: "Advanced"
        },
        {
          id: 6,
          title: "QiSkit Schematics",
          description: "Explore QiSkit schematics and how they can be used to create quantum circuits.",
          image: "https://example.com/images/computer-science.jpg",
          category: "Basic"
        },
        {
          id: 7,
          title: "IBM Quantum Composer",
          description: "Learn how to use the IBM Quantum Composer to create quantum circuits.",
          image: "https://example.com/images/computer-science.jpg",
          category: "Advanced"
        }
      ]);

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
        // console.log("Received start-course with title:", title);
        if(title === "The Quantum Computer") {
          
          // Navigate to the quantum computer route
          // router.push('/quantum-computer');
          if (window.ipcRenderer) {
              // console.log('ipcRenderer exists');
              window.ipcRenderer.send('open-unity-window');
          } else {
            console.log('ipcRenderer does not exist');
          }
        } else if (title === "QiSkit Schematics") {
          console.log("clicked scheme")
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
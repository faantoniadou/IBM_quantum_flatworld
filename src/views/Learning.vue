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
    <CourseCard 
      v-for="course in courses" 
      :key="course.id" 
      :title="course.title" 
      :description="course.description" 
      :image="course.image"
      @start-course="checkCourse"
    />
  </div>

  <BackButton/>
  <!-- <QuantumComputer v-if="showGame.valueOf"/> -->
  <QuantumComputer />


</template>


<script>
import { ref } from "vue";
import BackButton from '../components/BackButton.vue';
import CourseCard from '../components/CourseCard.vue';
import QuantumComputer from '../components/QuantumComputer.vue';


export default {
  name: 'Learning',
  components: {
    BackButton,
    CourseCard,
    QuantumComputer
  },

  setup() {
    // Reactive properties using 'ref'
    // const router = useRouter();
    // const route = useRoute();

    // const showGame = ref(false);
    const courses = ref([
        {
          id: 1,
          title: "Introduction to Quantum Computing",
          description: "Learn the basics of quantum computing, including qubits, superposition, quantum teleportation, and more.",
          image: "https://example.com/images/computer-science.jpg"
        },
        {
          id: 2,
          title: "The Quantum Computer",
          description: "Explore the inner workings of a quantum computer, including the quantum gates and circuits that make it work.",
          image: "https://example.com/images/computer-science.jpg",
        },
        {
          id: 3,
          title: "Quantum Algorithms",
          description: "Find out how quantum computers can be used to solve complex problems, including Shor's algorithm and Grover's algorithm.",
          image: "https://example.com/images/computer-science.jpg"
        },
        {
          id: 4,
          title: "Quantum Cryptography",
          description: "Learn how quantum computers can be used to create unbreakable encryption schemes.",
          image: "https://example.com/images/computer-science.jpg"
        }
      ]);

      // Method to check the course title and show the game
      const checkCourse = (title) => {
        console.log("Received start-course with title:", title);
        if(title === "The Quantum Computer") {
          // showGame.value = true;
          
          // Navigate to the quantum computer route
          // router.push('/quantum-computer');
          if (window.ipcRenderer) {
            window.ipcRenderer.send('open-unity-window');
          }
        }
      };

      // Return reactive properties and methods to the template
      return {
        // showGame,
        courses,
        checkCourse
      };
    },
  };

</script>

<style scoped>

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
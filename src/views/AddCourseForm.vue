<template>
  <div class="container">
    <div class="Teaching">
      <div class="card" style="display: flex; justify-content: center; width: 100%; height: 100px;">
        <Card style="width: 14em; padding: 0px; font-size: 32px; margin-top: 10px;">
          <template #header>
            Add Course
          </template>
        </Card>
      </div>
    </div>
  </div>

  <div class="add-course-form">
    <div class="flex flex-column gap-2" style="padding: 20px;">
        <label for="title" style="display: block; margin-bottom: 10px;">Course Title</label>
        <InputText id="title" v-model="value" aria-describedby="title-help" placeholder="Title" style="width: 300px;"/>
    </div>
    <div class="flex flex-column gap-2" style="padding: 20px;">
        <label for="description" style="display: block; margin-bottom: 10px;">Course Description</label>
        <InputText id="description" v-model="value" aria-describedby="title-help" placeholder="Description" style="width: 300px;"/>
    </div>
    <div class="flex flex-column gap-2" style="padding: 20px;">
        <label for="levels" style="display: block; margin-bottom: 10px;">Course Level</label>
        <Dropdown v-model="selectedLevel" :options="categories" optionLabel="level" placeholder="Select a Level" class="['w-full md:w-14rem', { 'p-invalid': errorMessage }]" style="width: 300px;"/>
    </div>
    <div class="button-container" style="width:100%; padding: 20px; ">
      <Button label="Submit" :loading="loading" class="p-button-lg" style="padding: 20px; font-size: 15px; width: 300px; height:45px; background-color: #7027ab;"/>
    </div>
  </div>

                

  <BackButton/>
</template>

<script>
import { ref } from 'vue';
import axios from 'axios';
import BackButton from '../components/BackButton.vue';

axios.defaults.baseURL = process.env.VUE_APP_API_BASE_URL;

export default {
  name: 'AddCourseForm',

  components: {
    BackButton,
  },

  setup() {
    const course = ref({
      title: '',
      description: '',
      image: '',
      category: null,
    });

    const selectedLevel = ref();

    const categories = ref([
      { level: 'Basic', code: 'Basic' },
      { level: 'Intermediate', code: 'Intermediate' },
      { level: 'Advanced', code: 'Advanced' },
    ]);

    const addCourse = async () => {
      try {
        await axios.post('/add-course', course.value);
        alert('Course added successfully');
        course.value = {
          title: '',
          description: '',
          image: '',
          category: '',
        };
      } catch (error) {
        console.error('Error adding course:', error);
        alert('Error adding course');
      }
    };

    return {
      course,
      categories,
      addCourse,
      selectedLevel,
    };
  },
};
</script>

<style scoped>
.add-course-form {
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
  display: block;
}

.flex {
  display: block;
}


button {
  padding: 10px 20px;
  background-color: #28a745;
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #218838;
}
</style>

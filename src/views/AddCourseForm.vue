<template>
  <div class="add-course-form">
    <Card title="Add New Course">
      <form @submit.prevent="addCourse">
        <div class="p-field">
          <label for="title">Course Title</label>
          <InputText id="title" v-model="course.title" required />
        </div>
        <div class="p-field">
          <label for="description">Course Description</label>
          <InputText id="description" v-model="course.description" required rows="4"></InputText>
        </div>
        <div class="p-field">
          <label for="category">Course Category</label>
          <Dropdown id="category" v-model="course.category" :options="categories" optionLabel="name" required></Dropdown>
        </div>
        <Button type="submit" label="Add Course" icon="pi pi-check" />
      </form>
    </Card>
  </div>
  <BackButton/>
</template>

<script>
import { ref } from 'vue';
import axios from 'axios';
import BackButton from '../components/BackButton.vue';

// import { Card } from 'primevue/card';
// import { InputText } from 'primevue/inputtext';
// import { InputTextarea } from 'primevue/inputtextarea';
// import { Dropdown } from 'primevue/dropdown';
// import { Button } from 'primevue/button';

axios.defaults.baseURL = process.env.VUE_APP_API_BASE_URL;

export default {
  name: 'AddCourse',

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

    const categories = [
      { name: 'Basic', code: 'Basic' },
      { name: 'Intermediate', code: 'Intermediate' },
      { name: 'Advanced', code: 'Advanced' },
    ];

    const addCourse = async () => {
      try {
        await axios.post('/add-course', course.value);
        alert('Course added successfully');
        course.value = {
          title: '',
          description: '',
          image: '',
          category: null,
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
    };
  },
};
</script>

<style scoped>
.add-course-form {
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
}

.p-field {
  margin-bottom: 15px;
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

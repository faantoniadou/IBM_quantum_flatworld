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
      <InputText id="title" v-model="course.title" aria-describedby="title-help" placeholder="Title" style="width: 300px;"/>
    </div>
    <div class="flex flex-column gap-2" style="padding: 20px;">
      <label for="description" style="display: block; margin-bottom: 10px;">Course Description</label>
      <InputText id="description" v-model="course.description" aria-describedby="title-help" placeholder="Description" style="width: 300px;"/>
    </div>
    <div class="flex flex-column gap-2" style="padding: 20px;">
      <label for="levels" style="display: block; margin-bottom: 10px;">Course Level</label>
      <Dropdown id="levels" v-model="course.category" :options="categories" optionLabel="level" placeholder="Select a Level" class="['w-full md:w-14rem', { 'p-invalid': errorMessage }]" style="width: 300px;"/>
    </div>

    <div class="card" style="width: 341px; padding: 20px;">
      <Toast />
      <FileUpload :name="'demo[]'" @upload="onFileSelect" :multiple="true" :maxFileSize="10000000000">
        <template #empty>
          <p>Drag and drop Unity WebGL build folders here to upload.</p>
        </template>
      </FileUpload>

    </div>

    <div class="button-container" style="width:100%; padding: 20px; ">
      <Button label="Submit" @click="handleSubmit" :loading="loading" class="p-button-lg" style="padding: 20px; font-size: 15px; width: 300px; height:45px; background-color: #793dae;"/>
    </div>
  </div>

  <BackButton/>
</template>

<script>
import { ref } from 'vue';
import axios from 'axios';
import BackButton from '../components/BackButton.vue';
import { useToast } from "primevue/usetoast"; 

axios.defaults.baseURL = process.env.VUE_APP_API_BASE_URL;
axios.defaults.withCredentials = true;

export default {
  name: 'AddCourseForm',

  components: {
    BackButton,
  },

  setup() {
    const course = ref({
      title: '',
      description: '',
      category: null,
    });

    const selectedLevel = ref();
    const loading = ref(false);
    const selectedFiles = ref(null);

    const categories = ref([
      { level: 'Basic', code: 'Basic' },
      { level: 'Intermediate', code: 'Intermediate' },
      { level: 'Advanced', code: 'Advanced' },
    ]);

    const addCourse = async () => {
      try {
        loading.value = true;
        
        // extract level property from selected category
        course.value.category = course.value["category"].level;
        
        await axios.post('/add-course', course.value);
        alert('Course added successfully');
        // go back to the previous page
        window.history.back();
        course.value = {
          title: '',
          description: '',
          category: null,
        };
      } catch (error) {
        console.error('Error adding course:', error);
        alert('Error adding course');
      } finally {
        loading.value = false;
      }
    };

    const handleSubmit = async () => {
      if (validateForm()) {
        const formData = new FormData();
        formData.append('course', JSON.stringify(course.value));
        
        if (selectedFiles.value) {
          selectedFiles.value.forEach(file => {
            formData.append('demo[]', file);
          });
        }

        try {
          await axios.post('/upload', formData);
          await addCourse();
        } catch (error) {
          console.error('Error uploading files and adding course:', error);
        }
      }
    };

    const validateForm = () => {
      if(!course.value.title || !course.value.description || !course.value.category) {
        alert('Please fill in all the fields');
        return false;
      }
      return true;
    };

    const toast = useToast(); 

    return {
      course,
      categories,
      selectedLevel,
      handleSubmit,
      loading,
      selectedFiles,
      toast,
      uploadUrl: axios.defaults.baseURL + '/upload',
    };
  },
  methods: {
    onFileSelect(event) {
      this.selectedFiles = event.files;
    },
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
<template>
  <Menubar :model="items" style="padding: 20px">
    <template #start>
      <img
        alt="logo"
        src="../assets/logo.png"
        height="20"
        width="100"
        class="mr-2"
      />
    </template>
    <template #end>
      <InputText placeholder="Search" type="text" />
    </template>
  </Menubar>
</template>

<script>
import { ref } from 'vue';
const items = ref([
  {
    label: 'File',
    icon: 'pi pi-fw pi-file',
    items: [
      {
        label: 'New',
        icon: 'pi pi-fw pi-plus',
      },
    ],
  },
]);

export default {
  name: 'MenubarComponent',
  setup() {
    return items;
  },
};
</script>

<template>
  <div class="card">
      <DataTable :value="gates" tableStyle="min-width: 50rem">
          <template #header>
              <div class="flex flex-wrap align-items-center justify-content-between gap-2">
                  <span class="text-xl text-900 font-bold">Qiskit Gates</span>
                  <Button icon="pi pi-refresh" rounded raised />
              </div>
          </template>
          <Column field="name" header="Name"></Column>
          <Column header="Icon">
              <template #body="slotProps">
                  <img :src="`/circuit_icons/${slotProps.data.name}.png`" :alt="slotProps.data.name" class="w-6rem shadow-2 border-round" />
              </template>
          </Column>
          <template #footer> In total there are {{ gates ? gates.length : 0 }} gates. </template>
      </DataTable>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';

onMounted(() => {
  // You can make an API call here if needed. For this example, I'm setting the data directly.
  gates.value = [
      { name: 'h', description: 'Hadamard gate' },
      { name: 'x', description: 'Pauli-X gate' },
      { name: 'y', description: 'Pauli-Y gate' },
      { name: 'z', description: 'Pauli-Z gate' },
      { name: 'cx', description: 'CNOT gate' },
      { name: 'cy', description: 'Controlled-Y gate' },
      { name: 'cz', description: 'Controlled-Z gate'},
      { name: 'ccx', description: 'Toffoli gate' },
      { name: 'swap', description: 'Swap gate' },
      { name: 'r', description: 'Arbitrary single-qubit rotation gate' },
      { name: 'rx', description: 'Rotation around the x-axis' },
      { name: 'ry', description: 'Rotation around the y-axis' },
      { name: 'rz', description: 'Rotation around the z-axis' },
      { name: 'ch', description: 'Controlled-Hadamard gate' },
      { name: 'crx', description: 'Controlled rotation around the x-axis' },
      { name: 'cry', description: 'Controlled rotation around the y-axis' },
      { name: 'crz', description: 'Controlled rotation around the z-axis' },
      { name: 'id', description: 'Identity gate' },
  ];
});

const gates = ref([]);

</script>

<style scoped>

</style>

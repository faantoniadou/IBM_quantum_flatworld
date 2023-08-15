<template>
  <div class="card" style="margin-left: 60px; margin-right: 60px;">
      <DataTable :value="gates" :resizableColumns="false" showGridlines :tableProps="{ style: { minWidth: '10rem' } }" rowGroupMode="rowspan">
          <template #header>
              <div class="flex flex-wrap align-items-center justify-content-between gap-2">
                  <span class="text-xl text-900 font-bold" style="color: black;">Qiskit Gates</span>
              </div>
          </template>
          <Column field="title" header="Name" style="width: 200px" sortable></Column>
          <Column header="Icon" style="min-width: 200px;">
              <template #body="slotProps">
                  <img :src="`/circuit_icons/${slotProps.data.name}.png`" :alt="slotProps.data.name" class="w-6rem shadow-2 border-round" />
              </template>
          </Column>
          <Column field="description" header="Description" style="width: 800px;"></Column>
      </DataTable>
  </div>
  <BackButton/>
</template>

<script setup>
// This component creates a table of some gates available in Qiskit.
import { ref, onMounted } from 'vue';
import BackButton from '../components/BackButton.vue';

onMounted(() => {
  // we can make an API call here if needed. For this example, I'm setting the data directly.
  gates.value = [
      { name: 'h', title: 'Hadamard gate', description: 'The Hadamard gate creates a superposition state.' },
      { name: 'x', title: 'Pauli-X gate', description: 'The Pauli-X gate is a bit-flip gate.'},
      { name: 'y', title: 'Pauli-Y gate', description: 'The Pauli-Y gate is a bit and phase flip gate.'},
      { name: 'z', title: 'Pauli-Z gate', description: 'The Pauli-Z gate ' },
      { name: 'cx', title: 'CNOT gate', description: 'The CNOT gate flips the target qubit if the control qubit is in the |1⟩ state.' },
      { name: 'cy', title: 'Controlled-Y gate', description: 'The Controlled-Y gate flips the target qubit if the control qubit is in the |1⟩ state.' },
      { name: 'cz', title: 'Controlled-Z gate', description: 'The Controlled-Z gate flips the target qubit if the control qubit is in the |1⟩ state.' },
      { name: 'ccx', title: 'Toffoli gate', description: 'The Toffoli gate flips the target qubit if both control qubits are in the |1⟩ state.' },
      { name: 'swap', title: 'Swap gate', description: 'The Swap gate swaps the state of two qubits.' },
      { name: 'r', title: 'Arbitrary single-qubit rotation gate', description: 'The Arbitrary single-qubit rotation gate rotates the state of a qubit around the Bloch sphere.' },
      { name: 'rx', title: 'Rotation around the x-axis', description: 'The Rotation around the x-axis gate rotates the state of a qubit around the x-axis of the Bloch sphere.' },
      { name: 'ry', title: 'Rotation around the y-axis', description: 'The Rotation around the y-axis gate rotates the state of a qubit around the y-axis of the Bloch sphere.' },
      { name: 'rz', title: 'Rotation around the z-axis', description: 'The Rotation around the z-axis gate rotates the state of a qubit around the z-axis of the Bloch sphere.' },
      { name: 'ch', title: 'Controlled-Hadamard gate', description: 'The Controlled-Hadamard gate creates a superposition state on the target qubit if the control qubit is in the |1⟩ state.' },
      { name: 'crx', title: 'Controlled rotation around the x-axis', description: 'The Controlled rotation around the x-axis gate rotates the state of a qubit around the x-axis of the Bloch sphere if the control qubit is in the |1⟩ state.' },
      { name: 'cry', title: 'Controlled rotation around the y-axis', description: 'The Controlled rotation around the y-axis gate rotates the state of a qubit around the y-axis of the Bloch sphere if the control qubit is in the |1⟩ state.' },
      { name: 'crz', title: 'Controlled rotation around the z-axis', description: 'The Controlled rotation around the z-axis gate rotates the state of a qubit around the z-axis of the Bloch sphere if the control qubit is in the |1⟩ state.' },
      { name: 'id', title: 'Identity gate', description: 'The Identity gate acts on a single Qubit. The I Gate does nothing. It is however used for calculations.' }
  ];
});

const gates = ref([]);

</script>

<style scoped>

</style>

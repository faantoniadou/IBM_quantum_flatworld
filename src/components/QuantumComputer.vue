<template>
  <div style="width: 800px; height: 600px; border: 1px solid #f00">
    <UnityVue :unity="unityContext" />
  </div>
</template>

<script setup>
// import { ref, onMounted } from 'vue';
import { onMounted } from 'vue';
import UnityWebgl from 'unity-webgl';
import UnityVue from 'unity-webgl/vue';


onMounted(() => {
  const unityContext = new UnityWebgl({
    loaderUrl: '/unity-vr/Build/unity-vr.loader.js',
    dataUrl: "/unity-vr/Build/unity-vr.data",
    frameworkUrl: "/unity-vr/Build/unity-vr.framework.js",
    codeUrl: "/unity-vr/Build/unity-vr.wasm"
  });

  unityContext
    .on('device', () => alert('click device ...'))
    .on('error', error => {
      console.error('Unity Error:', error);
    })
    .on('loaded', () => {
      console.log('Unity loaded');
    });
});






// export default {
//   name: 'QuantumComputer',
//   components: {
//     UnityWebgl: UnityWebgl.vueComponent,
//   },
//   data() {
//     return {
//       unityContext: null,
//       unityReady: false,
//       loading: true,
//     };
//   },
//   created() {
//     this.unityContext = new UnityWebgl({
//       loaderUrl: '/unity-vr/Build/unity-vr.loader.js',
//       dataUrl: "/unity-vr/Build/unity-vr.data",
//       frameworkUrl: "/unity-vr/Build/unity-vr.framework.js",
//       codeUrl: "/unity-vr/Build/unity-vr.wasm"
//     });

    
//     this.unityContext
//       .on('beforeMount', ctx => {
//         console.log('beforeMount', ctx);
//       })
//       .on('mounted', ctx => {
//         console.log('mounted', ctx);
//         this.unityReady = true;
//       })
//       .on('beforeUnmount', ctx => {
//         console.log('beforeUnmount', ctx);
//       })
//       .on('progress', progress => {
//         console.log('loaded : ', progress);
//       });

//     this.unityContext.on('device', () => alert('click device ......'));
//   },
//   beforeUnmount() {
//     // Call any cleanup or destroy methods provided by UnityWebgl here
//     // this.unityContext.destroy(); // This is just an example, check the actual method in the library
//   }
// }
</script>

<style scoped>
/* s */
</style>

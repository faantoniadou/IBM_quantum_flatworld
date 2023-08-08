<template>
  <!-- Placeholder for Unity game -->
  <UnityWebgl :unity="unityContext" v-if="unityReady" width="1000" height="900"></UnityWebgl>
</template>

<script>
import UnityWebgl from 'unity-webgl';

export default {
  name: 'QuantumComputer',
  components: {
    UnityWebgl: UnityWebgl.vueComponent,
  },
  data() {
    return {
      unityContext: null,
      unityReady: false,
      loading: true,
    };
  },
  created() {
    this.unityContext = new UnityWebgl({
      loaderUrl: '/unity-vr/Build/unity-vr.loader.js',
      dataUrl: "/unity-vr/Build/unity-vr.data",
      frameworkUrl: "/unity-vr/Build/unity-vr.framework.js",
      codeUrl: "/unity-vr/Build/unity-vr.wasm"
    });

    
    this.unityContext
      .on('beforeMount', ctx => {
        console.log('beforeMount', ctx);
      })
      .on('mounted', ctx => {
        console.log('mounted', ctx);
        this.unityReady = true;
      })
      .on('beforeUnmount', ctx => {
        console.log('beforeUnmount', ctx);
      })
      .on('progress', progress => {
        console.log('loaded : ', progress);
      });

    this.unityContext.on('device', () => alert('click device ......'));
  },
  beforeUnmount() {
    // Call any cleanup or destroy methods provided by UnityWebgl here
    // this.unityContext.destroy(); // This is just an example, check the actual method in the library
  }
}
</script>

<style scoped>
canvas {
  width: 1000px;
  height: 900px;
}
</style>

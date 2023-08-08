<template>
  <!-- Placeholder for Unity game -->
  <UnityWebgl :unity="unityContext" width="1000" height="900"></UnityWebgl>
</template>

<script>
/* eslint-disable no-unused-vars */
import UnityWebgl from 'unity-webgl';
import VueUnity from 'unity-webgl/vue';

const unityContext = new UnityWebgl({
  loaderUrl: '../../public/Build/unity-vr.loader.js',
  dataUrl: "../../public/Build/unity-vr.data",
  frameworkUrl: "../../public/Build/unity-vr.framework.js",
  codeUrl: "../../public/Build/unity-vr.wasm"
})

unityContext
  .on('beforeMount', ctx => {
    console.log('beforeMount', ctx)
  })
  .on('mounted', ctx => {
    console.log('mounted', ctx)
  })
  .on('beforeUnmount', ctx => {
    console.log('beforeUnmount', ctx)
  })
  .on('progress', progress => {
    console.log('loaded : ', progress)
  })

unityContext.on('device', () => alert('click device ......'))

export default {
  name: 'QuantumComputer',
  components: {
    UnityWebgl: UnityWebgl.vueComponent,
  },
  data() {
    return {
      unityContext: UnityWebgl,
      loading: true,
    };
  }
}
</script>

<style scoped>
canvas {
  width: 1000px;
  height: 900px;
}
</style>

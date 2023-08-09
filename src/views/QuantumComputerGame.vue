<!-- <template>
  <UnityWebgl :unity="unityContext" v-if="unityReady" width="6000" height="10000"></UnityWebgl>
</template>

<script>
// import QuantumComputer from '../components/QuantumComputer.vue';
import UnityWebgl from 'unity-webgl';
// import VueUnity from 'unity-webgl/vue'

export default {
  name: 'QuantumComputerGame',
  data() {
    return {
      loading: false,
      unityContext: null,
      unityReady: false,
    };
  },
  created() {
    this.unityContext = new UnityWebgl({
      loaderUrl: '/static/Build/unity-vr.loader.js',
      dataUrl: "/static/Build/unity-vr.data",
      frameworkUrl: "/static/Build/unity-vr.framework.js",
      codeUrl: "/static/Build/unity-vr.wasm"
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

  components: {
    UnityWebgl: UnityWebgl.vueComponent,
    // VueUnity
  },

  methods: {
    load() {
      this.loading = true;
      // do something here, then set loading back to false when finished
    },
  },
}
</script> -->



<template>
  <iframe src="/Build/index.html" width="800" height="600"></iframe>
</template>

<script>
export default {
  name: 'QuantumComputerGame'
}
</script>

<style scoped>
iframe {
  border: none;
  overflow: hidden;
}
</style>

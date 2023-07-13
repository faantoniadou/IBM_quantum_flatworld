const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  css: {
    extract: true, // FALSE: causes a problem with SSR, prefer :style
  },
  pluginOptions: {
    lintStyleOnBuild: true,
    stylelint: {},
  }
})

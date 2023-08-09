const { defineConfig } = require('@vue/cli-service');
module.exports = defineConfig({
  runtimeCompiler: true,
  transpileDependencies: true,
  css: {
    extract: true, // FALSE: causes a problem with SSR, prefer :style
  },
  pluginOptions: {
    lintStyleOnBuild: true,
    stylelint: {},
    electronBuilder: {
      preload: '/src/preload.js',
  },
  chainWebpack: config => {
    config.module
      .rule('vue')
      .use('vue-loader')
      .tap(options => {
        options.compilerOptions = {
          ...options.compilerOptions,
          isCustomElement: tag => tag.startsWith('Unity-')
        }
        return options
      })
    }
  },
});

const path = require('path')
const webpack = require('webpack')
const CopyWebpackPlugin = require('copy-webpack-plugin');
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin

function resolve(dir) {
  return path.join(__dirname, dir)
}

// vue.config.js
module.exports = {
  /*
    Vue-cli3:
    Crashed when using Webpack `import()` #2463
    https://github.com/vuejs/vue-cli/issues/2463

   */
  /*
  pages: {
    index: {
      entry: 'src/main.js',
      chunks: ['chunk-vendors', 'chunk-common', 'index']
    }
  },
  */
  // configureWebpack: {
  //   plugins: [
  //     // Ignore all locale files of moment.js
  //     new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/)
  //   ]
  // },

  configureWebpack: config => {
    if (process.env.NODE_ENV === 'production') {
      return {
        plugins: [
          new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
          new CopyWebpackPlugin([{
            from: 'node_modules/@aspnet/signalr/dist/browser/signalr.min.js',
            to: 'static'
          }, {
            from: 'node_modules/abp-web-resources/Abp/Framework/scripts/libs/abp.signalr-client.js',
            to: 'static'
          }, {
            from: 'src/lib/abp.js',
            to: 'static'
          }])
        ]
      }
    } else {
      return {
        plugins: [
          new CopyWebpackPlugin([
            //   {
            //   from: 'node_modules/@aspnet/signalr/dist/browser/signalr.min.js',
            //   to: 'dist'
            // }, {
            //   from: 'node_modules/abp-web-resources/Abp/Framework/scripts/libs/abp.signalr-client.js',
            //   to: 'dist'
            // }, 
            {
              from: 'src/lib/abp.js',
              to: 'static'
            }]),
          // 依赖大小分析工具
          new BundleAnalyzerPlugin(),
        ]
      }
    }
  },

  chainWebpack: (config) => {
    config.resolve.alias
      .set('@$', resolve('src'))
      .set('@api', resolve('src/api'))
      .set('@assets', resolve('src/assets'))
      .set('@comp', resolve('src/components'))
      .set('@views', resolve('src/views'))
      .set('@layout', resolve('src/layout'))
      .set('@static', resolve('src/static'))

    const svgRule = config.module.rule('svg')
    svgRule.uses.clear()
    svgRule
      .oneOf('inline')
      .resourceQuery(/inline/)
      .use('vue-svg-icon-loader')
      .loader('vue-svg-icon-loader')
      .end()
      .end()
      .oneOf('external')
      .use('file-loader')
      .loader('file-loader')
      .options({
        name: 'assets/[name].[hash:8].[ext]'
      })
    /* svgRule.oneOf('inline')
      .resourceQuery(/inline/)
      .use('vue-svg-loader')
      .loader('vue-svg-loader')
      .end()
      .end()
      .oneOf('external')
      .use('file-loader')
      .loader('file-loader')
      .options({
        name: 'assets/[name].[hash:8].[ext]'
      })
    */
  },

  css: {
    loaderOptions: {
      less: {
        modifyVars: {
          /* less 变量覆盖，用于自定义 ant design 主题 */

          /*
          'primary-color': '#F5222D',
          'link-color': '#F5222D',
          'border-radius-base': '4px',
          */
        },
        javascriptEnabled: true
      }
    }
  },

  devServer: {
    // development server port 8000
    port: 8000,
    proxy: {
      '/api': {
        target: 'http://10.211.55.7:3001/',
        ws: false,
        changeOrigin: true,
      },
    }
  },

  // disable source map in production
  productionSourceMap: false,
  lintOnSave: undefined,
  // babel-loader no-ignore node_modules/*
  transpileDependencies: []
}

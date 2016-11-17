var path = require('path')
var projectRoot = path.resolve(__dirname, '../')

var webpack = require('webpack')
var HtmlWebpackPlugin = require('html-webpack-plugin')

module.exports = {
  entry: __dirname + "/src/index.js",
  output: {
    path: __dirname + "/dist",
    filename: "index.js"
  },
  resolve: {
    //自动扩展文件后缀名，意味着我们require模块可以省略不写后缀名
    extensions: ['', '.js', '.vue'],
    fallback: [path.join(__dirname, '../node_modules')],
    //模块别名定义，方便后续直接引用别名，无须多写长长的地址
    alias: {
      // AppStore : 'js/stores/AppStores.js',//后续直接 require('AppStore') 即可
      // AppAction : 'js/actions/AppAction.js',
      // 'src': path.resolve(__dirname, '../src'),
      'css': path.resolve(__dirname,'../src/css'),
      'assets': path.resolve(__dirname, '../src/assets'),
      'json': path.resolve(__dirname, '../src/json'),
      // 'views': path.resolve(__dirname,'../src/view'),
      'components': path.resolve(__dirname, '../src/components')
    }
  },
  devServer: {
    port: 9999,
    colors: true,//终端中输出结果为彩色
    historyApiFallback: true,//不跳转
    inline: true,//实时刷新
    hot: true
  },
  module: {
    loaders: [
      {
        test: /\.css$/,
        loader: 'style-loader!css-loader'
      },
      {
        test: /\.js$/,
        exclude: /node_modules/,
        loader: 'babel'
      },
      {
        test: /\.vue$/,
        loader: 'vue'
      },
      {
        test: /\.json$/,
        loader: 'json'
      }
    ]
  },
  plugins: [
    new HtmlWebpackPlugin({
      filename: 'index.html',
      template: 'index.html'
    }),
    new webpack.HotModuleReplacementPlugin()
  ]
}
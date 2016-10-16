// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueRouter from 'vue-router'

import App from './App'

// 0. 如果使用模块化机制编程， 要调用 Vue.use(VueRouter)
Vue.use(VueRouter)

// 1. 定义(路由) 组件, 可以从其它文件 import 进来
const Foo = { template: '<div>foo</div>' }
const Bar = { template: '<div>bar</div>' }

// 2. 定义路由
const routes = [
  { path: '/', component: App },
  { path: '/foo', component: Foo },
  { path: '/bar', component: Bar }
]

// 3. 创建 Router 实例,传入 'routes' 配置
const router = new VueRouter({
  routes, // 缩写, 相当于 routes: routes
  linkActiveClass: 'active' // 全局配置 <router-link> 的默认『激活 class 类名』, 默认值: "router-link-active"
})

/* eslint-disable no-new */
new Vue({
  el: '#app',
  render: h => h(App),
  router: router
}).$mount('#app')

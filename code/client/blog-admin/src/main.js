// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueRouter from 'vue-router'
import VueResource from 'vue-resource'

import routes from './routes'

import App from './App'
// import FirstComponent from './components/firstcomponent'
// import SecondComponent from './components/secondcomponent'

// import ToDo from './components/todo.vue'

// // 0. 如果使用模块化机制编程， 要调用 Vue.use(VueRouter)
// Vue.use(VueRouter)

// // 1. 定义(路由) 组件, 可以从其它文件 import 进来
// const Foo = { template: '<div>foo</div>' }
// const Bar = { template: '<div>bar</div>' }

// // 2. 定义路由
// const routes = [
//   { path: '/', component: App },
//   { path: '/foo', component: Foo },
//   { path: '/bar', component: Bar },
//   { path: '/todo', component: ToDo }
// ]

// // 3. 创建 Router 实例,传入 'routes' 配置
// const router = new VueRouter({
//   routes, // 缩写, 相当于 routes: routes
//   linkActiveClass: 'active' // 全局配置 <router-link> 的默认『激活 class 类名』, 默认值: "router-link-active"
// })

const router = new VueRouter({
  routes,
  linkActiveClass: 'is-active'
})

Vue.use(VueRouter)
Vue.use(VueResource)

// const router = new VueRouter({
//   model: 'history',
//   base: __dirname,
//   routes: [
//     {
//       path: '/first',
//       component: FirstComponent
//     },
//     {
//       path: '/Second',
//       component: SecondComponent
//     }
//   ]
// })

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router: router,
  render: h => h(App)
}).$mount('#app')

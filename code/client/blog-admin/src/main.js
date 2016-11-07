// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueRouter from 'vue-router'
import VueResource from 'vue-resource'

import routes from './routes'

import App from './App'

const router = new VueRouter({
  routes,
  linkActiveClass: 'is-active'
})

Vue.use(VueRouter)
Vue.use(VueResource)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router: router,
  render: h => h(App)
}).$mount('#app')

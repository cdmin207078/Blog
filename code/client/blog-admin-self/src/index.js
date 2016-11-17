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

new Vue({
  el: '#app',
  router,
  render: h => h(App)
}).$mount('#app')
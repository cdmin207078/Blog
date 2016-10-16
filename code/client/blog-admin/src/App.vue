<template>
  <div id="app">
    <!--<img src="./assets/logo.png">-->
    <!--<hello></hello>-->

    <router-link to="/foo">Go to Foo</router-link>
    <router-link :to="'/Bar'">Go to Bar</router-link>
    
    <!-- 命名的路由 -->
    <router-link :to="{ name: 'user', params: { userId: 123 }}">User</router-link>
    
    <!-- 带查询参数，下面的结果为 /register?plan=private -->
    <router-link :to="{ path: 'register', query: { plan: 'private' }}">Register</router-link>


    <!--<router-view></router-view>-->
  </div>
</template>

<script>
import Hello from './components/Hello'

export default {
  name: 'app',
  components: {
    Hello
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
.active {
  color: yellow;
  background-color:red;
}
</style>

<template>
  <div id="app">
    <img src="./assets/logo.png" alt="">
    <h1>{{ msg }}</h1>
    <first-component></first-component>
    
    <ul>
      <li><router-link to="/first">first page</router-link></li>
      <li><router-link to="/second">second page</router-link></li>
    </ul>

    <router-view class="view"></router-view>
  </div>
</template>

<script>
import FirstComponent from './components/firstcomponent'
import SecondComponent from './components/secondcomponent'

export default {
  components: {
    FirstComponent,
    SecondComponent
  },
  data () {
    return {
      msg: 'Hello Vue!'
    }
  }
}
</script>

<style>
  body {
    font-family: "Myriad Set Pro","Helvetica Neue","Helvetica","Arial",sans-serif
  }
</style>
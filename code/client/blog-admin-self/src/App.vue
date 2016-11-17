<template>
  <div id="app">
    <navbar></navbar>
    <sidebar></sidebar>

    <div class="app-main">
      <div class="container is-fluid is-marginless">
        <router-view></router-view>
      </div>
    </div>

  </div>
</template>

<script>
import Navbar from './view/Navbar'
import Sidebar from './view/SideBar'

export default {
  components: {
    Navbar,
    Sidebar
  }
}
</script>

<style>
  @import "css/bulma.css";
  
  .app-main {
    margin-left: 190px;
    padding-top: 60px;
  }
</style>
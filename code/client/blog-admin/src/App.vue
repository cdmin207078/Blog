<template>
  <div id="app">
    <navbar></navbar>
    <sidebar></sidebar>
    <app-main></app-main>
  </div>
</template>

<script>
import Navbar from 'components/layout/Navbar'
import Sidebar from 'components/layout/Sidebar'
import AppMain from 'components/layout/AppMain'

export default {
  components: {
    Navbar,
    Sidebar,
    AppMain
  }
}
</script>


<style>
  @import '~bulma/css/bulma.css';
  @import './css/fix-bulma.css';
</style>
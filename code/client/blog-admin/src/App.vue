<template>
  <div id="app">
    <navbar></navbar>
    <sidebar></sidebar>
    <app-main></app-main>
  </div>
</template>

<script>
import FirstComponent from 'components/firstcomponent'
import SecondComponent from 'components/secondcomponent'

import Navbar from 'components/layout/Navbar'
import Sidebar from 'components/layout/Sidebar'
import AppMain from 'components/layout/AppMain'

export default {
  components: {
    FirstComponent,
    SecondComponent,
    Navbar,
    Sidebar,
    AppMain
  },
  data () {
    return {
      msg: 'Hello Vue!'
    }
  }
}
</script>

<style>
  @import '~bulma/css/bulma.css'
</style>
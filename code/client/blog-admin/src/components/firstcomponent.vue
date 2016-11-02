<template>
  <div id="firsttemplate">
    <h1>I am a title.</h1>
    <a>write by {{ author }}</a>
    <a class="button is-success">Success</a>
  </div>
</template>

<script>
export default {
  data () {
    return {
      author: '岸部真明'
    }
  }
}
</script>

<style>

</style>
<template>
  <div>
    <h1>写文章</h1>


    <h3>文章编号: {{ $route.params.id }}</h3>
  </div>
</template>

<script>
  export default {
    created: function () {
      console.log(1)
    }
  }
</script>
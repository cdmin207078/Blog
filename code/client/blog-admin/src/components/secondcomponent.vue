<template>
	<div id="secondcomponent">
		<h1>I am Author page.</h1>
		<a> write by {{ author }}</a>
		<p>故园旧梦 - <a href="http://music.163.com/#/m/song?id=153374&userid=30149137">网易云音乐</a></p>
    
    <ul>
      <li v-for="article in articles">
        {{ article.title }}
      </li>
    </ul>
	</div>
</template>

<script>
export default {
  data () {
    return {
      author: '谭宝硕',
      articles: []
    }
  },
  mounted: function () {
    this.$http.jsonp('https://api.douban.com/v2/movie/top250?count=10', {}, {
      header: {

      },
      emulateJSON: true
    }).then(function (response) {
      // 处理正确回调
      this.articles = response.data.subjects
    }, function (response) {
      // 处理错误逻辑
      console.log(response)
    })
  }
}
</script>

<style>
</style>
<template>
	<div class="container is-fluid">
		<div class="box">


			<h1 class="title">文章列表</h1>

			<img src="../../assets/logo.png" alt="">
			<table class="table is-bordered is-striped is-narrow">
				<thead>
					<tr>
						<th>Title</th>
						<th>Content</th>
						<th>CreateTime</th>
						<th>View / Reply</th>
						<th>Option</th>
					</tr>
				</thead>
				<tbody>
					<tr v-for="article in articles">
						<td>{{ article.title }}</td>
						<td>{{ article.content }}</td>
						<td>{{ article.createtime }}</td>
						<td>100 / 100 </td>
						<td class="is-icon">
							<router-link to="/">查看</router-link>
							<router-link :to="{ path: 'article/edit', query: { id: article.id }}">编辑</router-link>
							<a href="javascript:;">删除</a>
						</td>
					</tr>
				</tbody>
			</table>

		</div>
	</div>

</template>

<script>
  // import service from '../../services/app.service'
  
  export default {
    data () {
      return {
        articles: []
      }
    },
    beforeCreate () {
    },
    created () {
      this.articles = this.$http.get('static/getArticles.json').then(function (response) {
        console.log(response)
        this.articles = response.data.data
        console.log(this.articles)
      }, function (response) {
        console.log('请求失败,未找到资源 static/getArticles.json')
      })
    }
  }
</script>

<style>

</style>
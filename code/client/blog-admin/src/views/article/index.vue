<template>
  <div class="container is-fluid">
    <div class="box">


      <h1 class="title">文章列表</h1>

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
              <router-link to="article/edit/1">编辑</router-link>
              <a href="javascript:;">删除</a>
            </td>
          </tr>          
        </tbody>
      </table>

    </div>
  </div>

</template>

<script>
export default {
  data () {
    return {
      articles: [
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' },
        { id: 1, title: '成都', content: '余路还要走多久 你攥着我的手', createtime: '2016-11-3' }
      ]
    }
  },
  ready () {
    console.log('ready')
  }
}
</script>

<style>
</style>
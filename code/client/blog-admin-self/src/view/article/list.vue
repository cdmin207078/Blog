<template>
  <div>
    <section class="section is-shadow-border">
      <div class="heading">
        <h1 class="title">Article's</h1>
        <hr />
      </div>

      <router-link class="button is-medium" to="/article/add">写文章</router-link>
      <!--<a href="/article/add" class="button is-medium">New</a>-->
      <br /><br />

      <table class="table is-bordered is-narrow">
        <thead>
          <tr>
            <th>title</th>
            <th>content</th>
            <th>views / reply</th>
            <th>option</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="ar in articles">
            <td>{{ ar.title }}</td>
            <td>{{ ar.content }}</td>
            <td></td>
            <td>
              <a class="button is-small">View</a>
              <router-link :to="{ name: 'editArticle', params: { id: ar.id }}" class="button is-small">Edit</router-link>
              <a class="button is-small is-danger">Hide</a>
            </td>
          </tr>
        </tbody>
      </table>

      <nav class="pagination">
        <ul>
          <li>
            <a class="button">1</a>
          </li>
          <li>
            <span>...</span>
          </li>
          <li>
            <a class="button">45</a>
          </li>
          <li>
            <a class="button is-primary">46</a>
          </li>
          <li>
            <a class="button">47</a>
          </li>
          <li>
            <span>...</span>
          </li>
          <li>
            <a class="button">86</a>
          </li>
        </ul>
      </nav>
      
    </section>
  </div>
</template>

<script>
  import service from '../../services'

  export default {
    data() {
      return {
        articles: []
      }
    },
    mounted() {
      service.getArticles(null,(response) => {
        this.articles = response.data;
      });
      // this.$http.get('mock/articles.json').then((response) => {
      //   // success callback
      //   this.articels = response.data
      // });
    }
  }
</script>
import Vue from 'Vue'

export default {
  getArticles(params,success) {
    // if(process.env.NODE_ENV === 'development') {
      // return this.$http.get('mock/articles.json');
    // }
    // return this.$http.post('/api/article/getArticles',params);
    return Vue.http.get('mock/articles.json').then((response) => {
      success(response);
    });
  },
  saveArticle(params) {
    if(process.env.NODE_ENV === 'development') {
      return this.$http.get('mock/ok.json');
    }
    return this.$http.post('/api/article/save',params);
  }
}
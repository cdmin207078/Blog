// import { getJSON, post } from '../utils/ajax'
export default {
  // 获取文章列表, 无分页
  getArticles () {
    if (process.env.NODE_ENV === 'development') {
      return this.$http.get('json/getArticles.json')
    }

    return this.$http.get('/api/getArticles')
  }
}

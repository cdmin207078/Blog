// routes.js
import ArticleList from './view/article/list'
import ArticleEdit from './view/article/edit'

export default [
  {
    name: 'articleList',
    path: '/article/list',
    component: ArticleList,
  },
  {
    name: 'addArticle',
    path: '/article/add',
    component: ArticleEdit
  },
  {
    name: 'editArticle',
    path: '/article/edit/:id(\\d+)',
    component: ArticleEdit
  }
]
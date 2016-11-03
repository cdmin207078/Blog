import ArticleIndex from './views/article/index'
import ArticleEdit from './views/article/edit'

export default [
  {
    path: '/article',
    component: ArticleIndex
  },
  {
    path: '/article/add',
    component: ArticleEdit
  },
  {
    path: '/article/edit:id',
    component: ArticleEdit
  }
]

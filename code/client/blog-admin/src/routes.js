import ArticleIndex from './views/article/index'
import ArticleEdit from './views/article/edit'

export default [
  {
    path: '/article',
    component: ArticleIndex,
    children: [
      {
        path: 'add',
        component: ArticleEdit
      },
      {
        path: 'edit/:id',
        component: ArticleEdit
      }
    ]
  }
]

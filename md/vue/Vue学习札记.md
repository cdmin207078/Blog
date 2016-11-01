#Vue 学习札记

Vue.js 的目标是通过尽可能简单的 API 实现响应的数据绑定和组合的视图组件。

## 响应的数据绑定
Vue.js 的核心是一个响应的数据绑定系统，它让数据与 DOM 保持同步非常简单。

##组件系统

>  Vue.js 组件其实都是被扩展的 Vue 实例

可以扩展 Vue 构造器，从而用预定义选项创建可复用的组件构造器：

```javascript
var MyComponent = Vue.extend({
  // 扩展选项
})

// 所有的 `MyComponent` 实例都将以预定义的扩展选项被创建
var myComponentInstance = new MyComponent()
```

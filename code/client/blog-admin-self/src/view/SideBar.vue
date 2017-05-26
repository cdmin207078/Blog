<template>
  <aside class="menu app-sidebar">
    <p class="menu-label">
      控制中心
    </p>
    
    <ul class="menu-list">
      <!-- 使用 router-link 组件来导航. -->
      <!-- 通过传入 `to` 属性指定链接. -->
      <!-- <router-link> 默认会被渲染成一个 `<a>` 标签 -->
      <li><router-link to="/article/add">写文章</router-link></li>
      <li><router-link to="/article/">文章列表</router-link></li>
    </ul>

    <!-- 路由出口 -->
    <!-- 路由匹配到的组件将渲染在这里 -->
    <router-view></router-view>

  </aside>
</template>

<style>
.app-sidebar{
    position: fixed;
    top: 50px;
    left: 0;
    bottom: 0;
    padding: 20px 0 50px;
    width: 180px;
    min-width: 45px;
    max-height: 100vh;
    height: calc(100% - 50px);
    z-index: 1024 - 1;
    background: #FFF;
    box-shadow: 0 2px 3px rgba(17, 17, 17, 0.1), 0 0 0 1px rgba(17, 17, 17, 0.1);
    overflow-y: auto;
    overflow-x: hidden;
}
.app-sidebar .menu-label {
  padding-left: 5px;
}
</style>
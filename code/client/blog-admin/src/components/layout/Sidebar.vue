<template>
<aside class="menu app-sidebar">

  <p class="menu-label">内容管理</p>
  <ul class="menu-list">
    <li>
      <router-link></router-link>
    </li>
    <li><a>写文章</a></li>    
    <li><a class="is-active">文章管理</a></li>
  </ul>

  <p class="menu-label">用户管理</p>
  <ul class="menu-list">
    <li><a>用户信息</a></li>
  </ul>

  <p class="menu-label">系统</p>
  <ul class="menu-list">
    <li><a>运行日志</a></li>
    <li><a>修改密码</a></li>
    <li><a>退出</a></li>
    
  </ul>
</aside>
</template>

<style>
  .app-sidebar {
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
</style>
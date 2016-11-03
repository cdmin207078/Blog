<template>
  <div class="container is-fluid">
    <div class="box">
      <h1 class="title">撰写文章</h1>

      <div>
        <label class="label">标题</label>
        <input class="input" type="text" placeholder="文章标题">
      </div>

      <br />

      <div>
        <label class="label">内容</label>
        <textarea class="textarea" name="" id="" placeholder="正文内容"></textarea>
      </div>

      <br />
      <div>
        <button class="button is-primary">提交</button>
        <button class="button is-link">取消</button>
      </div>

    </div>
  </div>
</template>

<script>
  export default {
    created: function () {
      console.log(1)
    }
  }
</script>
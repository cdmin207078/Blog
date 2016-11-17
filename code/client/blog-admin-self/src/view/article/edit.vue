<template>
  <div>
    <section class="section is-shadow-border">
      <div class="heading">
        <h1 class="title">New Article</h1>
        <hr />
      </div>

      <h1 class="title">id: {{ articleid }}</h1>

      <p class="control">
        <label for="" class="label">title</label>
        <input class="input is-medium" type="text" placeholder="title" v-model="article.title">
      </p>

      <p class="control">
        <label class="label">content</label>
        <textarea id="editor" class="textarea" placeholder="文若流沙,思若泉涌">{{ article.content }}</textarea>
      </p>


      <p class="control">
        <button class="button is-primary" @click="save">Save</button>
        <button class="button is-link">Cancel</button>
      </p>

    </section>
  </div>
</template>

<style>
  @import '~simditor/styles/simditor.css';
</style>

<script>
  import Simditor from 'simditor'
  import $ from 'jquery'

  export default {
    data() {
      return {
        isAdd: true,        
        article: null,
        editor: null,
        article: {
          id: 0,
          title: 'AA',
          content: 'AA'
        }
      }
    },
    created() {
      this.articleid = this.$route.params.id;
    },
    mounted() {
      this.initeditor();
      
      let id = this.$route.params.id;
      
      if(id){
        console.log('has id');
        this.isAdd = false;
      }
    },
    methods: {
      initeditor() {
        this.editor  = new Simditor({
          textarea: '#editor',
          toolbarFloatOffset: 50,
          toolbar: [ 'title','bold','italic','underline','strikethrough','fontScale','color','ol','ul','blockquote','code','table','link','image','hr','indent','outdent','alignment',]
        })
      },
      save() {
        this.article.content = this.editor.getValue();
        console.log(this.article)

        this.$http.post('/api/article/save').then((response) => {
          alert('成功') 
        },(response) => {
          alert('请求失败')
        });
      }
    },
  }
</script>
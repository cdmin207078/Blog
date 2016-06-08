#Jquery 插件开发札记

> 相关资料链接
[jQuery插件开发精品教程，让你的jQuery提升一个台阶](http://www.cnblogs.com/Wayou/p/jquery_plugin_tutorial.html) - 博客园 刘哇勇的部落格

## jQuery插件开发方式
根据《jQuery高级编程》的描述，jQuery插件开发方式主要有三种：

 - 通过`$.extend()`来扩展jQuery
 - 通过`$.fn` 向jQuery添加新的方法
 - 通过`$.widget()`应用jQuery UI的部件工厂方式创建

## $.extend()
向jQuery添加了一个自定义函数,然后通过`$`直接调用.
但这种方式无法利用jQuery强大的选择器带来的便利，要处理DOM元素以及将插件更好地运用于所选择的元素身上，还是需要使用第二种开发方式。你所见到或使用的插件也大多是通过此种方式开发。
```javascript
// 使用 $.extend()来扩展Jquery
$.extend({
	sayHello : function (name) {
		console.log('hello, ' + (name? name: 'Dude') + '!');
	},
	log:function (message) {
		var now = new Date(),
			y = now.getFullYear(),
            m = now.getMonth() + 1, //！JavaScript中月分是从0开始的
            d = now.getDate(),
            h = now.getHours(),
            min = now.getMinutes(),
            s = now.getSeconds(),
            time = y + '/' + m + '/' + d + ' ' + h + ':' + min + ':' + s;
        console.log(time + ' My App: ' + message);
	}
});

$.sayHello();
$.sayHello('chen.ning');
$.log('呵呵哒');
```

##$.fn.xxPlugin()
本上就是往$.fn上面添加一个方法，名字是我们的插件名称。然后我们的插件代码在这个方法里面展开
```javascript
$.fn.myPlugin = function() {
    //在这里面,this指的是用jQuery选中的元素
    //example :$('a'),则this=$('a')
    this.css('color', 'red');
}
```
在插件名字定义的这个函数内部，this指代的是我们在调用该插件时，用jQuery选择器选中的元素，一般是一个jQuery类型的集合。比如$('a')返回的是页面上所有a标签的集合，且这个集合已经是jQuery包装类型了，也就是说，在对其进行操作的时候可以直接调用jQuery的其他方法而不需要再用美元符号来包装一下。


##支持链式调用
我们都知道jQuery一个时常优雅的特性是支持链式调用，选择好DOM元素后可以不断地调用其他方法。
要让插件不打破这种链式调用，只需return一下即可。
```javascript
$.fn.myPlugin = function() {

	//要让插件不打破这种链式调用，只需return一下即可。
    return this.each(function() {
        //对每个元素进行操作
        $(this).append(' ' + $(this).attr('href'));
    }))

    //或者
    //return this;
}
```

##接受参数
在处理插件参数的接收上，通常使用jQuery的extend方法，上面也提到过，但那是给extend方法传递单个对象的情况下，这个对象会合并到jQuery身上，所以我们就可以在jQuery身上调用新合并对象里包含的方法了，像下面的例子。当给extend方法传递一个以上的参数时，它会将所有参数对象合并到第一个里。同时，如果对象中有同名属性时，合并的时候后面的会覆盖前面的。

利用这一点，我们可以在插件里定义一个保存插件参数默认值的对象，同时将接收来的参数对象合并到默认对象上，最后就实现了用户指定了值的参数使用指定的值，未指定的参数使用插件默认值。


**保护好默认参数**
注意到下面代码调用extend时会将defaults的值改变，这样不好，因为它作为插件因有的一些东西应该维持原样，另外就是如果你在后续代码中还要使用这些默认值的话，当你再次访问它时它已经被用户传进来的参数更改了。

一个好的做法是将一个新的空对象做为$.extend的第一个参数，defaults和用户传递的参数对象紧随其后，这样做的好处是所有值被合并到这个空对象上，保护了插件里面的默认值。

![asd](http://images.cnitblog.com/blog/431064/201402/281858115012654.png)
```javascript
$.fn.myPlugin = function(options) {
    var defaults = {
        'color': 'red',
        'fontSize': '12px'
    };
    var settings = $.extend({},defaults, options);
    return this.css({
        'color': settings.color,
        'fontSize': settings.fontSize
    });
}
```


到此，插件可以接收和处理参数后，就可以编写出更健壮而灵活的插件了。若要编写一个复杂的插件，代码量会很大，如何组织代码就成了一个需要面临的问题，没有一个好的方式来组织这些代码，整体感觉会杂乱无章，同时也不好维护，所以将插件的所有方法属性包装到一个对象上，用面向对象的思维来进行开发，无疑会使工作轻松很多。


##面向对象的插件开发
为什么要有面向对象的思维，因为如果不这样，你可能需要一个方法的时候就去定义一个function，当需要另外一个方法的时候，再去随便定义一个function，同样，需要一个变量的时候，毫无规则地定义一些散落在代码各处的变量。

还是老问题，不方便维护，也不够清晰。当然，这些问题在代码规模较小时是体现不出来的。

如果将需要的重要变量定义到对象的属性上，函数变成对象的方法，当我们需要的时候通过对象来获取，一来方便管理，二来不会影响外部命名空间，因为所有这些变量名还有方法名都是在对象内部。

```javascript
<!-- 面向对象 插件开发 -->
<!-- 对象定义 -->
<script type="text/javascript">
	var Beautifier = function (ele,opt) {
		this.$element = ele,
		this.defaults = {
			'color': 'bLue',
			'fontSize': '20px',
			'textDecoration':'line-through',
			'backgroundColor':'#fff'
		},
		this.options = $.extend({},this.defaults,opt);
	}

	Beautifier.prototype = {
		beautify: function () {
			return this.$element.css({
				'color': this.options.color,
				'fontSize': this.options.fontSize,
				'textDecoration': this.options.textDecoration,
				'backgroundColor': this.options.backgroundColor
			});
		}
	}
</script>

<!-- 插件定义 -->
<script type="text/javascript">
;
	$.fn.SetCss = function(options) {
		var beauti = new Beautifier(this,options);
		
		return beauti.beautify();
		//或者 
		// return this;
	}
</script>

<!-- 使用插件 -->
<script type="text/javascript">
	$(function(){
		$('a').SetCss({
			'color': 'red',
			'backgroundColor':'#eee',
			'fontSize': '18px',
			'textDecoration':'underline'
		}).mouseover(function(){
			alert($(this).text());
		});
	});
</script>
```


##关于命名空间

### 用自调用匿名函数包裹你的代码
始终用自调用匿名函数包裹你的代码中，那么就不会污染全局命名空间，同时不会和别的代码冲突。

```javascript
(function() {
    //定义Beautifier的构造函数
    var Beautifier = function(ele, opt) {
        this.$element = ele,
        this.defaults = {
            'color': 'red',
            'fontSize': '12px',
            'textDecoration': 'none'
        },
        this.options = $.extend({}, this.defaults, opt)
    }
    //定义Beautifier的方法
    Beautifier.prototype = {
        beautify: function() {
            return this.$element.css({
                'color': this.options.color,
                'fontSize': this.options.fontSize,
                'textDecoration': this.options.textDecoration
            });
        }
    }
    //在插件中使用Beautifier对象
    $.fn.myPlugin = function(options) {
        //创建Beautifier的实体
        var beautifier = new Beautifier(this, options);
        //调用其方法
        return beautifier.beautify();
    }
})();
```

### 代码开头加一个分号
充当自调用匿名函数的第一对括号与上面别人定义的函数相连，因为中间没有分号，代码无法正常解析了，所以报错。
所以好的做法是我们在代码开头加一个分号，这在任何时候都是一个好的习惯。

```javascript
var foo=function(){
    //别人的代码
}//注意这里没有用分号结尾

//开始我们的代码。。。
;(function(){
    //我们的代码。。
    alert('Hello!');
})();
```

###将系统变量以变量形式传递到插件内部
当我们这样做之后，window等系统变量在插件内部就有了一个局部的引用，可以提高访问速度，会有些许性能的提升
最后我们得到一个非常安全结构良好的代码：

```javascript
;(function($,window,document,undefined){

})(jQuery,window,document);
```

> 因为 ecmascript 执行JS代码是从里到外，因此把全局变量window或jQuery对象传进来，就避免了到外层去寻找，提高效率。undefined在老一辈的浏览器是不被支持的，直接使用会报错，js框架要考虑到兼容性，因此增加一个形参undefined。
> 还有，不要用window.undefined传递给形参，有可能window.undefined被其他人修改了，最好就是甚么都不传，形参的undefined就是真正的undefined了。

```javascript
var undefined = 8;  
(function( window ) {   
    alert(window.undefined); // 8  
    alert(undefined); // 8  
})(window);  

<!-- 传入 undefined -->
var undefined = 8;  
(function( window, undefined ) {   
    alert(window.undefined);  // 8  
    alert(undefined); // 此处undefined参数为局部的名称为undefined变量，值为undefined  
})(window);  
```

##代码混淆与压缩




##其它事项

**引号的使用**既然都扯了这些与插件主题无关的了，这里再多说一句，**一般HTML代码里面使用双引号，而在JavaScript中多用单引号**，比如下面代码所示：

```javascript
var name = 'Wayou';
document.getElementById(‘example’).innerHTML = '< a href="http: //wayouliu.duapp.com/">'+name+'</a>'; //href=".." HTML中保持双引号，JavaScript中保持单引号
```

一方面，HTML代码中本来就使用的是双引号，另一方面，在JavaScript中引号中还需要引号的时候，要求我们单双引号间隔着写才是合法的语句，除非你使用转意符那也是可以的。再者，坚持这样的统一可以保持代码风格的一致，不会出现这里字符串用双引号包着，另外的地方就在用单引号。
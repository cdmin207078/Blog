#CSS - Transition \ Transform \ Animation


> https://www.w3.org/TR/css3-transitions/  -- * w3c css transition *
  http://www.zhangxinxu.com/wordpress/2010/11/css3-transitions-transforms-animation-introduction/  -- *张鑫旭 CSS3 Transitions, Transforms和Animation使用简介与应用展示*
  http://www.zhangxinxu.com/wordpress/2012/09/css3-3d-transform-perspective-animate-transition/ -- *张鑫旭 transform 3D* 
  http://www.w3school.com.cn/cssref/pr_transform.asp  -- *w3cschool css transform*
  http://www.bbs0101.com/archives/248.html -- *transform 属性说明*

---

## transitions - 过渡


**transition** 允许css的属性值在一定的时间区间内平滑地过渡。这种效果可以在鼠标单击、获得焦点、被点击或对元素任何改变中触发，并圆滑地以动画效果改变CSS的属性值.
> `transition-property` 指定过渡的性质，比如transition-property:backgrond 就是指backgound参与这个过渡
  `transition-duration` 指定这个过渡的持续时间
  `transition-delay`    延迟过渡时间
  `transition-timing-function` 指定过渡类型，有ease | linear | ease-in | ease-out | ease-in-out | cubic-bezier 

并不是什么属性改变都为触发transition动作效果，比如页面的自适应宽度，当浏览器改变宽度时，并不会触发transition的效果。
timing-function 取值参考 : [Properties from CSS](http://www.w3.org/TR/css3-transitions/#properties-from-css-)

## transform - 变换 \ 改变 \ 改观

####skew - 倾斜

####scale - 缩放

| 代码 | 描述 |
| :-------- | :--------|
| transform: scale(a); | 元素x和y方向均缩放a倍 |
| transform: scale(a, b); | 元素x方向缩放a倍，y方向缩放b倍 |
| transform: scaleX(a); | 元素x方向缩放a倍，y方向不变 |
| transform: scaleY(b); |元素y方向缩放b倍，x方向不变 |

####rotate - 翻转

####translate - 移动



## animation - 动画



## Sample - 实例 
```html
<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8" />
	<title></title>
	<style type="text/css">
	* { margin: 0 0; padding: 0 0; }
	div { margin: 10px 20px; border: 1px #444 dashed; }

	h1 { border-bottom: 5px #ccf solid; max-width: 300px; }

	.animte03 {

		/*-webkit-transition-property: background-color;
		-webkit-transition-duration: 0.5s;
		-webkit-transition-timing-function: ease;*/	

		transition: all 0.5s ease;
		-webkit-transition: all 0.5s ease;
		-moz-transition: all 0.5s ease;
		-o-transition: all 0.5s ease;	

		/*transition-delay: 1s;*/
	}
	#night {
		width: 100px; 
		height: 100px;
	}

	#night:hover {
		/*background-color: #555;*/
		color: #eee;
		/*border-radius: 20px;*/
		transform: rotateY(30deg);
	}

	#boxzone {
		position: relative;
	}
	.box {
		width: 50px;
	}

	#boxzone:hover .box{
		margin-left: 500px;
		transform: rotate(360deg);

	}

	.transition-ease {
		transition: all 1s ease;
		-webkit-transition: all 1s ease;
		-moz-transition: all 1s ease;
		-o-transition: all 1s ease;	
	}
	.transition-linear {
		transition: all 1s linear;
		-webkit-transition: all 1s linear;
		-moz-transition: all 1s linear;
		-o-transition: all 1s linear;	
	}
	.transition-ease-in {
		transition: all 1s ease-in;
		-webkit-transition: all 1s ease-in;
		-moz-transition: all 1s ease-in;
		-o-transition: all 1s ease-in;	
	}
	.transition-ease-out {
		transition: all 1s ease-out;
		-webkit-transition: all 1s ease-out;
		-moz-transition: all 1s ease-out;
		-o-transition: all 1s ease-out;	
	}
	.transition-ease-in-out {
		transition: all 1s ease-in-out;
		-webkit-transition: all 1s ease-in-out;
		-moz-transition: all 1s ease-in-out;
		-o-transition: all 1s ease-in-out;	
	}
	.transition-cubic-bezier {
		transition: all 1s cubic-bezier(1, 0.15, 0.26, 0.84);
		-webkit-transition: all 1s cubic-bezier(1, 0.15, 0.26, 0.84);
		-moz-transition: all 1s cubic-bezier(1, 0.15, 0.26, 0.84);
		-o-transition: all 1s cubic-bezier(1, 0.15, 0.26, 0.84);
	}

	</style>

	<style type="text/css">
	#tf {
		perspective: 20px;
		transform: ;
	}
	#tf .box {
		margin: 30px 30px;
	}
	.skew {
		transform: skew(-15deg);
	}
	.scale {
		transform: scale(1.5);
	}
	.rotate {
		transform: rotate(15deg);
	}
	.translate {
		transform: translate(10px,20px);
	}

	.skew-scale-rotate-translate {
		transform: skew(-15deg) scale(1.5) rotate(15deg) translate(10px,20px);
	}
	</style>


	<style type="text/css">
	#img_p_logo {
		margin: 20px 30px;
		padding: 3px ;
		border: 1px #ccc solid;
		box-shadow: 0px 0px 15px #ddd;
		/*transform: scale(0.8);*/
	}

	@keyframes glow {
		0% {
			box-shadow: 0 0 20px rgba(240,240,240,0.5) ;
		}
		100% {
			box-shadow: 0 0 20px rgba(0,0,150,1) ;
			/*transform: perspective(1000px) rotateX(30deg) rotateY(-15deg) rotateZ(30deg);*/
			/*transform: rotate(180deg);*/
			transform: perspective(2000px) rotateX(5deg) rotateZ(-1deg);
		}
	}

	#img_p_logo:hover {
		-webkit-animation: glow 1s ease-in-out  infinite alternate;
		-o-animation: glow 1s ease-in-out  infinite alternate;
		animation: glow 1s ease-in-out  infinite alternate;
	}

	h1 { text-transform: capitalize;}
	</style>
</head>
<body>

<h1>transition</h1>
<div id="night" class="animte03">鼠标滑过,夜幕低垂</div>

<div id="boxzone">
	<div id="box1" class="box transition-ease">
		<span>ease</span>
	</div>	
	<div id="box2" class="box transition-linear">
		<span>linear</span>
	</div>	
	<div id="box3" class="box transition-ease-in">
		<span>ease-in</span>
	</div>	
	<div id="box4" class="box transition-ease-out">
		<span>ease-out</span>
	</div>	
	<div id="box5" class="box transition-ease-in-out">
		<span>ease-in-out</span>
	</div>	
	<div id="box6" class="box transition-cubic-bezier">
		<span>cubic-bezier</span>
	</div>	
</div>

<hr />
<h1>transform</h1>
<div id="tf">
	<div class="box skew">skew - 倾斜</div>
	<div class="box scale">scale - 缩放</div>
	<div class="box rotate">rotate - 旋转</div>
	<div class="box translate">translate - 移动</div>
	<div class="box skew-scale-rotate-translate">skew-scale-rotate-translate - 综合</div>
</div>

<hr />
<h1>animation</h1>
<img id="img_p_logo" src="https://avatars1.githubusercontent.com/u/1762278?v=3&s=460" />


</body>
</html>
```
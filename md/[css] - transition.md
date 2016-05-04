#CSS - Transition \ Transform \ Animation


> https://www.w3.org/TR/css3-transitions/  -- * w3c css transition *
  http://www.zhangxinxu.com/wordpress/2010/11/css3-transitions-transforms-animation-introduction/  -- * 张鑫旭 CSS3 Transitions, Transforms和Animation使用简介与应用展示 *
  http://www.w3school.com.cn/cssref/pr_transform.asp  -- * w3cschool css transform *
  http://www.bbs0101.com/archives/248.html -- * transform 属性说明 *

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

## animations - 动画



## Sample - 实例 
```html
<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8" />
	<title></title>
	<style type="text/css">
	* { margin: 0 0; padding: 0 0; }
	div {
		margin: 10px 20px;
		border: 1px #444 dashed;

	}
	.animte03 {
		/*-webkit-transition-property: background-color;
		-webkit-transition-duration: 0.5s;
		-webkit-transition-timing-function: ease;*/	

		transition: background-color 0.5s ease;
		-webkit-transition: background-color 0.5s ease;
		-moz-transition: background-color 0.5s ease;
		-o-transition: background-color 0.5s ease;	
	}
	#night {
		width: 100px; 
		height: 100px;
		transform: skew(-80deg);
	}
	#night:hover {
		background-color: #555;
		color: #eee;
	}

	#boxzone {
		position: relative;
	}
	.box {
		width: 50px;
	}

	#boxzone:hover .box{
		margin-left: 500px;
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

</head>
<body>

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

</body>
</html>
```
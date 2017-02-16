# Lesson 5: SimpleTrigger

> 原文: https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/simpletriggers.html

> 参考: http://www.cnblogs.com/shanyou/archive/2007/08/19/861940.html


如果需要让任务只在某个时刻执行一次, 或者, 在某个时刻开始, 然后按照某个时间间隔重复执行若干次. 例如: 让触发器在2017年8月20日上午11:23:54秒执行, 然后每个隔10秒钟重复执行一次, 并且这样重复5次. 那么 **`SimpleTrigger`** 就可以满足你的要求.

通过这样的描述, 你可能很惊奇地发现SimpleTrigger包括这些属性：开始时间, 结束时间, 重复次数, 重复间隔, 所有这属性都是你期望它所应具备的. 只有结束时间属性有些特殊.

重复次数可以为零, 正整数 或 常量值 `SimpleTrigger.RepeatIndefinitely`. 重复间隔属性必须为 `TimeSpan.Zero` 或正 TimeSpan 值. **注意：如果指定的重复间隔时间是 0，那么会导致触发器按照‘重复数量’定义的次数并发触发（或者接近并发）**.

If you’re not already familiar with the DateTime class, you may find it helpful for computing your trigger fire-times, depending on the startTimeUtc (or endTimeUtc) that you’re trying to create.

EndTime（如果这个属性被设置）属性会覆盖重复次数属性, 这对创建一个每隔10秒就触发一次直到某个时间结束的触发器非常有用, 这就可以不计算开始时间和结束时间之间的重复数量. 也可以指定一个结束时间, 然后使用REPEAT_INDEFINITELY作为重复数量(甚至可以指定一个大于结束时间之前实际重复次数的整数作为重复次数). 也就是说, EndTime属性控制权高于重复次数属性。



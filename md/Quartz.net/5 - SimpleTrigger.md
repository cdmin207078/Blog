# Lesson 5: SimpleTrigger

> 原文: https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/simpletriggers.html

> 参考: http://www.cnblogs.com/shanyou/archive/2007/08/19/861940.html


如果需要让任务只在某个时刻执行一次, 或者, 在某个时刻开始, 然后按照某个时间间隔重复执行若干次. 例如: 让触发器在2017年8月20日上午11:23:54秒执行, 然后每个隔10秒钟重复执行一次, 并且这样重复5次. 那么 **`SimpleTrigger`** 就可以满足你的要求.

通过这样的描述, 你可能很惊奇地发现SimpleTrigger包括这些属性：开始时间, 结束时间, 重复次数, 重复间隔, 所有这属性都是你期望它所应具备的. 只有结束时间属性有些特殊.

重复次数可以为零, 正整数 或 常量值 `SimpleTrigger.RepeatIndefinitely`. 重复间隔属性必须为 `TimeSpan.Zero` 或正 TimeSpan 值. **注意：如果指定的重复间隔时间是 0，那么会导致触发器按照‘重复数量’定义的次数并发触发（或者接近并发）**.

If you’re not already familiar with the DateTime class, you may find it helpful for computing your trigger fire-times, depending on the startTimeUtc (or endTimeUtc) that you’re trying to create.

EndTimeUtc（如果这个属性被设置）属性会覆盖重复次数属性, 这对创建一个每隔10秒就触发一次直到某个时间结束的触发器非常有用, 这就可以不计算开始时间和结束时间之间的重复数量. 也可以指定一个结束时间, 然后使用REPEAT_INDEFINITELY作为重复数量(甚至可以指定一个大于结束时间之前实际重复次数的整数作为重复次数). 也就是说, EndTime属性控制权高于重复次数属性。

SimpleTrigger 实例使用 `TriggerBuilder`(用于触发器的主要属性) 和 `WithSimpleSchedule` 扩展方法(用于SimpleTrigger特定的属性)构建

**创建一个指定时间执行的触发器, 不重复执行**
```csharp
var myStartTime = new DateTimeOffset(DateTime.Now.AddMinutes(1));

IJobDetail job = JobBuilder.Create<HelloJob>()
    .WithIdentity("job1", "group1")
    .Build();

ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartAt(myStartTime)
    .Build();
```

**创建一个指定时间执行的触发器, 重复 10 次, 每次间隔 3s**
```csharp
var myStartTime = new DateTimeOffset(DateTime.Now.AddMinutes(1));

ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartAt(myStartTime)         //如果不指定 starttime, 则表示立即执行
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(2)
        .WithRepeatCount(10))     //注意, 10次重复表示将总共发生11次. 第一次触发之后, 再重复触发10次
    .Build();
```

**创建一个触发器, 在未来的 5分钟时, 只执行一次**
```csharp
ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Second))    // 使用 DateBuilder 来创建一个未来时间
    .Build();
```

**创建一个触发器,立即触发, 之后每 5分钟触发一次, 直到 22:00**
```csharp
var trigger = TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .WithSimpleSchedule(x => x
        .WithIntervalInMinutes(5)
        .RepeatForever())
    .EndAt(DateBuilder.DateOf(22, 0, 0))
    .Build();
```

**创建一个触发器,立即触发, 之后在每个小时整点重复触发一次, 触发间隔两小时, 无限重复**
```csharp
var trigger = TriggerBuilder.Create()
    .WithIdentity("trigger8")                   // 不指明 group, 则默认为 "default" 组
    .StartAt(DateBuilder.EvenHourDate(null))    // 未来的每个小时整点 (00:00 分钟 和 秒 都是0)
    .WithSimpleSchedule(x => x
        .WithIntervalInHours(2)
        .RepeatForever())
    .Build();
```


## SimpleTrigger 触发失败

> Trigger的另一个重要属性就是它的“misfire instruction(未触发指令)”. 如果因为scheduler被关闭而导致持久的触发器“错过”了触发时间, 这时, 未触发就发生了. 不同类型的触发器有不同的未触发指令, 缺省情况下, 他们会使用一个“智能策略”指令——根据触发器类型和配置的不同产生不同动作. 当scheduler开始时, 它查找所有未触发的持久triggers, 然后按照每个触发器所配置的未触发指令来更新它们. 当您在自己的项目中开始使用Quartz.NET时, 应熟悉定义在各个类型触发器上的未触发指令. 关于未触发指令信息的详细说明将在每种特定的类型触发器的指南课程中给出. 

SimpleTrigger有几个指令, 可以用来告知Quartz.NET当触发失败时应该做什么. 这些指令在 **MisfirePolicy.SimpleTrigger** 中定义为常量, 具体有如下指令:

- MisfireInstruction.IgnoreMisfirePolicy
- MisfirePolicy.SimpleTrigger.FireNow
- MisfirePolicy.SimpleTrigger.RescheduleNowWithExistingRepeatCount
- MisfirePolicy.SimpleTrigger.RescheduleNowWithRemainingRepeatCount
- MisfirePolicy.SimpleTrigger.RescheduleNextWithRemainingCount
- MisfirePolicy.SimpleTrigger.RescheduleNextWithExistingCount

如果使用了“智能策略”指令, SimpleTrigger会根据给定的SimpleTrigger实例的配置和状态在其各种 MISFIRE 指令之间动态选择. `SimpleTrigger.UpdateAfterMisfire` 方法的文档解释了此动态行为的确切详细信息。

您可以在创建 SimpleTrigger时, 指定 "未触发"指令.
```csharp
trigger = TriggerBuilder.Create()
    .WithIdentity("trigger7", "group1")
    .WithSimpleSchedule(x => x
        .WithIntervalInMinutes(5)
        .RepeatForever()
        .WithMisfireHandlingInstructionNextWithExistingCount())
    .Build();
```
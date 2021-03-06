# Lesson 6: CronTrigger

> 原文: https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/crontriggers.html

> 参考: http://www.cnblogs.com/shanyou/archive/2007/08/25/869073.html

如果你需要像日历那样按日程来触发任务, 而不是像 SimpleTrigger 那样每隔特定的间隔时间触发, CronTriggers通常比SimpleTrigger更有用.

使用CronTrigger, 你可以指定诸如“每个周五中午”, 或者“每个工作日的9:30”或者“从每个周一、周三、周五的上午9：00到上午10：00之间每隔五分钟”这样日程安排来触发. 甚至, 象SimpleTrigger一样, CronTrigger也有一个StartTime以指定日程从什么时候开始, 也有一个（可选的）EndTime以指定何时日程不再继续.

## Cron Expressions - Cron 表达式

Cron表达式 被用来配置CronTrigger实例. Cron表达式 是一个由7个子表达式组成的字符串, 每个子表达式都描述了一个单独的日程细节, 这些子表达式用空格分隔, 分别表示：

- 1.Seconds
- 2.Minutes
- 3.Hours
- 4.Day-of-Month
- 5.Month
- 6.Day-of-Week
- 7.Year (可选)

例如: **`"0 0 12 ? * WED"`** 表示 **`每周三的中午12：00`**

单个子表达式可以包含 范围/列表. 例如：前面例子中的周几这个字段（这里是"WED"）可以被替换为"MON-FRI", "MON, WED, FRI"或者甚至"MON-WED,SAT".

通配符 (*) 可以被用来表示域中 "每个" 可能的值. 因此在 "Month" 域中的 * 表示每个月, 而在Day-Of-Week域中的 * 则表示 "周中的每一天".

所有的域中的值都有特定的合法范围. 例如：秒和分域的合法值为0到59, 小时的合法范围是0到23, Day-of-Month中值得合法凡范围是0到31, 但是需要注意不同的月份中的天数不同. 月份的合法值是0到11. 或者用字符串JAN,FEB MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV 及DEC来表示. Days-of-Week可以用1到7来表示（1 = 星期日）或者用字符串SUN, MON, TUE, WED, THU, FRI 和SAT来表示.

'/' 字符用来表示值的增量. 例如, 如果分钟域中放入'0/15', 它表示“每隔15分钟, 从0开始”，如果在份中域中使用'3/20', 则表示“小时中每隔20分钟, 从第3分钟开始”或者另外相同的形式就是'3,23,43'.

'?' 字符可以用在 day-of-month 及 day-of-week 域中, 它用来表示“没有指定值”. 这对于需要指定一个或者两个域的值而不需要对其他域进行设置来说相当有用.

'L' 字符可以在day-of-month及day-of-week中使用, 这个字符是 "last" 的简写, 但是在两个域中的意义不同. 例如, 在 day-of-month 域中的"L"表示这个月的最后一天, 即, 一月的31日, 非闰年的二月的28日. 如果它用在day-of-week中, 则表示"7"或者"SAT". 但是如果在day-of-week域中, 这个字符跟在别的值后面, 则表示"当月的最后的周XXX". 例如："6L" 或者 "FRIL"都表示本月的最后一个周五. 当使用 'L' 选项时, 最重要的是不要指定列表或者值范围, 否则会导致混乱.

'W' 字符用来指定距离给定日最接近的周几 (在day-of-week域中指定), 例如：如果你为day-of-month域指定为"15W",则表示“距离月中15号最近的周几”.

'#' 表示表示月中的第几个周几, 例如：day-of-week域中的"6#3" 或者 "FRI#3"表示“月中第三个周五”. 

## Example Cron Expressions (表达式示例)
这里有几个表达式示例与说明, 更多内容可以参见 API 文档

`"0 0/5 * * * ?"` -- **每5分钟执行一次**

`"10 0/5 * * * ?"` -- **在每分钟的10秒后每隔5分钟触发一次的表达式(例如. 10:00:10 am, 10:05:10等.)**

`"0 30 10-13 ? * WED,FRI"` -- **在每个周三和周五的10：30，11：30，12：30触发的表达式**

`"0 0/30 8-9 5,20 * ?"` -- **在每个月的5号, 20号的8点和10点之间每隔半个小时触发一次且不包括10点, 只是8：30, 9：00, 9：30**

> 有些情况, 无法用单个触发器表示, 例如“上午9:00至10:00之间每5分钟，下午1:00至10:00之间每20分钟”. 在这种情况下的解决方案是简单地创建两个触发器, 并注册它们以运行相同的作业。


## Building CronTriggers (创建 CronTrigger)

CronTrigger 实例使用 `TriggerBuilder`(用于触发器的主要属性) 和 `WithCronSchedule` 扩展方法(用于CronTrigger特定的属性)构建. 同时也可以使用 CronScheduleBuilder 的静态方法来创建.

**每天 8:00 - 17:00, 从0分0秒开始, 每 2分钟时, 执行一次**

```csharp
trigger = TriggerBuilder.Create()
    .WithIdentity("trigger3", "group1")
    .WithCronSchedule("0 0/2 8-17 * * ?")
    .ForJob("myJob", "group1")
    .Build();
```

**每天 10:42 执行**

```csharp
// 使用 CronScheduleBuilder 静态方法创建
trigger = TriggerBuilder.Create()
    .WithIdentity("trigger3", "group1")
    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(10, 42))
    .ForJob(myJobKey)
    .Build();
```
或者
```csharp
trigger = TriggerBuilder.Create()
    .WithIdentity("trigger3", "group1")
    .WithCronSchedule("0 42 10 * * ?")
    .ForJob("myJob", "group1")
    .Build();
```


**周三 10:43触发, 指定时区**

```csharp
trigger = TriggerBuilder.Create()
    .WithIdentity("trigger3", "group1")
    .WithSchedule(CronScheduleBuilder
        .WeeklyOnDayAndHourAndMinute(DayOfWeek.Wednesday, 10, 42)
        .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time")))
    .ForJob(myJobKey)
    .Build();
```
或者
```csharp
trigger = TriggerBuilder.Create()
    .WithIdentity("trigger3", "group1")
    .WithCronSchedule("0 42 10 ? * WED", x => x
        .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time")))
    .ForJob(myJobKey)
    .Build();
```

## CronTrigger Misfire Instructions (CronTrigger 触发失败说明)

下面指令, 用来告知Quartz.NET当触发失败时应该怎么做. 这些指令被定义作为常数, 该指令包括:

- MisfireInstruction.IgnoreMisfirePolicy
- MisfireInstruction.CronTrigger.DoNothing
- MisfireInstruction.CronTrigger.FireOnceNow

所有的 Trigger 默认的未触发指令为 **MisfireInstrution.SmartPolicy(智能策略)**, CronTrigger 为 **MisfireInstruction.CronTrigger.FireOnceNow**. API 文档的CronTrigger.UpdateAfterMisfire()方法有相关信息的详细介绍. 

在创建 CronTrigger 时, 可以指定 未触发指令:
```csharp
trigger = TriggerBuilder.Create()
    .WithIdentity("trigger3", "group1")
    .WithCronSchedule("0 0/2 8-17 * * ?", x => x
        .WithMisfireHandlingInstructionFireAndProceed())
    .ForJob("myJob", "group1")
    .Build();
```

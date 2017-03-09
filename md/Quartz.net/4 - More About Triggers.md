# Lesson 4: More About Triggers

> 原文: https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/more-about-triggers.html

> 参考: http://www.cnblogs.com/shanyou/archive/2007/08/19/861931.html

同Job一样, trigger非常容易使用, 但它有一些可选项需要注意和理解. 同时, trigger有不同的类型, 您按照具体需求进行选择.

## Common Trigger Attributes

所有类型的触发器, 除了包含用来追踪其身份的 **TriggerKey**属性以外, 还有一些其它重要属性. 这些属性可以在创建触发器的时候, 使用 TriggerBuilder设置.

- **`JobKey`** - 触发器触发时执行的 Job(作业) 标识
- **`StartTimeUtc`** - 表示触发器何时开始生效. 该值是一个 `DateTimeOffset` 对象, 用于定义给定日历日期的时间点. 例如: 你在一月份的时候创建一个计划, "每月第5天执行"的触发器, 如果 **StartTimeUtc** 设置为 4月1日, 那么之前几个月将会空闲.
- **`EndTimeUtc `** - 表示触发器何时不再生效. 例如: 触发器指定 "每月5号执行" 并且 "结束日期为7月1号", 那么 6月5号 则是最后一次触发时间.

其它的属性, 会在之后的内容中详细介绍.

## Priority (优先级)

当您有很多触发器 (或Quartz.NET线程池中的工作线程很少) 时, Quartz.NET可能没有足够的资源立即触发所有计划同时触发的触发器. 在这种情况下, 您可能想要控制哪些触发器在可用的Quartz.NET工作线程上优先触发. 为此, 您可以在触发器上设置priority属性. 如果N个触发器同时触发, 但当前仅有Z个工作线程, 则将首先执行具有最高优先级的触发器. 如果您不在触发器上设置优先级, 则它将使用默认优先级 **5**. 优先级为整数值,可正可负。

**Note:** 仅当触发器具有相同的触发时间时才比较优先级, 计划在10:59触发的触发器将总是在一个计划在11:00触发之前触发. 
**Note:** 当检测到触发器的作业需要恢复时, 将按与原始触发器相同的优先级安排其恢复

## Misfire Instructions (未触发指令)

Trigger的另一个重要属性就是它的“misfire instruction(未触发指令)”. 如果因为scheduler被关闭而导致持久的触发器“错过”了触发时间, 这时, 未触发就发生了. 不同类型的触发器有不同的未触发指令, 缺省情况下, 他们会使用一个“智能策略”指令——根据触发器类型和配置的不同产生不同动作. 当scheduler开始时, 它查找所有未触发的持久triggers, 然后按照每个触发器所配置的未触发指令来更新它们. 当您在自己的项目中开始使用Quartz.NET时, 应熟悉定义在各个类型触发器上的未触发指令. 关于未触发指令信息的详细说明将在每种特定的类型触发器的指南课程中给出. 


## Calendars (日历)
Quartz Calendar对象在trigger被存储到scheduler时与trigger相关联, Calendar可用于在trigger触发日程中的排除时间块. 例如：你想要创建一个在每个工作日上午9：30触发一个触发器, 那么就添加一个排除所有节假日的 Calendar.

Calendar可以是任何实现Calendar接口的序列化对象, 看起来如下:

```csharp
namespace Quartz
{
    public interface ICalendar : ICloneable
    {
        //
        // Summary:
        //     Set a new base calendar or remove the existing one. Get the base calendar.
        ICalendar CalendarBase { get; set; }
        //
        // Summary:
        //     Gets or sets a description for the Quartz.ICalendar instance - may be useful
        //     for remembering/displaying the purpose of the calendar, though the description
        //     has no meaning to Quartz.
        string Description { get; set; }

        //
        // Summary:
        //     Determine the next UTC time that is 'included' by the Calendar after the given
        //     UTC time.
        DateTimeOffset GetNextIncludedTimeUtc(DateTimeOffset timeUtc);
        //
        // Summary:
        //     Determine whether the given UTC time is 'included' by the Calendar.
        bool IsTimeIncluded(DateTimeOffset timeUtc);
    }
} 
```

尽管 `Calendar` 能够排除毫秒精度的时间, 为了提供方便, Quartz提供了一个 `HolidayCalendar`, 这个类可以排除整天的时间.

Calendars 必须被实例化, 然后通过 **AddCalendar** 方法注册到 scheduler 中, 如果使用HolidayCalendar, 在实例化之后, 你可以使用它的 **AddExcludedDate(DateTime excludedDate)** 方法来定义你想要从日程表中排除的时间. 同一个calendar实例可以被用于多个trigger中, 如下: 

```csharp
HolidayCalendar cal = new HolidayCalendar();
cal.AddExcludedDate(someDate);

sched.AddCalendar("myHolidays", cal, false);

ITrigger t = TriggerBuilder.Create()
        .WithIdentity("myTrigger")
        .ForJob("myJob")
        .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(9, 30)) // execute job daily at 9:30
        .ModifiedByCalendar("myHolidays") // but not on holidays
        .Build();

// .. schedule job with trigger
ITrigger t2 = TriggerBuilder.Create()
        .WithIdentity("myTrigger2")
        .ForJob("myJob2")
        .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(11, 30)) // execute job daily at 11:30
        .ModifiedByCalendar("myHolidays") // but not on holidays
        .Build();

// .. schedule job with trigger2 
```

触发器创建的细节将在接下来的小节中介绍, 我梦现在只要知道, 上面的代码创建了两个 Trigger(触发器), 每天都会由 scheduler 调度执行. 但是, 将跳过日历排除的的时间.

关于  ICalendar 的是各种实现, 请参阅 命名空间 `Quartz.Impl.Calendar` 中的类. 

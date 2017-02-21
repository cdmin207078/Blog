# Lesson 9: JobStores

> 原文: https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/job-stores.html

> 参考: http://www.cnblogs.com/shanyou/archive/2007/08/25/869110.html

JobStroe 负责保存所有的 scheduler “工作数据”：Job、Trigger、Calendar 等等, 选择合适的JobStore就显得非常重要, 如果你理解了不同的JobStore之间的差别, 那么选择就变得非常简单. 在您提供给SchedulerFactory的属性文件(或对象) 中声明您的调度程序应使用哪个JobStore(以及它的配置设置), 用于生成您的调度程序实例.

> 不要在代码中直接使用JobStore实例, 处于某些原因, 有很多开发者会这么做. JobStore 在 Quartz 中应该是在幕后工作, 你只需要告诉 Quartz 使用那个 JobStore (通过配置文件), 然后在你的代码中使用 Scheduler 对象即可.

## RAMJobStore

RAMJobStore 是使用上最简单的 JobStore, 它也拥有最高的性能(从 CPU 时间来计算). 顾名思义, 它将保存所有数据在 RAM(内存) 中, 这也是为什么它最轻快并且配置最简单. 缺点就是当应用结束时所有的日程信息都会丢失, 这意味着RAMJobStore不能满足Jobs和Triggers的 non-volatility(持久性). 对于有些应用来说, 这是可以接受的,甚至是期望的行为. 但是对于其他应用来说, 这将是灾难.

**配置 Quartz 使用 RAMJobStore**

```xml
quartz.jobStore.type = Quartz.Simpl.RAMJobStore, Quartz
```

要使用RAMJobStore(假设你使用StdSchedulerFactory), 你不需要做任何特殊的事情. Quartz.NET的默认配置使用RAMJobStore作为作业存储实现.

## ADO.NET Job Store (AdoJobStore)

AdoJobStore 顾名思义, 它通过ADO.NET将所有的数据保存在数据库中. 因此, 配置比RAMJobStore 稍显复杂, 也没有RamJobStore那么快. 但是性能的缺陷不是非常差, 尤其是如果你在数据库表的主键上建立索引.

要使用AdoJobStore, 首先必须创建一套Quartz使用的数据库表, 可以在Quartz 的database\tables找到创建库表的SQL脚本. 如果没有找到你的数据库类型的脚本, 那么找到一个已有的, 修改成为你数据库所需要的. 需要注意的一件事情就是所有Quartz库表名都以 QRTZ_ 作为前缀(例如：表"QRTZ_TRIGGERS",及"QRTZ_JOB_DETAIL"). 实际上, 可以你可以将前缀设置为任何你想要的前缀, 只要你告诉AdoJobStore那个前缀是什么即可(在你的Quartz属性文件中配置). 对于一个数据库中使用多个scheduler实例, 那么配置不同的前缀可以创建多套库表, 十分有用.






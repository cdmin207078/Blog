# Lesson 2: Jobs And Triggers

## Quzrtz API

重要的 接口(`interface`) & 类(`class`)

- `IScheduler` - the main API for interacting with the scheduler.
- `IJob` - an interface to be implemented by components that you wish to have executed by the scheduler.
- `IJobDetail` - used to define instances of Jobs.
- `ITrigger` - a component that defines the schedule upon which a given Job will be executed.
- `JobBuilder` - used to define/build JobDetail instances, which define instances of Jobs.
- `TriggerBuilder` - used to define/build Trigger instances.

```csharp
static void Main(string[] args)
{
    // construct a scheduler factory
    ISchedulerFactory schedFact = new StdSchedulerFactory();


    // get a scheduler
    IScheduler sched = schedFact.GetScheduler();
    sched.Start();

    // 定义job, 并指定使用的处理类 HelloJob
    IJobDetail job = JobBuilder.Create<HelloJob>()
        .WithIdentity("job", "group1")
        .Build();

    // 任务启动时触发job, 每1s 触发一次, 重复 15次
    ITrigger trigger = TriggerBuilder.Create()
        .WithIdentity("trigger", "group1")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(1)
            .WithRepeatCount(15))
        .Build();

    // 使用usingJobData 传递 job 参数
    IJobDetail jobDumb = JobBuilder.Create<DumbJob>()
            .WithIdentity("jobDumb", "group1")
            .UsingJobData("version", "1.0.0.0")
            .Build();

    ITrigger triggerDumb = TriggerBuilder.Create()
        .WithIdentity("triggerDumb", "group1")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(1)
            .WithRepeatCount(5))
        .Build();

    // 绑定 job 与 trigger
    sched.ScheduleJob(job, trigger);
    sched.ScheduleJob(jobDumb, triggerDumb);


    Thread.Sleep(3000);
    Console.WriteLine("暂停所有");
    sched.PauseAll();

    Thread.Sleep(3000);
    Console.WriteLine("恢复所有");
    sched.ResumeAll();

    Thread.Sleep(3000);
    Console.WriteLine("关闭所有");
    sched.Shutdown();

}

```

```csharp
public class HelloJob : IJob
{
    public void Execute(IJobExecutionContext context)
    {
        Console.WriteLine(context.JobDetail.Key.Name + " Hello " + DateTime.Now.ToString());
    }
}
```

```csharp
// 限定 job 须顺序执行, 限制并发
[DisallowConcurrentExecution]
class DumbJob : IJob
{
    public void Execute(IJobExecutionContext context)
    {
        var key = context.JobDetail.Key;
        var dataMap = context.JobDetail.JobDataMap;

        var version = dataMap.GetString("version");

        var rand = new Random().Next(1, 100);

        Console.WriteLine("dumb start - " + rand + " : Hello DumbJob, version : " + version);

        Thread.Sleep(10000);

        Console.WriteLine("dumb over - " + rand + " .");

    }
}
```

## Jobs and Triggers

Job 是实现 `IJob` 接口的类，它只有一个简单的方法：**`Execute`**

```csharp
namespace Quartz
{
    public interface IJob
    {
        void Execute(JobExecutionContext context);
    }
}
```

当 `Job` 的 `Trigger` 触发时, `Execute` 方法 由 `Scheduler` 的工作线程调用. 传递给此方法的 `JobExecutionContext` 对象, 提供了此 `Job` 其“`run-time`(运行时)” 环境信息:  一个 `Scheduler` 句柄, 一个 `Trigger` 句柄, `Job` 的 `JobDetail` 对象, 和其它一些项目.

`JobDetail` 对象在 `Job` 加入到 `Scheduler` 时创建, 它包含 `Job` 的各种属性设置. 以及一个 `JobDataMap` 对象 - 用来存储 `Job` 实例的状态信息. 它本质上是定义了一个 `Job` 实例.

`Trigger` 用来触发执行 `Job`. 当你期望排定一个 `Job`, 你可以创建一个 `Trigger`对象, 配置调整其属性来达到你的排定计划.`Trigger` 也包含一个 `JobDataMap` 对象, 可以用来通过触发器传递参数给 `Job`. 
Quzrtz 包含几个不同类型的 `Trigger`, 最常用的是 `SimpleTrigger` (*ISimpleTrigger*) 和 `CronTrigger` (*ICronTrigger*).

`SimpleTrigger` 用来触发只需执行一次或者在给定时间触发并且重复N次且每次执行延迟一定时间的任务. `CronTrigger`按照排定日期触发，例如“每个周五”，每个月10日中午或者10：15分。

> 为什么要分为 `Jobs`和 `Triggers`? 很多任务日程管理器没有将Jobs和Triggers进行区分. 一些产品中只是将 `job` 简单地定义为一个带有一些小任务标识的执行时间。其他产品则更像Quartz中 `job` 和 `trigger` 的联合。而开发Quartz的时候，我们决定对日程和按照日程执行的工作进行分离。在我们看来这有很多好处。
> 
> 例如：`jobs` 可以被创建并且存储在 job scheduler 中，而不依赖于 `trigger`. 而且很多 `triggers` 可以关联一个 `job`. 另外的好处就是这种“松耦合”能使与日程中的Job相关的trigger过期后重新配置这些Job,这样以后就能够重新将这些Job纳入日程而不必重新定义它们。这样就可以更改或者替换trigger而不必重新定义与之相连的job标识符。

## 身份标识

当向Quartz scheduler中注册Jobs 和Triggers时，它们要给出标识它们的名字。Jobs 和Triggers也可以被放入“组”中。“组”对于后续维护过程中，分类管理Jobs和Triggers非常有用。Jobs和Triggers的名字在组中必须唯一，换句话说，Jobs和Triggers真实名字是它的名字+组。如果使Job或者Trigger的组为‘null’，这等价于将其放入缺省的Scheduler.DEFAULT_GROUP组中。
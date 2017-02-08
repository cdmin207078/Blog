# Lesson 2: Jobs And Triggers

## Jobs and Triggers

Job 是实现 `IJob` 接口的类，它只有一个简单的方法：

**IJob Interface**
```csharp
namespace Quartz
{
    public interface IJob
    {
        void Execute(JobExecutionContext context);
    }
}
```





## Quzrtz API

重要的 接口(`interface`) & 类(`class`)

- `IScheduler` - the main API for interacting with the scheduler.
- `IJob` - an interface to be implemented by components that you wish to have executed by the scheduler.
- `IJobDetail` - used to define instances of Jobs.
- `ITrigger` - a component that defines the schedule upon which a given Job will be executed.
- `JobBuilder` - used to define/build JobDetail instances, which define instances of Jobs.
- `TriggerBuilder` - used to define/build Trigger instances.

**Example**
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
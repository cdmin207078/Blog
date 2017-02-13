#Lesson 3: More About Jobs & JobDetails

如你所见，`Job` 相当容易实现。本文将会向你介绍 `Jobs` 的相关详细信息. 关于 `Execute` 方法, `IJob` 接口, `JobDetails` 类

`JobDetails` 实例使用 `JobBuilder` 类来创建. `JobBuilder` 允许你使用 Fluent 方式来描述配置你的 `Jobs`

现在，我们花一些时间来讨论Quartz中Jobs的本质和Job实例的生命周期。首先让我们回顾一下第一课中所看到的代码片断：

```csharp
// construct a scheduler factory
ISchedulerFactory schedFact = new StdSchedulerFactory();

// get a scheduler
IScheduler sched = schedFact.GetScheduler();
sched.Start();

// define the job and tie it to our HelloJob class
IJobDetail job = JobBuilder.Create<HelloJob>()
	.WithIdentity("myJob", "group1")
	.Build();

// Trigger the job to run now, and then every 40 seconds
ITrigger trigger = TriggerBuilder.Create()
  .WithIdentity("myTrigger", "group1")
  .StartNow()
  .WithSimpleSchedule(x => x
	  .WithIntervalInSeconds(40)
	  .RepeatForever())
  .Build();
  
sched.ScheduleJob(job, trigger);
```

```csharp
public class HelloJob : IJob
{
  public void Execute(IJobExecutionContext context)
  {
    Console.WriteLine("HelloJob is executing.");
  }
}
```
> **注意** 我们给 scheduler(调度器) 传入了一个 `JobDetail` 实例，而且这个 `JobDetail` 实例只是简单提供了类名来引用被执行的 Job. 每次 scheduler 执行 job 时, 它就创建这个类的新实例, 然后调用该实例的 `Execute` 方法. 这种行为的后果之一是 `Job` 必须具有一个无参数构造函数。另一个后果是，在 `Job` 类上定义数据字段没有意义 - **因为它们的值不会在 Job 执行之间保留**

你可能现在想要问"如何为Job实例提供属性/配置？"和"如何跟踪Job在执行之间的状态？"这些问题的答案都是一样︰ `JobDataMap`, 它是 `JobDetail` 对象的一部分.

## JobDataMap
JobDataMap被用来保存一系列的(序列化的)对象, 这些对象在Job执行时可以得到。JobDataMap实现了IDictionary接口, 而且还增加了一些存储和读取基本类型数据的便捷方法。

> 在 Job 添加到 Scheduler 之前,将数据放入 JobDataMap 中:

```csharp
// define the job and tie it to our DumbJob class
IJobDetail job = JobBuilder.Create<DumbJob>()
	.WithIdentity("myJob", "group1") // name "myJob", group "group1"
	.UsingJobData("jobSays", "Hello World!")
	.UsingJobData("myFloatValue", 3.141f)
	.Build();
```

> Job 运行期间从 JobDataMap 中获取数据

```csharp
public class DumbJob : IJob
{
	public void Execute(JobExecutionContext context)
	{
	  JobKey key = context.JobDetail.Key;

	  JobDataMap dataMap = context.JobDetail.JobDataMap;

	  string jobSays = dataMap.GetString("jobSays");
	  float myFloatValue = dataMap.GetFloat("myFloatValue");

	  Console.Error.WriteLine("Instance " + key + " of DumbJob says: " + jobSays + ", and val is: " + myFloatValue);
	}
} 
```

如果使用 JobStore, 那么必须注意存放在JobDataMap中的内容. 因为放入JobDataMap中的内容将被序列化, 而且容易出现类型转换问题. 很明显, 标准.NET类型将是非常安全的, 但除此之外的类型, 任何时候, 只要有人改变了你要序列化其实例的类的定义, 就要注意是否打破了程序的兼容性.

或者, 您可以将AdoJobStore和JobDataMap设置为只有.net基本类型可以存储在映射中的模式,从而消除以后的序列化问题的任何可能性.







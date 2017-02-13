# Lesson 3: More About Jobs & JobDetails

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

如果你的 Job 包含与传入参入具有相同名称的属性, 并且具有 public 访问权限的 setter. 那么 Quartz 默认的 JobFactory 实现里, 在创建Job 实例时将会自动给这些属性赋值, 从而避免在 execute 方法里显示手动的从 map 中获取值.

Trigger 也具有 JobDataMap. 如果你有一个 Job 用到多个 trigger 来 定时 / 重复触发执行, 并且每个独立的触发器都需要带有不同的参数输入, 那么这个将会非常有用.

JobExecutionContext 中的 MergedJobDataMap 是 JobDetail 上的 JobDataMap 与 Trigger 上的 JobDataMap的合并, 若存在相同名称的参数, 则 trigger 会覆盖 JobDetail 上的值.


```csharp
IJobDetail jobDumb = JobBuilder.Create<DumbJob>()
      .WithIdentity("jobDumb", "group1")
      .UsingJobData("version", "1.0.0.0")
      .UsingJobData("ServerIp", "192.168.0.6")
      .Build();

ITrigger triggerDumb = TriggerBuilder.Create()
      .WithIdentity("triggerDumb", "group1")
      .UsingJobData("ServerIp", "127.0.0.1")	// 将会覆盖 JobDetail 中相同的 ServerIp 参数
      .StartNow()
      .WithSimpleSchedule(x => x
          .WithIntervalInSeconds(1)
          .WithRepeatCount(5))
      .Build();
```

```csharp
class DumbJob : IJob
{
    public string ServerIp { private get; set; }		// 定义同名属性, 则不需要再手动从 map 中获取参数

    public void Execute(IJobExecutionContext context)
    {
      Console.WriteLine("serverIp : " + ServerIp);
    }
}
```

## Job "Instances" (作业实例)
许多用户会困惑于究竟什么是 **Job Instance (作业实例)**, 在这个小节, 我们将解答您的疑惑, 并向您介绍 **Job State (作业状态)** 和 **并发**

You can create a single job class, and store many ‘instance definitions’ of it within the scheduler by creating multiple instances of JobDetails - each with its own set of properties and JobDataMap - and adding them all to the scheduler.

For example, you can create a class that implements the IJob interface called “SalesReportJob”. The job might be coded to expect parameters sent to it (via the JobDataMap) to specify the name of the sales person that the sales report should be based on. They may then create multiple definitions (JobDetails) of the job, such as “SalesReportForJoe” and “SalesReportForMike” which have “joe” and “mike” specified in the corresponding JobDataMaps as input to the respective jobs.

When a trigger fires, the JobDetail (instance definition) it is associated to is loaded, and the job class it refers to is instantiated via the JobFactory configured on the Scheduler. The default JobFactory simply calls the default constructor of the job class using Activator.CreateInstance, then attempts to call setter properties on the class that match the names of keys within the JobDataMap. You may want to create your own implementation of JobFactory to accomplish things such as having your application’s IoC or DI container produce/initialize the job instance.

In “Quartz speak”, we refer to each stored JobDetail as a “job definition” or “JobDetail instance”, and we refer to a each executing job as a “job instance” or “instance of a job definition”. Usually if we just use the word “job” we are referring to a named definition, or JobDetail. When we are referring to the class implementing the job interface, we usually use the term “job type”.


## Job State and Concurrency (作业状态 与 并发)
下来我们来了解一下作业的状态数据与并发. 这里有两个特性, 可以加在Job Class上, 用来影响Job 的行为

**`DisallowConcurrentExecution`** - 通知 Quartz 不要同时执行该 Job

**`PersistJobDataAfterExecution`** - 它在 Execute 方法成功完成(不抛出异常) 之后告知 Quartz 更新 JobDetail 的 JobDataMap 的存储副本, 以便下一次执行同一个 作业(JobDetail) 接收更新的值而不是原始存储的值. 与 **`DisallowConcurrentExecution`** 属性一样，这适用于 **Job definition instance(作业定义实例)**, 而不是 **Job class Instance(作业类实例)**, 

> 如果您使用 **`PersistJobDataAfterExecution`** 属性, 你应该考虑同时使用 **`DisallowConcurrentExecution`** 属性, 以避免在同一作业（JobDetail）的两个实例同时执行时, 数据可能存在混淆（竞态条件）.

## Other Attributes Of Jobs
Here’s a quick summary of the other properties which can be defined for a job instance via the JobDetail object:

Durability - if a job is non-durable, it is automatically deleted from the scheduler once there are no longer any active triggers associated with it. In other words, non-durable jobs have a life span bounded by the existence of its triggers.

RequestsRecovery - if a job “requests recovery”, and it is executing during the time of a ‘hard shutdown’ of the scheduler (i.e. the process it is running within crashes, or the machine is shut off), then it is re-executed when the scheduler is started again. In this case, the JobExecutionContext.Recovering property will return true.

## JobExecutionException

Finally, we need to inform you of a few details of the IJob.Execute(..) method. The only type of exception that you should throw from the execute method is the JobExecutionException. Because of this, you should generally wrap the entire contents of the execute method with a ‘try-catch’ block. You should also spend some time looking at the documentation for the JobExecutionException, as your job can use it to provide the scheduler various directives as to how you want the exception to be handled.













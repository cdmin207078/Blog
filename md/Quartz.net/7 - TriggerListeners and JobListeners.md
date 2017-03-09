# Lesson 7: TriggerListeners and JobListeners

> 原文: https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/trigger-and-job-listeners.html

> 参考: http://www.cnblogs.com/shanyou/archive/2007/08/25/869088.html

**监听器** 是在 scheduler事件发生时能够执行动作的对象, TriggerListeners接收与triggers相关的事件, 而JobListeners则接收与Job相关的事件.

Trigger相关的事件包括: trigger触发、trigger未触发, 以及trigger完成(由trigger触发的任务执行完成)


```csharp
public interface ITriggerListener
{
  string Name { get; }
	 
  void TriggerFired(ITrigger trigger, IJobExecutionContext context);
	 
  bool VetoJobExecution(ITrigger trigger, IJobExecutionContext context);
	 
  void TriggerMisfired(ITrigger trigger);
	 
  void TriggerComplete(ITrigger trigger, IJobExecutionContext context, int triggerInstructionCode);
}
```

与 Job 相关的事件包括：即将被执行的Job的通知 和 Job已经执行完毕的通知.

```csharp
public interface IJobListener
{
  string Name { get; }

  void JobToBeExecuted(IJobExecutionContext context);

  void JobExecutionVetoed(IJobExecutionContext context);

  void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException);
} 
```

## Using Your Own Listeners - 使用自定义监听器
要创建一个Listeners(监听器), 只需创建一个对象, 实现ITriggerListener和/或IJobListener接口. 在 scheduler 运行时注册, 并指定一个名称(或者设置其 Name 属性).

为了方便起见, 不用手动实现这些接口, 您的类也可以扩展类 `JobListenerSupport`或 `TriggerListenerSupport`, 并简单地覆盖您感兴趣的事件.

Listeners(监听器)是使用调度器的ListenerManager注册的, 它描述了监听器想要接收事件的作业/触发器. 

监听器在运行时向scheduler注册，并且不被存储在jobs 和triggers的JobStore中, Jobs和Trigger只存储了与他们相关的监听器的名字. 因此, 每次应用运行的时候, 都需要向scheduler重新注册监听器.

**针对 指定Job(作业) 添加 Listeners**
```csharp
scheduler.ListenerManager.AddJobListener(myJobListener, KeyMatcher<JobKey>.KeyEquals(new JobKey("myJobName", "myJobGroup")));
```

**针对 一个group其中所有 Job 添加 Listeners**
```csharp
scheduler.ListenerManager.AddJobListener(myJobListener, GroupMatcher<JobKey>.GroupEquals("myJobGroup"));
```

**针对 两个group其中所有 Job 添加 Listeners**
```csharp
scheduler.ListenerManager.AddJobListener(myJobListener,
	OrMatcher<JobKey>.Or(GroupMatcher<JobKey>.GroupEquals("myJobGroup"), GroupMatcher<JobKey>.GroupEquals("yourGroup")));
```

**向所有 Job 添加 Listeners**
```csharp
scheduler.ListenerManager.AddJobListener(myJobListener, GroupMatcher<JobKey>.AnyGroup());
```

> Quartz的大多数用户不使用监听器, 但是当应用需要创建事件通知而Job本身不能显式通知应用, 则使用监听器非常方便.


# Lesson 8: SchedulerListeners

> 原文: https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/scheduler-listeners.html

> 参考: http://www.cnblogs.com/shanyou/archive/2007/08/25/869090.html

SchedulerListeners 同TriggerListeners 及 JobListeners 非常相似, SchedulerListeners只接收与特定trigger 或job无关的Scheduler自身事件通知.

Scheduler相关的事件包括: 增加job或者trigger, 移除Job或者trigger, scheduler内部发生的错误, scheduler将被关闭的通知, 以及其他.

```csharp
public interface ISchedulerListener
{
	void JobScheduled(Trigger trigger);

	void JobUnscheduled(string triggerName, string triggerGroup);

	void TriggerFinalized(Trigger trigger);

	void TriggersPaused(string triggerName, string triggerGroup);

	void TriggersResumed(string triggerName, string triggerGroup);

	void JobsPaused(string jobName, string jobGroup);

	void JobsResumed(string jobName, string jobGroup);

	void SchedulerError(string msg, SchedulerException cause);

	void SchedulerShutdown();
} 
```

除了不分“全局”或者“非全局”监听器外, SchedulerListeners创建及注册的方法同其他监听器类型十分相同, 所有实现Quartz.ISchedulerListener接口的对象都是SchedulerListeners.

**添加 SchedulerListeners**
```csharp
scheduler.ListenerManager.AddSchedulerListener(mySchedListener);
```

**移除 SchedulerListeners**
```csharp
scheduler.ListenerManager.RemoveSchedulerListener(mySchedListener);
```
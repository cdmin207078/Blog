# Lesson 10: Configuration, Resource Usage and SchedulerFactory -  配置、资源使用以及SchedulerFactory

Quartz以模块方式构架, 因此, 要使它运行, 几个组件必须很好的咬合在一起. 幸运的是, 已经有了一些现存的助手可以完成这些工作.

在Quartz进行工作之前需要被配置的组件主要有: 

- ThreadPool
- JobStore
- DataSources (如果需要)
- Scheduler

**ThreadPool(线程池)** 提供了一组用于Quartz在执行作业时使用的线程. 池中的线程数越多, 可以并发运行的作业数量就越多. 但是, 太多的线程可能会使您的系统崩溃. 大多数用户发现5个或者相近的线程就已经足够了, 因为任何给定的时间段内都不超过100个作业要运行, 而且作业通常不安排在同一时刻运行，同时任务活动时间很短(完成快速). 其他的用户发现需要10, 15, 50, 甚至100个线程. 因为每个schedules都有成千上万的触发器, 并且在给定的时刻会有平均10到100个任务在运行. 确定schedule的线程池中的线程数量的合理值取决于用scheduler来做什么, 除了尽可能少地设置线程数量, 使得任务执行时线程够用外, 没有其他实用的准则.

> 注意：如果触发器触发的时间到了, 却没有可用的线程, 那么Quartz将会让这个任务等待, 直到有线程可用. 这样,任务的执行将比它因该执行的时间晚若干毫秒. 如果scheduler的配置的“misfire threshold(未触发极限)”时限中仍然没有线程可用, 这甚至会导致“未触发(misfire)”.

在Quartz.Spi命名空间中定义了一个 IThreadPool 接口, 您可以创建一个自己的 ThreadPool(线程池)实现, Quartz提供了一个简单的（但非常令人满意的）线程池 Quartz.Simpl.SimpleThreadPool. 这个线程池只是简单地在它的池中保持固定数量的线程, 不增长也不缩小. 但是它非常健壮且经过良好的测试, 差不多每个Quartz用户都使用这个池.


**JobStores** 和 **DataSrouces** 在第九课中已经讨论过. 值得注意的是, 所有 JobStores 都实现了 IJobStore 接口, 如果已有的 JobStores 不能满足你的要求, 你可以自己开发一个.

最后你需要创建自己的 **Scheduler** 实例, Scheduler本身需要一个名字, 和 JobStore 和 ThreadPool 实例. 


## StdSchedulerFactory

**StdSchedulerFactory** 实现了 **ISchedulerFactory** 接口, 它使用一组属性（NameValueCollection）来创建和初始化Quartz Scheduler. 属性通常存储在文件中并从文件加载, 也可以通过编写程序来直接操作工厂. 简单地调用工厂的getScheduler()就可以产生一个scheduler 并初始化(包括它的 ThreadPool, JobStore 和 DataSources), 并且返回一个公共的接口句柄 - IScheduler. 

## DirectSchedulerFactory

DirectSchedulerFactory是SchedulerFactory的另一个实现, 如果希望用更加程序化的方式创建Scheduler非常有用, 但一般情况下, 我们不建议这样, 原因如下: 
- 它需要用户非常了解他们想要干什么
- 它不允许声明式的配置. 换句话说, 它使用硬编码的方式设置scheduler

## Logging - 日志

Quartz用 [Common.Logging framework](http://netcommon.sourceforge.net/) 来满足它所有的日志需要, Quartz不会产生太多的日志信息, 通常只是一些初始化信息以及只有在任务执行时发生的一些严重问题的信息. 



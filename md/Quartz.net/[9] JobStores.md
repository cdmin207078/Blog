# Lesson 9: JobStores

> 原文: https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/job-stores.html

> 参考: http://www.cnblogs.com/shanyou/archive/2007/08/25/869110.html

> 参考: http://blog.csdn.net/Uhzgnaw/article/details/46358333  -java Quartz 储存方式之JDBC JobStoreTX & JobStoreCMT


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

目前, Job Store 的默认内部实现只有 JobStoreTX, 它自己创建事务. 这与 Java版的 Quartz不同, 你还可以选择JobStoreCMT, 使quartz通过JobStoreCMT来的使用来让你的应用容器管理quartz的事务

最后, 需要设置一个数据源, 让 AdoJobStore 可以链接到你的数据库. Quartz属性中定义数据源的相关信息.

**配置 AdoJobStore 使用 DriverDelegate**
```xml
quartz.jobStore.driverDelegateType = Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz
```

接下来, 需要为JobStore指定所使用的数据库表前缀

**配置AdoJobStore 的数据库表前缀**
```xml
quartz.jobStore.tablePrefix = QRTZ_
```

然后, 您需要设置JobStore应该使用哪个数据源. 命名的数据源还必须在Quartz属性中定义. 在这里, 我们指定Quartz应该使用数据源名称“myDS”(在配置属性中的其他位置定义).

**配置 AdoJobStore 使用数据源源的名字**
```xml
quartz.jobStore.dataSource = myDS
```

最后，需要配置数据源的使用的Ado.net数据提供者和数据库连接串，数据库连接串是标准的Ado.net 数据库连接的连接串。数据库提供者是关系数据库同Quartz.net之间保持低耦合的数据库的连接提供者.

**配置 AdoJobStore 使用数据源源的数据库连接串和数据库提供者**
```xml
quartz.dataSource.myDS.connectionString = Server=localhost;Database=quartz;Uid=quartznet;Pwd=quartznet
quartz.dataSource.myDS.provider = MySql-50
```

目前支持以下数据库提供程序, **您可以并应该使用最新版本的驱动程序（如果有更新版本），只需重新绑定程序集引用**

- SqlServer-20 - SQL Server driver for .NET Framework 2.0
- OracleODP-20 - Oracle’s Oracle Driver
- OracleODPManaged-1123-40 Oracle’s managed driver for Oracle 11
- OracleODPManaged-1211-40 Oracle’s managed driver for Oracle 12
- MySql-50 - MySQL Connector/.NET v. 5.0 (.NET 2.0)
- MySql-51 - MySQL Connector/:NET v. 5.1 (.NET 2.0)
- MySql-65 - MySQL Connector/:NET v. 6.5 (.NET 2.0)
- SQLite-10 - SQLite ADO.NET 2.0 Provider v. 1.0.56 (.NET 2.0)
- Firebird-201 - Firebird ADO.NET 2.0 Provider v. 2.0.1 (.NET 2.0)
- Firebird-210 - Firebird ADO.NET 2.0 Provider v. 2.1.0 (.NET 2.0)
- Npgsql-20 - PostgreSQL Npgsql

如果您的调度程序非常忙碌(几乎总是执行与线程池大小相同数量的作业, 那么您应该将数据源中的连接数设置为线程池大小1+ 1). 这通常配置在ADO.NET连接字符串中 -  有关详细信息，请参阅驱动程序详细信息


“quartz.jobStore.useProperties”config参数可以设置为“true”(默认为false), 这表示AdoJobStore JobDataMaps中的所有值都是字符串, 能以“名字-值”对的方式存储而不是以复杂对象的序列化形式存储在BLOB字段中.
这样做, 从长远来看非常安全, 这样避免了对存储在BLOB中的非字符串的序列化对象的类型转换问题.

**配置AdoJobStore以将字符串用作JobDataMap值(推荐)**
```xml
quartz.jobStore.useProperties = true
```






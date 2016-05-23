# asp.net mvc 页面缓存

> http://www.cnblogs.com/iamlilinfeng/p/4419362.html  - MVC缓存 @李林峰
https://msdn.microsoft.com/zh-cn/library/system.web.mvc.outputcacheattribute.aspx - MSDN OutputCacheAttribute 类

缓存是将信息（数据或页面）放在内存中以避免频繁的数据库存储或执行整个页面的生命周期，直到缓存的信息过期或依赖变更才再次从数据库中读取数据或重新执行页面的生命周期。在系统优化过程中，缓存是比较普遍的优化做法和见效比较快的做法。
MVC缓存本质上还是.NET的一套缓存体系，只不过该缓存体系应用在了MVC框架上。下面的示例把缓存应用在MVC上。

##Controller缓存
Control缓存即是把缓存应用到整个Control上，该Control下的所有Action都会被缓存起来。Control缓存的粒度比较粗，应用也比较少些
```csharp
[OutputCache(Duration = 10)]
public class ControlController : Controller { }
```

##Action缓存

> **`?`** 当Control与Action都应用了缓存时，以Action的缓存为主

```csharp
//[OutputCache(Duration = 10)]
public class ZenController : Controller
{
    [OutputCache(Duration = 10)]
    public ActionResult NowWithCache_10s()
    {
        ViewBag.Now = DateTime.Now;
            
        return PartialView("_Cache10");
    }

    [OutputCache(Duration = 5)]
    public ActionResult NowWithCache_5s()
    {
        ViewBag.Now = DateTime.Now;

        return PartialView("_Cache5");
    }
}
```

> 若 Controller 上 **没有设置输出缓存**, Action **设置有输出缓存**. 若希望 partialview 使用缓存,view须要用 `@Html.Action("viewname")` 而非 `@Html.Partial("_Cache10")`
  若 Controller 上 **设置有输出缓存**,则两种方法都会读取缓存记录

```csharp
[OutputCache(Duration = 10)]
public class ZenController : Controller
{
    [OutputCache(Duration = 5)]
    public ActionResult NowWithCache_5()
    {
        ViewBag.Now = DateTime.Now;

        return PartialView("_Cache5");
    }

    [OutputCache(Duration = 10)]
    public ActionResult NowWithCache_10()
    {
        ViewBag.Now = DateTime.Now;

        return PartialView("_Cache10");
    }

}
```
```html
<div>
    <span>5s:</span>
    @Html.Action("NowWithCache_5")
</div>
<br />
<div>
    <span>10s:</span>
    @Html.Partial("_Cache10") /* Html.Partial 不会使用缓存结果 */
</div>
```


## Question

1. 若 action 上使用配置文件定义的outputcache设置.当被用作partialview 使用时,会报错.

```csharp
[OutputCache(CacheProfile = "TestConfigCache")]
public ActionResult NowWithCache_5()
{
    ViewBag.Now = DateTime.Now;

    return PartialView("_Cache5");
}
```

```xml
 <system.web>
    <caching>
      <outputCacheSettings>
        <outputCacheProfiles>
          <add name="TestConfigCache" duration="5" enabled="true" varyByParam="none"/>
        </outputCacheProfiles>
      </outputCacheSettings>
    </caching>
    <compilation debug="true" targetFramework="4.5.1" />
    <httpRuntime targetFramework="4.5.1" />
  </system.web>
```

```html
<div>
    <span>5s:</span>
    /* 不报错 */
    @Html.Partial("_Cache5")

    /* 报错 子操作的 OutputCacheAttribute 仅支持 Duration、VaryByCustom 和 VaryByParam 值。请不要为子操作设置 CacheProfile、Location、NoStore、SqlDependency、VaryByContentEncoding 或 VaryByHeader 值 */
    @Html.Action("NowWithCache_5")
</div>
```
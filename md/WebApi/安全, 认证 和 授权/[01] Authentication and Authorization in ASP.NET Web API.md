# ASP.NET Web API 中的 认证 与 授权

当你已经创建好一个 Web API， 你现在想要控制对其的访问。在本系列文章中，我们将介绍针对未授权用户访问的一些设定选项，用来保护 web API。 这一系列将要涵盖 **认证** `Authentication` 与 **授权** `Authorization`

- **认证** (`Authentication`) - 确定用户身份。例如：Alice 用她的 用户名 和 密码 登录，服务器使用密码来认证 Alice。
- **授权** (`Authorization`) - 决定是否允许用户执行操作。例如：Alice 有权限 获取资源， 但不能创建资源。

本系列中的第一篇文章概述了ASP.NET Web API中的 **认证**(`Authentication`) 和 **授权**(`Authorization`)。 其它主题描述Web API的常见身份验证方案。

## 认证
Web API 假定该认证发生在 host(寄宿主机) 中。对于 web-hosting(web托管)而言, 主机是 IIS， 它将会使用 `HTTP Modules` 来进行认证。你可以配置你的项目使用 IIS 或 ASP.NET 内置的其它 认证模块， 或者也可以编写的 `HTTP moduel` 来进行自定义认证。

当宿主程序对用户进行认证时，将会创建一个 `principal` 对象。这是一个 `IPrincipal`对象，它表示当前代码运行时的一个安全上下文。宿主程序通过设置 `Thread.CurrentPrincipal`，将 `principal` 附加到当前线程。`principal` 包含了一个表示关联用户信息的 `Identity` 对象。如果用户认证通过，则 `Identity.IsAuthenticated = true`。对于匿名请求 `IsAuthenticated = false`。
> 更多关于 `principas` 请参见：[Role-Based Security](http://msdn.microsoft.com/en-us/library/shz8h065.aspx)

### 使用HTTP Message Handlers 进行身份验证
你可以在 [HTTP message handler](https://www.asp.net/web-api/overview/working-with-http/http-message-handlers)中创建认证逻辑来替代使用宿主的身份认证机制。在这种情况下，`message handler` 检查 Http请求 并 设置`principal`。

何时应该使用 `message handlers` 来做 认证呢？ 这里了给出一些参考权衡：

- An HTTP module sees all requests that go through the ASP.NET pipeline. A message handler only sees requests that are routed to Web API.
- You can set per-route message handlers, which lets you apply an authentication scheme to a specific route.
- HTTP modules are specific to IIS. Message handlers are host-agnostic, so they can be used with both web-hosting and self-hosting.
- HTTP modules participate in IIS logging, auditing, and so on.
- HTTP modules run earlier in the pipeline. If you handle authentication in a message handler, the principal does not get set until the handler runs. Moreover, the principal reverts back to the previous principal when the response leaves the message handler.

一般来说，如果你不需要支持 self-hosting(自托管)，http module 是一个更好的选择。 如果需要支持 self-hosting(自托管)，建议使用 message handler。


### 设置 Principal

如果你的应用程序执行任何自定义认证逻辑，则必须在两个地方设置 principal：

- **Thread.CurrentPrincipal** - 此属性是在.NET中设置线程主体的标准方法
- **HttpContext.Current.User** - 此属性是ASP.NET 专用的

> 对于 web-hosting(web托管)，必须在两个地方设置 principal， 否则 **`安全上下文`**$^1$ 可能会变得不一致。 对于 self-hosting(自托管)，`HttpContext.Current` 为 null。为了让你的代码跟寄宿环境无关，因此，在分配给 `HttpContext.Current` 之前检查是否为 null

```csharp
private void SetPrincipal(IPrincipal principal)
{
    Thread.CurrentPrincipal = principal;
    if (HttpContext.Current != null)
    {
        HttpContext.Current.User = principal;
    }
}
```

## 授权

**授权** 发生在 pipeline(管线) 的后期, 更接近于 Controller(控制器)。这可以让更细粒度的控制授权资源.

- `Authorization filters`(授权过滤器)，在 `Controller - Action` 之前运行。 如果请求未被授权，filter 将返回错误响应，并且不会调用 `Action`。
- 在 Controller, Action 内部，你可以从 `ApiController.User` 获得当前 `principal` 对象。例如：你可以基于一个用户名的筛选列表，仅返回属于该用户的资源。

![](https://media-www-asp.azureedge.net/media/3994461/webapi_auth01.png)


### 使用 [Authorize] 属性
Web API提供了一个内置的授权过滤器 `AuthorizeAttribute`。 此过滤器检查用户是否已通过身份验证。 如果没有，则返回HTTP状态代码401（Unauthorized），而不调用操作。
你你已将过滤器应用到 globally(全局), 具体controller(控制器), 具体 action(动作)。

**Globally** (全局)：若要限制每个 web api 的访问，则在全局的过滤器列表中添加 `AuthorizeAttribute`

```csharp
public static void Register(HttpConfiguration config)
{
    config.Filters.Add(new AuthorizeAttribute());
}
```

**Controller** (控制器级别)：若要限制针对某个 controller 的访问，则在具体的 controller 上添加 `AuthorizeAttribute`

```csharp
[Authorize]
public class ValuesController : ApiController
{
    public HttpResponseMessage Get(int id) { ... }
    public HttpResponseMessage Post() { ... }
}
```

**Action**：若要限制某个 action 的访问，则在具体的 action 上添加 `AuthorizeAttribute`

```csharp

public class ValuesController : ApiController
{
    public HttpResponseMessage Get() { ... }

    // Require authorization for a specific action.
    [Authorize]
    public HttpResponseMessage Post() { ... }
}
```

另外，你还可以约束一个 controller 的访问, 但允许匿名访问指定的 Action，这需要使用 `[AllowAnonymous]` 属性。在下面的示例中，Post 方法被约束了，而 Get 方法允许被匿名访问：

```csharp
[Authorize]
public class ValuesController : ApiController
{
    [AllowAnonymous]
    public HttpResponseMessage Get() { ... }

    public HttpResponseMessage Post() { ... }
}
```

在之前的例子中，过滤器只允许认证通过的用户访问限制性资源，未通过的则被禁止访问。你也可以针对 **特定用户** 或者 **特定角色** 来做访问限制。

```csharp
// 访问用户限制:
[Authorize(Users = "Alice,Bob")]
public class ValuesController : ApiController { ... }
   
// 访问角色限制:
[Authorize(Roles = "Administrators")]
public class ValuesController : ApiController { ... }
```

> Web API控制器的 AuthorizeAttribute 过滤器位于 System.Web.Http 命名空间中。 对于 System.Web.Mvc 命名空间中的MVC控制器有一个类似的过滤器，它与Web API控制器不兼容。

### 自定义 Authorization Filters

一个自定义 authorization filter 需要派生自以下几个类型：

- **AuthorizeAttribute**  派生自此类，基于当前用户 和 用户的角色 执行授权逻辑。
- **AuthorizationFilterAttribute** 派生自此类，以执行不必基于 当前用户 或 角色 的同步授权逻辑。
- **IAuthorizationFilter** 实现此接口，以执行异步授权逻辑逻辑。例如：如果您的授权逻辑需要进行异步I/O或网络调用。(如果你的授权逻辑是 CPU-bound(cpu密集型)的，那么直接继承自 AuthorizationFilterAttribute将会更简单，这样便不需要编写异步方法了。)

下图展示了AuthorizeAttribute类的类层次结构

![](https://media-www-asp.azureedge.net/media/3994467/webapi_auth02.png)

### 在Controller Action 中授权

在很多情况下，您可能允许请求继续，然后根据 `principal` 来确定改变接下来的行为。例如：您返回的信息可能会根据用户的角色而有所不同。在 controller 的方法中，你可以通过 `ApiController.user` 属性得到当前的`principal` 对象。

```csharp
public HttpResponseMessage Get()
{
    if (User.IsInRole("Administrators"))
    {
        // ...
    }
}
```


## 总结
这篇文章属于 ASP.NET **授权** 与 **认证** 的概览文章，其中第一部分讲述了认证流程：何时认证、在何处认证。认证的方法，设置 principal(安全主体) 对象。第二部分讲述了认证之后，授权的时间，授权方法。全局授权过滤，controller级授权过滤，以及具体action级授权过滤，或者也可以在 action 中根据 principal 来做逻辑判断。

-------------------

## 参考文献

原文链接: https://www.asp.net/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api

[翻译参考] ASP.NET Web API身份验证和授权: http://www.cnblogs.com/youring2/archive/2013/03/09/2950992.html

HttpModule的认识: http://www.cnblogs.com/tangself/archive/2011/03/28/1998007.html

[0]. **principal (安全主体)** ：http://www.cnblogs.com/artech/archive/2011/06/30/principal_Identity02.html
[1]. **安全上下文** ：http://www.cnblogs.com/fish-li/archive/2012/05/07/2486840.html#_label4






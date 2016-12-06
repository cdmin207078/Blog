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

何时应该使用 `message handlers` 来做 认证呢？ Here are some tradeoffs:

- 
- 
- 
- 
- 





-------------------

## 参考文献

原文链接: https://www.asp.net/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api
ASP.NET Web API身份验证和授权: http://www.cnblogs.com/youring2/archive/2013/03/09/2950992.html
HttpModule的认识: http://www.cnblogs.com/tangself/archive/2011/03/28/1998007.html

WebDAV 是超文本传输协议 (HTTP) 的一组扩展,为 Internet 上计算机之间的编辑和文件管理提供了标准.利用这个协议用户可以通过Web进行远程的基本文件操作,如拷贝、移动、删除等。在IIS 7.0中，WebDAV是作为独立扩展模块，需要单独进行下载，而IIS 7.5中将集成WebDAV，然而WebDav把Put，Delete给咔嚓了。所以在IIS 7.5上部署的RESTful服务（WCF Data Service，WCF Rest Service,ASP.NET Web API，ASP.Net MVC）就悲剧了,当发送Put请求就会发生HTTP Error 405.0 – Method  Not Allowed错误，解决方法也很简单，在Web.config里面加入如下设置：

```xml
<system.webServer> 
  <modules> 
    <remove name="WebDAVModule" /> 
  </modules> 
  <handlers> 
    <remove name="WebDAV" /> 
  </handlers> 
</system.webServer>
```

####参考
http://brockallen.com/2012/10/18/cors-iis-and-webdav/
http://www.cnblogs.com/shanyou/archive/2012/03/23/2414252.html
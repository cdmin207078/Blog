# mysql-5.7.16-winx64 安装

> 此教程为 zip 解压文件配置安装,非 exe 安装

## 官网下载压缩包

下载地址: 
http://dev.mysql.com/get/Downloads/MySQL-5.7/mysql-5.7.16-winx64.zip

下载完毕,解压到任意目录. 本例解压目录为: `D:\mysql-5.7.16-winx64`.

在解压目录中新建`my.ini` 文件,具体内容如下.

## 配置my.ini

当mysql服务器启动时它会读取这个文件，设置相关的运行环境参数

*下方罗列了基本配置,其它高级配置,可以参考文末,参考链接.*
```ini
[client]

# 设置mysql客户端默认字符集
default-character-set = utf8
 
# 设置mysql客户端连接服务端时默认使用的端口
port = 9527 


[mysqld]

# 默认字符集为utf8
default-character-set = utf8

# mysql服务端默认监听(listen on)的TCP/IP端口
port = 9527 

# 基准路径，其他路径都相对于这个路径
basedir = "D:\mysql-5.7.16-winx64/" 

# mysql数据库文件所在目录
datadir = "D:\mysql-5.7.16-winx64/data/" 

# mysql服务器支持的最大并发连接数(用户数)
# 但总会预留其中的一个连接给管理员使用超级权限登录，即使连接数目达到最大限制。
# 如果设置得过小而用户比较多，会经常出现“Too many connections”错误。
max_connections = 100

# SQL模式为strict模式
sql_mode = NO_ENGINE_SUBSTITUTION,STRICT_TRANS_TABLES 

```

## 配置系统path

> 计算机 > 属性 > 高级系统设置 > 环境变量

在windows系统环境变量`path`, 加入:  `;D:\mysql-5.7.16-winx64\bin;`

## 注册mysql服务,启动服务,初始化

### 注册服务

具体操作是在命令行中执行以下命令 `(需要以管理员身份运行命令行)`：

需要切换到bin目录，否则，会将服务目录指定为C:\Program Files\MySQL\MySQL Server 5.7\mysqld

增加服务命令：`mysqld install MySQL --defaults-file="D:\mysql-5.7.16-winx64\my.ini"` 

移除服务命令为：`mysqld remove`


### 开启服务

启动mysql命令为： `net start mysql`

关闭mysql命令为：`net stop mysql`

#### net start mysql无法启动

高级版本mysql解压出来, 不含`data`目录, 需要初始化一下
使用 `mysqld --initialize` 或者 `--initialize-insecure`
使用`--initialize` 初始化的需要看日志，会显示一个随机密码，使用此密码登陆
使用`--initialize-insecure` 初始化的，可以直接进行`mysql -u root`登陆，如果不行，可以使用`mysql -u root --skip-password`登陆，登陆成功后修改密码。


首次安装好是没有密码的,需要修改密码

使用 root 登陆
> `mysql -u root`

修改密码

> `mysql>` `use mysql;`

> `mysql>` `update user set password=PASSWORD('123456') WHERE user='root';  --修改密码`

> `mysql>` `fluesh privileges;  --刷新授权,使修改生效`

### 开启远程登陆配置

> `mysql>` `GRANT ALL PRIVILEGES ON *.* TO 'root'@'%' IDENTIFIED BY 'youpassword' WITH GRANT OPTION;`

> `mysql>` `FLUSH PRIVILEGES;`



## mysql 常用命令
1. mysql 服务启动 / 停止
> `net start mysql`
  `net stop mysql`

2. 连接到本机上的MYSQL
> `mysql -u [username] -p `

  回车之后,输入密码

3. 连接到远程主机上的MYSQL
假设远程主机的IP为：110.110.110.110, 数据库为blog 用户名为root
> `mysql -h 110.110.110.110 -u root -p blog`



## 参考链接

http://www.cnblogs.com/wenthink/p/MySQLInstall.html - 安装 mysql-5.7.5-m15-winx64
http://www.cnblogs.com/feichexia/archive/2012/11/27/mysqlconf.html - MySQL配置文件mysql.ini参数详解
http://blog.csdn.net/hhhbbb/article/details/7207751 - mysql的InnoDB参数详解
http://www.cnblogs.com/qq0827/p/3331981.html - MySql服务无法启动

## **安装完毕之后, mysql 扫盲好文章**

http://www.cnblogs.com/lina1006/archive/2011/04/29/2032894.html - 浅谈MySql的存储引擎（表类型）
http://www.cnblogs.com/sopc-mc/archive/2011/11/01/2232212.html - 【整理】MySQL引擎
http://www.cnblogs.com/zhangzhu/archive/2013/07/04/3172486.html - mysql 命令大全
http://blog.chinaunix.net/uid-23215128-id-2951624.html - Mysql开启远程连接方法


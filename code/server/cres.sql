-- --------------------------------------------------------
-- 主机:                           127.0.0.1
-- 服务器版本:                        5.7.16-log - MySQL Community Server (GPL)
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 导出 jif_blog 的数据库结构
CREATE DATABASE IF NOT EXISTS `jif_blog` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `jif_blog`;


-- 导出  表 jif_blog.article 结构
CREATE TABLE IF NOT EXISTS `article` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` text NOT NULL COMMENT '文章标题',
  `Content` text NOT NULL COMMENT '内容',
  `AllowComments` tinyint(1) NOT NULL COMMENT '是否允许评论',
  `Published` tinyint(1) NOT NULL COMMENT '是否已经发布',
  `IsDeleted` tinyint(1) NOT NULL COMMENT '是否已经删除',
  `CreateTime` datetime NOT NULL,
  `CreateUserId` int(11) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8 COMMENT='文章表';

-- 正在导出表  jif_blog.article 的数据：~1 rows (大约)
DELETE FROM `article`;
/*!40000 ALTER TABLE `article` DISABLE KEYS */;
INSERT INTO `article` (`Id`, `Title`, `Content`, `AllowComments`, `Published`, `IsDeleted`, `CreateTime`, `CreateUserId`, `UpdateTime`, `UpdateUserId`) VALUES
	(14, '九九八十一', '英雄名不堪得 何必较我混沌徒费口沫', 0, 0, 0, '2016-11-25 17:28:57', 0, NULL, NULL);
/*!40000 ALTER TABLE `article` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

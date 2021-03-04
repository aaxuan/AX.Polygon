
DROP TABLE IF EXISTS `base_systemlog`;
CREATE TABLE IF NOT EXISTS `base_systemlog` (
  `Id`                 varchar(50)     NOT NULL,
  `LogType`            varchar(50)     NOT NULL       COMMENT '日志类型',
  `ExecuteUrl`         varchar(200)    NOT NULL       COMMENT '页面地址',
  `ExecuteUser`        varchar(50)     NOT NULL       COMMENT '用户名称',
  `ExecuteParam`       text            NOT NULL       COMMENT '请求参数',
  `ExecuteResult`      text            NOT NULL       COMMENT '请求结果',
  `LogMessage`      text            NOT NULL       COMMENT '日志内容',
  `LogTime`            datetime        NOT NULL       COMMENT '执行时间',
  PRIMARY KEY (`Id`)
) ENGINE = InnoDB COMMENT '日志表';


DROP TABLE IF EXISTS `base_user`;
CREATE TABLE IF NOT EXISTS `base_user` (
  `Id`                 varchar(50)     NOT NULL,
  `UserName`           varchar(50)     NOT NULL       COMMENT '用户名',
  `LoginName`           varchar(50)     NOT NULL       COMMENT '登录名',
  `Password`           varchar(50)     NOT NULL       COMMENT '加密后密码',
  `Salt`           varchar(50)      NOT NULL       COMMENT '加密盐值',
  `Gender`           varchar(50)      NULL       COMMENT '性别',
  `Birthday`           varchar(50)      NULL       COMMENT '出生年月',
  `Email`           varchar(50)      NULL       COMMENT 'Email',
  `Mobile`           varchar(50)      NULL       COMMENT '手机',
  `QQ`           varchar(50)      NULL       COMMENT 'QQ',
  `Wechat`           varchar(50)      NULL       COMMENT 'Wechat', 
  `LoginCount`           int      NULL       COMMENT 'Wechat',  
  `IsOnline`           bit      NULL       COMMENT '是否在线', 
  `FirstVisit`           datetime      NULL       COMMENT '首次登录', 
  `PreviousVisit`           datetime      NULL       COMMENT '上次登录', 
  `LastVisit`           datetime      NULL       COMMENT '最后登录', 
    `Remark`           varchar(500)      NULL       COMMENT '状态', 
  PRIMARY KEY (`Id`)
) ENGINE = InnoDB COMMENT '用户表';

INSERT INTO `test`.`base_user` (`Id`, `UserName`, `LoginName`, `Password`, `Salt`) VALUES ('9999', '系统管理员', 'admin', 'dSPGKr23Yoxana2Pl9jYxcBA7eNlNeUxqKN0i2yufgA=', '')
       
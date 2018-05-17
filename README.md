# WcfServiceDemo
wcf service demo with wcf extensions demo

围绕着WCF实现的自娱自乐的代码


### 介绍
*****
\SyndicationServiceLibrary1 : [WCF联合](https://docs.microsoft.com/zh-cn/dotnet/framework/wcf/feature-details/wcf-syndication)，没写

\WcfExtensions : [WCF扩展](http://www.hdrorz.name/archives/176)

\WcfService1 : [WCF WebService](https://docs.microsoft.com/zh-cn/dotnet/framework/wcf/feature-details/hosting-in-internet-information-services)，基本没写

\WcfServiceHost :  [WCF 寄宿windows服务](https://docs.microsoft.com/zh-cn/dotnet/framework/wcf/feature-details/hosting-in-a-windows-service-application)，使用了[Topshelf]（https://github.com/Topshelf/Topshelf）方案创建windows服务

\WcfServiceLibrary1 : WCF服务 (使用了webHttpBinding，使用了WcfExtensions)


### TODO：
*****
1. <s>返回消息格式序列化为protobuf</s> 读了protobuf文档好几遍，好像必须通过模板编译进行code generate才行。然而可以通过这个项目[protobuf-net](https://github.com/mgravell/protobuf-net)实现，DEMO：[ProtoBuf.Services](https://github.com/maingi4/ProtoBuf.Services)
2. websocket（其实也没啥意义

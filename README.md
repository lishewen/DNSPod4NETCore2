# DNSPod4NETCore2
## DNSPod API For NETCore 2.x 公共类库，兼容.Net Core 3
### 支持 Asp .Net Core DI
> 移植自：https://gitee.com/zhengwei804/DNSPodForNET

> 现已上架Nuget：[![DNSPod4NETCore2][1.1]][1.2][![DNSPod4NETCore2][nuget-img-base]][nuget-url-base]

[1.1]: https://img.shields.io/nuget/v/DNSPod4NETCore2.svg?style=flat
[1.2]: https://www.nuget.org/packages/DNSPod4NETCore2

[nuget-img-base]: https://img.shields.io/nuget/dt/DNSPod4NETCore2.svg
[nuget-url-base]: https://www.nuget.org/packages/DNSPod4NETCore2

# DNSPod4NETCore2.Cli
## 配上一个Cli的例子，可用于修改域名记录，使用 .net core global tools 实现
### 可通过下面命令安装获取
`dotnet tool install -g dnspod`
### 使用
`dnspod [option]`
### 参数表
* -t,--Token  :     API Token.格式：[api_id],[api_token]
* -d,--DomainName  :     域名.如：lishewen.com
* -r,--RecordId  :     RecordId.不输入则通过Record.List接口查询返回，多一次请求
* -v,--Value  :     记录值.
* -s,--SubDomain  :     子域名.如：www
* --type  :     记录类型.如：A、TXT等。不输入则默认为A记录

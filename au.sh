#!/usr/bin/bash

# 配置信息
# 请填入你api的id，加上半角逗号，加上api的token
Token="[api_id],[api_token]"
DomainName="xxx.com"
SubDomain="_acme-challenge"
# 脚本路径
PATH="/root"

# 调用 Cli，自动设置 DNS TXT 记录。
# $CERTBOT_VALIDATION 是 Certbot 的内置变量，代表需要为 DNS TXT 记录设置的值
$PATH/.dotnet/tools/dnspod -t $Token -d $DomainName -v $CERTBOT_VALIDATION -s $SubDomain --type TXT

# DNS TXT 记录刷新时间
/usr/bin/sleep 30

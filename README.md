# AiKuaiCtrl
## 第三方网页通过模拟http请求更改爱快设置
疫情期间，不能出门，小孩看电视时间过长，小孩学习能力又超强，各种限制均被破解，只有祭出杀招，通过路由限制电视网络，无奈操作太复杂，老人用着不便，于是做了个网页开关方便老人使用。

## 注意
1. 未做错误处理，请一定保证配置正确
2. 爱快未提供接口，我用的是模拟http的方式控制的，我这里是3.3.3版本，不保证其他版本也有效 

## 使用方法
1. 首先在爱快 行为管控 > 应用协议控制 http://192.168.1.1/#/behavior/pro-control 页面加入你需要控制的内容
2. 找到对应规则的ID，如果清空后再新加的一般ID是1，可F12打开chrome等的调试工具，点击规则的启用、停用后查看请求Request，此例子说明ID为1
`{"func_name":"acl_l7","action":"down","param":{"id":"1"}}`
3. 更改appsettings.json 中配置 填入你对应的配置
 ```
 "AppSettings": {
    //爱快登陆地址
    "akurl": "http://192.168.1.1",
    //规则ID
    "aclId": "1",
    //用户名
    "username": "admin",
    //密码
    "passwd": "admin",
    //pass前缀(一般不用改)
    "passH": "salt_11",
    //本地监听地址 默认为5010
    "urls": "http://*:5010"

  }
```

## 启动方法
安装dotnet 3.1后，直接执行dotnet akWXHelper.dll  
windows下可直接运行akWXHelper.exe  
也可通过dotnetrun在docker中运行  
启动后再网页中访问http://127.0.0.1:5010 即可测试

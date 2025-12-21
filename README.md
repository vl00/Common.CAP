# Common.CAP

[![NuGet](https://img.shields.io/nuget/v/Common.CAP.svg?style=flat)](https://www.nuget.org/packages/Common.CAP#versions-body-tab)

目前dotnetcore.cap7,8,10 消费者过滤器只能单个并不支持中间件的，这里提供一种支持中间件的实现方法...

1. 先引用包 Common.CAP 到你项目
2. 编写中间件和全局注册
   ```cs
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class Middler1 : CapSubscribeMiddlerAttribute
   {
       public override Task ExecuteAsync(CapSubscribeExecutingContext context, CapSubscribeMiddlerExecuteFunc next)
       {
           // your codes ...
       }
   }
   
   // 局部使用:
   //[Middler1(Order = 100)]
   public class Class1 : BaseExecCap, ICapSubscribe
   {
       [Middler1] // 局部使用
       public Task Method() { ... }
   }
   
   // 全局注册:
   services.AddCap(...);
   services.AddSupportCapSubscribeFilterToMiddlers((_, f) => 
   {
       //f.AddMiddler(new Middler1(), -1000); // 可全局注册
       // ...
   });
   ```
3. cd Sample1 && dotnet build
4. cd .. && 运行 ./rg.cap.bat 生成
5. cd Sample1 && dotnet run

See more: 
- [实现简单的状态和结果保存](./Sample3/README.md)
- [Common.CAP 实现分布式saga(事务)流程](./Sample2/README.md)


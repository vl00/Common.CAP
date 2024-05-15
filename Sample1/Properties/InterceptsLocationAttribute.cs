using System;

namespace System.Runtime.CompilerServices
{
#pragma warning disable CS9113 // 参数未读。
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    /*public*/ sealed partial class InterceptsLocationAttribute(string filePath, int line, int character) : Attribute
    { }
#pragma warning restore CS9113 // 参数未读。
}

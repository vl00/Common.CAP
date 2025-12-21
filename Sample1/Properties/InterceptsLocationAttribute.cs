using System;

namespace System.Runtime.CompilerServices;

#pragma warning disable CS9113 // 参数未读。
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
sealed class InterceptsLocationAttribute : Attribute
{
	public InterceptsLocationAttribute(string filePath, int line, int character) { } // 2023 net8.0
	public InterceptsLocationAttribute(int version, string data) { } // 2024新版 net9.0+
}
#pragma warning restore CS9113 // 参数未读。
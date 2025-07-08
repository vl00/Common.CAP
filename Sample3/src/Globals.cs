using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace Common;

public static partial class Globals
{
    public static IServiceProvider ServiceProvider;

    public static string Exnv(string str) => Environment.ExpandEnvironmentVariables(str);
}

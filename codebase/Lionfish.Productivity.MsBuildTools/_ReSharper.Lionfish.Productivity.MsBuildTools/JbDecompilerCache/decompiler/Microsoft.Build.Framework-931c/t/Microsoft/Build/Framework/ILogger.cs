// Type: Microsoft.Build.Framework.ILogger
// Assembly: Microsoft.Build.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\Microsoft.Build.Framework.dll

using System.Runtime.InteropServices;

namespace Microsoft.Build.Framework
{
  [ComVisible(true)]
  public interface ILogger
  {
    LoggerVerbosity Verbosity { get; set; }

    string Parameters { get; set; }

    void Initialize(IEventSource eventSource);

    void Shutdown();
  }
}

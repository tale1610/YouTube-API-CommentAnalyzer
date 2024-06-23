#region Assembly SharpEntropy, Version=1.0.7182.26900, Culture=neutral, PublicKeyToken=null
// C:\Users\tasko\.nuget\packages\opennlp\1.3.5\lib\net45\SharpEntropy.dll
// Decompiled with ICSharpCode.Decompiler 8.1.1.7464
#endregion

namespace SharpEntropy;

public class CustomPatternPredicate
{
    private int mOutcomePattern;

    private double[] mParameters;

    private string mName;

    public int OutcomePattern
    {
        get
        {
            return mOutcomePattern;
        }
        set
        {
            mOutcomePattern = value;
        }
    }

    public int ParameterCount => mParameters.Length;

    public string Name
    {
        get
        {
            return mName;
        }
        set
        {
            mName = value;
        }
    }

    protected internal CustomPatternPredicate(int outcomePattern, double[] parameters)
    {
        mOutcomePattern = outcomePattern;
        mParameters = parameters;
    }

    protected internal CustomPatternPredicate(string name, double[] parameters)
    {
        mName = name;
        mParameters = parameters;
    }

    public double GetParameter(int index)
    {
        return mParameters[index];
    }
}
#if false // Decompilation log
'213' items in cache
------------------
Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\mscorlib.dll'
------------------
Resolve: 'Microsoft.Win32.Registry, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'Microsoft.Win32.Registry, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\Microsoft.Win32.Registry.dll'
------------------
Resolve: 'System.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Runtime.dll'
------------------
Resolve: 'System.Security.Principal.Windows, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Principal.Windows, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Security.Principal.Windows.dll'
------------------
Resolve: 'System.Security.Permissions, Version=0.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Security.Permissions, Version=6.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Users\tasko\.nuget\packages\system.security.permissions\6.0.0\lib\net6.0\System.Security.Permissions.dll'
------------------
Resolve: 'System.Collections, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Collections.dll'
------------------
Resolve: 'System.Collections.NonGeneric, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.NonGeneric, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Collections.NonGeneric.dll'
------------------
Resolve: 'System.Collections.Concurrent, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.Concurrent, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Collections.Concurrent.dll'
------------------
Resolve: 'System.ObjectModel, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ObjectModel, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.ObjectModel.dll'
------------------
Resolve: 'System.Console, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Console, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Console.dll'
------------------
Resolve: 'System.Runtime.InteropServices, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.InteropServices, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Runtime.InteropServices.dll'
------------------
Resolve: 'System.Diagnostics.Contracts, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Contracts, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Diagnostics.Contracts.dll'
------------------
Resolve: 'System.Diagnostics.StackTrace, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.StackTrace, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Diagnostics.StackTrace.dll'
------------------
Resolve: 'System.Diagnostics.Tracing, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Tracing, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Diagnostics.Tracing.dll'
------------------
Resolve: 'System.IO.FileSystem.DriveInfo, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.FileSystem.DriveInfo, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.IO.FileSystem.DriveInfo.dll'
------------------
Resolve: 'System.IO.IsolatedStorage, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.IsolatedStorage, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.IO.IsolatedStorage.dll'
------------------
Resolve: 'System.ComponentModel, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.ComponentModel.dll'
------------------
Resolve: 'System.Threading.Thread, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Thread, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Threading.Thread.dll'
------------------
Resolve: 'System.Reflection.Emit, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Reflection.Emit.dll'
------------------
Resolve: 'System.Reflection.Emit.ILGeneration, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit.ILGeneration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Reflection.Emit.ILGeneration.dll'
------------------
Resolve: 'System.Reflection.Emit.Lightweight, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit.Lightweight, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Reflection.Emit.Lightweight.dll'
------------------
Resolve: 'System.Reflection.Primitives, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Primitives, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Reflection.Primitives.dll'
------------------
Resolve: 'System.Resources.Writer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Resources.Writer, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Resources.Writer.dll'
------------------
Resolve: 'System.Runtime.CompilerServices.VisualC, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.CompilerServices.VisualC, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Runtime.CompilerServices.VisualC.dll'
------------------
Resolve: 'System.Runtime.Serialization.Formatters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Formatters, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Runtime.Serialization.Formatters.dll'
------------------
Resolve: 'System.Security.AccessControl, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.AccessControl, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Security.AccessControl.dll'
------------------
Resolve: 'System.IO.FileSystem.AccessControl, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.FileSystem.AccessControl, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.IO.FileSystem.AccessControl.dll'
------------------
Resolve: 'System.Threading.AccessControl, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.AccessControl, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Users\tasko\.nuget\packages\system.threading.accesscontrol\6.0.0\lib\net6.0\System.Threading.AccessControl.dll'
------------------
Resolve: 'System.Security.Claims, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Claims, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Security.Claims.dll'
------------------
Resolve: 'System.Security.Cryptography, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Security.Cryptography.dll'
------------------
Resolve: 'System.Text.Encoding.Extensions, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Text.Encoding.Extensions, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Text.Encoding.Extensions.dll'
------------------
Resolve: 'System.Threading, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Threading.dll'
------------------
Resolve: 'System.Threading.Overlapped, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Overlapped, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Threading.Overlapped.dll'
------------------
Resolve: 'System.Threading.ThreadPool, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.ThreadPool, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Threading.ThreadPool.dll'
------------------
Resolve: 'System.Threading.Tasks.Parallel, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Tasks.Parallel, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Threading.Tasks.Parallel.dll'
------------------
Resolve: 'System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '6.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Runtime.dll'
------------------
Resolve: 'System.Security.AccessControl, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.AccessControl, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '6.0.0.0', Got: '8.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Security.AccessControl.dll'
------------------
Resolve: 'System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\8.0.4\ref\net8.0\System.Runtime.dll'
#endif

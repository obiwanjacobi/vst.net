# SharpGen Tools

Result is located in `.\obj\Debug\net5.0\SharpGen\Generated`.

Requires Custom Layout / Marshalling

- FVariant
- AudioBusBuffers
- Event

---

Problem:

```
Error	MSB4018	The "GetGeneratedHeaderNames" task failed unexpectedly.
System.ArgumentNullException: Value cannot be null.
Parameter name: input
   at System.Text.RegularExpressions.Regex.Match(String input)
   at SharpGen.Config.ConfigFile.ExpandString(String str, Boolean expandDynamicVariable, Logger logger)
   at SharpGen.Config.ConfigFile.GetVariable(String variableName, Logger logger)
   at SharpGen.Config.ConfigFile.<>c__DisplayClass95_0.<ExpandString>b__0(Match match)
   at System.Text.RegularExpressions.RegexReplacement.Replace(MatchEvaluator evaluator, Regex regex, String input, Int32 count, Int32 startat)
   at System.Text.RegularExpressions.Regex.Replace(String input, MatchEvaluator evaluator, Int32 count, Int32 startat)
   at System.Text.RegularExpressions.Regex.Replace(String input, MatchEvaluator evaluator)
   at SharpGen.Config.ConfigFile.ExpandString(String str, Boolean expandDynamicVariable, Logger logger)
   at SharpGen.Config.ConfigFile.Verify(Logger logger)
   at SharpGen.Config.ConfigFile.Verify(Logger logger)
   at SharpGen.Config.ConfigFile.Load(ConfigFile root, String[] macros, Logger logger, KeyValue[] variables)
   at SharpGenTools.Sdk.Tasks.SharpGenTaskBase.LoadConfig(ConfigFile config)
   at SharpGenTools.Sdk.Tasks.SharpGenTaskBase.Execute()
   at Microsoft.Build.BackEnd.TaskExecutionHost.Microsoft.Build.BackEnd.ITaskExecutionHost.Execute()
   at Microsoft.Build.BackEnd.TaskBuilder.<ExecuteInstantiatedTask>d__26.MoveNext()
```

Fix:

??

---

Problem:

Severity	Code	Description	Project	File	Line	Suppression State
Error	MSB4018	The "GenerateCSharp" task failed unexpectedly.
System.OverflowException: Value was either too large or too small for an Int32.
   at System.Number.ParseInt32(String s, NumberStyles style, NumberFormatInfo info)
   at SharpGen.Generator.EnumCodeGenerator.<>c__DisplayClass1_0.<GenerateCode>b__0(CsEnumItem item)
   at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
   at SharpGen.Generator.EnumCodeGenerator.<GenerateCode>d__1.MoveNext()
   at System.Linq.Enumerable.<SelectManyIterator>d__17`2.MoveNext()
   at Microsoft.CodeAnalysis.CSharp.SyntaxFactory.List[TNode](IEnumerable`1 nodes)
   at SharpGen.Generator.RoslynGenerator.GenerateCompilationUnit[T](String csNamespace, IEnumerable`1 elements, IMultiCodeGenerator`2 generator)
   at SharpGen.Generator.RoslynGenerator.Run(CsSolution solution, String rootDirectory, String generatedCodeFolder, Boolean includeAssemblyNameFolder)
   at SharpGenTools.Sdk.Tasks.GenerateCSharp.Execute()
   at Microsoft.Build.BackEnd.TaskExecutionHost.Microsoft.Build.BackEnd.ITaskExecutionHost.Execute()
   at Microsoft.Build.BackEnd.TaskBuilder.<ExecuteInstantiatedTask>d__26.MoveNext()	SharpGenToolsTest	C:\Users\marc\.nuget\packages\sharpgentools.sdk\1.2.1\build\SharpGenTools.Sdk.targets	259	


Fix:

SharpGen assumes Int32 for enums and this error occurrs when an enum-item is bigger or smaller than an Int32. 
Almost always the enum-item value is specified as an unsigned value and therefor out-of-bounds for the signed int.

This problem seems to be fixed in the SharpGen source code - just not released yet.

```xml
<mapping>
	<remove enum-item="kInvalidTypeID" />
</mapping>
```

---

Problem: 

CppInterface [FUnknown]/Method long FUnknown::queryInterface([In] const char* _iid,[In] void** obj) error : Unknown type found [long]
https://github.com/SharpGenTools/SharpGenTools/issues/216

Fix:

Add to mapping file:

```xml
<bindings>
	<bind from="long" to="System.Int32" />
	<bind from="unsigned long" to="System.UInt32" />
</bindings>
```

---

Problem:

Build.

```
C:\Users\marc\.nuget\packages\sharpgentools.sdk\1.2.1\build\SharpGenTools.Sdk.targets(173,5): Preprocess warning CX0002: In file included from C:\Users\marc\Documents\MyProjects\public\Jacobi\Public\GitHub\vst.net\Source3\Code\SharpGenToolsTest/obj\Debug\net5.0\SharpGen/SharpGen-MSBuild.h:18:
2>C:\Users\marc\.nuget\packages\sharpgentools.sdk\1.2.1\build\SharpGenTools.Sdk.targets(173,5): Preprocess warning CX0002: In file included from C:\Users\marc\Documents\MyProjects\public\Jacobi\Public\GitHub\vst.net\Source3\Code\SharpGenToolsTest/obj\Debug\net5.0\SharpGen/JacobiVst3.h:9:
2>C:\Users\marc\.nuget\packages\sharpgentools.sdk\1.2.1\build\SharpGenTools.Sdk.targets(173,5): Preprocess warning CX0002: In file included from C:\Users\marc\Documents\MyProjects\_libs\VST_SDK\VST3_SDK\pluginterfaces\base\funknown.h:21:
2>C:\Users\marc\.nuget\packages\sharpgentools.sdk\1.2.1\build\SharpGenTools.Sdk.targets(173,5): Preprocess warning CX0002: In file included from C:\Users\marc\Documents\MyProjects\_libs\VST_SDK\VST3_SDK\pluginterfaces/base/smartpointer.h:21:
2>C:\Users\marc\.nuget\packages\sharpgentools.sdk\1.2.1\build\SharpGenTools.Sdk.targets(173,5): Preprocess warning CX0002: In file included from C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\VC\Tools\MSVC\14.29.30133\include\utility:9:
2>C:\Users\marc\.nuget\packages\sharpgentools.sdk\1.2.1\build\SharpGenTools.Sdk.targets(173,5): Preprocess warning CX0002: #error STL1000: Unexpected compiler version, expected Clang 11.0.0 or newer.
2>C:\Users\marc\.nuget\packages\sharpgentools.sdk\1.2.1\build\SharpGenTools.Sdk.targets(173,5): Preprocess warning CX0002:  ^
2>C:\Users\marc\.nuget\packages\sharpgentools.sdk\1.2.1\build\SharpGenTools.Sdk.targets(173,5): Preprocess warning CX0002: 1 error generated.
2>C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\VC\Tools\MSVC\14.29.30133\include\yvals_core.h(551,2): Preprocess error CX0001:  STL1000: Unexpected compiler version, expected Clang 11.0.0 or newer.
```

Fix:

https://github.com/SharpGenTools/SharpGenTools/issues/195#issuecomment-850306789
Download last version of CastXml.exe
Add path to .csproj file

```xml
<PropertyGroup>
	<CastXmlPath>C:\My Program Files\castxml\bin\castxml.exe</CastXmlPath>
</PropertyGroup>
```

---

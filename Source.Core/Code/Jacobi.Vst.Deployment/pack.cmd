:: x86
xcopy "..\Release\Jacobi.*" "lib\netcore\" /F /Y
xcopy "..\Jacobi.Vst.Core\bin\x86\Release\netcoreapp3.1\Jacobi.*" "lib\netcore\" /F /Y
xcopy "..\Jacobi.Vst.Framework\bin\x86\Release\netcoreapp3.1\Jacobi.*" "lib\netcore\" /F /Y
xcopy "..\Release\ijwhost.dll" "runtimes\win10-x86\native\" /F /Y

:: x64
xcopy "..\x64\Release\Jacobi.*" "lib\netcore\" /F /Y
xcopy "..\Jacobi.Vst.Core\bin\x6\Release\netcoreapp3.1\Jacobi.*" "lib\netcore\" /F /Y
xcopy "..\Jacobi.Vst.Framework\bin\x64\Release\netcoreapp3.1\Jacobi.*" "lib\netcore\" /F /Y
xcopy "..\x64\Release\ijwhost.dll" "runtimes\win10-x64\native\" /F /Y

nuget pack VST.NET.nuspec

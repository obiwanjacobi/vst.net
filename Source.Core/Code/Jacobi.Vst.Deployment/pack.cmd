:: x86
xcopy "..\Release\Jacobi.Vst.Interop.*" "pack\runtimes\win10-x86\lib\netcoreapp3.1\" /F /Y
xcopy "..\Jacobi.Vst.Core\bin\x86\Release\netcoreapp3.1\Jacobi.Vst.Core.*" "pack\runtimes\win10-x86\lib\netcoreapp3.1\" /F /Y
xcopy "..\Jacobi.Vst.Framework\bin\x86\Release\netcoreapp3.1\Jacobi.Vst.Framework.*" "pack\runtimes\win10-x86\lib\netcoreapp3.1\" /F /Y
xcopy "..\Release\ijwhost.dll" "pack\runtimes\win10-x86\native\" /F /Y

:: x64
xcopy "..\x64\Release\Jacobi.Vst.Interop.*" "pack\runtimes\win10-x64\lib\netcoreapp3.1\" /F /Y
xcopy "..\Jacobi.Vst.Core\bin\x64\Release\netcoreapp3.1\Jacobi.Vst.Core.*" "pack\runtimes\win10-x64\lib\netcoreapp3.1\" /F /Y
xcopy "..\Jacobi.Vst.Framework\bin\x64\Release\netcoreapp3.1\Jacobi.Vst.Framework.*" "pack\runtimes\win10-x64\lib\netcoreapp3.1\" /F /Y
xcopy "..\x64\Release\ijwhost.dll" "pack\runtimes\win10-x64\native\" /F /Y

nuget pack .\pack\VST.NET.nuspec

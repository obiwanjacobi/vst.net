REM CALL "c:\Program Files (x86)\Microsoft Visual Studio 9.0\VC\vcvarsall.bat"

Msbuild /t:Clean /p:Configuration=Debug /p:Platform="Any CPU" ..\..\Samples\Jacobi.Vst.Samples.sln
Msbuild /t:Clean /p:Configuration=Release /p:Platform="Any CPU" ..\..\Samples\Jacobi.Vst.Samples.sln

CALL "c:\Program Files (x86)\Microsoft Visual Studio 9.0\VC\vcvarsall.bat"

Msbuild /p:Configuration=Debug /p:Platform=x86 ..\..\Code\Jacobi.Vst.sln

pause
CALL "c:\Program Files (x86)\Microsoft Visual Studio 9.0\VC\vcvarsall.bat"

Msbuild /p:Configuration=Release /p:Platform="Any CPU (x64)" ..\..\Code\Jacobi.Vst.sln

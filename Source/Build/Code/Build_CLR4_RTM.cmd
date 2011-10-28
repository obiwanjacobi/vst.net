CALL "C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"

CALL Clean_CLR4.cmd

Msbuild /p:Configuration=Debug /p:Platform="Any CPU (RTM)" ..\..\Code\Jacobi.Vst.Clr4.sln
Msbuild /p:Configuration=Release /p:Platform="Any CPU (RTM)" ..\..\Code\Jacobi.Vst.Clr4.sln

Msbuild /p:Configuration=Debug /p:Platform="x86" ..\..\Code\Jacobi.Vst.Clr4.sln
Msbuild /p:Configuration=Release /p:Platform="x86" ..\..\Code\Jacobi.Vst.Clr4.sln

Msbuild /p:Configuration=Debug /p:Platform="x64" ..\..\Code\Jacobi.Vst.Clr4.sln
Msbuild /p:Configuration=Release /p:Platform="x64" ..\..\Code\Jacobi.Vst.Clr4.sln

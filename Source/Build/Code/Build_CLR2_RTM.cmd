REM CALL "c:\Program Files (x86)\Microsoft Visual Studio 9.0\VC\vcvarsall.bat"

CALL Clean_CLR2.cmd
Msbuild /m /p:Configuration=Debug /p:Platform="Any CPU (RTM)" ..\..\Code\Jacobi.Vst.Clr2.sln
Msbuild /m /p:Configuration=Release /p:Platform="Any CPU (RTM)" ..\..\Code\Jacobi.Vst.Clr2.sln

CALL Clean_CLR2.cmd
Msbuild /m /p:Configuration=Debug /p:Platform="x86" ..\..\Code\Jacobi.Vst.Clr2.sln
Msbuild /m /p:Configuration=Release /p:Platform="x86" ..\..\Code\Jacobi.Vst.Clr2.sln

CALL Clean_CLR2.cmd
Msbuild /m /p:Configuration=Debug /p:Platform="x64" ..\..\Code\Jacobi.Vst.Clr2.sln
Msbuild /m /p:Configuration=Release /p:Platform="x64" ..\..\Code\Jacobi.Vst.Clr2.sln

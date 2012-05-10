CALL "c:\Program Files (x86)\Microsoft Visual Studio 9.0\VC\vcvarsall.bat"

REM Build dependencies
CALL ..\Code\Clean_CLR2.cmd
Msbuild /t:Clean /p:Configuration=Debug /p:Platform="Any CPU (RTM)" ..\..\Code\Jacobi.Vst.sln
Msbuild /t:Clean /p:Configuration=Release /p:Platform="Any CPU (RTM)" ..\..\Code\Jacobi.Vst.sln

CALL ..\Code\Clean_CLR2.cmd
Msbuild /p:Configuration=Debug /p:Platform="Any CPU (RTM)" ..\..\Code\Jacobi.Vst.sln
Msbuild /p:Configuration=Release /p:Platform="Any CPU (RTM)" ..\..\Code\Jacobi.Vst.sln

CALL Clean_CLR2.cmd
Msbuild /p:Configuration=Debug /p:Platform="Any CPU" ..\..\Samples\Jacobi.Vst.Samples.sln
Msbuild /p:Configuration=Release /p:Platform="Any CPU" ..\..\Samples\Jacobi.Vst.Samples.sln

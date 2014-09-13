REM CALL "c:\Program Files (x86)\Microsoft Visual Studio 9.0\VC\vcvarsall.bat" x86

Msbuild /m /t:Clean /p:Configuration=Debug /p:Platform="Any CPU (RTM)" ..\..\Code\Jacobi.Vst.Clr2.sln
Msbuild /m /t:Clean /p:Configuration=Release /p:Platform="Any CPU (RTM)" ..\..\Code\Jacobi.Vst.Clr2.sln

Msbuild /m /t:Clean /p:Configuration=Debug /p:Platform="x86" ..\..\Code\Jacobi.Vst.Clr2.sln
Msbuild /m /t:Clean /p:Configuration=Release /p:Platform="x86" ..\..\Code\Jacobi.Vst.Clr2.sln

Msbuild /m /t:Clean /p:Configuration=Debug /p:Platform="x64" ..\..\Code\Jacobi.Vst.Clr2.sln
Msbuild /m /t:Clean /p:Configuration=Release /p:Platform="x64" ..\..\Code\Jacobi.Vst.Clr2.sln
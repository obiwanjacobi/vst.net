msbuild ..\Jacobi.Vst.sln /t:Clean /p:Configuration=Debug /p:Platform=x86
msbuild ..\Jacobi.Vst.sln /t:Clean /p:Configuration=Debug /p:Platform=x64
msbuild ..\Jacobi.Vst.sln /t:Build /p:Configuration=Debug /p:Platform=x86
msbuild ..\Jacobi.Vst.sln /t:Build /p:Configuration=Debug /p:Platform=x64

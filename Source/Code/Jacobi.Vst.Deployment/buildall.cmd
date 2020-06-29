msbuild ..\Jacobi.Vst.sln /t:Clean /p:Configuration=Release /p:Platform=x86
msbuild ..\Jacobi.Vst.sln /t:Clean /p:Configuration=Release /p:Platform=x64
msbuild ..\Jacobi.Vst.sln /t:Build /p:Configuration=Release /p:Platform=x86
msbuild ..\Jacobi.Vst.sln /t:Build /p:Configuration=Release /p:Platform=x64

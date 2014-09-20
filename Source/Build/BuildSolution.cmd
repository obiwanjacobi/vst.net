Msbuild /m /t:Clean /p:Configuration=Debug /p:Platform="x86" %1
Msbuild /m /p:Configuration=Debug /p:Platform="x86" %1

Msbuild /m /t:Clean /p:Configuration=Release /p:Platform="x86" %1
Msbuild /m /p:Configuration=Release /p:Platform="x86" %1

Msbuild /m /t:Clean /p:Configuration=Debug /p:Platform="x64" %1
Msbuild /m /p:Configuration=Debug /p:Platform="x64" %1

Msbuild /m /t:Clean /p:Configuration=Release /p:Platform="x64" %1
Msbuild /m /p:Configuration=Release /p:Platform="x64" %1
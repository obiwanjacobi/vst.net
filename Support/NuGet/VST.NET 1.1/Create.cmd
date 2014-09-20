CALL ..\..\..\Source\Build\CleanDir x86\lib
CALL ..\..\..\Source\Build\CleanDir x64\lib

cd ..
CALL CopyFromBuildResults 1.1
cd VST.NET 1.1

CALL ..\CreateNuGetPackages 1.1

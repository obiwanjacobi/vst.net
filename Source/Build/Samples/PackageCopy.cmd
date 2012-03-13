REM Gather files from ..\..\Samples\BuildResults into ..\_Packages


REM AnyCPU

copy ..\..\Samples\BuildResults\%1\AnyCPU\Debug\*.* ..\_Packages\Samples\%1\AnyCPU\Debug
copy ..\..\Samples\BuildResults\%1\AnyCPU\Release\*.dll ..\_Packages\Samples\%1\AnyCPU\Release

REM Gather files from ..\..\Code\BuildResults into ..\_Packages


REM AnyCPU

copy ..\..\Code\BuildResults\CLR2\AnyCPU\Debug\*.* ..\_Packages\Code\CLR2\AnyCPU\Debug
copy ..\..\Code\BuildResults\CLR2\AnyCPU\Release\*.dll ..\_Packages\Code\CLR2\AnyCPU\Release

REM x86

copy ..\..\Code\BuildResults\CLR2\x86\Debug\*.* ..\_Packages\Code\CLR2\x86\Debug
copy ..\..\Code\BuildResults\CLR2\x86\Release\*.dll ..\_Packages\Code\CLR2\x86\Release

copy ..\..\Code\BuildResults\CLR2\Win32\Debug\*.dll ..\_Packages\Code\CLR2\x86\Debug
copy ..\..\Code\BuildResults\CLR2\Win32\Debug\*.pdb ..\_Packages\Code\CLR2\x86\Debug
copy ..\..\Code\BuildResults\CLR2\Win32\Debug\*.xml ..\_Packages\Code\CLR2\x86\Debug
copy ..\..\Code\BuildResults\CLR2\Win32\Release\*.dll ..\_Packages\Code\CLR2\x86\Release

REM x64

copy ..\..\Code\BuildResults\CLR2\x64\Debug\*.dll ..\_Packages\Code\CLR2\x64\Debug
copy ..\..\Code\BuildResults\CLR2\x64\Debug\*.pdb ..\_Packages\Code\CLR2\x64\Debug
copy ..\..\Code\BuildResults\CLR2\x64\Debug\*.xml ..\_Packages\Code\CLR2\x64\Debug
copy ..\..\Code\BuildResults\CLR2\x64\Release\*.dll ..\_Packages\Code\CLR2\x64\Release


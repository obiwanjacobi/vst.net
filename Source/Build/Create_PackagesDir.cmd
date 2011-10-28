REM This script will create some folders that will hold the packages.

md _Packages
cd _Packages

md Code
md Samples

cd Code
CALL ..\..\CreateCLRDirs.cmd
cd ..

cd Samples
CALL ..\..\CreateCLRDirs.cmd
cd ..

cd ..
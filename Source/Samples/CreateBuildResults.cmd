REM This script will create some folders that will hold the build results.

md BuildResults
cd BuildResults

md CLR2

cd CLR2
md AnyCPU

cd AnyCPU
md Debug
md Release
cd ..

cd ..
cd ..

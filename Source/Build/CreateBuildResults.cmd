REM This script will create some folders that will hold the build results.

md BuildResults
cd BuildResults

md CLR2
md CLR4

cd CLR2
md x86
md x64
md AnyCPU
md Win32

cd x86
md Debug
md Release
cd ..

cd x64
md Debug
md Release
cd ..

cd AnyCPU
md Debug
md Release
cd ..

cd Win32
md Debug
md Release
cd ..

cd ..
cd CLR4
md x86
md x64
md AnyCPU
md Win32

cd x86
md Debug
md Release
cd ..

cd x64
md Debug
md Release
cd ..

cd AnyCPU
md Debug
md Release
cd ..

cd Win32
md Debug
md Release
cd ..

cd ..
cd ..
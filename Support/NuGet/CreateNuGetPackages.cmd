cd x86
..\..\nuget pack "VST.NET %1 x86.nuspec"
cd ..

cd x64
..\..\nuget pack "VST.NET %1 x64.nuspec"
cd ..
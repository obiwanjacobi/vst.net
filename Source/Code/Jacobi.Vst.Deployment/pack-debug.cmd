del *.nupkg
nuget pack ".\plugin\VST.NET-Plugin.nuspec" -Properties "config=Debug"
nuget pack ".\host\VST.NET-Host.nuspec"

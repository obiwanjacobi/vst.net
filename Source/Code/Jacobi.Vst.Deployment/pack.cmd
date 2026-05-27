del *.nupkg
nuget pack ".\plugin\VST.NET-Plugin.nuspec" -Properties "config=Release"
nuget pack ".\host\VST.NET-Host.nuspec"

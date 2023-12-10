# VST.NET Deployment

- Builds Nuget packages for Plugin and Host.
- Includes a post-build step that uses vstnet CLI to make a deployment.

## Resources

https://natemcmaster.com/blog/2017/11/11/build-tools-in-nuget/

Create a build bin log: `dotnet build /bl` Use [MSBuild Structured log viewer](https://www.msbuildlog.com/)
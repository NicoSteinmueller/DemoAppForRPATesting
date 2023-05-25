## Publish app to docker
```shell
cd DemoAppForRPATesting
dotnet publish --os linux --arch x64 /t:PublishContainer -c Release
```
https://learn.microsoft.com/de-de/dotnet/core/docker/publish-as-container

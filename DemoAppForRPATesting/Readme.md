## Publish app to docker
```shell
cd DemoAppForRPATesting
dotnet publish --os linux --arch x64 /t:PublishContainer -c Release
```
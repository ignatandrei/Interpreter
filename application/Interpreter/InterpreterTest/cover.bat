dotnet tool install -g dotnet-reportgenerator-globaltool --version 4.0.0-alpha12
dotnet test  --no-build --no-restore /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
reportgenerator "-reports:coverage.opencover.xml" "-targetdir:cover"
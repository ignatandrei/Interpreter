dotnet tool install -g dotnet-reportgenerator-globaltool --version 4.0.0-alpha12
echo 'test'
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
echo 'report'
reportgenerator "-reports:coverage.opencover.xml" "-targetdir:cover"
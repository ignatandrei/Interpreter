# Interpreter


<img src='https://img.shields.io/nuget/v/interpreterdll.svg' />
<img src='https://img.shields.io/vso/build/ignatandrei/5fdb2dc7-9742-4619-a886-c8ed63bc791a/7.svg' />



Interpreter for creating strings from usual data

It supports the following configuration:

#env:<SomeEnvironemntVariable# - example : #env:Path# 

#now:<SomeFormat># - example:#now:yyyyMMddHHmmss#
 
#utcnow:<SomeFormat># - example:#utcnow:yyyyMMddHHmmss#

#guid:Someformat# - example:  #guid:n#

@static:<SomeClass.SomeStaticFunction>@  - example :  @static:Environment.CurrentDirectory@  

TwoSteps:
@static:Path.GetPathRoot(#static:Directory.GetCurrentDirectory()#)@";

 Reading from appsettings.json :
 #file:<NameOfJsonEntry># - example #file:SqlServerConnectionString#  -  read from 
  
  {
  "SqlServerConnectionString": "Server=(local)\\SQL2016;Database=tempdb;Trusted_Connection=True;"
}
 

 
 
 

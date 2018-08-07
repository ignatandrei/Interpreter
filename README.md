# Interpreter


<img src='https://img.shields.io/nuget/v/interpreterdll.svg' />
<img src='https://img.shields.io/vso/build/ignatandrei/5fdb2dc7-9742-4619-a886-c8ed63bc791a/7.svg' />

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/ignatandrei/Interpreter/blob/master/LICENSE)  

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
 

For more examples please read http://msprogrammer.serviciipeweb.ro/2018/07/16/interpreterpart-1-of-n/ 


For the whole interpret , please read http://msprogrammer.serviciipeweb.ro/category/interpreter/ 
 
For where it is used, please see 

"FolderName": "@static:Environment.CurrentDirectory@" from
https://github.com/ignatandrei/AOP_With_Roslyn/blob/master/AOPRoslyn/processme.txt

Even if [dotnet aop](https://github.com/ignatandrei/AOP_With_Roslyn/) is running from dot net tools folder(<user>\.dotnet\tools\.store\dotnet-aop) , it processes files from current directory


 
 

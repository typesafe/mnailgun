require 'rake'

desc 'builds and packages'
task :default => [:build, :test]

desc 'builds mnailgun'
task :build do
	sh "\"C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\msbuild.exe\" src\\mnailgun.sln /target:Build -p:Configuration=Release"
end

desc 'executes the unit tests for mnailgun'
task :test do
	sh "packages\\NUnit.2.5.10.11092\\tools\\nunit-console src\\Typesafe.Mailgun.Tests\\bin\\Debug\\Typesafe.Mailgun.Tests.dll"
end

desc 'Creates the nuget packages for mnailgun'
task :nuget do
	sh "packages\\NuGet.CommandLine.1.5.21005.9019\\tools\\nuget pack src\\mnailgun.nuspec"
end

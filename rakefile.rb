require 'rake'

nuget_path = "packages\\NuGet.CommandLine.1.5.21005.9019\\tools\\nuget"
nunit_path = "packages\\NUnit.2.5.10.11092\\tools\\nunit-console"
msbuild_path = "\"C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\msbuild.exe\""

load "VERSION.txt"
now = Time.now
ASM_VERSION = "#{BUILD_VERSION}.#{now.year + now.month + now.day + now.hour + now.min + now.sec}"

desc 'builds and tests'
task :default => [:build, :test]

desc 'builds mnailgun'
task :build => :version do
	sh "#{msbuild_path} src\\mnailgun.sln /target:Build -p:Configuration=Release"
end

desc 'executes the unit tests for mnailgun'
task :test do
	sh "#{nunit_path} src\\Typesafe.Mailgun.Tests\\bin\\Debug\\Typesafe.Mailgun.Tests.dll"
end

desc 'Creates the nuget packages for mnailgun'
task :nuget => :version do
	sh "#{nuget_path} pack nuget\\mnailgun.nuspec -Symbols -OutputDirectory bin"
end

task :version do
	replace "nuget\\mnailgun.nuspec", /<version>[0-9]+.[0-9]+.[0-9]+<\/version>/, "<version>#{BUILD_VERSION}</version>"
	replace "src\\Typesafe.Mailgun\\Properties\\AssemblyInfo.cs", /AssemblyVersion\("[0-9]+.[0-9]+.[0-9]+.[0-9]+"\)/, "AssemblyVersion(\"#{ASM_VERSION}\")"
	replace "src\\Typesafe.Mailgun\\Properties\\AssemblyInfo.cs", /AssemblyFileVersion\("[0-9]+.[0-9]+.[0-9]+.[0-9]+"\)/, "AssemblyFileVersion(\"#{ASM_VERSION}\")"
end

def replace(file, pattern, value)
	original = IO.read file
	replaced = original.gsub(pattern, value)
	if(original != replaced)
		File.open(file, 'w') { |f| f.write(replaced) }
		puts "replaced file " + file
	end
end

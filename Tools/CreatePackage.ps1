# $msbuild = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
$msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"

.\IncrementVersion-cs.ps1 ..\Blaze\Blaze

$slnFilePath = "..\Blaze\Blaze.sln"
& $msbuild $slnFilePath /p:Configuration=Release /t:Clean
& $msbuild $slnFilePath /p:Configuration=Release /t:Rebuild

cd ..\Blaze\Blaze
.\NuGetPackup.exe

ni ..\..\Published -type directory -Force
move *.nupkg ..\..\Published -Force
explorer ..\..\Published

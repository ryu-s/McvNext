Param([string] $configuration="Release",[string] $solutionDir=".\")

$OutputDir = "${solutionDir}Output\${configuration}"
$PluginDirName = "plugins"
$PluginDirPath = "${OutputDir}\plugins"

# MainViewPlugin
dotnet build ${solutionDir}MainViewPlugin\MainViewPlugin.csproj
Copy-Item -Recurse -Path "${solutionDir}MainViewPlugin\bin\${configuration}" -Destination "${solutionDir}MainViewPlugin\bin\MainViewPlugin"
Compress-Archive -Path "${solutionDir}MainViewPlugin\bin\MainViewPlugin" -Destination "${solutionDir}MainViewPlugin\bin\MainViewPlugin.zip"
Copy-Item -Path "${solutionDir}MainViewPlugin\bin\MainViewPlugin.zip" -Destination $PluginDirPath
Remove-Item -Path "${solutionDir}MainViewPlugin\bin\MainViewPlugin.zip"
Remove-Item -Recurse -Path "${solutionDir}MainViewPlugin\bin\MainViewPlugin"

# BouyomiChanPlugin
dotnet build ${solutionDir}BouyomiChanPlugin\BouyomiChanPlugin.csproj
Copy-Item -Recurse -Path "${solutionDir}BouyomiChanPlugin\bin\${configuration}" -Destination "${solutionDir}BouyomiChanPlugin\bin\BouyomiChanPlugin"
Compress-Archive -Path "${solutionDir}BouyomiChanPlugin\bin\BouyomiChanPlugin" -Destination "${solutionDir}BouyomiChanPlugin\bin\BouyomiChanPlugin.zip"
Copy-Item -Path "${solutionDir}BouyomiChanPlugin\bin\BouyomiChanPlugin.zip" -Destination $PluginDirPath
Remove-Item -Path "${solutionDir}BouyomiChanPlugin\bin\BouyomiChanPlugin.zip"
Remove-Item -Recurse -Path "${solutionDir}BouyomiChanPlugin\bin\BouyomiChanPlugin"

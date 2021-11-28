# 出力先のディレクトリを作成

Param([string] $configuration="Release",[string] $solutionDir=".\")

$OutputDir = "Output\${configuration}"
$PluginDirName = "plugins"

#Remove-Item -Recurse ${solutionDir}Output
New-Item -Force -Path $solutionDir -Name $OutputDir -ItemType "directory"
New-Item -Force -Path ${solutionDir}${OutputDir} -Name $PluginDirName -ItemType "directory"

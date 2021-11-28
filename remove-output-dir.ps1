# 出力先のディレクトリを削除

Param([string] $configuration="Release",[string] $solutionDir=".\")

$OutputDir = "Output\${configuration}"

Remove-Item -Recurse -Path ${OutputDir}

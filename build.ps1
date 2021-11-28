# 使い方
# $ build [[-configuration ]CONFIGURATION]

Param([string] $configuration="Release")


# 出力先のディレクトリを作成
.\init-output-dir.ps1 -configuration $configuration

# McvCoreをビルド
dotnet build .\McvCore\McvCore.csproj --configuration $configuration

# 同梱するプラグインをビルド
.\build-plugins.ps1 -configuration $configuration

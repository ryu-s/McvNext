using Plugin;

namespace UnknownLiveSiteCommentProviderPluginTrait;
/// <summary>
/// 
/// こんな形式にすれば複数の形式のデータを扱える
/// </summary>
public interface IPostingData1
{
    public IPostingData2? Data2 { get; }
    public IPostingData3? Data3 { get; }
}
public interface IPostingData2
{
    string Name { get; }
    string Comment { get; }
}
public interface IPostingData3
{
    public string? LiveTitle { get; }
    public string? ElapsedTime { get; }
    public int? CurrentViewers { get; }

}
public interface IUnknownLiveSiteSettings:IPluginSettings
{
    string Memo { get; }
}
public interface IUnknownLiveSiteSettingsDiff : IPluginSettingsDiff
{
    string? Memo { get; }
}
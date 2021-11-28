using Plugin;

namespace Mcv.A17Live;
public interface IA17LiveMessage : ISiteMessage { }
public interface IA17LiveComment:IA17LiveMessage
{
    string Message { get; }
}
public interface IA17LiveSettings : IPluginSettings
{
}
public interface IA17LiveSettingsDiff : IPluginSettingsDiff
{
    public static IA17LiveSettingsDiff GetDiff(IA17LiveSettings original, IA17LiveSettings modified)
    {
        return new A17LiveSettingsDiff
        {
        };
    }
}
class A17LiveSettingsDiff : IA17LiveSettingsDiff
{
    public bool? IsAllChat { get; set; }
}
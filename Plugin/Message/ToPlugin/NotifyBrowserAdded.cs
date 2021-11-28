namespace Plugin.Message.ToPlugin
{
    public class NotifyBrowserAdded
    {
        public NotifyBrowserAdded(BrowserProfileId browserProfileId, string name, string? profileName)
        {
            BrowserProfileId = browserProfileId;
            Name = name;
            ProfileName = profileName;
        }

        public BrowserProfileId BrowserProfileId { get; }
        public string Name { get; }
        public string? ProfileName { get; }
    }
}

using Plugin;

namespace UnknownLiveSiteCommentProviderPlugin;
public class NormalComment : ISiteMessage
{
    public NormalComment(string authorName, string Message)
    {
        AuthorName = authorName;
        this.Message = Message;
    }

    public string AuthorName { get; }
    public string Message { get; }
}

public class Metadata : ISiteMessage
{
    public int CurrentViewers { get; }
    public Metadata(int currentViewers)
    {
        CurrentViewers = currentViewers;
    }
}
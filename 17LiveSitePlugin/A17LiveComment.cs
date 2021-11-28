using Mcv.A17Live;

namespace Mcv.Plugin.A17Live;

class A17LiveComment : IA17LiveComment
{
    public string Message { get; }
    public A17LiveComment(string message)
    {
        Message = message;
    }
}


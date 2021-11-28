
using System;

namespace Plugin.Message.ToCore
{
    public class AddException{
        public AddException(Exception ex)
        {
            Ex = ex;
        }

        public Exception Ex { get; }
    }
}
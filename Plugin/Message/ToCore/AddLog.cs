namespace Plugin.Message.ToCore
{
    public class AddLog
    {
        public AddLog(string log)
        {
            Log = log;
        }

        public string Log { get; }
    }
}
namespace Plugin
{

    public interface IConnectionStatus
    {
        string Name { get; }
        IInput Input { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IConnectionStatusDiff SetDiff(IConnectionStatusDiff req);
    }

}
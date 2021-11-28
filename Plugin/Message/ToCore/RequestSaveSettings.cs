namespace Plugin.Message.ToCore
{
    /// <summary>
    /// データの保存を要求する
    /// </summary>
    public class RequestSaveSettings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginName">保存する時のファイル名に使う。重複しなければ何でも良い</param>
        /// <param name="data">保存するデータ</param>
        public RequestSaveSettings(string pluginName, string data)
        {
            PluginName = pluginName;
            Data = data;
        }

        public string PluginName { get; }
        public string Data { get; }
    }
}
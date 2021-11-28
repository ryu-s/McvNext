namespace Plugin
{

    public interface IConnectionStatusDiff
    {
        string? Name { get; set; }
        IInput? Input { get; set; }
        //bool IsChanged()
        //{
        //    var props = GetType().GetProperties();
        //    foreach (var prop in props)
        //    {
        //        var val = prop.GetValue(this);
        //        if (val != null)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
    public static class ConnectionStatusDiffExtension
    {
        public static bool IsChanged(this IConnectionStatusDiff diff)
        {
            var props = diff.GetType().GetProperties();
            foreach (var prop in props)
            {
                var val = prop.GetValue(diff);
                if (val != null)
                {
                    return true;
                }
            }
            return false;
        }
    }

}

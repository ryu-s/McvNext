namespace Plugin
{
    public class StringInput : IInput
    {
        public StringInput(string raw)
        {
            Raw = raw;
        }

        public string Raw { get; }
        public override bool Equals(object? obj)
        {
            if (!(obj is StringInput b))
            {
                return false;
            }
            return Raw == b.Raw;
        }
        public override int GetHashCode()
        {
            return Raw.GetHashCode();
        }
    }
}

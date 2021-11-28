using System;

namespace Plugin
{

    public class BrowserProfileId
    {
        private readonly Guid _guid;

        public BrowserProfileId(Guid guid)
        {
            _guid = guid;
        }
        public override string ToString()
        {
            return _guid.ToString();
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is BrowserProfileId b))
            {
                return false;
            }
            return _guid.Equals(b._guid);
        }
        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }
    }
}
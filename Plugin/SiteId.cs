using System;

namespace Plugin
{
    public class SiteId
    {
        private readonly Guid _guid;

        public SiteId(Guid guid)
        {
            _guid = guid;
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is SiteId b))
            {
                return false;
            }
            return _guid.Equals(b._guid);
        }
        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }
        public override string ToString()
        {
            return $"{{\"_guid\":\"{_guid}\"}}";
        }
    }
}
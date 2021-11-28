using System;

namespace Plugin
{
    public class PluginId
    {
        private readonly Guid _guid;

        public PluginId(Guid guid)
        {
            _guid = guid;
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is PluginId b))
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
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
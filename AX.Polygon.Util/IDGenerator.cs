using System;

namespace AX.Polygon.Util
{
    public interface IDGenerator
    {
        public string CreateID();
    }

    public class GuidGenerator : IDGenerator
    {
        public string CreateID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
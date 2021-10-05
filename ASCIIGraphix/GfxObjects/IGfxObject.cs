using System.Collections.Generic;

namespace ASCIIGraphix.GfxObjects
{
    public interface IGfxObject
    {
        public List<IGfxObject> Children { get; }
    }
}
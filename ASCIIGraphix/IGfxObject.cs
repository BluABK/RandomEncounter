using System.Collections.Generic;

namespace ASCIIGraphix
{
    public interface IGfxObject
    {
        public List<IGfxObject> Children { get; }
    }
}
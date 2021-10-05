using System.Collections.Generic;
using ASCIIGraphix;
using ASCIIGraphix.GfxObjects;

namespace EncounterGraphix
{
    public interface IFrame
    {
        List<GfxObject> Elements { get; }
    }
}
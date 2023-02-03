using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskExtension
{
    public static bool ContainsLayer(this LayerMask mask, int layer)
    {
        return (mask & (1 << layer)) != 0;
    }

}


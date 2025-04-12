using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChildShuffler : MonoBehaviour
{
    public void ShuffleChildren()
    {
        var children = GetComponentsInChildren<Transform>()
            .Where(t => t != transform)
            .ToArray();
        
        foreach (var child in children.OrderBy(x => Random.value))
        {
            child.SetAsLastSibling();
        }
    }
}

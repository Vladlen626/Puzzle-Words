using System;
using UnityEngine;

public class ClusterSlot : MonoBehaviour
{
    private int index;
    private DraggableCluster currentDraggableCluster;
    public void Initialize(int inIndex)
    {
        index = inIndex;
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>().size = Vector2.one * 200;
        }
    }

    public DraggableCluster GetCluster()
    {
        return currentDraggableCluster;
    }

    public int GetIndex()
    {
        return index;
    }

    public bool TryAttachCluster(DraggableCluster newDraggableCluster)
    {
        if (currentDraggableCluster)
        {
            var newClusterPreviousSlot = newDraggableCluster.TryGetPreviousSlot();
            if (newClusterPreviousSlot)
            {
                currentDraggableCluster.TryAttachToSlot(newClusterPreviousSlot);
            }
            else
            {
                currentDraggableCluster.ReturnToPanel();
            }
        }
        
        currentDraggableCluster = newDraggableCluster;
        newDraggableCluster.transform.SetParent(transform);
        newDraggableCluster.transform.localPosition = Vector3.zero;
        return true;
    }
    
    public void ClearCluster()
    {
        currentDraggableCluster = null;
    }
    
}
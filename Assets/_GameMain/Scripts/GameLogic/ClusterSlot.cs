using System;
using UnityEngine;

public class ClusterSlot : MonoBehaviour
{
    private DraggableCluster currentCluster;

    private void Start()
    {
        Initialize(Vector2.one * 200);
    }

    public void Initialize(Vector2 size)
    {
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>().size = size;
        }
    }

    public bool TryAttachCluster(DraggableCluster newCluster)
    {
        if (currentCluster)
        {
            var newClusterPreviousSlot = newCluster.TryGetPreviousSlot();
            if (newClusterPreviousSlot)
            {
                currentCluster.TryAttachToSlot(newClusterPreviousSlot);
            }
            else
            {
                currentCluster.ReturnToPanel();
            }
        }
        
        currentCluster = newCluster;
        newCluster.transform.SetParent(transform);
        newCluster.transform.localPosition = Vector3.zero;
        return true;
    }
    
    public void ClearCluster()
    {
        currentCluster = null;
    }
    
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ClusterValidator : MonoBehaviour
{
    public void ValidateClustersInRow(WordRow wordRow)
    {
        var slots = wordRow.GetSlots();
        List<DraggableCluster> lockedClusters = new List<DraggableCluster>();
        
        foreach (var slot in slots)
        {
            var cluster = slot.GetCluster();
            if (cluster == null) continue;

            if (cluster.IsLocked())
            {
                lockedClusters.Add(cluster);
            }
        }
        
        foreach (var slot in slots)
        {
            var cluster = slot.GetCluster();
            if (cluster == null) continue;

            if (cluster.GetIndex() != slot.GetIndex())
            {
                ReturnCluster(cluster);
                continue;
            }

            if (lockedClusters.Count == 0)
            {
                LockCluster(cluster);
                lockedClusters.Add(cluster);
            }
            else
            {
                if (CheckWordConsistency(cluster, lockedClusters))
                {
                    LockCluster(cluster);
                    return;
                }
                
                ReturnCluster(cluster);
            }
        }
    }

    private bool CheckWordConsistency(DraggableCluster newCluster, List<DraggableCluster> lockedClusters)
    {
        return newCluster.GetTargetWord() == lockedClusters.First().GetTargetWord();
    }

    private void LockCluster(DraggableCluster cluster)
    {
        cluster.Lock();
    }

    private void ReturnCluster(DraggableCluster cluster)
    {
        cluster.ReturnToPanel();
    }
}
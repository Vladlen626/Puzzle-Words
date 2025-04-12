using System.Collections.Generic;
using UnityEngine;

public class WordRow : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;

    private List<ClusterSlot> slots;
    public void Initialize(int slotCount) 
    {
        for (var i = 0; i < slotCount; i++) 
        {
            var slot =  Instantiate(slotPrefab, transform);
            var clusterSlot = slot.GetComponent<ClusterSlot>();
            
            if (!clusterSlot)
            {
                clusterSlot = slot.AddComponent<ClusterSlot>();
            }
            
            clusterSlot.Initialize(i);
            slots.Add(clusterSlot);
        }
    }

    public ClusterSlot[] GetSlots()
    {
        return slots.ToArray();
    }

    public bool IsValid()
    {
        foreach (var clusterSlot in slots)
        {
            var cluster = clusterSlot.GetCluster();
            if (!cluster || !cluster.IsLocked())
            {
                return false; 
            }
        }

        return true;
    }
}
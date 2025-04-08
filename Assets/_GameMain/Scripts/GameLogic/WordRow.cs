using UnityEngine;

public class WordRow : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    public void Initialize(int slotCount) 
    {
        for (var i = 0; i < slotCount; i++) 
        {
            var slot =  Instantiate(slotPrefab, transform);
            slot.GetComponent<ClusterSlot>().Initialize();
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCluster : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Transform clusterPanel;
    [SerializeField] private LayerMask slotLayerMask;

    private ClusterSlot previousSlot;
    private bool isLocked;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isLocked) return;
        previousSlot?.ClearCluster();
        transform.SetParent(GetComponentInParent<Canvas>().transform);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var slot = Physics2D.OverlapPoint(transform.position, slotLayerMask);

        if (slot)
        {
            var clusterSlot = slot.GetComponent<ClusterSlot>();
            TryAttachToSlot(clusterSlot);
        }
        else
        {
            ReturnToPanel();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;
        transform.position = eventData.position;
    }

    public bool TryAttachToSlot(ClusterSlot slot)
    {
        if (slot.TryAttachCluster(this))
        {
            previousSlot = slot;
            return true;
        }

        ReturnToPanel();
        return false;
    }

    public ClusterSlot TryGetPreviousSlot()
    {
        return previousSlot;
    }
    
    public void ReturnToPanel()
    {
        transform.SetParent(clusterPanel);
        transform.localPosition = Vector3.zero;

        if (previousSlot)
        {
            previousSlot.ClearCluster();
            previousSlot = null;
        }
    }
}
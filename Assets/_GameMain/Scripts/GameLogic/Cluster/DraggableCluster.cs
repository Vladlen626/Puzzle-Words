using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCluster : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private LayerMask slotLayerMask;

    public Action<ClusterData> OnInit;

    private ClusterData clusterData;
    private Transform clusterPanel;
    private ClusterSlot previousSlot;

    public void Initialize(ClusterData data, Transform inClusterPanel)
    {
        clusterData = data;
        clusterPanel = inClusterPanel;
        OnInit?.Invoke(clusterData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (clusterData.IsLocked.Value) return;
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
        if (clusterData.IsLocked.Value) return;
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

    public string GetTargetWord()
    {
        return clusterData.TargetWord;
    }

    public bool IsLocked()
    {
        return clusterData.IsLocked.Value;
    }

    public int GetIndex()
    {
        return clusterData.Index;
    }

    public void Lock()
    {
        clusterData.IsLocked.Value = true;
    }

    public ClusterSlot TryGetPreviousSlot()
    {
        return previousSlot;
    }

    public void ReturnToPanel()
    {
        transform.SetParent(clusterPanel);
        transform.SetAsFirstSibling();
        transform.localPosition = Vector3.zero;
        clusterData.IsLocked.Value = false;

        if (previousSlot)
        {
            previousSlot.ClearCluster();
            previousSlot = null;
        }
    }
}
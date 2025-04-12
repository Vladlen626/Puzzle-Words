using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ClusterVisualController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clusterLettersText;
    [SerializeField] private Outline outline;

    private CompositeDisposable disposables = new CompositeDisposable();
    private DraggableCluster cluster;

    private void Awake()
    {
        cluster = GetComponent<DraggableCluster>();
        cluster.OnInit += Initialize;
    }

    private void Initialize(ClusterData data)
    {
        data.IsLocked
            .Subscribe(UpdateVisual)
            .AddTo(disposables);

        clusterLettersText.text = data.Text;
        UpdateVisual(data.IsLocked.Value);
    }

    private void UpdateVisual(bool isLocked)
    {
        outline.effectColor = isLocked ? Color.red : Color.green;
    }

    private void OnDestroy()
    {
        disposables.Dispose();
        cluster.OnInit -= Initialize;
    }
}
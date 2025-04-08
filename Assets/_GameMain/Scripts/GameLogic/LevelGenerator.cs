using System;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private LevelData levelDataTEST;
    [SerializeField] private Transform clustersContainer;
    [SerializeField] private Transform wordsContainer;
    [SerializeField] private GameObject wordRowPrefab;
    [SerializeField] private GameObject clusterPrefab;

    private void Start()
    {
        GenerateSlots(levelDataTEST);
        GenerateClusters(levelDataTEST);
    }

    private void GenerateSlots(LevelData levelData)
    {
        foreach (var wordData in levelData.words)
        {
            var row = Instantiate(wordRowPrefab, wordsContainer);
            var wordRow = row.GetComponent<WordRow>();
            wordRow.Initialize(wordData.clusters.Length);
        }
    }

    private void GenerateClusters(LevelData levelData)
    {
        foreach (var wordData in levelData.words)
        {
            foreach (var cluster in wordData.clusters)
            {
                var clusterObj = Instantiate(clusterPrefab, clustersContainer);
                var draggable = clusterObj.GetComponent<DraggableCluster>();
                draggable.Initialize(cluster, wordData.word, clustersContainer);
            }
        }
    }
}
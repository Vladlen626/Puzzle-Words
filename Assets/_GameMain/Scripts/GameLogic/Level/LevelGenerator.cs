using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform clustersContainer;
    [SerializeField] private Transform wordsContainer;
    [SerializeField] private GameObject wordRowPrefab;
    [SerializeField] private GameObject clusterPrefab;
    
    public List<WordRow> GenerateWordRows(LevelData levelData)
    {
        var wordRows = new List<WordRow>();
        
        foreach (var wordData in levelData.words)
        {
            var row = Instantiate(wordRowPrefab, wordsContainer);
            var wordRow = row.GetComponent<WordRow>();
            wordRow.Initialize(wordData.clusters.Length);
            wordRows.Add(wordRow);
        }

        return wordRows;
    }

    public void GenerateClusters(LevelData levelData)
    {
        foreach (var wordData in levelData.words)
        {
            for (var i = 0; i < wordData.clusters.Length; i++)
            {
                var clusterObj = Instantiate(clusterPrefab, clustersContainer);
                
                var data = clusterObj.GetComponent<ClusterData>();
                data.Initialize(wordData.clusters[i], wordData.word, i);
                
                var draggable = clusterObj.GetComponent<DraggableCluster>();
                draggable.Initialize(data, clustersContainer);
            }
        }
        
        clustersContainer.GetComponent<ChildShuffler>()?.ShuffleChildren();
    }
}
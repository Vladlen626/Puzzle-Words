using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class LevelController : MonoBehaviour
{
    [SerializeField] private Button validateButton;
    [SerializeField] private LevelData levelPlaceHolder;
    [Inject] private LevelGenerator generator;
    [Inject] private ClusterValidator clusterValidator;
        
    private LevelData currentLevel;
    private List<WordRow> wordRows;

    public LevelData GetLevelData()
    {
        return currentLevel;
    }

    // _____________ Private _____________
    
    private void Start()
    {
        GenerateLevel(levelPlaceHolder);
        validateButton.onClick.AddListener(ValidateClusters);
    }
    
    private void GenerateLevel(LevelData levelData)
    {
        wordRows = generator.GenerateWordRows(levelData);
        generator.GenerateClusters(levelData);
    }
    
    private void ValidateClusters()
    {
        foreach (var wordRow in wordRows)
        {
            clusterValidator.ValidateClustersInRow(wordRow);
        }
        ValidateLevel();
    }
    
    private void ValidateLevel()
    {
        foreach (var wordRow in wordRows)
        {
            if (wordRow.IsValid())
                return;
        }
        LevelComplete();
    }

    private void LevelComplete()
    {
        
    }

   
}
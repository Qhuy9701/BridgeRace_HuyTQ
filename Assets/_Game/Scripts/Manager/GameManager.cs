using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private int level = 1;
    public int Level { get => level; set => level = value; }


    private bool isStartGame;
    public bool IsStartGame { get => isStartGame; set => isStartGame = value; }


    private bool isWinning;
    public bool IsWinning { get => isWinning; set => isWinning = value; }


    private bool isLose;
    public bool IsLose { get => isLose; set => isLose = value; }


    public override void Awake()
    {
        base.Awake();
        LoadGameData();
    }

    public void LoadGameData()
    {
        Level = PlayerPrefs.GetInt("level", 1);
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("level", Level);
    }
}

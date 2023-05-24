using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject panelStartGame;
    [SerializeField] private GameObject panelWinLevelGame;
    [SerializeField] private GameObject panelLoseLevelGame;
    [SerializeField] private GameObject panelFinalWin;

    public event Action OnNextLevel;


    public void BtnStartGame() {
        panelStartGame.SetActive(false);
        GameManager.Instance.IsStartGame = true;
    }
    public void ShowPanelWin()
    {
        panelWinLevelGame.SetActive(true);
    }

    public void HidePaneWin()
    {
        panelWinLevelGame.SetActive(false);
    }

    public void ShowPanelLose()
    {
        panelLoseLevelGame.SetActive(true);
    }

    public void HidePaneLose()
    {
        panelLoseLevelGame.SetActive(false);
    }

    public void ShowPanelFinalWin()
    {
        panelFinalWin.SetActive(true);
    }

    public void BtnNextLevel()
    {
        HidePaneWin();
        HidePaneLose();
        ObjectPooling.Instance._dicGameObject.Clear();

        GameManager.Instance.Level++;
        GameManager.Instance.SaveGameData();
        LevelManager.Instance.RemoveLastMap();
        LevelManager.Instance.LoadMapAtCurrentLevel();
        GameManager.Instance.IsWinning = false;
        GameManager.Instance.IsLose = false;

        OnNextLevel?.Invoke();
    }

    public void BtnQuit()
    {
        Application.Quit();
    }
}

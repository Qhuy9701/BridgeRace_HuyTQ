using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> // chi co duy nhat 1 script
{

    private LevelController currentLevel;
    public LevelController CurrentLevel { get => currentLevel; set => currentLevel = value; }


    private void Start()
    {
        LoadMapAtCurrentLevel();
    }

    public void RemoveLastMap()
    {
        Destroy(currentLevel.gameObject);
    }

    public void LoadMapAtCurrentLevel()
    {
        GameObject go = Resources.Load($"Levels/Level {GameManager.Instance.Level}") as GameObject;
        if(go == null)
        {
            UIManager.Instance.ShowPanelFinalWin();
            return;
        }
        currentLevel = Instantiate(go).GetComponent<LevelController>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public int level;
    public bool isWinPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterProps character))
        {
            if (isWinPos)
            {
                if (character is Player)
                {
                    GameManager.Instance.IsWinning = true;
                    character.HandLeWin();
                    UIManager.Instance.ShowPanelWin();
                    return;
                } 
                if(character is Bot) {
                    character.HandLeWin();
                    GameManager.Instance.IsLose = true;
                    UIManager.Instance.ShowPanelLose();
                    return; 
                }
            }

            if (level - 1 != character.AtFloor) return;
            character.HandleNextFloor();
        }
    }
}

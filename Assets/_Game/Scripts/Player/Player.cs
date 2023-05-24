using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterProps
{
    [SerializeField] private PlayerMovement playerMovement;

    public PlayerMovement PlayerMovement => playerMovement;

    private void Update()
    {
        HandleUpdateAnim();
    }
    private void HandleUpdateAnim()
    {

        if (GameManager.Instance.IsStartGame)
        {
            if(GameManager.Instance.IsLose)
            {
                ChangeAnim("Dead");
                return;
            }

            if (IsCharacterFirstWin)
            {
                ChangeAnim("Win");
                return;
            }

            if (PlayerMovement.IsNotMoving())
            {
                ChangeAnim("Idle");
                return;
            }
            if (!PlayerMovement.IsNotMoving())
            {
                ChangeAnim("Run");
                return;
            }
        }
    }

    public override void HandLeWin()
    {
        base.HandLeWin();
        playerMovement.enabled= false;
    }

    protected override void ResetFLoor()
    {
        base.ResetFLoor();  
        playerMovement.enabled= true;
    }
}

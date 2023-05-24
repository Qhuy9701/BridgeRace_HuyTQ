using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    [SerializeField] DataColor dataColor;
    [SerializeField] private ColorType color;

    [SerializeField] private MeshRenderer mesh;

    [SerializeField] private BoxCollider wall;

    public bool IsSameColor(ColorType colorType) => this.color == colorType;
    public void ChangeColor(ColorType colorType)
    {
        this.color = colorType;
        mesh.material = dataColor.GetMat(colorType);
    }

    private void TurnOnWall()
    {
        wall.enabled = true;
    }

    private void TurnOffWall()
    {
        wall.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterProps character))
        {
            if (character is Player)
            {
                Player player = (Player)character;

                if (player.NumberBlockOwner <= 0 && !IsSameColor(player.Color))
                {
                    if (player.PlayerMovement.DirectionZ > 0)
                    {
                        TurnOnWall();
                    }
                    else if (player.PlayerMovement.DirectionZ <= 0)
                    {
                        TurnOffWall();
                    }
                    return;
                }
                if (IsSameColor(ColorType.None))
                {
                    ChangeColor(player.Color);
                    if (player.PlayerMovement.DirectionZ > 0)
                    {
                        TurnOffWall();
                    }
                    player.RemoveBlockAndSetBackToGround();
                    return;
                }

                if (IsSameColor(player.Color))
                {
                    if (player.PlayerMovement.DirectionZ > 0)
                    {
                        TurnOffWall();
                    }
                    return;
                }
                if (!IsSameColor(player.Color))
                {
                    ChangeColor(player.Color);
                    if (player.PlayerMovement.DirectionZ > 0)
                    {
                        TurnOffWall();
                    }
                    player.RemoveBlockAndSetBackToGround();
                    return;
                }
            }
            else if (character is Bot)
            {
                Bot bot = (Bot)character;

                if (bot.NumberBlockOwner <= 0 && !IsSameColor(bot.Color))
                {
                    return;
                }
                if (IsSameColor(ColorType.None))
                {
                    ChangeColor(bot.Color);
                    bot.RemoveBlockAndSetBackToGround();
                    return;
                }
                if (!IsSameColor(bot.Color))
                {
                    ChangeColor(bot.Color);
                    bot.RemoveBlockAndSetBackToGround();
                    return;
                }
            }
        }
    }
}

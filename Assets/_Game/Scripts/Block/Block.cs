using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private ColorType color;

    [SerializeField] private Vector3 oriPos;


    public bool IsSameColor(ColorType colorType) => this.color == colorType;

    public void SavePos()
    {
        oriPos = transform.localPosition;
    }
    public void LoadPos()
    {
        transform.localPosition = oriPos;
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterProps character))
        {
            if(IsSameColor(character.Color))
            {
                character.ListToCollectBlock.Remove(transform.position);
                character.NumberBlockOwner++;
                transform.SetParent(character.BackPoint);
                transform.localRotation = Quaternion.identity;
                transform.localPosition = new Vector3(0, character.NumberBlockOwner * 0.3f, 0);

                if(character is Bot)
                {
                    Bot bot = (Bot)character;
                    bot.HasTarget= false;
                    //character.GetComponent<Bot>().HasTarget= true;
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProps : MonoBehaviour
{
    private Vector3 oriPos;
    protected BlockSpawner blockSpawner;

    [SerializeField] protected Transform backPoint;
    public Transform BackPoint => backPoint;


    [SerializeField] protected int atFloor = 1;
    public int AtFloor => atFloor;


    [SerializeField] private int numberBlockOwner;
    public int NumberBlockOwner { get => numberBlockOwner; set => numberBlockOwner = value; }


    [SerializeField] protected List<Vector3> listToCollectBlock = new List<Vector3>();
    public List<Vector3> ListToCollectBlock => listToCollectBlock;


    [SerializeField] protected ColorType color;
    public ColorType Color => color;


    [SerializeField] protected int indexPrefab;


    [SerializeField] private bool isCharacterFirstWin;
    public bool IsCharacterFirstWin => isCharacterFirstWin;


    // anim
    private string currentAnimName;
    [SerializeField] private Animator anim;


    private void Start()
    {
        UIManager.Instance.OnNextLevel += ResetFLoor;

        OriPos();
        OnInit();
    }


    public void OriPos()
    {
        oriPos = transform.position;
    }

    protected virtual void OnInit()
    {
        listToCollectBlock.Clear();
        blockSpawner = LevelManager.Instance.CurrentLevel.GetCurrentBlockSpawnerInFloor(AtFloor - 1);

        // gan list can collect tu blockSpawner tang moi
        listToCollectBlock = blockSpawner.DataBlockPrefab[indexPrefab].listBlockPos;
    }

    protected virtual void ResetFLoor()
    {
        isCharacterFirstWin = false;
        backPoint.gameObject.SetActive(true);
        NumberBlockOwner = 0;
        atFloor = 1;
        transform.position = oriPos;

        Block[] blocks = backPoint.GetComponentsInChildren<Block>();
        foreach (Block item in blocks)
        {
            Destroy(item.gameObject);
        }
        OnInit();
    }


    public virtual void HandleNextFloor()
    {
        atFloor++;
        OnInit();
        blockSpawner.GenerateBlock(indexPrefab, listToCollectBlock);
    }


    public void RemoveBlockAndSetBackToGround()
    {
        NumberBlockOwner--;
        Transform blockRemove = backPoint.GetChild(backPoint.childCount - 1);
        blockRemove.SetParent(blockSpawner.transform);
        blockRemove.GetComponent<Block>().LoadPos();
        listToCollectBlock.Add(blockRemove.transform.position);
    }

    public virtual void HandLeWin()
    {
        backPoint.gameObject.SetActive(false);
        isCharacterFirstWin = true;
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            if (!string.IsNullOrEmpty(currentAnimName))
                anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : CharacterProps
{
    public enum StateBot
    {
        Idle, SeekingBlock, BuildBridge, Winning, Lose
    }
    public StateBot stateBot;

    private NavMeshAgent navMeshAgent;
    [SerializeField] private Vector3 currentTargetPoint;
    [SerializeField] private int needBlockToBuild = 5;
    [SerializeField] private bool hasTarget;
    public bool HasTarget { get => hasTarget; set => hasTarget = value; }

    [SerializeField] private Transform currentTargetBridge;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void OnInit()
    {
        base.OnInit();

        // tim currentTargetBridge
        Transform allBridge = LevelManager.Instance.CurrentLevel.GetCurrentBridgeInFloor(AtFloor - 1);
        int numberBridge = allBridge.childCount;
        currentTargetBridge = allBridge.GetChild(Random.Range(0, numberBridge)).Find("End Point");
    }

    protected override void ResetFLoor()
    {
        base.ResetFLoor();
        ResetBot();
    }

    private void Update()
    {
        if (GameManager.Instance.IsWinning)
        {
            stateBot = StateBot.Lose;
            ChangeAnim("Dead");
            navMeshAgent.isStopped = true;
            return;
        }

        if (GameManager.Instance.IsLose)
        {
            if (IsCharacterFirstWin)
            {
                stateBot = StateBot.Winning;
                ChangeAnim("Win");

            }
            else
            {
                stateBot = StateBot.Idle;
                ChangeAnim("Idle");
            }
            navMeshAgent.isStopped = true;
            return;
        }

        if (GameManager.Instance.IsStartGame)
        {
            HandleUpdateAnim();

            if (stateBot == StateBot.Idle)
            {
                stateBot = StateBot.SeekingBlock;
                needBlockToBuild = Random.Range(4, 15);
                HasTarget = false;
            }

            if (stateBot == StateBot.SeekingBlock)
            {
                if (!HasTarget)
                {
                    if (NumberBlockOwner < needBlockToBuild)
                    {
                        FindBlock();
                    }
                    else
                    {
                        stateBot = StateBot.BuildBridge;
                        MoveToTarget(currentTargetBridge.position);
                    }
                }
            }
            if (stateBot == StateBot.BuildBridge)
            {
                if (NumberBlockOwner == 0) // dk de dung tim gach tiep
                {
                    stateBot = StateBot.Idle;
                }
            }
        }
    }

    private void HandleUpdateAnim()
    {
        if (navMeshAgent.velocity.sqrMagnitude < 0.1f)
        {
            ChangeAnim("Idle");
        }
        else
        {
            ChangeAnim("Run");
        }
    }

    public override void HandleNextFloor()
    {
        base.HandleNextFloor();
        stateBot = StateBot.Idle; // reset ve idle de tim gach tang tiep theo
    }

    public override void HandLeWin()
    {
        base.HandLeWin();
        navMeshAgent.ResetPath();
    }

    private void ResetBot()
    {
        stateBot = StateBot.Idle;
        navMeshAgent.ResetPath();
        //ChangeAnim("Idle");

    }
    private void FindBlock()
    {
        if (listToCollectBlock.Count <= 0) return;
        int randomBlockToCollect = Random.Range(0, listToCollectBlock.Count);
        Vector3 blockPos = listToCollectBlock[randomBlockToCollect];
        currentTargetPoint = blockPos;
        MoveToTarget(currentTargetPoint);
        HasTarget = true;
    }
    private void MoveToTarget(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }

}

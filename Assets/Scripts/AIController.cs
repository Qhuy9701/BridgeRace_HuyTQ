using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public List<Vector3> brickPos = new List<Vector3>();
    public NavMeshAgent agent;
    public int onstage = 0;
    GameObject nextStage;
    GameObject currentStage;
    Vector3 nextStagePos;
    int playerteam;
    public int bricktocollect = 0;
    void Start()
    {
        playerteam = GetComponent<CharacterBehavior>().playerteam;
        currentStage = GameManage.GMinstance.Stage[onstage];
        nextStage = GameManage.GMinstance.Stage[onstage + 1];
        agent = GetComponent<NavMeshAgent>();
        nextStagePos = nextStage.transform.position;
        FindBrick();
        GetBrick();
    }

    // Update is called once per frame

    void Update()
    {
        
    }
    

    public bool FindBrick()
    {
        brickPos.Clear();
        int brickamount = currentStage.GetComponent<Stage>().brickonstage.Count;
        int brickwithsamecolor = 0;
        for (int i = 0; i < brickamount; i++)
        {
            if (currentStage.GetComponent<Stage>().brickonstage[i].GetComponent<BrickBehavior>().brickteam == playerteam)
            {
                brickwithsamecolor++;
                Vector3 brickpos = currentStage.GetComponent<Stage>().brickonstage[i].transform.position;
                brickPos.Add(brickpos);
            }
        }
        if (brickPos.Count > 0) return true;
        else return false;
    }
    public void GetBrick()
    {
        if (brickPos.Count>0)
        {
            int count = brickPos.Count - 1;
            agent.SetDestination(brickPos[count]);  
        }
        else GotoNextStage();
    }


    void GotoNextStage()
    {
        agent.SetDestination(nextStagePos);
        bricktocollect = 0;
    }
    
}

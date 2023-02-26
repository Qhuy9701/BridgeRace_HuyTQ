using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public List<Vector3> brickPos = new List<Vector3>(); // Danh sách vị trí gạch cần thu thập
    public NavMeshAgent agent; // Đối tượng NavMeshAgent để di chuyển
    public int onstage = 0; // Số sân khởi đầu
    GameObject nextStage; // Sân tiếp theo để di chuyển tới
    GameObject currentStage; // Sân hiện tại
    Vector3 nextStagePos; // Vị trí của sân tiếp theo
    int playerteam; // Đội của người chơi
    public int bricktocollect = 0; // Số gạch còn lại cần thu thập

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

    // Hàm tìm vị trí của gạch cùng màu
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
        if (brickPos.Count > 0) return true; // Nếu có gạch cùng màu thì trả về true
        else return false; // Ngược lại trả về false
    }

    // Hàm di chuyển đến vị trí của gạch cùng màu gần nhất
    public void GetBrick()
    {
        if (brickPos.Count > 0)
        {
            int count = brickPos.Count - 1;
            agent.SetDestination(brickPos[count]);
        }
        else GotoNextStage(); // Nếu không còn gạch cùng màu thì di chuyển đến sân tiếp theo
    }

    // Hàm di chuyển đến sân tiếp theo
    void GotoNextStage()
    {
        agent.SetDestination(nextStagePos);
        bricktocollect = 0; // Đặt lại số gạch còn lại cần thu thập về 0
    }

    // Hàm cập nhật mỗi frame
    void Update()
    {
        // Nếu đang ở trạng thái di chuyển và NavMeshAgent đã đến vị trí đích
        if (agent.pathPending == false && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (brickPos.Count > 0)
            {
                // đến được vị trí gạch, lấy gạch
                brickPos.RemoveAt(brickPos.Count - 1);
                bricktocollect++;
                // kiểm tra nếu đã lấy đủ số lượng gạch, thì đi đến stage kế tiếp
                if (bricktocollect >= 3)
                {
                    GotoNextStage();
                }
                else
                {
                    // chưa lấy đủ số lượng gạch, tìm gạch tiếp theo
                    GetBrick();
                }
            }
            else
            {
                // không tìm thấy gạch nữa, đi đến stage kế tiếp
                GotoNextStage();
            }
        }
    }
}


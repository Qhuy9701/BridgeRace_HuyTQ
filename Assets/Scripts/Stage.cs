using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    
    public List<Vector3> respawnPos = new List<Vector3>();
    private List<GameObject> brickoffstage = new List<GameObject>();
    public List<GameObject> brickonstage = new List<GameObject>();
    public List<int> playeronthisstage;
    private Vector3 startPos;
    private float delay = 5f;
    [SerializeField] private int amountX = 5;
    [SerializeField] private int amountZ = 5;
    [SerializeField] private float brickfloat = 1.3f;
    [SerializeField] private float distance = 2.5f;



    void Start()
    {
        startPos = new(transform.position.x + -amountX / 2 * distance, transform.position.y + 0, transform.position.z + -amountZ / 2 * distance);
        playeronthisstage = new List<int>();
        SpawnBrickOnStage();
        InvokeRepeating("RespawnBrick", 0, delay);
    }
    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    #region collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Character") == true)
        {
            int playerindex;
            playerindex = collision.collider.gameObject.GetComponent<CharacterBehavior>().playerteam;
            playeronthisstage.Add(playerindex);
            ActivateBrick(playerindex);

        }
    }

    #endregion

    #region spawn brick
    void SpawnBrickOnStage()
    {
        for (int i = 0; i < amountX; i++)
        {
            for (int j = 0; j < amountZ; j++)
            {
                Vector3 pos = new(startPos.x + i * distance, startPos.y + brickfloat, startPos.z + j * distance);
                GameObject obj = BrickSpawner.spawnerinstance.BricktoSpawn(transform) as GameObject;
                obj.transform.position = pos;
                obj.transform.rotation = Quaternion.Euler(0, 0, 90);
                brickoffstage.Add(obj);
            }
        }
    }
    void ActivateBrick(int team)
    {   
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < brickoffstage.Count; i++)
        {
            GameObject brickOn = brickoffstage[i];
            int teamofbrick = brickoffstage[i].GetComponent<BrickBehavior>().brickteam;
            if (teamofbrick == team)
            {
                brickOn.SetActive(true);
                list.Add(brickOn);
            }
        }
        if (list.Count > 0)
        {
            for(int j= 0; j < list.Count; j++)
            {
                brickoffstage.Remove(list[j]);
                brickonstage.Add(list[j]);
            }
        }
        
    }
    #endregion
    void RespawnBrick()
    {
        if (respawnPos.Count > 0)
        {
            for (int i = 0; i < respawnPos.Count; i++)
            {
                GameObject brick = BrickSpawner.spawnerinstance.BricktoSpawn(transform) as GameObject;
                brick.transform.position = respawnPos[i];
                brick.transform.rotation = Quaternion.Euler(0, 0, 90);
                brickoffstage.Add(brick);
            }
            for (int j = 0; j < playeronthisstage.Count; j++)
            {
                ActivateBrick(playeronthisstage[j]);
            }
            respawnPos.Clear();
        }
    }

    
}

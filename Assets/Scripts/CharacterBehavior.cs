using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBehavior : MonoBehaviour
{
    public List<GameObject> Backpack = new List<GameObject>();
    public int playerteam;
    public int maxstep;
    public float zlimit;
    private float distance = 1f;
    private GameObject currentlyonstage;
    private GameObject currentlyonbridge;
    private bool islimited = false;

    bool isBot;
    void Start()
    {
        isBot = this.TryGetComponent(out NavMeshAgent agent);

    }

    // Update is called once per frame
    void Update()
    {
        if (islimited == true) Limitonbridge();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage") == true)
        {
            currentlyonstage = collision.gameObject;
            maxstep = -1;
            islimited = false;
            zlimit = collision.transform.position.z + 10f;
        }
        else if (collision.gameObject.CompareTag("Bridge") == true)
        {
            currentlyonbridge = collision.gameObject;

        }
        else if(collision.gameObject.CompareTag("Block")==true && isBot == true)
        {
            GetComponent<AIController>().FindBrick();
            GetComponent<AIController>().GetBrick();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick") == true)
        {
            if (other.gameObject.GetComponent<BrickBehavior>().brickteam == playerteam)
            {
                if (isBot == true)
                {
                    GetComponent<AIController>().brickPos.Remove(other.transform.position);
                    GetComponent<AIController>().GetBrick();
                }

                currentlyonstage.GetComponent<Stage>().respawnPos.Add(new Vector3(other.transform.position.x, 1.3f, other.transform.position.z));
                currentlyonstage.GetComponent<Stage>().brickonstage.Remove(other.gameObject);
                Backpack.Add(other.gameObject);
                other.transform.SetParent(transform);
                other.transform.localRotation = Quaternion.Euler(0, 0, 90);
                other.transform.localPosition = new(0, Backpack.Count * distance / 2, -distance);
                other.GetComponent<Collider>().enabled = false;

            }
        }
        else if (other.gameObject.CompareTag("Step") == true)
        {
            int currentstepteam = other.GetComponent<Step>().stepteam;
            int currentstepindex = other.GetComponent<Step>().currentindex;

            if (maxstep < currentstepindex)
            {
                if (playerteam == currentstepteam)
                {
                    maxstep = currentstepindex;
                    islimited = false;
                }
                else
                {
                    if (Backpack.Count > 0)
                    {
                        //remove brick
                        GameObject brickinbp = Backpack[Backpack.Count - 1];
                        brickinbp.GetComponent<Collider>().enabled = true;
                        brickinbp.transform.parent = null;
                        Backpack.RemoveAt(Backpack.Count - 1);
                        BrickSpawner.spawnerinstance.Brickpool.Add(brickinbp);
                        brickinbp.SetActive(false);
                        //remove gach tro ve spawner
                        currentlyonbridge.GetComponent<Bridge>().NewStep(currentstepindex, playerteam);
                        maxstep = currentstepindex;
                        islimited = false;
                    }
                    else
                    {
                        zlimit = other.transform.position.z - 1.5f;
                        islimited = true;
                        if (isBot == true)
                        {
                            GetComponent<AIController>().FindBrick();
                            GetComponent<AIController>().GetBrick();
                        }
                    }
                }
            }
            else islimited = false;
        }
    }


    private void Limitonbridge()
    {
        if (this.transform.position.z >= zlimit) { transform.position = new(transform.position.x, transform.position.y, zlimit); }
    }
}

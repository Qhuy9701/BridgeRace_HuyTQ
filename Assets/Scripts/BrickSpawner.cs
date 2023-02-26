using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameObject prefab;
    public static BrickSpawner spawnerinstance;
    public List<GameObject> Brickpool;
    [SerializeField] private int poolsize = 100;
    [SerializeField] private Material[] brickMats = new Material[4];
    private void Awake()
    {
        spawnerinstance = this;
        Brickpool = new List<GameObject>();
        for (int i = 0; i < poolsize; i++)
        {
            GameObject gameobj = Instantiate(prefab);
            gameobj.transform.parent = transform;
            int rand = Random.Range(0, 4);
            gameobj.GetComponent<BrickBehavior>().brickteam = rand;
            gameobj.GetComponent<MeshRenderer>().material = brickMats[rand];
            gameobj.SetActive(false);
            Brickpool.Add(gameobj);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject BricktoSpawn(Transform transform)
    {
        if (Brickpool.Count > 0)
        {
            GameObject lastbrick = Brickpool[Brickpool.Count - 1];
            Brickpool.RemoveAt(Brickpool.Count - 1);
            lastbrick.transform.SetParent(transform);
            return lastbrick;
        }
        else return null;
    }


}

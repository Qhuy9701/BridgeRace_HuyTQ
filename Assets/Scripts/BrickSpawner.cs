using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameObject prefab;
    public static BrickSpawner Instance;
    public List<GameObject> brickpool;
    [SerializeField] private int poolsize = 100;
    [SerializeField] private Material[] brickMats = new Material[4];
private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
    }
    else
    {
        Destroy(gameObject);
        return;
    }

    for (int i = 0; i < poolsize; i++)
    {
        GameObject gameobj = Instantiate(prefab, transform);
        int rand = Random.Range(0, brickMats.Length);
        gameobj.GetComponent<BrickBehavior>().brickteam = rand;
        gameobj.GetComponent<MeshRenderer>().material = brickMats[rand];
        gameobj.SetActive(false);
        brickpool.Add(gameobj);
    }
}

public GameObject SpawnBrick(Transform parentTransform)
{
    if (brickpool.Count == 0)
    {
        return null;
    }

    GameObject lastBrick = brickpool[brickpool.Count - 1];
    brickpool.RemoveAt(brickpool.Count - 1);
    lastBrick.transform.SetParent(parentTransform);
    lastBrick.SetActive(true);

    return lastBrick;
}
}
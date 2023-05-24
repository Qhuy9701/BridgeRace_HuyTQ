using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DataBlockPrefab
{
    public int prefabIndex;
    public GameObject blockPrefab;
    public List<Vector3> listBlockPos;
}

public class BlockSpawner : MonoBehaviour
{
    private List<Vector3> listPosSpawnAllBlock = new List<Vector3>();
    private int maxBrickType = 25;
    [SerializeField] private int startPosSpawn = -5, endPosSpawn = 5;


    [SerializeField] private DataBlockPrefab[] dataBlockPrefab;
    public DataBlockPrefab[] DataBlockPrefab { get => dataBlockPrefab; set => dataBlockPrefab = value; }

    public bool hasGenerateBrick;


    private void Start()
    {
        GeneratePosAndShufflePos();

        if (hasGenerateBrick)
        {
            for (int i = 0; i < DataBlockPrefab.Length; i++)
            {
                GenerateBlock(DataBlockPrefab[i].prefabIndex, DataBlockPrefab[i].listBlockPos);
            }
        }
    }

    private void GeneratePosAndShufflePos()
    {
        for (int i = startPosSpawn; i < endPosSpawn; i++)
        {
            for (int j = startPosSpawn; j < endPosSpawn; j++)
            {
                listPosSpawnAllBlock.Add(new Vector3(i * 2, 0, j * 2));
            }
        }
        listPosSpawnAllBlock.Shuffle();
    }

    public void GenerateBlock(int index, List<Vector3> listContainBlock)
    {
        for (int i = 0; i < maxBrickType; i++)
        {
            GameObject block = ObjectPooling.Instance.GetGameObject(DataBlockPrefab[index].blockPrefab);
            int randomPosIndex = UnityEngine.Random.Range(0, listPosSpawnAllBlock.Count);
            block.SetActive(true);
            block.transform.SetParent(transform);
            block.transform.localPosition = listPosSpawnAllBlock[randomPosIndex]; // set local pos  = listPosSpawnAll
            block.GetComponent<Block>().SavePos();
            listPosSpawnAllBlock.RemoveAt(randomPosIndex);
            listContainBlock.Add(block.transform.position); // add global pos vao listcontainBlock
        }
    }

}

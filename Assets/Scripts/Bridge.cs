using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private float BridgeLength = 20f;
    [SerializeField] private GameObject step;
    [SerializeField] private Material[] stepMats = new Material[4];
    public List<GameObject> stepList = new List<GameObject>();
    private float stepheight = 0.1f;
    private int a = 0;
    private float stepfirstPos;
    void Start()
    {
        stepfirstPos = transform.position.z - (int)BridgeLength / 2 + 0.5f;
        for (int i = 0; i < (int)BridgeLength; i++)
        {
            GameObject obj = Instantiate(step, new Vector3(transform.position.x, transform.position.y + stepheight, stepfirstPos + i), Quaternion.identity) as GameObject;
            stepList.Add(obj);
            obj.GetComponent<Step>().currentindex = i;
            obj.transform.SetParent(transform);
            obj.GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stepList[a].GetComponent<Renderer>().material = stepMats[0];
            a++;
        }
    }

    public GameObject NewStep(int index, int team)
    {
        if (index < stepList.Count)
        {
            stepList[index].GetComponent<Renderer>().enabled = true;
            stepList[index].GetComponent<Renderer>().material = stepMats[team];
            stepList[index].GetComponent<Step>().stepteam = team;

            return stepList[index];
        }
        else
        {
            return null;
        }
    }
}

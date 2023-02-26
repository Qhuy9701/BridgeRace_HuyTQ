using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public List<GameObject> Stage = new List<GameObject>();
    public static GameManage GMinstance;
    private void Awake()
    {
        GMinstance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

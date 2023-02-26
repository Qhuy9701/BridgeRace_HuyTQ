using UnityEngine;
using UnityEngine.AI;

public class Block : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Renderer>().enabled = true;
            if (other.gameObject.TryGetComponent(out NavMeshAgent agent) == true)
            {
                other.gameObject.GetComponent<AIController>().onstage++;
            }
        }
    }

}

using UnityEngine;
using UnityEngine.AI;

public class Block : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Renderer>().enabled = true;
        }
    }

}

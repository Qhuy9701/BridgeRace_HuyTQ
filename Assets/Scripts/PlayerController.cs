using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private Rigidbody thisbody;
    [SerializeField] private GameObject model;
    private float leftright;
    private float updown;
    private float speed = 5f;
    public static PlayerController instance;
    private Vector3 camoffset;
    private Animator thisanimator;
    private bool isRunning = false;
    Ray testray;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        thisbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
        camoffset = Camera.main.transform.position - transform.position;
        thisanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        leftright = Input.GetAxis("Horizontal");
        updown = Input.GetAxis("Vertical");
        if (updown != 0 || leftright != 0)
        {
            thisbody.rotation = Quaternion.LookRotation(new Vector3(leftright, 0, updown), Vector3.up);
            thisbody.velocity = new(leftright * speed, 0, updown * speed);
            isRunning = true;
            thisanimator.SetBool("isRunning", isRunning);
            // animation run
        }
        else
        {
            //animation stop running
            isRunning = false;
            thisanimator.SetBool("isRunning", isRunning);
        }

    }
    
    private void LateUpdate()
    {
        testray = new Ray(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), thisbody.velocity*5);
        
        _camera.transform.position = transform.position + camoffset;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(testray);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    public float playerSpeed =10f;
    public float minDistance = 2f, maxDistance = 5f, recenterDuration = 0.5f;

    public static Crowd instance;

    public Vector2 Center { get; set; }
    public List<Transform> CrowdList { get; set; }

    private Vector3 velocity;

    void Awake()
    {
        instance = this;
        Center = new Vector2(transform.position.x, transform.position.z);
        CrowdList = new List<Transform>();
    }

    void Start ()
    {

    }

    void Update()
    {
        Vector3 newPosition;
        if (CrowdList.Count > 0)
        {
            Vector3 center = Vector3.zero;
            foreach (Transform gary in CrowdList)
            {
                center += gary.transform.position;
            }
            center /= CrowdList.Count;
            Vector3 smoothedCenter = Vector3.SmoothDamp(transform.position, center, ref velocity, recenterDuration);
            newPosition = (new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * playerSpeed * Time.deltaTime) + smoothedCenter;
        }
        else
        {
            newPosition = (new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * playerSpeed * Time.deltaTime) + transform.position;
        }        
        transform.position = newPosition;
        Center = new Vector2(transform.position.x, transform.position.z);
    }
}

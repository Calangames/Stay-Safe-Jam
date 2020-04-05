using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    public float playerSpeed =10f;
    public float minDistance = 2f, maxDistance = 5f, recenterDuration = 0.5f;
    public Transform cameraPivot, startingGary;

    public static Crowd instance;

    public Vector2 Center { get; set; }
    public List<Transform> CrowdList { get; set; }

    private SphereCollider sphereCollider;
    private Vector3 velocity;
    private Vector3 moveDirection;
    private float cameraDirection;

    void Awake()
    {
        instance = this;
        Center = new Vector2(transform.position.x, transform.position.z);
        CrowdList = new List<Transform>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Start ()
    {
        transform.position = startingGary.position;
    }

    void Update()
    {
        if (CrowdList.Count > 0)
        {
            Vector3 newPosition;
            Vector3 center = Vector3.zero;
            foreach (Transform gary in CrowdList)
            {
                center += gary.transform.position;
            }
            center /= CrowdList.Count;
            Vector3 smoothedCenter = Vector3.SmoothDamp(transform.position, center, ref velocity, recenterDuration);
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime, moveDirection.y, Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime);
            cameraDirection = cameraPivot.eulerAngles.y;
            moveDirection = Quaternion.AngleAxis(cameraDirection, Vector3.up) * moveDirection;
            newPosition = moveDirection + smoothedCenter;
            transform.position = newPosition;
        }    

        Center = new Vector2(transform.position.x, transform.position.z);
    }

    public void ChangeSize(float minDistanceIncrement, float maxDistanceIncrement)
    {
        minDistance += minDistanceIncrement;
        maxDistance += maxDistanceIncrement;
        sphereCollider.radius += maxDistanceIncrement;
    }
}

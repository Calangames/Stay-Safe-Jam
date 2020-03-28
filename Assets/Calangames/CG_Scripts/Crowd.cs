using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    public float playerSpeed;
    public float minDistance = 2f, maxDistance = 5f;

    public static Crowd instance;

    public Vector2 Center { get; set; }
    public List<Transform> CrowdList { get; set; }

    void Awake()
    {
        instance = this;
        Center = new Vector2(transform.position.x, transform.position.z);
    }

    void Start ()
    {

    }

    void Update()
    {
        Vector3 newPosition = (new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * playerSpeed * Time.deltaTime) + transform.position;
        transform.position = newPosition;
        Center = new Vector2(transform.position.x, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {

    }
}

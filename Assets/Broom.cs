using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    float speed = 30f, lifeInSeconds = 2f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("Destroy", lifeInSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}

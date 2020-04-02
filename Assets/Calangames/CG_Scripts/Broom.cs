using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    public int damage = 2;
    public float speed = 30f, lifeInSeconds = 2f;

    private Rigidbody rb;
    private bool canHit = true;

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
        if (canHit && other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().ReceiveDamage(damage);
            Destroy(gameObject);
            canHit = false;
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
        canHit = false;
    }
}

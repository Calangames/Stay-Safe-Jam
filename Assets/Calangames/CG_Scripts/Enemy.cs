using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Image healthBarFill;
    public float pathRefreshDelay = 0.2f;
    public int maxLife = 10;

    private NavMeshAgent navMeshAgent;
    private bool following;
    private WaitForSeconds delay;
    private int life;
    private float fillVelocity;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        delay = new WaitForSeconds(pathRefreshDelay);
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            navMeshAgent.SetDestination(Crowd.instance.transform.position);
        }
        float newFillAmount = (float)life / maxLife;
        healthBarFill.fillAmount = Mathf.SmoothDamp(healthBarFill.fillAmount, newFillAmount, ref fillVelocity, 0.1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crowd"))
        {
            following = true;
        }
    }

    public void ReceiveDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator RefreshPath()
    {
        while (following)
        {
            navMeshAgent.SetDestination(Crowd.instance.transform.position);
            yield return delay;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float pathRefreshDelay = 0.2f;

    private NavMeshAgent navMeshAgent;
    private bool following;
    private WaitForSeconds delay;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        delay = new WaitForSeconds(pathRefreshDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            navMeshAgent.SetDestination(Crowd.instance.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crowd"))
        {
            following = true;
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

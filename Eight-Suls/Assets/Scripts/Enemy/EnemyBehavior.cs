using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [Header("Vision Settings")]
    [SerializeField] private float radius = 5;
    [SerializeField] [Range(0, 360)] private float angle = 90;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private float targetOffset;
    
    NavMeshAgent navAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetNavMeshAgent();
        StartCoroutine(FOVRoutine());
    }

    private void GetNavMeshAgent()
    {
        if (!gameObject.GetComponent<NavMeshAgent>())
        {
            Debug.LogError("NO NAV MESH AGENT ON ENEMY: " + gameObject.name);
            return;
        }
        navAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitEnemy()
    {
        //perchance write a coroutine that prevents damage for a time
        health -= 1;
        if(health < 1)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FOVRoutine()
    {
        //how often the coroutine runs
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FeildOfViewCheck();
        }
    }

    private void FeildOfViewCheck()
    {
        //this is what actually looks for the player
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        //If anything is in our array it has picked up our player
        if (rangeChecks.Length != 0)
        {
            //the only thing in the targetmask is the player, so we use the first index
            Transform target = rangeChecks[0].transform;
            //establishes direction to enemy rotation to player location
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //gets the angle between the forward direction and the normalized vector to the target and compares it to half the angle we established in the beginning.
            //the angle is halved because half of the angle is to the left and half is to the right
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //starts raycast from center of enemy, toward the player, from the distance to the player, only checking objects in the obstructionMask
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    navAgent.destination = rangeChecks[0].transform.position + (target.position - transform.position).normalized * -targetOffset;
                }
            }
        }
    }

}

using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    NavMeshAgent navAgent;
    [SerializeField] private int health = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetNavMeshAgent();
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


}

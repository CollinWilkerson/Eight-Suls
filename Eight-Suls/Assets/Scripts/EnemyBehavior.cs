using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] private int health = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

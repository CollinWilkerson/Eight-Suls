using System.Collections;
using UnityEngine;

public class MeleeEnemyBehavior : MonoBehaviour
{
    [SerializeField] private string[] attacks;
    [SerializeField] private float attackSpeed = 1f;

    private Animator animator;
    private float stunTime = 0f;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            animator.Play(attacks[Random.Range(0, attacks.Length)]);
            yield return new WaitForSeconds(attackSpeed);
            if(stunTime > 0f)
            {
                yield return new WaitForSeconds(stunTime);
                stunTime = 0f;
            }
        }
    }
    
    public void Stun(float stunTime)
    {
        this.stunTime = stunTime;
    }
}

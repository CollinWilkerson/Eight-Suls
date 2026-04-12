using System.Collections;
using UnityEngine;

public class MeleeEnemyBehavior : MonoBehaviour
{
    [SerializeField] private string[] attacks;
    Animator animator;

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
            yield return new WaitForSeconds(1f);
        }
    }
}

using System.Collections;
using UnityEngine;

public class ParryIndicator : MonoBehaviour
{
    [SerializeField] private MeleeEnemyBehavior attackController;
    [SerializeField] private float stunTime;

    private BoxCollider swordHitbox;

    private void Start()
    {
        swordHitbox = gameObject.GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SwordBehavior>())
        {
            if (collision.gameObject.GetComponent<SwordBehavior>().IsGuarding())
            {
                StartCoroutine(Stun());
                attackController.Stun(stunTime);
            }
        }
    }

    private IEnumerator Stun()
    {
        swordHitbox.enabled = false;
        yield return new WaitForSeconds(stunTime);
        swordHitbox.enabled = true;
    }
}

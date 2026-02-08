using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisionHandler : MonoBehaviour
{
    public UnityEvent PlayerDamaged;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided With: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("EnemyWeapon"))
        {
            PlayerDamaged.Invoke();
        }
    }
}

using UnityEngine;

public class BackupDamageOption : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("I HIT THE PLAYER");
            FindAnyObjectByType<PlayerCollisionHandler>().PlayerDamaged.Invoke();
        }
    }
}

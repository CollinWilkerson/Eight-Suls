using UnityEngine;

public class TeleortKnife : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody rb;
    private bool isPrimed = false;


    public void PrimeOnThrow()
    {
        rb.isKinematic = false;
        isPrimed = true;
        if(FindAnyObjectByType<KnifeSpawner>() != null)
        {
            FindAnyObjectByType<KnifeSpawner>().SpawnKnife();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPrimed)
        {
            player.transform.position = transform.position;
            isPrimed = false;
        }
    }
}

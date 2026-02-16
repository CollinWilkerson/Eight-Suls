using UnityEngine;

public class TeleortKnife : MonoBehaviour
{
    [SerializeField] GameObject player;
    private bool isPrimed = false;


    public void PrimeOnThrow()
    {
        isPrimed = true;
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

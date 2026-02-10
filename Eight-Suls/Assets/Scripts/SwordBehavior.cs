using UnityEngine;

//I feel like this is missing some nuance
//An eaiser way to interpret this might be to interpret the players move as one of 8 possible slashes
//or a stab like in skyward sword
public class SwordBehavior : MonoBehaviour
{
    [SerializeField] string damageType;

    private Vector3 hitLocation;
    private bool hitObject = false;
    private bool delayFrame = false;
    private static DamageDisplayBehavior damageDisplay;

    private void Start()
    {
        if(damageDisplay == null)
        {
            damageDisplay = FindAnyObjectByType<DamageDisplayBehavior>();
        }
    }

    private void Update()
    {
        if(hitObject)
        {
            if (delayFrame)
            {
                damageDisplay.ReportDamage(damageType, Vector3.Distance(hitLocation, transform.position) * Time.deltaTime);
                DamageDisplayBehavior.hitThisFrame = false;
                hitObject = false;
                delayFrame = false;
                return;
            }
            delayFrame = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision with" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!DamageDisplayBehavior.hitThisFrame)
            {
                DamageDisplayBehavior.hitThisFrame = true;
                hitLocation = transform.position;
                hitObject = true;
            }
        }
    }
}

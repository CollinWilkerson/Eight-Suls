using System.Collections;
using UnityEngine;

//I feel like this is missing some nuance
//An eaiser way to interpret this might be to interpret the players move as one of 8 possible slashes
//or a stab like in skyward sword
public class SwordBehavior : MonoBehaviour
{
    [SerializeField] string damageType;
    [SerializeField] float respawnTime = 1f;

    private Vector3 hitLocation;
    private bool hitObject = false;
    private bool delayFrame = false;
    private static DamageDisplayBehavior damageDisplay;
    private bool attackStance = true;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;
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
        if (collision.gameObject.CompareTag("Enemy") && attackStance)
        {
            if(collision.gameObject.GetComponent<EnemyBehavior>() != null)
            {
                collision.gameObject.GetComponent<EnemyBehavior>().HitEnemy();
            }
            if (!DamageDisplayBehavior.hitThisFrame)
            {
                DamageDisplayBehavior.hitThisFrame = true;
                hitLocation = transform.position;
                hitObject = true;
            }
        }
        if (collision.gameObject.CompareTag("EnemyWeapon"))
        {
            if (attackStance)
            {
                DestroySword();
            }
        }
    }

    public void ActivateOnPickup()
    {
        rb.isKinematic = false;
    }

    public void EnterGuardStance()
    {
        attackStance = false;
    }

    public void ExitGuardStance()
    {
        attackStance = true;
    }

    public void DestroySword()
    {
        rb.isKinematic = true;
        StartCoroutine(RespawnSword());
        gameObject.SetActive(false); //replace this with some animation
    }

    public IEnumerator RespawnSword()
    {
        yield return new WaitForSeconds(respawnTime);
        transform.position = startPosition;
        transform.rotation = startRotation;
        gameObject.SetActive(true);
    }
}

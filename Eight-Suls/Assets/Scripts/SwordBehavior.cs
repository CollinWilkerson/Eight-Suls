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
    private bool attackStance = true;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        startPosition = transform.localPosition;
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
        //Debug.Log("Sword Collision with" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy") && attackStance)
        {
            Debug.Log("Hit Enemy!");
            if(collision.gameObject.GetComponent<EnemyBehavior>() != null)
            {
                Debug.Log("Damage Enemy!");
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
            Debug.Log("Hit Enemy Weapon!");
            if (attackStance)
            {
                Debug.Log("Parried!");
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
        Debug.Log("Guarding!");
        attackStance = false;
    }

    public void ExitGuardStance()
    {
        attackStance = true;
    }

    public void DestroySword()
    {
        rb.isKinematic = true;
        FindFirstObjectByType<StanceController>().RespawnSword(this);
        gameObject.SetActive(false); //replace this with some animation
    }

    public void RespawnSword()
    {
        transform.localPosition = startPosition;
        transform.rotation = startRotation;
    }

    public bool IsGuarding()
    {
        return !attackStance;
    }
}

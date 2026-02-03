using UnityEngine;
using TMPro;

public class DamageDisplayBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackTypeText;
    [SerializeField] private TextMeshProUGUI attackDamageText;
    [SerializeField] private TextMeshProUGUI damageRecordText;

    private float recordDamage = 0f;
    public static bool hitThisFrame;

    public void ReportDamage(string damageType, float damageAmount)
    {
        attackTypeText.text = "Attack Type: " + damageType;
        attackDamageText.text = "Attack Damage: " + damageAmount;

        if(damageAmount > recordDamage)
        {
            recordDamage = damageAmount;
            damageRecordText.text = "Record Damage: " + damageAmount;
        }
    }
}

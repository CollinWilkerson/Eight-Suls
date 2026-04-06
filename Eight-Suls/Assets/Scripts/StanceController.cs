using System.Collections;
using UnityEngine;

public class StanceController : MonoBehaviour
{
    [SerializeField] float respawnTime = 1f;

    public void RespawnSword(SwordBehavior sword)
    {
        StartCoroutine(RespawnSwordCoroutine(sword));
    }

    private IEnumerator RespawnSwordCoroutine(SwordBehavior sword)
    {
        yield return new WaitForSeconds(respawnTime);
        sword.gameObject.SetActive(true);
        sword.RespawnSword();
    }
}

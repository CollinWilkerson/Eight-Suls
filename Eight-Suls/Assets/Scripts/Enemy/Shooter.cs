using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] float arrowSpeed = 3;
    [SerializeField] float spawnRate = 0.5f;

    private void Start()
    {
        StartCoroutine(FireArrow());
    }

    private IEnumerator FireArrow()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            SpawnArrow();
        }
    }


    private void SpawnArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = SpawnPoint.position;

        arrow.GetComponent<Rigidbody>().linearVelocity =  (Camera.main.transform.position - arrow.transform.position).normalized * arrowSpeed;
    }
}

using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;

    private void Start()
    {
        SpawnKnife();
    }

    public void SpawnKnife()
    {
        Instantiate(knifePrefab, transform.position, Quaternion.identity, transform);
    }
}

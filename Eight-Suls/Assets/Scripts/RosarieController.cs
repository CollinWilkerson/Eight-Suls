using UnityEngine;
using System.Collections.Generic;

public class RosarieController : MonoBehaviour
{
    [SerializeField] GameObject rosariePrefab;
    [SerializeField] Transform wristTransform;
    [SerializeField] GameObject[] rosaries;
    [SerializeField] float wristSize;

    private int health;
    private Stack<GameObject> rosarieObjects = new Stack<GameObject>();

    private void Start()
    {
        health = rosaries.Length;
        FindAnyObjectByType<PlayerCollisionHandler>().PlayerDamaged.AddListener(TakeDamage);
        foreach(GameObject r in rosaries)
        {
            rosarieObjects.Push(r);
        }
        //SpawnPrefabInCircle(rosariePrefab, wristTransform, health);
    }

    private void SpawnPrefabInCircle(GameObject spawnObj, Transform parentObj, int objectsToSpawn)
    {
        // thank you to this post: https://discussions.unity.com/t/how-to-instantiate-objects-in-a-circle-formation-around-a-point/226980/2
        for (int i = 0; i < objectsToSpawn; i++)
        {
            // angle of object on circles edge
            float radians = 2 * Mathf.PI / objectsToSpawn * i;

            float objX = Mathf.Sin(radians);
            float objY = Mathf.Cos(radians);

            Vector3 spawnPos = new Vector3(objX, objY, 0);
            //pretty sure i need quaternion identity to be something different but since im using spheres it should be okay
            rosarieObjects.Push(Instantiate(spawnObj, (spawnPos * wristSize) + parentObj.position, Quaternion.identity, parentObj));
        }
    }

    private void TakeDamage()
    {
        //may need to use TryPeek
        //could also use Stack.Count to add a red vingette on low health
        if(rosarieObjects.Peek() != null)
        {
            rosarieObjects.Pop().SetActive(false);
            return;
        }
        //if the player has no rosaries, they have no health so they should die
    }

    //probably better to set active and inactive but I am tired
    public void HealDamage(int healAmount)
    {
        int endHealth = rosarieObjects.Count + healAmount;
        if(endHealth > health)
        {
            endHealth = health;
        }

        for(int i = rosarieObjects.Count; i < endHealth; i++)
        {
            rosarieObjects.Push(rosaries[i]);
            rosaries[i].SetActive(true);
        }
    }
}

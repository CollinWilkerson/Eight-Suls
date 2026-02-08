using UnityEngine;

public class ScriptedRotation : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}

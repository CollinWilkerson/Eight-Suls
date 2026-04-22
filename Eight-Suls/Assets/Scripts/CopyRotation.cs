using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    [SerializeField] Transform CopyObject;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = CopyObject.rotation;
    }
}

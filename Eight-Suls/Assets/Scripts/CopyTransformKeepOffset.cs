using UnityEngine;

public class CopyTransformKeepOffset : MonoBehaviour
{
    [SerializeField] private Transform copyObject;
    private Vector3 initialPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position - copyObject.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialPosition + copyObject.position;
    }
}

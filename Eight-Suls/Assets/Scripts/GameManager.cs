using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform leftController;
    [SerializeField] Transform rightController;

    public static GameManager instance;

    private void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public float GetControllerDistance()
    {
        return Vector3.Distance(leftController.position, rightController.position);
    }
}

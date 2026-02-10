using System;
using System.Collections;
using UnityEngine;

public class HealPickupController : MonoBehaviour
{
    [SerializeField] int healAmount = 3;
    [SerializeField] float ripDistance = 0.5f;

    private bool isHeld;

    //I could probably do something with line renederer to make a strechy rope feel that binds to the players hands
    // see: https://github.com/CollinWilkerson/CW_Cubeathon/blob/main/CubeCarrer/Assets/Scripts/GrappleSwing.cs
    public void OnGrab()
    {
        isHeld = true;
        StartCoroutine(RipCheck());
    }

    public void OnRelease()
    {
        isHeld = false;
    }

    private IEnumerator RipCheck()
    {
        while (isHeld)
        {
            Debug.Log(GameManager.instance.GetControllerDistance());
            if (GameManager.instance.GetControllerDistance() > ripDistance)
            {
                FindAnyObjectByType<RosarieController>().HealDamage(healAmount);
                Destroy(gameObject);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

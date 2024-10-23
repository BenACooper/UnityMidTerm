using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //[SerializeField] private MeshRenderer doorRenderer;
    //[SerializeField] private Material defaultDoorMaterial, detectedDoorMaterial;
    [SerializeField] private Animator doorAnimator;

    private bool isLocked = true;

    private float timer = 0;

    private const float waitTime = 1.0f;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!isLocked && other.CompareTag("Player"))
    //    {
    //        //Reset he timer and also change colour of door.
    //        timer = 0;
    //        //doorRenderer.material = detectedDoorMaterial;
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (isLocked)
    //        return;

    //    if (!other.CompareTag("Player"))
    //    {
    //        return;
    //    }

    //    timer += Time.deltaTime;

    //    if (timer >= waitTime)
    //    {
    //        timer = waitTime;
    //        OpenDoor(true);
    //        //doorAnimator.SetBool("isOpen", true);
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        OpenDoor(false);
        //doorAnimator.SetBool("isOpen", false);
        //doorRenderer.material = defaultDoorMaterial;
    }

    public void LockDoor()
    {
        isLocked = true;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    public void OpenDoor(bool doorState)
    {
        if (!isLocked)
        {
            doorAnimator.SetBool("isOpen", doorState);
        }
    }
}

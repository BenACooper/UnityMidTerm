using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private bool isUp;
    [SerializeField] private float speed;

    Vector3 destination;
    bool isMoving;

    public void ToggleLift()
    {
        if (isMoving)
            return;

        if (isUp)
        {
            destination = transform.localPosition - new Vector3(0, moveDistance, 0);
            isUp = false;
        }
        else
        {
            destination = transform.localPosition + new Vector3(0, moveDistance, 0);
            isUp = true;
        }

        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, destination, speed * Time.deltaTime);
        }
        
        if (Vector3.Distance(transform.localPosition, destination) < 0.05f)
        {
            isMoving = false;
        }
    }
}

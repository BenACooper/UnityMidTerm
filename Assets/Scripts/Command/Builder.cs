using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private GameObject objectToBuild;
    [SerializeField] private Transform placementPoint;

    public void Build()
    {
        Debug.Log("Building...");

        // Create a rotation that adds a 180-degree rotation around the Y-axis
        Quaternion backwardRotation = Quaternion.Euler(placementPoint.rotation.eulerAngles.x, placementPoint.rotation.eulerAngles.y + 180, placementPoint.rotation.eulerAngles.z);

        // Instantiate the object with the modified rotation
        Instantiate(objectToBuild, placementPoint.position, backwardRotation);

        Destroy(gameObject);
    }
}

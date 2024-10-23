using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerTurnBehaviour : MonoBehaviour
{
    private PlayerInput input;

    [Header("Player Turn")]
    [SerializeField] private float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * input.mouseX);
    }
}

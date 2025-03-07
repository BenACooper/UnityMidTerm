using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager endingLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (endingLevel)
            {
                endingLevel.EndLevel();
            }
            Destroy(gameObject);
        }
    }
}

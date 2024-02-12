using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectFish : MonoBehaviour
{
    [SerializeField] private PenguinController penguinController;
    [SerializeField] private int fishValue = 1;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            penguinController.IncrementFishCount(fishValue);
            Destroy(gameObject);
        }
    }
}

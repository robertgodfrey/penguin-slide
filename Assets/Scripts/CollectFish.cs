using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectFish : MonoBehaviour
{
    [SerializeField] private PenguinController penguinController;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            penguinController.IncrementFishCount();
            Destroy(gameObject);
        }
    }
}

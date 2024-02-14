using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestinationIceberg : MonoBehaviour
{
    [SerializeField] private PenguinController penguinController;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            penguinController.EndGame(true);
        }
    }
}

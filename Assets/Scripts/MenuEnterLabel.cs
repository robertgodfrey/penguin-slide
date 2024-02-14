using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuEnterLabel : MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.grey;
    [SerializeField] private Color originalColor = Color.black;
    [SerializeField] private float flashSpeed = 1f;
    [SerializeField] private TextMeshProUGUI textComponent;

    void Start()
    {
        StartCoroutine(FlashRoutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }

    IEnumerator FlashRoutine()
    {
        while (true)
        {
            textComponent.color = flashColor;
            yield return new WaitForSeconds(flashSpeed);
            textComponent.color = originalColor;
            yield return new WaitForSeconds(flashSpeed);
        }
    }
}

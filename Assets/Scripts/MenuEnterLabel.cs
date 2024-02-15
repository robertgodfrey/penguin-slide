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
    [SerializeField] private int sceneToGoTo = 1;

    void Start()
    {
        StartCoroutine(FlashRoutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(sceneToGoTo, LoadSceneMode.Single);
        }
        if(Input.GetKey("escape"))
        {
            Application.Quit();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] InputField inputField;

    public void OnEndEdit()
    {
        Storage.instance.storedName = inputField.text;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowRecords()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

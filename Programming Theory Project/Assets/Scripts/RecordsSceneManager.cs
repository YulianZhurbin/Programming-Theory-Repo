using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordsSceneManager : MonoBehaviour
{
    [SerializeField] Text recordText;
    void Start()
    {
        Storage.instance.LoadRecord();
        RecordContainer recordContainer = Storage.instance.RecordContainer;
        if (recordContainer != null)
        {
            recordText.text = $"{recordContainer.name}:  {recordContainer.score}"; 
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

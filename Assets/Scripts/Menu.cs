using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void StartButton()
    {
 //       print("Start button pressed!");
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
//       print("Quit button pressed!");
    #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
    #else
            Application.Quit();
    #endif
    }
}

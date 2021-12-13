using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("West"))
        {
            SceneManager.LoadScene(sceneName: "challenge");
            //    Application.LoadLevel(name: "challenge");
        }

        if (Input.GetButtonDown("East"))
        {
            SceneManager.LoadScene(sceneName: "free");
            //  Application.LoadLevel(name: "free");
        }
    }
}
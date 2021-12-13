using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class START : MonoBehaviour
{
    public Button C;
    public Button F;

    // Start is called before the first frame update
    private void Start()
    {
        C.onClick.AddListener(PlayGame1);
        C.onClick.AddListener(PlayGame2);
    }

    // Update is called once per frame
    public void PlayGame1()
    {
        SceneManager.LoadScene("challenge");
    }

    public void PlayGame2()
    {
        SceneManager.LoadScene("free");
    }
}
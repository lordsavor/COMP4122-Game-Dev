using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sManage : MonoBehaviour
{

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {

    }
    public void Tutorial()
    {

    }
    public void Training()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sManage : MonoBehaviour
{

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Continue()
    {
        SceneManager.LoadScene("Stage_Select");
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

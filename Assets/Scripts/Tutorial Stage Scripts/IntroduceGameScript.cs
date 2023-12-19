using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroduceGameScript : MonoBehaviour
{
    public float textSpeed = 0.03f;
    public string[] texts;
    private string currentText;

    void Start()
    {
        currentText = "";
        StartCoroutine(DisplayTutorialText());
    }

    IEnumerator DisplayTutorialText() {
        for(int i = 0; i < texts.Length; i++) {
            for(int j = 0; j <= texts[i].Length; j++) {
                currentText = texts[i].Substring(0, j);
                this.GetComponent<TMP_Text>().text = currentText;
                yield return new WaitForSeconds(textSpeed);
            }
            yield return new WaitForSeconds(2f);
            this.GetComponent<TMP_Text>().text = "";
        }


    }
}

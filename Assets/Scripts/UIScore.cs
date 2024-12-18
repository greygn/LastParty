using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    private StringBuilder basicText = new StringBuilder("score: ");
    private StringBuilder currentText = new StringBuilder();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentText.Clear();
        currentText.Append(basicText);
        currentText.Append(GameManager.score);
        gameObject.GetComponent<Text>().text = currentText.ToString();
    }
}

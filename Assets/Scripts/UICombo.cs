using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UICombo : MonoBehaviour
{
    private StringBuilder basicText = new StringBuilder("COMBO" + Environment.NewLine + "x");
    private StringBuilder currentText = new StringBuilder();
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentText.Clear();
        currentText.Append(basicText);
        currentText.Append(GameManager.combo);
        gameObject.GetComponent<Text>().text = currentText.ToString();
    }

    static void Appear(Animator anim)
    {
        anim.SetInteger("State", 1);
    }

    void Increase(Animator anim)
    {
        anim.SetInteger("State", 2);
    }

    void Disappear(Animator anim)
    {
        anim.SetInteger("State", 3);
    }
}

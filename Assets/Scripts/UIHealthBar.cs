using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    Vector3 scale = new Vector3(1f, 1f, 1f);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scale = new Vector3((float)System.Math.Abs(GameManager.health) / GameManager.maxHealth, 1f, 1f);
        gameObject.transform.localScale = scale;
    }
}


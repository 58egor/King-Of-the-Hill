using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
    int size;
    void Start()
    {
        text = GetComponent<Text>();
        size = text.fontSize;
        Debug.Log("size" + size);
        text.fontSize = size * (Screen.width / 480);
    }

    // Update is called once per frame
    void Update()
    {
        text = GetComponent<Text>();
        Debug.Log("size" + size);
        text.fontSize = size * (Screen.width / 480);
    }
}

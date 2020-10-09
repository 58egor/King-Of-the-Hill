using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//класс для хранения данных о пользователях
[System.Serializable]
public class Data
{
    public string name;
    public int score;
    public Data()
    {
        score = 0;
        name = "";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public RectTransform prefab;
    public RectTransform content;
    // Start is called before the first frame update
    void Start()
    {
        OnReciveModels();
    }
    //функция заполения таблицы
    void OnReciveModels()
    {
        //получаем данные
        List<Data> data = SaveSystem.LoadData();
        //если данные есть
        if (data != null)
        {

            Debug.Log("data go");
            Debug.Log("data:" + data.Count);
            //то заплняем таблицу данными
            for(int i = 0; i < data.Count; i++)
            {
                var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
                instance.transform.SetParent(content, false);
                TestItemView go = new TestItemView(instance.transform);
                go.name.text = data[i].name;
                go.score.text = data[i].score + "";
                go.number.text = i + 1 + "";
            }
        }
        else
        {
            Debug.Log("Пусто");
        }
    }
    //класс с информацией об эелементах строки 
    public class TestItemView
    {
        public Text number;
        public Text name;
        public Text score;
        public TestItemView(Transform rootView)
        {
            number = rootView.Find("Number").GetComponent<Text>();
            name = rootView.Find("Name").GetComponent<Text>();
            score = rootView.Find("Score").GetComponent<Text>();
        }
    }

}

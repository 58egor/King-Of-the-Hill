using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour
{
    //точки для спавна объектов
    public GameObject[] SpawnPoints;
    //объекты которые падают
    public GameObject[] SpawnObjects;
    //таблица
    public RectTransform obj;
    //время между появлениями объектов
    public float timer = 1f;
    //переменная отсчитывающая повяление объектов
    float time = 0;
    //переменна отвечающая за появление/отключения таблицы
    bool active = true;
    //поле для воода имени игрока
    public InputField field;
    //изобрадения дла пехедоа между сценами
    public GameObject image;
    //скорость затемнения и осветления сцены
    public float fadeSpeed = 1.7f;
    //активна ли кнопка запуска игры
    bool ActiveLoad = false;
    //переменнаю отвечающая за разрешенияотсветления при запуске
    bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        //настраиваем камеру под размер телефона
        float ortSize = 100 / 200f;
        Camera.main.orthographicSize = ortSize;
        //проверяем существует ли файл информации об игроках
        string path = Application.persistentDataPath + "/data.fun";
        if (!File.Exists(path))
        {//если нет то создаем
            FileStream stream = new FileStream(path, FileMode.Create);
            stream.Close();
        }
        //проверяем существует ли файл информации об игроке
        path = Application.persistentDataPath + "/player.fun";
        if (!File.Exists(path))
        {
            //если нет то создаем
            FileStream stream1 = new FileStream(path, FileMode.Create);
            stream1.Close();
        }
        else
        {
            //загружаем последнее использовавшееся имя пользователя
            Data data = SaveSystem.LoadPlayer();
            field.text =data.name;
        }



    }
    
    // Update is called once per frame
    void Update()
    {
        //если время прошло
            if (time < 0)
        {
            //то спавним 1 из  SpawnObjects.Length объектов в одну из позиций
            int i = Random.Range(0, SpawnPoints.Length);
            int j = Random.Range(0, SpawnObjects.Length);
            Instantiate(SpawnObjects[j], SpawnPoints[i].transform.position, Quaternion.identity);
            time = timer;
        }
        else
        {
            time -= Time.deltaTime;
        }
        //если была нажата кнопка играть
        if (ActiveLoad)
        {
            //уменьшаем прозрачность темной кратинки
            Image Alpha = image.GetComponent<Image>();
            if (Alpha.color.a <= 1.0f)
            {
                Alpha.color = new Color(Alpha.color.r, Alpha.color.g, Alpha.color.b, Alpha.color.a + fadeSpeed * Time.deltaTime);
                Debug.Log("color");
            }
            //после чего загружаем сцену
            else
            {
                SceneManager.LoadScene(1);
            }
        }
        //при старте программы увеличиваем прозрачность  темной картинки
        if (!ActiveLoad && start)
        {
            Image Alpha = image.GetComponent<Image>();
            if (Alpha.color.a > 0f)
            {
                Alpha.color = new Color(Alpha.color.r, Alpha.color.g, Alpha.color.b, Alpha.color.a - fadeSpeed * Time.deltaTime);
                Debug.Log("color");
            }
            else
            {
                start = false;
                image.SetActive(false);
            }
        }
    }
    //функция кнопки играть
    public void NewGame()
    {
        //сохраняем имя пользователя
        Data data = new Data();
        if (field.text == "")
        {
            data.name = "Default";
        }
        else data.name = field.text;
        //и разрешаем затемнение экрана
        SaveSystem.SavePlayer(data);
        image.SetActive(true);
        ActiveLoad = true;

        
    }
    //фукнция кнопки показать таблицу лидеров
    public void AddInfo()
    {
        obj.gameObject.SetActive(active) ;
        active = !active;
    }
}

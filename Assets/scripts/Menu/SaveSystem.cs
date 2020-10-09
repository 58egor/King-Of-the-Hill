using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{
    //функция для сохранения информации обо всех игроках
    public static void SaveData(Data data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.fun";
        FileStream stream = new FileStream(path, FileMode.Open);
        List<Data> list = new List<Data>();
        //если данные есть
        if (stream.Length!=0)
        {
            Debug.Log("считываю данные");
            //вытаскиваем их из файла
            list = formatter.Deserialize(stream) as List<Data>;
            Debug.Log(list.Count);
            stream.Close();
            //пересоздаем файл
            stream = new FileStream(path, FileMode.Create);
        }
        //добавляем новые данные
        list.Add(data);
        //записываем
        formatter.Serialize(stream, list);
        stream.Close();
    }
    //функция для сохранения информации об игроке
    public static void SavePlayer(Data data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        //пересоздаем файл
        FileStream stream = new FileStream(path, FileMode.Create);
        //сохраняем информацию
        formatter.Serialize(stream, data);
        stream.Close();
    }
    //функция для загрузки инофрмации обо всех пользователяъ
    public static List<Data> LoadData()
    {
        string path = Application.persistentDataPath + "/data.fun";
        //проверяем существует ли файл
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //проверяем что файл не пустой
            if (stream.Length != 0)
            {
                //загружаем данные
                List<Data> data = formatter.Deserialize(stream) as List<Data>;
                Debug.Log(data.Count);
                stream.Close();
                //вызываем функцию удаления одиннаковых строчек
                data = delete(data);
                //сортируем данные по уменьшению количеству очков
                data = sort(data); 
                return data;
            }
            else
            {
                stream.Close();
                return null;
            }
        }
        else
        {
            Debug.LogError("Save File not found in");
            return null;
        }

    }
    //загружаем данные об пользователе
    public static Data LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();
            Debug.Log(data.name);
            return data;
        }
        else
        {
            Debug.LogError("Save File not found in");
            return null;
        }

    }
    //функция сортировки
    public static List<Data> sort(List<Data> data)
    {
        for (int j = 0; j < data.Count-1; j++)//сортируем полученные значения по убыванию
        {
            for (int i2 = j + 1; i2 < data.Count; i2++)
            {
                if (data[j].score < data[i2].score)
                {
                    Data copy = data[j];
                    data[j] = data[i2];
                    data[i2] = copy;
                }

            }
        }
        return data;
    }
    //функция удаления одинаковых строчек
    public static List<Data> delete(List<Data> data)
    {
        for (int j = 0; j < data.Count-1; j++)//удаляем одинаковые строчки
        {
            for (int i2 = j + 1; i2 < data.Count; i2++)
            {
                if (data[j].score == data[i2].score && data[j].name == data[i2].name)
                {
                    data.RemoveAt(j);
                }

            }
        }
        return data;
    }
}

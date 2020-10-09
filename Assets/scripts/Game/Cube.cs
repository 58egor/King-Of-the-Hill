using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    //имя анимации
    private string animationName = "New Animation";
    //координаты на которые будем сдвигать родительский объект кубика
    private Vector2 delta;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //если было взаимодействие с игроком
        if (other.gameObject.name == "player")
        {
            //получаем количество очков
            int score = Camera.main.GetComponent<control>().chet;
            //выгружаем информацию об игроке для добавления его счета
            Data data = SaveSystem.LoadPlayer();
            //запоминаем кол-во очков
            data.score = score;
            //сохраняем
            SaveSystem.SaveData(data);
            //вызваем функцию проигрыша
            Camera.main.GetComponent<control>().Lose();
        }
    }
    public void Delete()
    {
        //при удалении кубика удаляем его родительский объект
        Destroy(transform.parent.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //если анимация закончилась
        if (!GetComponent<Animation>().IsPlaying(animationName))
        {
            //перетаскиваем родительский объект на место кубика
            delta = transform.position-transform.parent.position;
            delta.y = -0.7f;
            transform.parent.Translate(delta);
            //заново проигрываем анимацию;
            GetComponent<Animation>().Play(animationName);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class control : MonoBehaviour, IBeginDragHandler,IDragHandler
{
    //правильная позиция шара поz
    int numb = 0;
    //позиция шара по y
    float up = 0;
    //позиция шара по х
    float upx = 0.65f;
    //очки
    public int chet = 0;
    //позиции камеры по х и у
    float camY = 0;
    float camX;
    //текст с кол-во очков игрока
    public Text score;
    //на пацзе ли игра
    bool OnPause = false;
    //меню паузы
    public GameObject PauseObject;
    //картнка которую затемняем и наоборот
    public GameObject Dark;
    //родительский объект картинки
    public GameObject DarFather;
    //затемнять или осветлять картинку
    bool DarkActive = true;
    //текст с информацией сколько очков набрал игрок
    public Text PayseText;
    //начало нажатие на экран
    public Vector2 startPos;
    //растояние которое было пройденно пальцем
    public Vector2 direction;
    //палец был отпущен
    public bool directionChosen;
    //прогирали?
    public bool lose = false;
    //возвращаемся в менб или заново?
    public bool home = true;
    //функия вызывающаяся при свайпах
    public void OnBeginDrag(PointerEventData evenData)
    {

        if (sphere.GetComponent<sphere>().OnGround && !DarkActive)
        {
            if (evenData.delta.x > 0)//свайп влево
            {
                //если мы не на краю
                if (numb + 1 < 3)
                {
                    //то вычисляем силу
                    sphere.constraints = RigidbodyConstraints.None;
                    Vector3 pos = sphere.transform.position;
                    //нужная нам позиция= позиция шара с z+1
                    pos.z = numb + 1;
                    //Вычисляем расстояние до точки, куда нам нужно попасть
                    float distance = Vector3.Distance(sphere.transform.position, pos);
                    //Вычисляем направление
                    Vector3 dir = (pos - sphere.transform.position).normalized;
                    //Выставляем координату "у"-угол
                    dir.y = 0.5f;
                    //вычисляем силу
                    var force = Mathf.Sqrt(distance * 10f) * dir * sphere.mass;
                    Debug.Log(force);
                    //применяем силу
                    sphere.AddForce(force, ForceMode.VelocityChange);
                    numb++;
                    //отмечаем что шарик не на земле
                    sphere.GetComponent<sphere>().OnGround = false;
                }
            }
            if (evenData.delta.x < 0)//свайп вправо
            {
                //если мы не на краю
                if (numb - 1 > -3)
                {//то вычисляем силу
                    sphere.constraints = RigidbodyConstraints.None;
                    Vector3 pos = sphere.transform.position;
                    //нужная нам позиция= позиция шара с z-1
                    pos.z = numb - 1;
                    //Вычисляем расстояние до точки, куда нам нужно попасть
                    float distance = Vector3.Distance(sphere.transform.position, pos);
                    //Вычисляем направление
                    Vector3 dir = (pos - sphere.transform.position).normalized;
                    //Выставляем координату "у"-угол
                    dir.y = 0.5f;
                    var force = Mathf.Sqrt(distance * 10f) * dir * sphere.mass;
                    Debug.Log(force);
                    //применяем силу
                    sphere.AddForce(force, ForceMode.Impulse);
                    numb--;
                    //отмечаем что шарик не на земле
                    sphere.GetComponent<sphere>().OnGround = false;
                }
            }
        }
    }
    public void OnDrag(PointerEventData evenData)
    {
        
    }
    public Rigidbody sphere;
    // Start is called before the first frame update
    void Start()
    {
        //настраиваем камеру
        float ortSize = 100 / 200f;
        Camera.main.orthographicSize = ortSize;
        //получааем стартовые координаты камеры
        camX = transform.position.x;
        camY = transform.position.y;

    }
    
    // Update is called once per frame
    void Update()
    {
        //если надо затемнить или осветлить
        if (DarkActive)
        {
            Image Alpha = Dark.GetComponent<Image>();
            //увеличиваем прозрачности картинки
            if (Alpha.color.a >= 0f && !lose)
            {
                Alpha.color = new Color(Alpha.color.r, Alpha.color.g, Alpha.color.b, Alpha.color.a - 1.5f * Time.deltaTime);
                Debug.Log("color");
            }
            else
            {
                if (!lose)
                {
                    DarkActive = false;
                    DarFather.SetActive(false);
                }

            }
            //уменьшаем прозрачность картинки
            if (Alpha.color.a <= 1f && lose)
            {
                DarFather.SetActive(true);
                PauseObject.SetActive(false);
                Alpha.color = new Color(Alpha.color.r, Alpha.color.g, Alpha.color.b, Alpha.color.a + 1.5f * Time.deltaTime);
                Debug.Log("color");
            }
            else
            {
                if (lose && home)
                {//загрузка сцены меню
                    SceneManager.LoadScene(0);
                }
                if (lose && !home)
                {//загрузка сцены игры
                    SceneManager.LoadScene(1);
                }

            }

        }
        //если было касание
        if (Input.touchCount > 0 && sphere.GetComponent<sphere>().OnGround)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
                switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }
        //если палец был был опущен и шар на земле
        if (directionChosen && sphere.GetComponent<sphere>().OnGround && !DarkActive && !OnPause)
        {
            Debug.Log("Dir:"+direction);
            //проверяем что был только касание экрана
            if (direction == new Vector2(0, 0))
            {
                //то вычисляем силу
                sphere.constraints = RigidbodyConstraints.None;
                Vector3 pos = sphere.transform.position;
                //нужная нам позиция= позиция шара -y+0.7 и х+0.7
                pos.y = up + 0.7f;
                pos.x = upx - 0.7f;
                //Вычисляем расстояние до точки, куда нам нужно попасть
                float distance = Vector3.Distance(sphere.transform.position, pos);
                //вычислаяем направление
                Vector3 dir = (pos - sphere.transform.position).normalized;
                //выставляем угол
                dir.y = 1.5f;
                //расчитываем силу
                var force = Mathf.Sqrt(distance * 10f) * dir * sphere.mass;
                Debug.Log(force);
                //устанавливаем смлу
                sphere.AddForce(force, ForceMode.VelocityChange);
                up += 0.7f;
                upx -= 1.4f;
                directionChosen = false;
                sphere.GetComponent<sphere>().OnGround = false;
                chet++;
            }
            else
            {
                directionChosen = false;
                direction = new Vector2(0, 0);
            }
            
        }
        scr();
    }
    //устаналвиаем нужный счет
    void scr()
    {
        score.text = chet+"";
    }
    void FixedUpdate()
    {//изменяем камеру в соответсвии с расположение шарика
        Vector3 vec = sphere.transform.position;
        vec.y += camY;
        vec.x += camX;
        vec.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, vec, 0.25f);
        //если нажали кнопку назад на телефоне товызваем иил закрываем меню
        if (Input.GetKey(KeyCode.Escape))
        {
            if (OnPause)
            {
                Resume();
                OnPause = false;
                directionChosen = false;
            }
            else
            {
                Pause();
                OnPause = true;
                directionChosen = false;
            }
        }

    }
    //функция кнопки паузы
    public void Pause()
    {
        direction = new Vector2(1, 1);
        PauseObject.SetActive(true);
        Time.timeScale = 0f;
        OnPause = true;
    
    }
    //функция отключения паузы
    public void Resume()
    {
        if (!lose)
        {
            PauseObject.SetActive(false);
            Time.timeScale = 1f;
            OnPause = false;
        }
        else
        {
            DarkActive = true;
            home = false;
        }


    }
    //функция за попадание в меню
    public void Home()
    {
        PauseObject.SetActive(false);
        Time.timeScale = 1f;
        OnPause = false;
        DarkActive = true;
        home = true;
        lose = true;

    }
    //функция проигрыша и вызова меню проигрыша
    public void Lose()
    {
        sphere.gameObject.SetActive(false);
        PauseObject.SetActive(true);
        PayseText.text = "Your score:\n" + chet;
        lose = true;
    }
}

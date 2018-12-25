using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    //Снаряд
    public GameObject Burn;
    public float speed = 5;
    public float delta = 5;    //граница перемещения в покое
    public float direction;    //направление персонажа
    Vector3 start;   //начальная позиция персонажа
    Vector3 sc;    //scale персонажа
   
    bool chase;   // погоня
    bool repose = true;   //покой
    bool shoot = true; //стрельба

    RaycastHit2D r;
    public LayerMask mask;

    void Start()
    {
        direction = transform.localScale.x;
        start = transform.position;
    }


    void Update()
    {
        //Возвращение на место после погони
        if (!chase && !repose)
        {
            direction = Mathf.Sign(start.x - transform.position.x) * 1; //разность векторов показывает направление движения
            ChangeScale();
            transform.position = Vector3.MoveTowards(transform.position, start, speed * Time.deltaTime);
            //персонаж вернулся на место
            if (transform.position == start)
            { repose = true; }
        }

        //курсирование в покое
        if (direction > 0 && repose)
        {
            if (transform.position.x > start.x + delta)
            {
                direction = -1 * direction;
                ChangeScale();
            }
            transform.position += Vector3.right * direction * Time.deltaTime * speed;
        }
        else if (direction < 0 && repose)
        {
            if (transform.position.x < start.x - delta)
            {
                direction = -1 * direction;
                ChangeScale();
            }
            transform.position += Vector3.right * direction * Time.deltaTime * speed;
        }


        //луч
        r = Physics2D.Raycast(transform.position, Vector3.right * direction, 10f, mask);
        Debug.DrawRay(transform.position, Vector3.right * direction, Color.red);

        //Проверяем луч раз в секунду, чтобы ограничить частоту стрельбы
        if (shoot)
        {
            if (r.collider != null)
            {
                ToBurn();
            }
        }

    }

    /// <summary>
    /// Смена направления персонажа
    /// </summary>
    private void ChangeScale()
    {
        sc = transform.localScale;
        sc.x = direction;
        transform.localScale = sc;
    }

    //пока персонаж в пределах видимости - погоня
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (!chase)
            {
                chase = true;
                repose = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, coll.gameObject.transform.position, speed/2 * Time.deltaTime);
            direction = Mathf.Sign(coll.gameObject.transform.position.x - transform.position.x) * 1;
            ChangeScale();
        }
    }

    //прекращение погони
    void OnTriggerExit2D(Collider2D coll)
    {
        chase = false;
    }

    /// <summary>
    /// Стрельба
    /// </summary>
    void ToBurn()
    {
        shoot = false;
        GameObject burn = Instantiate(Burn, transform.position, Quaternion.identity);
        burn.GetComponent<Burn>().direction = direction;
        Invoke("Shoot", 1);
    }
    /// <summary>
    /// Стрельба раз в секунду
    /// </summary>
    void Shoot()
    {
        shoot = true;
    }
   
}

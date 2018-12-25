using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// Персонаж
/// </summary>
public class PlayerControl : MonoBehaviour
{
    //для перемещений
    public float speed = 5;
    bool Flip;
    float horizontal;
    float vertical;
    //снаряды
    public GameObject Berry;
    public GameObject Pin_Berry;
    //ожог
    public GameObject burn;
    //пламя
    public GameObject fire;
    //жизнь
    public float HP = 100;
    //сбитые драконы
    public float DeadDragons = 0;
    public float EggeDragons = 0;
    //для прыжка
    Rigidbody2D rb;
    public float forse = 25;
    Vector3 point;    //точка GUI
    public Animator anim;

    TrailRenderer TR;

    bool groundchek;
    private bool winn;

    void Start()
    {
        Flip = gameObject.GetComponent<SpriteRenderer>().flipX;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        TR = GetComponent<TrailRenderer>();
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Moved();
        }

        TR.enabled = true;
        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");

        //Перемещение по горизонтали
        Debug.Log(CrossPlatformInputManager.GetButton("Horizontal"));
        if (CrossPlatformInputManager.GetButton("Horizontal"))
        {

            if (CrossPlatformInputManager.GetAxis("Horizontal") < 0 && !Flip)
            {
                FlipPlayer();
            }
            else if (CrossPlatformInputManager.GetAxis("Horizontal") > 0 && Flip)
            {
                FlipPlayer();
            }
            transform.position += new Vector3(CrossPlatformInputManager.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);

        }

        //Прыжок
        if (CrossPlatformInputManager.GetButton("Jump") && groundchek)
        {
            rb.AddForce(Vector2.up * forse * Time.deltaTime, ForceMode2D.Impulse);
            Invoke("Ground", 0.5f); //задержка ,чтобы успевал срабатывать импульс, иначе не прыгает.
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            BulletInst(Berry);
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            BulletInst(Pin_Berry);
        }
#endif
#if UNITY_EDITOR_WIN
        TR.enabled = false;
        horizontal = Input.GetAxis("Horizontal");

        //Перемещение по горизонтали
        if (Input.GetButton("Horizontal"))
        {

            if (Input.GetAxis("Horizontal") < 0 && !Flip)
            {
                FlipPlayer();
            }
            else if (Input.GetAxis("Horizontal") > 0 && Flip)
            {
                FlipPlayer();
            }
            transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);

        }

        //Прыжок
        if (Input.GetButton("Jump") && groundchek)
        {
            rb.AddForce(Vector2.up * forse * Time.deltaTime, ForceMode2D.Impulse);
            Invoke("Ground", 0.5f); //задержка ,чтобы успевал срабатывать импульс, иначе не прыгает.
        }

        if (Input.GetButtonDown("Fire1"))
        {
            BulletInst(Berry);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            BulletInst(Pin_Berry);
        }
#endif
        //Анимация движения
        if (horizontal != 0)
        {
            anim.SetBool("run", true);
            anim.SetBool("idle", false);
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("idle", true);
        }

        if (!groundchek)
        {
            if (rb.velocity.y > 0.3)
            {
                anim.SetBool("run", false);
                anim.SetBool("idle", false);
                anim.SetBool("down", false);
                anim.SetBool("up", true);
            }
            else if (rb.velocity.y < 0.3)
            {
                anim.SetBool("up", false);
                anim.SetBool("down", true);
            }
        }
        else
        {
            anim.SetBool("down", false);
            anim.SetBool("up", false);
        }

    }

    private void Moved()
    {
        if (Input.GetTouch(0).tapCount == 2)
        {
            BulletInst(Berry);
        }
        if (Input.touchCount <= 0) return;
        Vector2 delta = Input.GetTouch(0).deltaPosition;
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0 && Flip)
            {
                FlipPlayer();
            }
            if(delta.x < 0 && !Flip)
            {
                FlipPlayer();
            }
            transform.position += new Vector3((delta.x) / speed * Time.deltaTime, 0, 0);
        }
        else
        {
            if (delta.y > 0 && groundchek)
            {
                rb.AddForce(Vector2.up * forse * Time.deltaTime, ForceMode2D.Impulse);
                Invoke("Ground", 0.5f); //задержка ,чтобы успевал срабатывать импульс, иначе не прыгает.
            }
            else
            {
                Debug.Log("Down");
            }
        }
    }

    /// <summary>
    /// поворот персонажа
    /// </summary>
    private void FlipPlayer()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        Flip = !Flip;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //HP -= 10;
            if (HP <= 0)
            {
                FirePlayer();
            }
            else
                BurnPlayer();
        }
        if (collision.gameObject.tag == "egge")
        {
            HP += 50;
            EggeDragons += 1;
            Destroy(collision.gameObject);
            if (EggeDragons == 4)
            {
                winn = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "end")
        {
            groundchek = true;
        }
    }
    void Ground()
    { groundchek = false; }

    /// <summary>
    /// Смерть персонажа
    /// </summary>
    public void FirePlayer()
    {
        GameObject fireplayer = Instantiate(fire, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        Destroy(fireplayer, 2f);
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 1);
    }

    /// <summary>
    /// Ожог персонажа
    /// </summary>
    void BurnPlayer()
    {
        //GameObject burnplayer = Instantiate(burn, transform.position, Quaternion.identity);
        //Destroy(burnplayer, 0.5f);

    }

    /// <summary>
    /// Метод создания снарядов
    /// </summary>
    /// <param name="b"></param>
    private void BulletInst(GameObject b)
    {
        Vector3 theScale = transform.localScale;
        Vector3 BerryPos = new Vector3(transform.position.x - 2 * Mathf.Sign(theScale.x), transform.position.y + 1, transform.position.z);
        GameObject bull = Instantiate(b, BerryPos, Quaternion.identity);

        bull.GetComponent<Bullet>().direction = 1 * Mathf.Sign(theScale.x);
    }


    void OnGUI()
    {
        point = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(point.x, Screen.height - point.y - 100, 50, 20), HP.ToString());
        GUI.Box(new Rect(10, 20, 200, 30), "Сбитые драконы " + DeadDragons.ToString());
        GUI.Box(new Rect(10, 70, 200, 30), "Яйца драконов " + EggeDragons.ToString());
        if (winn)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 200, 30), "Урра, ты победил!!!");
            Destroy(gameObject, 2);
        }
    }

}

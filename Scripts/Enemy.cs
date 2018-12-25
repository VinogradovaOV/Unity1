using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Драконы
/// </summary>
public class Enemy : MonoBehaviour
{
    PlayerControl player;
    //взрыв
    public GameObject exp;
    public float direction;
    public float HP = 100; // заготовка

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            player.DeadDragons++;
            HP -= 50;
            Explosion();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Взрыв дракона
    /// </summary>
    void Explosion()
    {
        GameObject exploy = Instantiate(exp, transform.position, Quaternion.identity);
        Destroy(exploy, 0.5f);
    }

}

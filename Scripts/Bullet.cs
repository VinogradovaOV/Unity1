using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Снаряды персонажа
/// </summary>
public class Bullet : MonoBehaviour
{
    public float speed = 10;
    bool up = false;
    float y;
    public float direction = 1;

    void Start()
    {
        up = true;
        y = transform.position.y;
    }

    void Update()
    {
        if (transform.position.y < (y + 1.5) && up)
        {
            transform.position += Vector3.up * speed/2 * Time.deltaTime;
        }
        else
        {
            up = false;
            transform.position += Vector3.up * speed/2 * Time.deltaTime;
            transform.position += Vector3.right * speed * Time.deltaTime * direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "end")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 2);
    }
}

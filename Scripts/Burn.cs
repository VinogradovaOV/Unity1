using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Снаряды драконов
/// </summary>
public class Burn : MonoBehaviour {

    //взрыв
    public GameObject exp;
    Rigidbody2D rb;
    public float direction;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x + 1.84f * direction, transform.position.y - 0.77f, transform.position.z);
        rb.AddForce(Vector2.right * 5 * direction, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag =="end")
        {
            Destroy(gameObject, 2);
        }
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerControl>().HP -= 10;
            if (coll.gameObject.GetComponent<PlayerControl>().HP <= 0)
            {
                coll.gameObject.GetComponent<PlayerControl>().FirePlayer();
            }
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(
                Vector2.left * coll.gameObject.transform.localScale.x * 5 + Vector2.up*5, 
                ForceMode2D.Impulse);
            GameObject exploy = Instantiate(exp, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(exploy, 0.2f);
        }
    }
}

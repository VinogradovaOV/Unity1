using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject Player;
    Vector3 PlayerPos;
    float leftp = 0;
    float rightp = 47;
    float up = -6f;
    float down = -27;
	void Start () {

        PlayerPos = Player.transform.position;
        up = PlayerPos.y;
	}
	void Update () {
        if (Player != null)
        {
            PlayerPos = Player.transform.position;
            if (PlayerPos.x > leftp && PlayerPos.x < rightp)
            {
                transform.position = new Vector3(PlayerPos.x, transform.position.y, transform.position.z);
            }
            if(PlayerPos.y < up && PlayerPos.y > down)
            {
                transform.position = new Vector3(transform.position.x, PlayerPos.y - up, transform.position.z);
            }
        }
		
	}
}

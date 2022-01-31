using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private int touchedGround = 0;
    private bool touchedRam;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Holder")
        {
            if(GameManager.instance != null)
            {
                if (Random.Range(0, 2) > 1)
                {
                    GameManager.instance.PlaySound(3);
                }
                else
                {
                    GameManager.instance.PlaySound(4);
                }
            }
        }
        if (collision.gameObject.tag == "Ram")
        {
            touchedRam = true;
            if (GameManager.instance != null)
            {
                if (Random.Range(0, 2) > 1)
                {
                    GameManager.instance.PlaySound(2);
                }
                else
                {
                    GameManager.instance.PlaySound(5);
                }
            }
        }
        if (collision.gameObject.tag == "Table")
        {
            touchedRam = true;
            if (GameManager.instance != null)
            {
                if (Random.Range(0, 2) > 1)
                {
                    GameManager.instance.PlaySound(2);
                }
                else
                {
                    GameManager.instance.PlaySound(5);
                }
            }
        }
        if (collision.gameObject.tag == "Ground")
        {
            touchedGround++;
            if (touchedGround <= 3)
            {
                if (GameManager.instance != null)
                {
                    if (Random.Range(0, 2) > 1)
                    {
                        GameManager.instance.PlaySound(3);
                    }
                    else
                    {
                        GameManager.instance.PlaySound(4);
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Net")
        {
            if (touchedRam)
            {
                GameManager.instance.IncrementBalls(1);
            }
            else
            {
                GameManager.instance.IncrementBalls(2);
            }
            if(GameManager.instance != null)
            {
                GameManager.instance.PlaySound(1);
            }
        }
    }
}

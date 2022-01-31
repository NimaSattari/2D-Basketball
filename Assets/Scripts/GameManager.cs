using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private AudioSource audio;
    [SerializeField] AudioClip rim_hit1, rim_hit2, bounce1, bounce2, net_sound;
    private BallCreator ballCreator;
    private int index = 0;
    private int balls = 10;

    private void Awake()
    {
        MakeSingleton();
        audio = GetComponent<AudioSource>();
        ballCreator = GetComponent<BallCreator>();
    }

    private void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnLevelWasLoaded()
    {
        if(Application.loadedLevelName == "GamePlay")
        {
            CreateBall();
        }
        GameObject.Find("Ball Text").GetComponent<Text>().text = "" + balls;
    }
    public void IncrementBalls(int increment)
    {
        balls += increment;
        if (balls > 10)
        {
            balls = 10;
        }
        GameObject.Find("Ball Text").GetComponent<Text>().text = "" + balls;
    }
    public void DecrementBalls()
    {
        balls --;
        if (balls > 10)
        {
            balls = 10;
        }
        GameObject.Find("Ball Text").GetComponent<Text>().text = "" + balls;
    }
    public void CreateBall()
    {
        ballCreator.CreateBall(index);
    }
    public void SetBallIndex(int index)
    {
        this.index = index;
    }
    public void PlaySound(int id)
    {
        switch (id)
        {
            case 1:
                audio.PlayOneShot(net_sound);
                break;
            case 2:
                if (Random.Range(0, 2) > 1)
                {
                    audio.PlayOneShot(rim_hit1);
                }
                else
                {
                    audio.PlayOneShot(rim_hit2);
                }
                break;
            case 3:
                if (Random.Range(0, 2) > 1)
                {
                    audio.PlayOneShot(bounce1);
                }
                else
                {
                    audio.PlayOneShot(bounce2);
                }
                break;
            case 4:
                if (Random.Range(0, 2) > 1)
                {
                    audio.PlayOneShot(bounce1, 0.5f);
                }
                else
                {
                    audio.PlayOneShot(bounce2, 0.5f);
                }
                break;
            case 5:
                if (Random.Range(0, 2) > 1)
                {
                    audio.PlayOneShot(rim_hit1, 0.5f);
                }
                else
                {
                    audio.PlayOneShot(rim_hit2, 0.5f);
                }
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator ballAnim;
    [SerializeField] bool ballBool;

    void Start()
    {
        ballAnim = GameObject.Find("BallHolder").GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public void Play()
    {
        Application.LoadLevel("GamePlay");
    }
    public void SelectBall()
    {
        if (!ballBool)
        {
            ballAnim.Play("Fade In");
            ballBool = true;
        }
        else if (ballBool)
        {
            ballAnim.Play("Fade Out");
            ballBool = false;
        }
    }
}

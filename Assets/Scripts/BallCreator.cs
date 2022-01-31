using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreator : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Sprite[] ballImages;
    private float minX = -5f, maxX = 5f, minY = -3f, maxY = 2f;

    public void CreateBall(int index)
    {
        GameObject gameBall = Instantiate(ball, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity) as GameObject;
        gameBall.GetComponent<SpriteRenderer>().sprite = ballImages[index];
    }
}

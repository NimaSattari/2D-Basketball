using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSelect : MonoBehaviour
{
    private List<Button> buttons = new List<Button>();
    private void Awake()
    {
        GetButtonAndAddListeners();
    }
    void GetButtonAndAddListeners()
    {
        GameObject[] btns = GameObject.FindGameObjectsWithTag("MenuBall");
        for (int i = 0; i < btns.Length; i++)
        {
            buttons.Add(btns[i].GetComponent<Button>());
            buttons[i].onClick.AddListener(() => SelectBall());
        }
    }
    public void SelectBall()
    {
        int index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        if(GameManager.instance != null)
        {
            GameManager.instance.SetBallIndex(index);
        }
    }
}

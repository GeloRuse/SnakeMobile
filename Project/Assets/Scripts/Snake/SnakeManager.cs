using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public GameManager gMan; //менеджер игры
    public SnakeMovement snake; //скрипт движения змеи
    public Eat eatScript; //скрипт поедания еды

    public Transform snakeCone; //конус

    public bool stopMoving; //состояние движения

    public int gemCount; //съедено кристаллов подряд

    public Transform border1; //граница уровня 1
    public Transform border2; //граница уровня 2

    private void Start()
    {
        eatScript.sMan = this;
        gMan = GetComponent<GameManager>();
        snake.AssignClamp(Utils.CalcClamp(border1, border2));
    }

    private void FixedUpdate()
    {
        if (snake != null && !stopMoving)
        {
            CalcSnake();
            CalcFever();
        }
    }

    /// <summary>
    /// Расчеты змеи
    /// </summary>
    private void CalcSnake()
    {
        eatScript.EatList();
        snake.Move();
    }

    /// <summary>
    /// Расчет состояния Fever
    /// </summary>
    private void CalcFever()
    {
        if (gemCount >= 3 && !eatScript.eatAll)
        {
            FeverStart();
        }
        if (!snake.CheckFever())
        {
            FeverEnd();
        }
    }

    /// <summary>
    /// Переход в состояние Fever
    /// </summary>
    public void FeverStart()
    {
        eatScript.eatAll = true;
        snakeCone.localScale = new Vector3(transform.localScale.x * 5, transform.localScale.y / 2, transform.localScale.z * 2);
        snake.FeverMode();
        gMan.ShowFever(true);
    }

    /// <summary>
    /// Выход из состояния Fever
    /// </summary>
    public void FeverEnd()
    {
        gemCount = 0;
        eatScript.eatAll = false;
        snakeCone.localScale = Vector3.one;
        snake.EndFeverMode();
        gMan.ShowFever(false);
    }

    /// <summary>
    /// Конец игры
    /// </summary>
    public void GameOver()
    {
        Destroy(snake.transform.root.gameObject);
        GetComponent<GameManager>().ShowGameOverScreen();
    }
}

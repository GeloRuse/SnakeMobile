using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public SnakeManager sMan; //менеджер змеи
    public GameManager gm; //менеджер игры

    private void OnTriggerEnter(Collider other)
    {
        //завершить игру, если достигнут конец уровня
        if (Utils.CompareTag(Utils.playerTag, other.gameObject))
        {
            sMan.stopMoving = true;
            gm.ShowGameOverScreen();
        }
    }
}

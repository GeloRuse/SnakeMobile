using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //если препятствие столкнулось с игроком не в режиме Fever - проигрываем
        if (Utils.CompareTag(Utils.playerTag, other.gameObject))
        {
            Eat eatScritp = other.gameObject.GetComponentInChildren<Eat>();
            if (!eatScritp.eatAll)
                eatScritp.sMan.GameOver();
        }
    }
}

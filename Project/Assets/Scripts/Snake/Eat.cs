using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public SnakeManager sMan; //менеджер змеи
    public GameManager gMan; //менеджер игры

    private Transform target; //место куда движется еда
    public bool eatAll; //состояние Fever
    private float speed = 10f; //скорость поедания еды

    private List<GameObject> foodList = new List<GameObject>(); //список еды, которую нужно съесть

    private void Start()
    {
        //обозначаем целью голову змеи
        target = sMan.snake.bodyParts[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        //определяем можно ли съесть объект, попавший в конус
        if (Utils.CompareLayer(Utils.entityLayer, other.gameObject)
            && (!Utils.CompareTag(Utils.obstacleTag, other.gameObject) || eatAll)
            && !foodList.Contains(other.gameObject))
        {
            foodList.Add(other.gameObject);
        }
    }

    /// <summary>
    /// Метод обработки списка еды
    /// </summary>
    public void EatList()
    {
        for (int i = foodList.Count - 1; i >= 0; i--)
        {
            EatTarget(foodList[i]);
        }
    }

    /// <summary>
    /// Процесс "поедания" пищи
    /// </summary>
    /// <param name="food">еда</param>
    private void EatTarget(GameObject food)
    {
        //двигаем еду к голове змеи
        food.transform.position = Vector3.MoveTowards(food.transform.position, target.position, Time.deltaTime * speed);
        speed *= 2f;
        //достигнув цели, определить тип еды
        if (Vector3.Distance(food.transform.position, target.position) < .5f)
        {
            food.gameObject.SetActive(false);
            foodList.Remove(food);
            //съели кристалл - увеличиваем счетчик
            if (Utils.CompareTag(Utils.gemTag, food.gameObject))
            {
                sMan.gemCount++;
                gMan.UpdateGems();
            }
            //съели еду не того цвета - проигрываем
            else if (!eatAll && food.GetComponent<Renderer>().sharedMaterial != transform.parent.GetComponentInChildren<Renderer>().sharedMaterial)
            {
                sMan.GameOver();
            }
            //съели "правильную" еду - сбрасываем счетчик кристаллов и увеличиваем счетчик еды
            else
            {
                sMan.gemCount = 0;
                gMan.UpdateFood();
            }
            speed = 10f;
            //увеличиваем размер змеи
            sMan.snake.AddBodyPart();
        }
    }
}

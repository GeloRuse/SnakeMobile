using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>(); //тело змеи

    public float minDistance; //минимальное расстояние между частями змеи

    public int startSize; //начальный размер
    public int maxSize; //максимальный размер

    public float speed; //скорость змеи

    public GameObject bodyPrefab; //Prefab части тела змеи
    private Transform curPart; //текущая часть тела змеи
    private Transform prevPart; //предыдущая часть тела змеи

    private InputManager inputManager; //менеджер управления
    private Vector3 targetPos; //вектор направления
    public bool isMoving; //змея движется в сторону нажатия
    private bool inFever; //состояние Fever
    private float feverTime; //текущее время действия Fever
    public float maxFeverTime = 5f; //максимальное время действия Fever

    private float xClamp;

    private void Awake()
    {
        //назначаем менеджер управления
        inputManager = InputManager.Instance;
    }

    /// <summary>
    /// Прием ввода при действующем объекте
    /// </summary>
    private void OnEnable()
    {
        inputManager.OnStartTouch += StartMove;
        inputManager.OnEndTouch += CancelMove;
    }

    /// <summary>
    /// Прекращение приема ввода при "отключенном" объекте
    /// </summary>
    private void OnDisable()
    {
        inputManager.OnStartTouch -= StartMove;
        inputManager.OnEndTouch -= CancelMove;
    }

    /// <summary>
    /// Начало нажатия
    /// </summary>
    private void StartMove()
    {
        isMoving = true;
    }

    /// <summary>
    /// Прекращение нажатия
    /// </summary>
    private void CancelMove()
    {
        isMoving = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //увеличиваем размер змеи до указанного
        for (int i = 0; i < startSize - 1; i++)
        {
            AddBodyPart();
        }
    }

    /// <summary>
    /// Метод расчета положения змеи
    /// </summary>
    public void Move()
    {
        //обновляем направляющий вектор пока есть нажатие
        if (isMoving)
            targetPos = new Vector3(Mathf.Clamp(inputManager.PrimaryPosition().x, -xClamp, xClamp), bodyParts[0].position.y, bodyParts[0].position.z + 1);
        else
            targetPos = bodyParts[0].position + Vector3.forward;
        //перемещение змеи в середину трассы во время состояния Fever
        if (inFever)
            bodyParts[0].position = Vector3.MoveTowards(bodyParts[0].position, new Vector3(0, bodyParts[0].position.y, bodyParts[0].position.z), Time.deltaTime * speed);
        //изменение позиции и поворота головы змеи
        bodyParts[0].position = Vector3.Lerp(bodyParts[0].position, targetPos, Time.deltaTime * speed);
        bodyParts[0].position = new Vector3(Mathf.Clamp(bodyParts[0].position.x, -xClamp, xClamp), bodyParts[0].position.y, bodyParts[0].position.z);
        bodyParts[0].LookAt(targetPos);

        //изменение позиции и поворота тела змеи
        for (int i = 1; i < bodyParts.Count; i++)
        {
            //назначение текущего и предыдущего элемента тела змеи
            curPart = bodyParts[i];
            prevPart = bodyParts[i - 1];
            float dis = Vector3.Distance(prevPart.position, curPart.position);
            //новая позиция текущей части тела змеи
            Vector3 newpos = prevPart.position;
            newpos.y = bodyParts[0].position.y;
            //время задержки перемещения
            float T = Time.deltaTime * dis / minDistance * speed;
            if (T > 0.5f)
                T = 0.5f;
            //изменение позиции и поворота текущей части тела змеи
            curPart.position = Vector3.Slerp(curPart.position, newpos, T);
            curPart.LookAt(prevPart);
        }
    }

    /// <summary>
    /// Увеличение змеи
    /// </summary>
    public void AddBodyPart()
    {
        //увеличиваем размер змеи, если не достигнут максимум
        if (bodyParts.Count < maxSize)
        {
            Transform newpart = (Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation)).transform;
            newpart.GetComponentInParent<Renderer>().sharedMaterial = bodyParts[0].GetComponentInChildren<Renderer>().sharedMaterial;
            newpart.SetParent(transform);
            bodyParts.Add(newpart);
        }
    }

    /// <summary>
    /// Время состояния Fever
    /// </summary>
    /// <returns>состояние Fever</returns>
    public bool CheckFever()
    {
        if (inFever)
            feverTime += Time.deltaTime;
        if (feverTime >= maxFeverTime)
            return false;
        return true;
    }

    /// <summary>
    /// Переход в состояние Fever
    /// </summary>
    public void FeverMode()
    {
        OnDisable();
        isMoving = false;
        inFever = true;
        speed *= 3;
    }

    /// <summary>
    /// Выход из состояния Fever
    /// </summary>
    public void EndFeverMode()
    {
        OnEnable();
        inFever = false;
        speed /= 3;
        feverTime = 0;
    }

    /// <summary>
    /// Назначение ограничения перемещения змеи по оси X
    /// </summary>
    /// <param name="clamp">ограничивающее число</param>
    public void AssignClamp(float clamp)
    {
        xClamp = clamp;
    }
}

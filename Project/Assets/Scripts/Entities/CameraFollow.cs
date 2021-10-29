using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //цель за которой следует камера
    public float offset; //насколько далеко камера от цели
    private Vector3 newPos; //переменная для хранения новой позиции камеры

    private void Start()
    {
        //назначем камеру для корректной работы управления
        InputManager.Instance.mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        //камера следует за змейкой по оси Z
        if (target != null)
        {
            newPos = transform.position;
            newPos.z = target.position.z + offset;
            transform.position = newPos;
        }
    }
}

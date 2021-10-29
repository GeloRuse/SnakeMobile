using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    public delegate void StartTouch(); //начало нажатия
    public event StartTouch OnStartTouch; //событие начала нажатия
    public delegate void EndTouch(); //прекращение нажатия
    public event EndTouch OnEndTouch; //событие прекращения нажатия

    private NewControls playerControls; //управление
    public Camera mainCamera; //основная камера

    private void Awake()
    {
        //назначение управления и камеры
        playerControls = new NewControls();
        mainCamera = Camera.main;
    }

    /// <summary>
    /// Включение управления
    /// </summary>
    private void OnEnable()
    {
        playerControls.Enable();
    }

    /// <summary>
    /// Отключение управления
    /// </summary>
    private void OnDisable()
    {
        playerControls.Disable();
    }

    /// <summary>
    /// Подпись событий управления
    /// </summary>
    private void Start()
    {
        playerControls.Touch.PrimaryContact.started += _ => StartTouchPrimary();
        playerControls.Touch.PrimaryContact.canceled += _ => EndTouchPrimary();
    }

    /// <summary>
    /// Метод запуска события по нажатию
    /// </summary>
    private void StartTouchPrimary()
    {
        if (OnStartTouch != null) OnStartTouch();
    }

    /// <summary>
    /// Метод запуска события по прекращению нажатия
    /// </summary>
    private void EndTouchPrimary()
    {
        if (OnEndTouch != null) OnEndTouch();
    }

    /// <summary>
    /// Метод определения места нажатия
    /// </summary>
    /// <returns>место нажатия в координатах</returns>
    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}

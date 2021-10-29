using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gammeoverScreen; //панель UI завершения уровня
    public GameObject feverText; //панель UI состояния Fever
    public Text gemScore; //текст счетчика кристаллов
    public Text foodScore; //текст счетчика еды

    private int gems; //кол-во кристаллов
    private int food; //кол-во еды

    /// <summary>
    /// Обновление счетчика кристаллов
    /// </summary>
    public void UpdateGems()
    {
        gems++;
        gemScore.text = gems.ToString();
    }

    /// <summary>
    /// Обновление счетчика еды
    /// </summary>
    public void UpdateFood()
    {
        food++;
        foodScore.text = food.ToString();
    }

    /// <summary>
    /// Состояние экрана Fever
    /// </summary>
    /// <param name="show">показать или скрыть</param>
    public void ShowFever(bool show)
    {
        feverText.SetActive(show);
    }

    /// <summary>
    /// Показать экран конца игры
    /// </summary>
    public void ShowGameOverScreen()
    {
        gammeoverScreen.SetActive(true);
    }

    /// <summary>
    /// Перезапуск уровня
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

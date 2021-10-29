using UnityEngine;

public class Utils : MonoBehaviour
{
    public static string playerTag = "Player"; //храним тэг игрока
    public static string obstacleTag = "Obstacle"; //.. препятствия
    public static string foodTag = "Food"; //.. еды
    public static string gemTag = "Gem"; //.. кристаллов
    public static string entityLayer = "Entities"; //храним слой сущностей
    public static string snakeLayer = "Snake"; //.. змеи

    /// <summary>
    /// Метод для конвертации позиции клика на экране
    /// </summary>
    /// <param name="camera">основная камера</param>
    /// <param name="position">место клика</param>
    /// <returns>конвертированное место клика</returns>
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = 10;
        return camera.ScreenToWorldPoint(position);
    }

    /// <summary>
    /// Метод для сравнения тэгов
    /// </summary>
    /// <param name="tag">тэг</param>
    /// <param name="go">объект сравнения</param>
    /// <returns>совпадение тэгов</returns>
    public static bool CompareTag(string tag, GameObject go)
    {
        if (go.tag.Equals(tag))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Метод для сравнения слоев
    /// </summary>
    /// <param name="layer">слой</param>
    /// <param name="go">объект сравнения</param>
    /// <returns>совпадение слоев</returns>
    public static bool CompareLayer(string layer, GameObject go)
    {
        if (go.layer == LayerMask.NameToLayer(layer))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Расчет границ уровня
    /// </summary>
    /// <param name="obj1">граница 1</param>
    /// <param name="obj2">граница 2</param>
    /// <returns>число для ограничения передвижения</returns>
    public static float CalcClamp(Transform obj1, Transform obj2)
    {
        return Vector3.Distance(obj1.position, obj2.position) / 2 - 1.5f;
    }
}

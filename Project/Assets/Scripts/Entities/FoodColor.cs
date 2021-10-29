using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodColor : MonoBehaviour
{
    public Material newColor; //цвет еды

    private void Start()
    {
        //красим еду в указанный цвет
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);
        List<Renderer> chR = new List<Renderer>();
        chR.AddRange(GetComponentsInChildren<Renderer>());
        foreach (Renderer r in chR)
            r.material = newColor;
    }
}

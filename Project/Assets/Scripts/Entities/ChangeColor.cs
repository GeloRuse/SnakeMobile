using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material newColor; //новый цвет змеи

    private void Start()
    {
        //красим ворота в указанный цвет
        GetComponent<Renderer>().material = newColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        //красим змею в указанный цвет
        if (Utils.CompareTag(Utils.playerTag, other.gameObject) || Utils.CompareLayer(Utils.snakeLayer, other.gameObject))
        {
            Renderer rd = other.GetComponent<Renderer>();
            List<Renderer> rds = new List<Renderer>();
            rds.AddRange(other.GetComponentsInChildren<Renderer>());
            if (rd != null)
                rd.material = newColor;
            foreach (Renderer r in rds)
                r.material = newColor;
        }
    }
}

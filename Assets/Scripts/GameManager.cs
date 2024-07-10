using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public TextMeshProUGUI text;
    
    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Labler labler = hit.collider.GetComponent<Labler>();

            if(!labler) { return; }
            text.text = $"Tile Position: ({labler.coords.x}, {labler.coords.y})";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public TextMeshProUGUI text;
    
    void Update()
    {
        DisplayPosition();
    }

    private void DisplayPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Tile labler = hit.collider.GetComponent<Tile>();
            

            if (!labler) { return; }
            text.text = $"Tile Position: ({labler.coords.x}, {labler.coords.y})";
        }
    }
}

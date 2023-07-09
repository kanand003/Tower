using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabel : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.yellow;
    [SerializeField] Color blockedColor = Color.blue;
    [SerializeField] Color exploredColor = Color.green;
    [SerializeField] Color pathColor = Color.white;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        SetLabelColour();
        ToggleLabels();
        label.enabled = true;
    }
    void SetLabelColour()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if(node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor; 
        }
    }
    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.Unitygridsize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.Unitygridsize);
        label.text = coordinates.x + "," + coordinates.y;
    }
    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}

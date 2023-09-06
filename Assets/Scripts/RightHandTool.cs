using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandTool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> tools;
    void Start()
    {
        setCurrentTool(null);
    }
    public void setCurrentTool(string toolName)
    {
        foreach (var tool in tools)
            tool.SetActive(tool.name == toolName);        
    }

}

using System;
using Studiosaurus;
using UnityEngine;
using UnityEngine.UI;

public class SelectableWindow : MonoBehaviour
{
    public Selectable[] Selectables => selectables;
    
    [SerializeField] private Selectable[] selectables;

    private void OnEnable()
    {
        Tab.SetCurrentSelectables(this);
    }
}

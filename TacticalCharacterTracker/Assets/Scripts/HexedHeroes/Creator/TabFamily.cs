using System.Collections.Generic;
using UnityEngine;

public class TabFamily : MonoBehaviour
{
    private List<TabButton> children = new();

    private TabButton activeTab;
    
    public void AddChild(TabButton button, bool isOnAtStart)
    {
        bool isSelected = isOnAtStart && activeTab == null;
        
        if (isSelected)
            activeTab = button;
        
        children.Add(button);
        button.SetIsSelected(isSelected);
    }

    public void ButtonSelected(TabButton selectedButton)
    {
        activeTab = selectedButton;
        foreach (var tabButton in children)
        {
            tabButton.SetIsSelected(tabButton == selectedButton);
        }
    }
}

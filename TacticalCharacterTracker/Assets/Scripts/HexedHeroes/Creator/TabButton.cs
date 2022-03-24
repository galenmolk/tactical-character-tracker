using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TabButton : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTabOpened = new UnityEvent();
    [SerializeField] private UnityEvent OnTabClosed = new UnityEvent();

    [SerializeField] private TabFamily family;
    [SerializeField] private bool isOnAtStart;
    
    private Button button;
    
    public void SetIsSelected(bool isSelected)
    {
        button.interactable = !isSelected;
        
        if (isSelected)
            OnTabOpened.Invoke();
        else
            OnTabClosed.Invoke();
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        family.AddChild(this, isOnAtStart);
    }

    private void OnClick()
    {
        family.ButtonSelected(this);
    }
}

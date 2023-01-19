using Ebla.Models;
using HexedHeroes.EncounterRunner;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class LockedButton : MonoBehaviour
{
    [SerializeField] private UnityEvent onLock; 
    [SerializeField] private UnityEvent onUnlock; 

    private Button button;

    protected abstract bool CanUnlock();

    protected void Lock()
    {
        button.interactable = false;
        onLock?.Invoke();
    }

    protected void Unlock()
    {
        button.interactable = true;
        onUnlock?.Invoke();
    }

    private void Awake()
    {
        button = GetComponent<Button>();

        EncounterRunner.OnDefaultLoaded += HandleDefaultLoaded;
        EncounterRunner.OnEncounterLoaded += HandleEncounterLoaded;
        BaseConfig.OnAnyConfigModified += HandleAnyConfigModified;
    }

    private void OnDestroy()
    {
        EncounterRunner.OnDefaultLoaded -= HandleDefaultLoaded;
        EncounterRunner.OnEncounterLoaded -= HandleEncounterLoaded;
        BaseConfig.OnAnyConfigModified -= HandleAnyConfigModified;
    }

    private void HandleDefaultLoaded()
    {
        Lock();
    }

    private void HandleAnyConfigModified()
    {
        ToggleLock();
    }

    private void HandleEncounterLoaded()
    {
        ToggleLock();
    }

    private void ToggleLock()
    {
        if (CanUnlock())
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }
}

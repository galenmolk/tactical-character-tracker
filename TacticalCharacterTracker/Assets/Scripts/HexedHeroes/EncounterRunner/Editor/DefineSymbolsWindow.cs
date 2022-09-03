using System.Text;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

public class DefineSymbolsWindow : EditorWindow
{
    // Shift+Alt+D
    private const string MENU_HOTKEY = " #&d";
    
    private const string MENU_ITEM_NAME = "Tools/Define Symbols";
    private const string WINDOW_TITLE = "Scripting Define Symbols";
    private const string CANCEL_BUTTON = "Cancel";
    private const string APPLY_BUTTON = "Apply";
    private const float WIDTH = 250f;
    private const float TOGGLE_HEIGHT = 25f;
    private const float TOGGLE_SPACING = 2f;
    private const char SYMBOL_DELIMITER = ';';
    private const float MIN_HEIGHT = 50f;
    
    private readonly StringBuilder checkedSymbolsString = new();
    
    private static DefineSymbolsWindow instance;
    private static BuildTargetGroup CurrentTargetGroup => EditorUserBuildSettings.selectedBuildTargetGroup;
    private static string CurrentSymbolsString => PlayerSettings.GetScriptingDefineSymbolsForGroup(CurrentTargetGroup);

    private bool[] checkedSymbols;
    private int totalSymbols;
    
    [MenuItem(MENU_ITEM_NAME + MENU_HOTKEY)]
    public static void OpenDefineSymbolsWindow()
    {
        instance = GetWindow(typeof(DefineSymbolsWindow), true, WINDOW_TITLE, true) as DefineSymbolsWindow;

        if (instance == null)
        {
            Debug.LogError($"{nameof(DefineSymbolsWindow)}: Could not find or create window.");
            return;
        }
        
        instance.Initialize();
        instance.Show();
    }

    private void Initialize()
    {
        totalSymbols = DefineSymbols.All.Count;
        checkedSymbols = new bool[totalSymbols];
        SetWindowSize();
        UpdateSelection();
    }

    private void SetWindowSize()
    {
        maxSize = new Vector2(WIDTH, Mathf.Max(totalSymbols * TOGGLE_HEIGHT, MIN_HEIGHT));
        minSize = maxSize;
    }
    
    private void UpdateSelection()
    {
        string currentSymbols = CurrentSymbolsString;
        for (int i = 0; i < totalSymbols; i++)
        {
            checkedSymbols[i] = currentSymbols.Contains(DefineSymbols.All[i]);
        }
    }
    
    private void OnGUI()
    {
        if (instance == null)
        {
            OpenDefineSymbolsWindow();
        }
        
        DrawSymbolToggles();
        
        GUILayout.FlexibleSpace();
        
        DrawButtons();
    }

    private void DrawSymbolToggles()
    {
        GUIStyle toggleStyle = new(GUI.skin.toggle) { fixedWidth = WIDTH };

        for (int i = 0; i < totalSymbols; i++)
        {
            checkedSymbols[i] = GUILayout.Toggle(checkedSymbols[i], DefineSymbols.All[i], toggleStyle, GUILayout.Width(WIDTH));
            GUILayout.Space(TOGGLE_SPACING);
        }
    }

    private void DrawButtons()
    {
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button(CANCEL_BUTTON))
        {
            Close();
        }

        if (!CanApplySymbols())
        {
            GUI.enabled = false;
        }

        if (GUILayout.Button(APPLY_BUTTON))
        {
            Apply();
        }

        if (!GUI.enabled)
        {
            GUI.enabled = true;
        }

        EditorGUILayout.EndHorizontal();
    }

    private bool CanApplySymbols()
    {
        // Don't try to apply new symbols if Editor is compiling.
        if (EditorApplication.isCompiling)
        {
            return false;
        }

        string appliedSymbolsString = CurrentSymbolsString;
        
        for (int i = 0, count = DefineSymbols.All.Count; i < count; i++)
        {
            bool isChecked = checkedSymbols[i];
            bool isApplied = appliedSymbolsString.Contains(DefineSymbols.All[i]);
            
            if (isChecked != isApplied)
            {
                return true;
            }
        }

        return false;
    }
    
    private void Apply()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(CurrentTargetGroup, GetCheckedSymbolsString());
        CompilationPipeline.RequestScriptCompilation();
    }

    private string GetCheckedSymbolsString()
    {
        checkedSymbolsString.Clear();
        for (int i = 0, count = DefineSymbols.All.Count; i < count; i++)
        {
            if (checkedSymbols[i])
            {
                checkedSymbolsString.Append(DefineSymbols.All[i]).Append(SYMBOL_DELIMITER);
            }
        }

        return checkedSymbolsString.ToString();
    }
}

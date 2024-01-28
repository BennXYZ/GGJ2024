using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance;

    public GlobalGameSettings GameSettings { get; private set; }

    public DebugUtils DebugUtils { get; private set; }
    public LevelRoot CurrentLevel { get; internal set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        instance = new GameObject().AddComponent<GameManager>();
        instance.gameObject.name = "GameManager";
        DontDestroyOnLoad(instance);
        instance.DebugUtils = Resources.Load<DebugUtils>("DebugUtils");
        Debug.Assert(instance.DebugUtils, @"No prefab named ""DebugUtils"" found in the Resources folder.");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        instance.GameSettings = Resources.Load<GlobalGameSettings>("GameSettings");
        Debug.Assert(instance.GameSettings, @"No prefab named ""GameSettings"" found in the Resources folder.");
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {

    }
}

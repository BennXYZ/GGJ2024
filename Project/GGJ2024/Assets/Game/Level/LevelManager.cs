using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    static int currentLevel = 1;
    const int maximalLevel = 1;

    public static void LoadLevel(int id)
    {
        currentLevel = Mathf.Clamp(id, 0, maximalLevel);
        SceneManager.LoadScene(id);
    }

    public static void ReloadLevel()
    {
        LoadLevel(currentLevel);
    }

    public static void LoadNextLevel()
    {
        LoadLevel(currentLevel + 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathEndCaller : MonoBehaviour
{
    public void ReloadLevel()
    {
        LevelManager.ReloadLevel();
    }
}

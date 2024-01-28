using UnityEngine;

public class WinTrigger : LevelObject
{
    [SerializeField]
    bool goToFirstScene = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (goToFirstScene)
                LevelManager.LoadLevel(0);
            else
                LevelManager.LoadNextLevel();
        }
    }
}

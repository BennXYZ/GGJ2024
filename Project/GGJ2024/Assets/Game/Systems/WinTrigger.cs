using UnityEngine;

public class WinTrigger : LevelObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.LoadNextLevel();
        }
    }
}

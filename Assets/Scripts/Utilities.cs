using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public static class Utilities
{
    public static int playerDeaths = 0;

    public static string UpdateDeathCount(ref int counReference)
    {
        counReference += 1;
        return $"Next time you'll be at number {counReference}.";
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Debug.Log($"Player deaths: {playerDeaths}");
        string message = UpdateDeathCount(ref playerDeaths);
        Debug.Log($"Player deaths: {playerDeaths}");
    }

    public static bool RestartLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1f;
        return true;
    }
}

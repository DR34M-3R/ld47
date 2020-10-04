using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldRebootAction : TimerAction
{
    protected override void Run()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
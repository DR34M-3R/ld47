using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldRebootAction : TimerAction
{
    public override void Run()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
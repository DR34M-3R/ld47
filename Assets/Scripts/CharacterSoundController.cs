using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    public static CharacterSoundController Instance;

    public AudioSource Source;
    public List<AudioClip> StepClips = new List<AudioClip>();
    public AudioClip ActionClip;
    public AudioClip GunShotClip;
    public bool StepBool;
    public bool TakeBool;
    public bool GunShotBool;
    public GroundType groundType;

    private void Awake() {
        Instance = this;
        Source = GetComponent<AudioSource>();
    }

    public void Update() {
        if(StepBool) Step();
        if(TakeBool) Take();
        if(GunShotBool) GunShot();
    }

    public void Step() {

        int id = Random.Range(0,StepClips.Count);
        Source.PlayOneShot(StepClips[id]);
        StepBool = false;
    }

    public void Take() {
        Source.PlayOneShot(ActionClip);
        TakeBool = false;
    }

    public void GunShot() {
        Source.PlayOneShot(GunShotClip);
        GunShotBool = false;
    }
}

public enum GroundType {
    grass,
    wood
}

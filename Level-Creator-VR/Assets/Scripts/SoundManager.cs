using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource UIAudio;
    [SerializeField]
    AudioSource UndoAudio;
    [SerializeField]
    AudioSource RedoAudio;
    [SerializeField]
    AudioSource PlaceAudio;
    [SerializeField]
    AudioSource EraseAudio;
    [SerializeField]
    AudioSource SaveAudio;

    void ButtonUI()
    {
        UIAudio.Play();
    }
    void Undo()
    {
        UndoAudio.Play();
    }
    void Redo()
    {
        RedoAudio.Play();
    }
    void Place()
    {
        PlaceAudio.Play();
    }
    void Erase()
    {
        EraseAudio.Play();
    }
    void Save()
    {
        SaveAudio.Play();
    }
}

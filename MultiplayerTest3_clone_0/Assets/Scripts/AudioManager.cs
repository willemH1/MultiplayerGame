using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource fireAudio;
    [SerializeField]
    private AudioSource Grampophone;
    public void playGramophone()
    {
        if(Grampophone.isPlaying == true)
        {
            Grampophone.Stop();
        }
        else
        {
            Grampophone.Play();
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        fireAudio.volume = 0f;
    }
    private void OnTriggerExit(Collider other)
    {
        fireAudio.volume = 1f;
    }
}

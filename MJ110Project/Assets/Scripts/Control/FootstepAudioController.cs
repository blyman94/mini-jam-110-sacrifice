using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private Mover3D mover3D;

    private void Update()
    {
        if (mover3D.MoveInput.magnitude > 0 && !footstepSource.isPlaying)
        {
            footstepSource.Play();
        }
        
        if(mover3D.MoveInput.magnitude <= 0 && footstepSource.isPlaying)
        {
            footstepSource.Stop();
        }
    }


}

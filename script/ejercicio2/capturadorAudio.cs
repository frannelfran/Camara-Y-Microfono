using UnityEngine;

public class CapturadorAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private string micDevice;
    private bool isRecording = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        micDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;

        if (micDevice == null)
        {
            Debug.LogWarning("No microphone detected.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && micDevice != null)
        {
            if (!isRecording)
            {
                audioSource.clip = Microphone.Start(micDevice, false, 10, 44100);
                audioSource.loop = false;
                isRecording = true;
                Debug.Log("Recording started...");
            }
            else
            {
                Microphone.End(micDevice);
                audioSource.Play();
                isRecording = false;
                Debug.Log("Playback started...");
            }
        }
    }
}
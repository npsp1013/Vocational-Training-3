using UnityEngine;
using System.Collections;
public class MicrophoneCapture : MonoBehaviour
{
    //Record to audio clip using microphone
    //Reference: https://docs.unity3d.com/ScriptReference/Microphone.html
    private byte[] speech;
    private bool recording;
    private AudioSource audioSource;

    void Start()
    {
        if (Microphone.devices.Length <= 0)
        {
            Debug.Log("Microphone not detected");
            return;
        }

        audioSource = this.GetComponent<AudioSource>();
    }

    public void StartCapture()
    {
        if (recording == false)
        {
            audioSource.clip = Microphone.Start(null, true, 20, 24000);
            recording = true;
        }
    }

    public void StopCapture()
    {
        if (recording == false) return;

        Microphone.End(null);
        recording = false;
        speech = WavUtility.FromAudioClip(audioSource.clip); //Audio clip to byte[]
        if (speech == null) return;
        StartCoroutine(this.GetComponent<Dialogflow>().Request(speech)); //Dialogflow Request

    }

    public IEnumerator StartCaptureAfterTime(float startTime, float Endtime)
    {
        yield return new WaitForSeconds(startTime);

        StartCapture();
        StartCoroutine(StopCaptureAfterTime(Endtime));
    }

    public IEnumerator StopCaptureAfterTime(float Endtime)
    {
        yield return new WaitForSeconds(Endtime);

        StopCapture();
    }
}
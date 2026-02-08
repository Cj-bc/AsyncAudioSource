using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class AsyncAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource _as;
    private int current = -1;

    public async UniTask PlayAsync(AudioClip clip, CancellationToken cancellationToken)
    {
        int id = ++current;
        try
        {
            current = id;
            _as.clip = clip;
            _as.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), cancellationToken: cancellationToken);
        } finally
        {
            if (current == id)
            {
                _as.Stop();
                current = -1;
            }
        }
    }
}
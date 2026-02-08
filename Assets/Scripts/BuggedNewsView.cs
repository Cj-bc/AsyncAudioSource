using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AsyncAudioSource.Examples.Bugged
{
    public class BuggedNewsView : MonoBehaviour
    {
        public record News()
        {
            public string Contents;
            public AudioClip Narration;
        }

        [SerializeField] private AudioSource audioSource;

        public async UniTask Render(News news, CancellationToken cancellationToken)
        {
            try
            {
                audioSource.clip = news.Narration;
                audioSource.Play();
                await UniTask.Delay(TimeSpan.FromSeconds(news.Narration.length), cancellationToken: cancellationToken);
            }
            finally
            {
                audioSource.Stop();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Devdog.General
{
    /// <summary>
    /// The AudioManager class is a singleton responsible for managing and playing multiple audio clips, especially within Unity GUI.
    /// It organizes handling of audio clips, audio mixer group, as well as audio sources.
    /// The class provides facilities for enabling/disabling audio, adding new audio sources, and playing short-lived audio clips.
    /// </summary>
    /// <remarks>
    /// This class handles an internally queued set of audio clips, which are populated on-demand. It also maintains a pool of audio sources which are created, reserved and reused as necessary.
    /// You can configure the initial reserve of audio sources via the 'reserveAudioSources' property.
    ///
    /// The AudioManager is designed to be performance-cognizant, hence it utilizes pooling for its audio sources and queues audio clips instead of playing them immediately to reduce performance overhead.
    ///
    /// However, it is noted that the 'AudioPlayOneShot' method, which allows for impromptu playing of audio clips, does not utilize pooling and hence may not be ideal for performance stringent scenarios.
    /// </remarks>
    /// <example>
    /// The following is an example of how to use the AudioManager:
    /// <code>
    /// // Assuming we have an instance of AudioClipInfo:
    /// AudioClipInfo clipInfo = ...;
    ///
    /// AudioManager.AudioPlayOneShot(clipInfo);
    /// </code>
    /// </example>
    public class AudioManager : ManagerBase<AudioManager>
    {
        private static List<AudioSource> _audioSources;
        private static GameObject _audioSourceGameObject;

        private static Queue<AudioClipInfo> _audioQueue = new();

        [Header("Settings")]
        public int reserveAudioSources = 8;

        public AudioMixerGroup audioMixerGroup;

        protected override void Awake()
        {
            base.Awake();

            StartCoroutine(WaitFramesAndEnable(5));
            enabled = false; // Set to enabled at start, initialize, then enable (to avoid playing sound during initialization)

            _audioQueue = new Queue<AudioClipInfo>(reserveAudioSources);
            CreateAudioSourcePool();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected void Update()
        {
            if (_audioQueue.Count > 0)
            {
                var source = GetNextAudioSource();
                var clip = _audioQueue.Dequeue();

                source.Play(clip);
            }
        }

        // Empty method to show enable / disable icons in Unity inspector.
        private void OnEnable()
        {
        }

        private IEnumerator WaitFramesAndEnable(int frames)
        {
            for (var i = 0; i < frames; i++) yield return null;

            enabled = true;
        }

        private void CreateAudioSourcePool()
        {
            _audioSources = new List<AudioSource>(reserveAudioSources);

            _audioSourceGameObject = new GameObject("_AudioSources");
            _audioSourceGameObject.transform.SetParent(transform);
            _audioSourceGameObject.transform.localPosition = Vector3.zero;

            for (var i = 0; i < _audioSources.Count; i++) _audioSources.Add(CreateNewAudioSource());
        }

        private static AudioSource CreateNewAudioSource()
        {
            var src = _audioSourceGameObject.AddComponent<AudioSource>();
            src.outputAudioMixerGroup = instance.audioMixerGroup;
            return src;
        }

        private static AudioSource GetNextAudioSource()
        {
            foreach (var audioSource in _audioSources)
                if (audioSource.isPlaying == false)
                    return audioSource;

            DevdogLogger.LogWarning(
                "All sources taken, creating new on the fly... Consider increasing reserved audio sources");

            var src = CreateNewAudioSource();
            _audioSources.Add(src);
            return src;
        }


        /// <summary>
        ///     Plays an audio clip, only use this for the UI, it is not pooled so performance isn't superb.
        /// </summary>
        public static void AudioPlayOneShot(AudioClipInfo clip)
        {
            if (clip == null || clip.audioClip == null) return;

            if (instance == null)
                DevdogLogger.LogWarning("AudioManager not found, yet trying to play an audio clip....");

            if (_audioQueue.Any(o => o.audioClip == clip.audioClip) == false) _audioQueue.Enqueue(clip);
        }
    }
}
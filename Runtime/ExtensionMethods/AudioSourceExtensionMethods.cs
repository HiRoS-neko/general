using UnityEngine;

namespace Devdog.General
{
    public static class AudioSourceExtensionMethods
    {
        /// <summary>
        /// Extension method for the AudioSource class that allows playing an AudioClip with specific options defined by AudioClipInfo instance.
        /// </summary>
        /// <param name="source">The AudioSource instance on which the method is invoked. This instance will be used to play the AudioClip.</param>
        /// <param name="info">An AudioClipInfo instance containing the volume, pitch, loop settings, and AudioClip reference to be used when playing the AudioClip.</param>
        /// <remarks>
        /// This method modifies the volume, pitch, loop and AudioClip settings of the AudioSource before playing the AudioClip.
        /// The settings are taken from the AudioClipInfo instance provided as an argument.
        /// After reconfiguration, the AudioSource plays the AudioClip.
        /// </remarks>
        public static void Play(this AudioSource source, AudioClipInfo info)
        {
            source.volume = info.volume;
            source.pitch = info.pitch;
            source.loop = info.loop;
            source.clip = info.audioClip;
            source.Play();
        }

        /// <summary>
        /// An extension method for the `AudioSource` class. It sets the `AudioSource` properties based on the input `LocalizedAudioClipInfo`
        /// and plays the corresponding audio clip.
        /// </summary>
        /// <param name="source">The `AudioSource` that the method is extending. It is where the audio will be played from.</param>
        /// <param name="info">A `LocalizedAudioClipInfo` object that contains the details of the audio clip to be played.</param>
        /// <remarks>
        /// The method modifies the `volume`, `pitch`, `loop`, and `clip` properties of the `AudioSource` before playing the sound.
        /// Please note that the `audioClip.val` is the actual `AudioClip` from the `LocalizedAudioClipInfo` to be played.
        /// </remarks>
        public static void Play(this AudioSource source, LocalizedAudioClipInfo info)
        {
            source.volume = info.volume;
            source.pitch = info.pitch;
            source.loop = info.loop;
            source.clip = info.audioClip.val;
            source.Play();
        }
    }
}
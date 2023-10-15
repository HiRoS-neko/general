using UnityEngine;

namespace Devdog.General
{
    public static class AnimatorExtensionMethods
    {
        /// <summary>
        /// This extension method is used to play a specific animation based on the given MotionInfo.
        /// </summary>
        /// <param name="animator">The Animator instance on which this method is called.</param>
        /// <param name="info">The MotionInfo object that contains the motion to be played, its speed, a bool indicating whether to crossfade, and the speed of the crossfade.</param>
        /// <remarks>
        /// If the "motion" in the passed "info" is null, the method will return immediately without playing any animation.
        /// The speed of the animator gets updated based on the "speed" from the passed "info".
        /// If the "crossFade" in the "info" is set to true, it will cause the animator to crossfade to the given animation with the speed as specified by the "crossFadeSpeed" in the "info".
        /// If "crossFade" is false, it simply plays the given animation immediately without any crossfading.
        /// </remarks>
        public static void Play(this Animator animator, MotionInfo info)
        {
            if (info.motion == null) return;

            animator.speed = info.speed;
            if (info.crossFade)
                animator.CrossFade(info.motion.name, info.crossFadeSpeed);
            else
                animator.Play(info.motion.name);
        }
    }
}
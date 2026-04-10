using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.General.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {

        // Tracks sound effect instances created so they can be paused, unpaused, and/or disposed.
        private List<SoundEffectInstance> _activeSoundEffectInstances = new();

        private bool IsMuted { get; set; }

        private float _Volume_Master;
        public float Volume_Master
        {
            get
            {
                if (IsMuted)
                {
                    return 0.0f;
                }
                else
                {
                    return _Volume_Master;
                }
            }
            set => _Volume_Master = Math.Clamp(value, 0.0f, 1.0f);
        }

        private float _Volume_Music;
        public float Volume_Music
        {
            get
            {
                if (IsMuted)
                {
                    return 0.0f;
                }
                else
                {
                    return Volume_Master * _Volume_Music;
                }
            }
            set
            {
                _Volume_Music = Math.Clamp(value, 0.0f, 1.0f);
                MediaPlayer.Volume = Volume_Music;
            }
        }

        private float _Volume_SFX;
        public float Volume_SFX
        {
            get
            {
                if (IsMuted)
                {
                    return 0.0f;
                }
                else
                {
                    return Volume_Master * _Volume_SFX;
                }
            }
            set => _Volume_SFX = Math.Clamp(value, 0.0f, 1.0f);
        }

        public AudioManager()
        {
            MediaPlayer.Volume = Volume_Music;
        }

        // Finalizer called when object is collected by the garbage collector.
        ~AudioManager() => Dispose(false);

        public void ToggleMute()
        {
            IsMuted = !IsMuted;
            MediaPlayer.Volume = Volume_Music;
        }


        public void Update()
        {
            for (int i = _activeSoundEffectInstances.Count - 1; i >= 0; i--)
            {
                SoundEffectInstance instance = _activeSoundEffectInstances[i];

                if (instance.State == SoundState.Stopped)
                {
                    if (!instance.IsDisposed)
                    {
                        instance.Dispose();
                    }
                    _activeSoundEffectInstances.RemoveAt(i);
                }
            }
        }

        public void PlaySoundEffect(SoundEffect soundEffect, float pitch, float pan, bool isLooped)
        {
            // Create an instance from the sound effect given.
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();

            // Apply the volume, pitch, pan, and loop values specified.
            soundEffectInstance.Volume = Volume_SFX;
            soundEffectInstance.Pitch = pitch;
            soundEffectInstance.Pan = pan;
            soundEffectInstance.IsLooped = isLooped;

            // Tell the instance to play
            soundEffectInstance.Play();

            // Add it to the active instances for tracking
            _activeSoundEffectInstances.Add(soundEffectInstance);

            return;
        }



        public void PlaySong(Song song, bool isRepeating = true)
        {
            // Check if the media player is already playing, if so, stop it.
            // If we do not stop it, this could cause issues on some platforms
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }

            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = isRepeating;


        }

        /// <summary>
        /// Pauses all audio.
        /// </summary>
        public void PauseAudio()
        {
            // Pause any active songs playing.
            MediaPlayer.Pause();

            // Pause any active sound effects.
            foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
            {
                soundEffectInstance.Pause();
            }
        }

        /// <summary>
        /// Resumes play of all previous paused audio.
        /// </summary>
        public void ResumeAudio()
        {
            // Resume paused music
            MediaPlayer.Resume();

            // Resume any active sound effects.
            foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
            {
                soundEffectInstance.Resume();
            }
        }

        /// <summary>
        /// Mutes all audio.
        /// </summary>
        public void MuteAudio()
        {
            // Set all volumes to 0
            MediaPlayer.Volume = 0.0f;
            SoundEffect.MasterVolume = 0.0f;

            IsMuted = true;
        }

        /// <summary>
        /// Unmutes all audio to the volume level prior to muting.
        /// </summary>
        public void UnmuteAudio()
        {
            IsMuted = false;
            // Restore the previous volume values.
            MediaPlayer.Volume = Volume_Music;
        }

       

    }
}

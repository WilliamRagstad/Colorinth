using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Colorinth.Managers
{
    public static class SoundEffectManager
    {
        #region Sounds

        private static SoundEffect _walkSoundEffect;

        #endregion
        public static void Initialize(ContentManager content)
        {
            _walkSoundEffect = content.Load<SoundEffect>("walk");
        }

        public static void Play(SoundEffect sound)
        {
            sound.Play(1, 0, 0);
        }

        public static void Walk() => Play(_walkSoundEffect);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Colorinth.Managers
{
    public static class SoundEffectManager
    {
        public static void Play(SoundEffect sound)
        {
            sound.Play(1, 0, 0);
        }
    }
}

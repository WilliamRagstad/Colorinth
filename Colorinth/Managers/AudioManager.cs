using System;
using System.Collections.Generic;
using System.Text;
using Colorinth.Model;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Colorinth.Managers
{
    public static class AudioManager
    {
        public static void Play(Song song)
        {
            MediaPlayer.Play(song);
        }

        public static void Stop()
        {
            MediaPlayer.Stop();
        }

        public static LoopingSound PlayLoop(Song song)
        {
            LoopingSound ls = new LoopingSound(song);
            ls.Start();
            return ls;
        }
    }
}

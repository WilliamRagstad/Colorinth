using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Colorinth.Managers;
using Microsoft.Xna.Framework.Media;

namespace Colorinth.Model
{
    public class LoopingSound
    {
        private readonly Song _song;
        private Task _loop;
        private bool _play;

        public LoopingSound(Song song)
        {
            _song = song;
        }

        public void Start()
        {
            _play = true;
            AudioManager.Play(_song);
            _loop = new Task(() =>
            {
                Thread.Sleep(_song.Duration);
                if (_play) Start();
            });
            _loop.Start();
        }

        public void Stop()
        {
            _play = false;
            AudioManager.Stop();
        }
    }
}

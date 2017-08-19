using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giest_ario_platformer.Managers
{
    class MusicManager
    {

        public static MusicManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MusicManager();
                return instance;
            }
        }

        private static MusicManager instance;

        private Song songPlaying;
        private String currentSong;
        private bool isPaused;

        public MusicManager()
        {
            isPaused = false;    
        }

        public void PlaySong(string _song)
        {
            if (currentSong != _song)
            {
                if (songPlaying != null)
                {
                    MediaPlayer.Stop();
                }
                if (_song != "")
                {

                    Song song = GameManager.Instance.Content.Load<Song>($@"Music\{_song}");
                    currentSong = _song;
                    MediaPlayer.Play(song);
                    MediaPlayer.IsRepeating = true;
                    songPlaying = song;
                }
                else
                {
                    songPlaying = null;
                    currentSong = _song;
                }
                
            }
        }

        public void Pause()
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                MediaPlayer.Pause();
            }
            else
            {
                MediaPlayer.Resume();
            }
            
        }
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhancements.Misc
{
    internal class FilePathSongAudioClipProvider : IFilePathSongAudioClipProvider
    {
        private string _songAudioClipPath;
        public string songAudioClipPath => _songAudioClipPath;

        public FilePathSongAudioClipProvider(string songAudioClipPath)
        {
            _songAudioClipPath = songAudioClipPath;
        }
    }
}

using System;
using Zenject;
using System.Linq;
using System.Collections.Generic;

namespace Enhancements.Breaktime
{
    public class BreaktimeManager : IInitializable, IDisposable
    {
        private BreaktimeLoader _loader;
        private BreaktimeSettings _settings;
        private IDifficultyBeatmap _difficultyBeatmap;
        private BeatmapObjectManager _beatmapObjectManager;
        private readonly Dictionary<Tuple<NoteLineLayer, int, float>, BeatmapObjectData> breaks = new Dictionary<Tuple<NoteLineLayer, int, float>, BeatmapObjectData>();

        public event Action<float> BreakDetected;

        public BreaktimeManager(BreaktimeLoader loader, BreaktimeSettings settings, IDifficultyBeatmap difficultyBeatmap, BeatmapObjectManager beatmapObjectManager)
        {
            _loader = loader;
            _settings = settings;
            _difficultyBeatmap = difficultyBeatmap;
            _beatmapObjectManager = beatmapObjectManager;
        }

        public void Initialize()
        {
            List<BeatmapObjectData> objects = new List<BeatmapObjectData>();
            var lines = _difficultyBeatmap.beatmapData.beatmapLinesData;
            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];
                for (int n = 0; n < line.beatmapObjectsData.Count(); n++)
                {
                    var objectData = line.beatmapObjectsData[n];
                    if (objectData.beatmapObjectType == BeatmapObjectType.Note)
                    {
                        objects.Add(objectData);
                    }
                }
            }
            objects = objects.OrderBy(x => x.time).ToList();
            for (int i = 0; i < objects.Count() - 1; i++)
            {
                var first = objects[i] as NoteData;
                var second = objects[i + 1];
                
                if (second.time - first.time > _settings.MinimumBreakTime)
                {
                    breaks.Add(new Tuple<NoteLineLayer, int, float>(first.noteLineLayer, first.lineIndex, first.time), second);
                }
            }

            _beatmapObjectManager.noteWasCutEvent += NoteCut;
            _beatmapObjectManager.noteWasMissedEvent += NoteEnded;
        }

        private void NoteCut(NoteController noteController, NoteCutInfo _)
        {
            NoteEnded(noteController);
        }

        private void NoteEnded(NoteController noteController)
        {
            var first = noteController.noteData as NoteData;
            if (breaks.TryGetValue(new Tuple<NoteLineLayer, int, float>(first.noteLineLayer, first.lineIndex, first.time), out BeatmapObjectData data))
            {
                BreakDetected?.Invoke(data.time);
            }
        }

        public void Dispose()
        {
            _beatmapObjectManager.noteWasMissedEvent -= NoteEnded;
            _beatmapObjectManager.noteWasCutEvent -= NoteCut;
        }
    }
}
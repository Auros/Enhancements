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
        private readonly Dictionary<int, BeatmapObjectData> breaks = new Dictionary<int, BeatmapObjectData>();

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
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (int n = 0; n < line.beatmapObjectsData.Length; n++)
                {
                    var objectData = line.beatmapObjectsData[n];
                    if (objectData.beatmapObjectType != BeatmapObjectType.Obstacle)
                    {
                        objects.Add(objectData);
                    }
                }
            }
            objects = objects.OrderBy(x => x.time).ToList();
            for (int i = 0; i < objects.Count() - 1; i++)
            {
                var first = objects[i];
                var second = objects[i + 1];

                if (second.time - first.time > _settings.MinimumBreakTime)
                {
                    breaks.Add(first.id, second);
                }
            }

            _beatmapObjectManager.noteWasCutEvent += NoteCut;
            _beatmapObjectManager.noteWasMissedEvent += NoteEnded;
        }

        private void NoteCut(INoteController noteController, NoteCutInfo _)
        {
            NoteEnded(noteController);
        }

        private void NoteEnded(INoteController noteController)
        {
            if (breaks.TryGetValue(noteController.noteData.id, out BeatmapObjectData data))
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
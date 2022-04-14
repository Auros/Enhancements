using System;
using Zenject;
using System.Linq;
using System.Collections.Generic;

namespace Enhancements.Breaktime
{
    public class BreaktimeManager : IInitializable, IDisposable
    {
        private readonly BreaktimeSettings _settings;
        private readonly IReadonlyBeatmapData _readonlyBeatmapData;
        private readonly BeatmapObjectManager _beatmapObjectManager;
        private readonly Dictionary<Tuple<NoteLineLayer, int, float>, BeatmapObjectData> breaks = new Dictionary<Tuple<NoteLineLayer, int, float>, BeatmapObjectData>();

        public event Action<float> BreakDetected;

        public BreaktimeManager(BreaktimeSettings settings, [InjectOptional] IReadonlyBeatmapData readonlyBeatmapData, [InjectOptional] BeatmapObjectManager beatmapObjectManager)
        {
            _settings = settings;
            _readonlyBeatmapData = readonlyBeatmapData;
            _beatmapObjectManager = beatmapObjectManager;
        }

        public void Initialize()
        {
            if (_readonlyBeatmapData != null)
            {
                List<BeatmapObjectData> objects = new List<BeatmapObjectData>();
                foreach (var item in _readonlyBeatmapData.allBeatmapDataItems)
                {
                    if (item is NoteData noteData)
                    {
                        objects.Add(noteData);
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
        }

        private void NoteCut(NoteController noteController, in NoteCutInfo _)
        {
            NoteEnded(noteController);
        }

        private void NoteEnded(NoteController noteController)
        {
            var first = noteController.noteData;
            if (breaks.TryGetValue(new Tuple<NoteLineLayer, int, float>(first.noteLineLayer, first.lineIndex, first.time), out BeatmapObjectData data))
            {
                BreakDetected?.Invoke(data.time);
            }
        }

        public void Dispose()
        {
            if (_readonlyBeatmapData != null)
            {
                _beatmapObjectManager.noteWasMissedEvent -= NoteEnded;
                _beatmapObjectManager.noteWasCutEvent -= NoteCut;
            }
        }
    }
}
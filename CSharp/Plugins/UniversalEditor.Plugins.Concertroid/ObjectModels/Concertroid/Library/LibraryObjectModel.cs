using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Concertroid.Library
{
    public class LibraryObjectModel : ObjectModel
    {
        private Character.CharacterCollection mvarCharacters = new Character.CharacterCollection();
        public Character.CharacterCollection Characters { get { return mvarCharacters; } }

        private Producer.ProducerCollection mvarProducers = new Producer.ProducerCollection();
        public Producer.ProducerCollection Producers { get { return mvarProducers; } }

        private static ObjectModelReference _omr = null;
        protected override ObjectModelReference MakeReferenceInternal()
        {
            if (_omr == null)
            {
                _omr = base.MakeReferenceInternal();
                _omr.Title = "Asset library";
                _omr.Path = new string[] { "Concertroid", "Asset library" };
                _omr.Description = "A container to hold various Concertroid assets, such as songs, musicians, producers, characters, costumes, and animations.";
            }
            return _omr;
        }

        public override void Clear()
        {
            mvarCharacters.Clear();
            mvarProducers.Clear();
        }

        public override void CopyTo(ObjectModel where)
        {
            LibraryObjectModel clone = (where as LibraryObjectModel);
            foreach (Character chara in mvarCharacters)
            {
                clone.Characters.Add(chara.Clone() as Character);
            }
            foreach (Producer producer in mvarProducers)
            {
                clone.Producers.Add(producer.Clone() as Producer);
            }
        }
    }
}

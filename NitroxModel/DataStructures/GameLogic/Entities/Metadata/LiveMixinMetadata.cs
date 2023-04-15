using BinaryPack.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NitroxModel.DataStructures.GameLogic.Entities.Metadata
{
    [Serializable]
    [DataContract]
    public class LiveMixinMetadata : EntityMetadata
    {
        [DataMember(Order = 1)]
        public string livemixin { get; }

        [IgnoreConstructor]
        protected LiveMixinMetadata()
        {
            // Constructor for serialization. Has to be "protected" for json serialization.
        }

        public LiveMixinMetadata(string LiveMixin)
        {
            livemixin = LiveMixin;
        }

        public override string ToString()
        {
            return $"[LiveMixinMetadata LiveMixin: {livemixin}]";
        }
    }
}

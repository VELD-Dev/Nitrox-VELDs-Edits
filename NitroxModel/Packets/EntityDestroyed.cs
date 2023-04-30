using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets;

[Serializable]
public class EntityDestroyed : Packet
{
    public NitroxId Id { get; }
    public bool IsBreakableEntity { get; }

    public EntityDestroyed(NitroxId id, bool isBreakableEntity = false)
    {
        Id = id;
        IsBreakableEntity = isBreakableEntity;
    }
}

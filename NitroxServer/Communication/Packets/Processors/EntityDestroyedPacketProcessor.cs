using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;
using NitroxModel.Serialization;
using NitroxServer.Communication.Packets.Processors.Abstract;
using NitroxServer.GameLogic;
using NitroxServer.GameLogic.Entities;
using NitroxServer.Serialization;

namespace NitroxServer.Communication.Packets.Processors;

public class EntityDestroyedPacketProcessor : AuthenticatedPacketProcessor<EntityDestroyed>
{
    private readonly PlayerManager playerManager;
    private readonly EntitySimulation entitySimulation;
    private readonly WorldEntityManager worldEntityManager;
    private readonly ServerConfig serverConfig;

    public EntityDestroyedPacketProcessor(PlayerManager playerManager, EntitySimulation entitySimulation, WorldEntityManager worldEntityManager, ServerConfig serverConfig)
    {
        this.playerManager = playerManager;
        this.worldEntityManager = worldEntityManager;
        this.entitySimulation = entitySimulation;
        this.serverConfig = serverConfig;
    }

    public override void Process(EntityDestroyed packet, Player destroyingPlayer)
    {
        entitySimulation.EntityDestroyed(packet.Id);

        // If the packet is a breakable resource, if the resources shared is disabled and if the respawn delay is 0 (disabled), destroy it forever.
        if (packet.IsBreakableEntity & serverConfig.BreakableResourcesShared == false & serverConfig.BreakableResourcesRespawnDelay == 0) return;

        if (worldEntityManager.TryDestroyEntity(packet.Id, out Optional<Entity> entity))
        {
            foreach (Player player in playerManager.GetConnectedPlayers())
            {
                bool isOtherPlayer = player != destroyingPlayer;
                if (isOtherPlayer && player.CanSee(entity.Value))
                {
                    player.SendPacket(packet);
                }
            }
        }
    }
}

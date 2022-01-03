﻿using Maple2.PacketLib.Tools;
using Maple2.Server.Constants;
using Maple2.Server.Core.Constants;
using Maple2.Server.Core.Network;
using Maple2.Server.Core.Packets;
using Microsoft.Extensions.Logging;

namespace Maple2.Server.Core.PacketHandlers;

public abstract class ResponseKeyHandler<T> : PacketHandler<T> where T : Session {
    public override ushort OpCode => RecvOp.RESPONSE_KEY;

    protected ResponseKeyHandler(ILogger logger) : base(logger) { }

    public override void Handle(T session, IByteReader packet) {
        long accountId = packet.ReadLong();
        int tokenA = packet.ReadInt();
        int tokenB = packet.ReadInt();

        logger.LogInformation("LOGIN USER: {AccountId}", accountId);
        // AuthData authData = AuthStorage.GetData(accountId);
        // if (authData == null) {
        //     throw new ArgumentException("Attempted connection to game with unauthorized account");
        // }
        // if (tokenA != authData.TokenA || tokenB != authData.TokenB) {
        //     throw new ArgumentException("Attempted login with invalid tokens...");
        // }

        session.Send(Packet.Of(SendOp.REQUEST_SYSTEM_INFO));
        ByteWriter moveResultPacket = Packet.Of(SendOp.MOVE_RESULT);
        moveResultPacket.WriteByte();
        session.Send(moveResultPacket);
    }
}

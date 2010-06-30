Public Module WS_Movement
    Public Sub On_SMSG_TIME_SYNC_REQ(ByRef Packet As PacketClass)
        Console.WriteLine("[{0}][World] You're now inside the world.", Format(TimeOfDay, "HH:mm:ss"))
    End Sub
End Module

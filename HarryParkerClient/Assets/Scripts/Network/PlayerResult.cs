using UnityEngine.Networking;

public class PlayerResult : MessageBase
{
    public int Result;
    public bool JustDoIt;
    public int PlayerID;

    public override void Serialize(NetworkWriter writer)
    {
        writer.Write(Result);
        writer.Write(JustDoIt);
        writer.Write(PlayerID);
    }

    public override void Deserialize(NetworkReader reader)
    {
        Result = reader.ReadInt32();
        JustDoIt = reader.ReadBoolean();
        PlayerID = reader.ReadInt32();
    }
}

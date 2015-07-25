using UnityEngine.Networking;

public class EndVotingMessage : MessageBase
{
    public bool isJustDoIt;

    public override void Serialize(NetworkWriter writer)
    {
        writer.Write(isJustDoIt);
    }

    public override void Deserialize(NetworkReader reader)
    {
        isJustDoIt = reader.ReadBoolean();
    }
}

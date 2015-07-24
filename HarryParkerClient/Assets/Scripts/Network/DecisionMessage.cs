using UnityEngine;
using UnityEngine.Networking;

public class DecisionMessage : MessageBase
{
    public string[] Decisions;

    public override void Serialize(NetworkWriter writer)
    {
        var outString = string.Join("\n", Decisions);
        writer.Write(outString);
    }

    public override void Deserialize(NetworkReader reader)
    {
        Decisions = reader.ReadString().Split('\n');
    }
}

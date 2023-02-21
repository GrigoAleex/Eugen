using TMPro;
using UnityEngine.Playables;

public class SubtitleBehavior : PlayableBehaviour
{
    public string text;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI subtitle = playerData as TextMeshProUGUI;
        subtitle.GetComponent<MachineWriter>().DisplayText(text);
    }
}

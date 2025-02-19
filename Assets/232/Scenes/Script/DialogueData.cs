using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    // 대사를 담을 문자열 배열
    public string[] dialogue;
}

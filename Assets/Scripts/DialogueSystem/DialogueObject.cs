using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObject", menuName = "Dialogue/DialogueObject", order = 0)]
public class DialogueObject : ScriptableObject {
    [SerializeField] [TextArea] private string[] Dialogue;
    [SerializeField] private Response[] responses;

    public string[] Dialogues => Dialogue;

    public bool HasRespones => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}

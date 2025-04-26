using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerInteract : MonoBehaviour
{
    public Transform NPCInteractPoint;
    public float NPCInteractionRadius = 0.4f;
    private DialogueAdvanceInput dialogueInput;

    public Transform buildingInteactPoint;
    public float buildingInteractionRadius = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        dialogueInput = FindObjectOfType<DialogueAdvanceInput>();
        dialogueInput.enableActionOnStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<DialogueRunner>().IsDialogueRunning == true)
        {
            return;
        }

        if (dialogueInput.enabled)
        {

            dialogueInput.enabled = false;

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckForNearbyNPC();
            Debug.Log("Testing...");

        }
    }

    public void CheckForNearbyNPC()
    {
        var allParticipants = new List<NPCInteractable>(FindObjectsOfType<NPCInteractable>());
        var target = allParticipants.Find(delegate (NPCInteractable p)
        {
            return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
            (p.transform.position - NPCInteractPoint.transform.position)// is in range?
            .magnitude <= NPCInteractionRadius;
        });
        if (target != null)
        {

            FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);

            dialogueInput.enabled = true;

        }
    }

    public void OnDrawGizmosSelected()
    {
        if(NPCInteractPoint == null)
        {
            return;
        }
        if (buildingInteactPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(NPCInteractPoint.position, NPCInteractionRadius);
        Gizmos.DrawWireSphere(buildingInteactPoint.position, buildingInteractionRadius);
    }
}


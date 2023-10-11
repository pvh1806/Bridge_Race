using UnityEngine;

public class NewStageBox : MonoBehaviour
{
    public Stage stage;
    private void OnTriggerEnter(Collider other)
    {
        CharacterController characterController = other.GetComponent< CharacterController>();
        if (characterController != null )
        {
            characterController.stage = stage;
            stage.InitColor(characterController.color, 20);
        }
    }
    
}
using TMPro;
using UnityEngine;

public class CharacterName : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private float nameFont = 5f; // Distance at which the name is displayed
    [SerializeField] private float nameDisplayDistance = 5f; // Distance at which the name is displayed
    private string characterName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (nameText.enabled == false)
        {
            nameText.enabled = true; // Enable the TextMeshPro component if it's disabled
        }
        characterName = transform.name; // Set the character name to the GameObject's name
        nameText.text = characterName;
        nameText.fontSize = nameFont; // Adjust the font size as needed
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * nameDisplayDistance);
        nameText.transform.position = screenPosition;
    }
}

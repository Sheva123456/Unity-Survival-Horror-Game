using UnityEngine;
using TMPro;
using NavKeypad;

public class Paper : MonoBehaviour
{
    public TMP_Text contentText;
    [TextArea]
    public string content;
    [SerializeField] private int digitIndex; // 0 = first digit, 1 = second, etc.
    [SerializeField] private Keypad keypad;

    private void Start()
    {
        string code = keypad.GetCode();
        string digit = code[digitIndex].ToString();
        content = content.Replace("{code}", digit);
        Debug.Log(code);
        contentText.text = content;

    }
}
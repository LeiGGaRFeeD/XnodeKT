using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnUndoButtonClick);
    }

    private void OnUndoButtonClick()
    {
        GameManager.Instance.UndoLastMove();
    }
}

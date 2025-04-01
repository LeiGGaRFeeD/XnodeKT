using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardSpriteRenderer;
    [SerializeField] private TextMesh dialogueText;
    [SerializeField] private TextMesh leftActionText;
    [SerializeField] private TextMesh rightActionText;

    private BoxCollider2D boxCollider;
    private bool isDragging = false;
    private Vector3 startPosition;
    private float sideMargin = 2.5f;
    private float sideThreshold = 1.0f;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
    }

    private void Update()
    {
        HandleDrag();
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0) && IsMouseOverCard())
        {
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;

            float xPos = transform.position.x;
            if (xPos > sideThreshold)
            {
                GameManager.Instance.SelectRightAction();
            }
            else if (xPos < -sideThreshold)
            {
                GameManager.Instance.SelectLeftAction();
            }
            else
            {
                ResetPosition();
            }
        }

        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = new Vector3(mousePosition.x, transform.position.y, 0);

            // Отображаем текст действия при перемещении карточки
            ShowActionText();
        }
    }

    private bool IsMouseOverCard()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return boxCollider.bounds.Contains(mousePosition);
    }

    private void ShowActionText()
    {
        float xPos = transform.position.x;
        if (xPos > sideThreshold)
        {
            rightActionText.gameObject.SetActive(true);
            leftActionText.gameObject.SetActive(false);
        }
        else if (xPos < -sideThreshold)
        {
            leftActionText.gameObject.SetActive(true);
            rightActionText.gameObject.SetActive(false);
        }
        else
        {
            leftActionText.gameObject.SetActive(false);
            rightActionText.gameObject.SetActive(false);
        }
    }

    public void SetCardContent(string dialogue, string leftAction, string rightAction)
    {
        dialogueText.text = dialogue;
        leftActionText.text = leftAction;
        rightActionText.text = rightAction;

        leftActionText.gameObject.SetActive(false);
        rightActionText.gameObject.SetActive(false);
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        leftActionText.gameObject.SetActive(false);
        rightActionText.gameObject.SetActive(false);
    }
}

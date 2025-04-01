using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private CardNodeGraph cardDeck;
    [SerializeField] private CardController cardPrefab;
    [SerializeField] private Transform cardSpawnPoint;

    private CardController currentCard;
    private CardNode currentCardNode;

    // Для системы отката ходов
    private class Move
    {
        public CardNode Node;
        public bool WasRightAction;

        public Move(CardNode node, bool wasRightAction)
        {
            Node = node;
            WasRightAction = wasRightAction;
        }
    }

    private List<Move> moveHistory = new List<Move>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        moveHistory.Clear();
        cardDeck.Start();
    }

    public void ShowCard(string characterName, string dialogue, string leftAction, string rightAction, CardNode node)
    {
        if (currentCard != null)
        {
            Destroy(currentCard.gameObject);
        }

        currentCard = Instantiate(cardPrefab, cardSpawnPoint.position, Quaternion.identity);
        currentCard.SetCardContent(dialogue, leftAction, rightAction);

        currentCardNode = node;
    }

    public void SelectLeftAction()
    {
        if (currentCardNode != null)
        {
            currentCardNode.ChooseLeft();
        }
    }

    public void SelectRightAction()
    {
        if (currentCardNode != null)
        {
            currentCardNode.ChooseRight();
        }
    }

    public void RecordMove(CardNode node, bool wasRightAction)
    {
        moveHistory.Add(new Move(node, wasRightAction));
    }

    public void UndoLastMove()
    {
        if (moveHistory.Count > 0)
        {
            // Удаляем последний ход из истории
            moveHistory.RemoveAt(moveHistory.Count - 1);

            // Если история пуста, начинаем игру сначала
            if (moveHistory.Count == 0)
            {
                StartGame();
                return;
            }

            // Иначе восстанавливаем последний ход из истории
            cardDeck.Start(); // Перезапускаем с начального узла

            // Воспроизводим все ходы из истории
            foreach (Move move in moveHistory)
            {
                ShowCard(
                    move.Node.characterName,
                    move.Node.dialogue,
                    move.Node.leftAction,
                    move.Node.rightAction,
                    move.Node
                );

                if (move.WasRightAction)
                {
                    move.Node.ChooseRight();
                }
                else
                {
                    move.Node.ChooseLeft();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] private CanvasGroup canvasBlack;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AbilityController abilityController;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void GameOverLevel()
    {
        playerMovement.DisableMovenent();
        abilityController.DisableAbilities();
    }
}

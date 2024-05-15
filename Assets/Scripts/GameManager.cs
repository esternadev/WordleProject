using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public KeyboardLayer KeyboardLayer => keyboardLayer;

    [SerializeField] private KeyboardLayer keyboardLayer;
}

// Copyright (c) Pixel Crushers. All rights reserved.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace PixelCrushers.DialogueSystem
{
    public class DialogueCharacterHelper:MonoBehaviour
    {
        public void startDialogue()
        {
            transform.DOShakePosition(1);
        }
    }
}
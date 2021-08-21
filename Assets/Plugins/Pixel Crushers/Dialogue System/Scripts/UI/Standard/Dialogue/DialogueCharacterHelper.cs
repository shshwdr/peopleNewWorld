// Copyright (c) Pixel Crushers. All rights reserved.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace PixelCrushers.DialogueSystem
{
    public class DialogueCharacterHelper:MonoBehaviour
    {
        float rotateDegree = 15;
        float rotateTime = 0.7f;
        Sequence mySequence;
        Animator animator;
        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }
        public void startDialogue()
        {
            
            animator.SetTrigger("talk");
            //mySequence.Kill();
            //mySequence = DOTween.Sequence();
            //mySequence.Append(transform.GetChild(0).DOLocalRotate(new Vector3(0, 0, rotateDegree), rotateTime));
            //mySequence.Append(transform.GetChild(0).DOLocalRotate(new Vector3(0, 0, 0), rotateTime));
            //mySequence.Append(transform.GetChild(0).DOLocalRotate(new Vector3(0, 0, -rotateDegree), rotateTime));
            //mySequence.Append(transform.GetChild(0).DOLocalRotate(new Vector3(0, 0, 0), rotateTime));
            //mySequence.SetLoops(-1);
            //mySequence.Play();
        }

        public void stopDialogue()
        {

            animator.SetTrigger("idle");
            //mySequence.Kill();
            //transform.GetChild(0).DOKill();
            //transform.DOKill();
            //transform.GetChild(0).rotation = Quaternion.identity;
        }
    }
}
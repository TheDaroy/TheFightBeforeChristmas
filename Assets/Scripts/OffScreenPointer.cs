using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenPointer : MonoBehaviour
{
    [SerializeField] private Camera UICamera;
    [SerializeField] private Sprite arrowSprite;

    private List<EnemyPointer> enemyPointers;

    private void Awake()
    {
        enemyPointers = new List<EnemyPointer>();
        CreatePointer(new Vector3(0, 4));
        CreatePointer(new Vector3(10, 2));
        CreatePointer(new Vector3(-20, 10));
    }

    private void Update()
    {
        foreach (EnemyPointer pointer in enemyPointers)
        {
            pointer.Update();
        }
    }

    public EnemyPointer CreatePointer(Vector3 targetPosition)
    {
        GameObject pointerGameObject = Instantiate(transform.GetChild(0).gameObject);
        pointerGameObject.SetActive(true);
        pointerGameObject.transform.SetParent(transform, false);
        EnemyPointer enemypointer = new EnemyPointer(targetPosition, UICamera, pointerGameObject, arrowSprite);
        //enemyPointers = enemypointer;
        
        enemyPointers.Add(enemypointer);
        return enemypointer;
    }
    public void DestroyPointer(EnemyPointer pointer)
    {
        enemyPointers.Remove(pointer);
        pointer.DestroySelf();
    }

    public class EnemyPointer
    {
        private Camera UICamera;
        private Vector3 targetPosition;
        private GameObject pointerGameObject;
        private Sprite arrowSprite;

        public RectTransform pointerRectTransform;
        private Image pointerImage;

        public EnemyPointer(Vector3 targetPosition, Camera uiCamera, GameObject pointerObject, Sprite arrowSprite)
        {
            this.targetPosition = targetPosition;
            this.UICamera = uiCamera;
            this.pointerGameObject = pointerObject;
            this.arrowSprite = arrowSprite;

            pointerRectTransform = pointerObject.GetComponent<RectTransform>();
            pointerImage = pointerGameObject.GetComponent<Image>();
        }

        public void Update()
        {
            Vector3 toPosition = targetPosition;
            Vector3 fromPosition = Camera.main.transform.position;
            fromPosition.z = 0;
            Vector3 dir = (toPosition - fromPosition).normalized;
            float angle = GetAngleFromVectorFloat(dir);
            pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

            float borderSize = 50;

            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
            bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize ||
                targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
            pointerRectTransform.GetComponent<Image>().enabled = isOffScreen;
            if (isOffScreen)
            {
                 Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
                cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);

                Vector3 pointerWorldPosition = UICamera.ScreenToWorldPoint(cappedTargetScreenPosition);
                pointerRectTransform.position = pointerWorldPosition;
                pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0);
                //if (!pointerRectTransform.GetComponent<Image>().enabled)
                //  Show(pointerWorldPosition);
            }
            //else if (pointerRectTransform.GetComponent<Image>().enabled)
            //Hide();
        }

        public float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }

        internal void DestroySelf()
        {
            Destroy(pointerGameObject);
        }
    }
}

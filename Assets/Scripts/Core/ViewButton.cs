using System;
using UnityEngine;
using UnityEngine.UI;

namespace Graf.Utils
{
    [RequireComponent(typeof(Image)), RequireComponent(typeof(Button))]
    public class ViewButton : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private Button _button;

        public event Action TabButton;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnTab);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnTab);
        }

        public void Init(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void SetSize(Vector2 size)
        {
            transform.GetComponent<RectTransform>().sizeDelta = size;
        }

        public void SetPosition(Vector2 position)
        {
            transform.localPosition = position;
        }

        private void OnTab()
        {
            TabButton?.Invoke();
        }

    }
}
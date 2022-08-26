using UnityEngine;
using UnityEngine.UI;

namespace Graf.Audio
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Selectable))]
    public class ButtonSoundTrigger : MonoBehaviour
    {
        void Awake()
        {
            var button = GetComponent<Button>();

            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlayOneShot("ButtonSound");
                });
            }


            if (button == null)
            {
                var toggle = GetComponent<Toggle>();

                if (toggle != null)
                {
                    toggle.onValueChanged.AddListener((isOn) =>
                    {
                        AudioManager.Instance.PlayOneShot("ButtonSound");
                    });
                }
            }
        }
    }
}
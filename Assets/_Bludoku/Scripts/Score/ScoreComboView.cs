using _Bludoku.Scripts.Boards;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Bludoku.Scripts.Score
{
    public class ScoreComboView : MonoBehaviour
    {
        [SerializeField] private Transform combo;
        [SerializeField] private TMP_Text comboText;
        [SerializeField] private GameObject rowIcon;
        [SerializeField] private GameObject columnIcon;
        [SerializeField] private GameObject boxIcon;

        private bool _isComboActive;
        private Tween _pulseTween;

        private void Awake()
        {
            combo.localScale = Vector3.zero;
        }

        public void UpdateBonus(bool isActive, ClearShape shape, int value)
        {
            comboText.text = $"+{value}";
            UpdateIcons(shape);

            if (_isComboActive == isActive)
                return;

            combo.DOKill();
            _pulseTween?.Kill();
            _pulseTween = null;

            if (isActive)
            {
                combo.DOScale(Vector3.one, 0.8f)
                    .SetEase(Ease.OutElastic)
                    .OnComplete(StartPulse);
            }
            else
            {
                combo.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            }

            _isComboActive = isActive;
        }

        private void UpdateIcons(ClearShape shape)
        {
            rowIcon.SetActive((shape & ClearShape.Row) != ClearShape.None);
            columnIcon.SetActive((shape & ClearShape.Column) != ClearShape.None);
            boxIcon.SetActive((shape & ClearShape.Box) != ClearShape.None);
        }

        private void StartPulse()
        {
            if (!_isComboActive)
                return;

            _pulseTween = combo.DOScale(1.08f, 0.45f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            combo.DOKill();
            _pulseTween?.Kill();
            _pulseTween = null;
        }
    }
}
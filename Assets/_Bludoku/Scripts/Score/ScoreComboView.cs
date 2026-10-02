using _Bludoku.Scripts.Boards;
using DG.Tweening;
using System;
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
        [SerializeField] private TMP_Text flyTextTemplate;
        [SerializeField] private Transform flyTarget;

        private const float FlyDuration = 0.5f;
        private const float PunchDuration = 0.3f;

        private bool _isComboActive;
        private int _value;
        private Tween _pulseTween;
        private Tween _flyTween;

        private void Awake()
        {
            combo.localScale = Vector3.zero;
        }

        public void UpdateBonus(bool isActive, ClearShape shape, int value)
        {
            _value = value;

            if (_flyTween == null)
                SetValueText(comboText, value);

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

        public void PlayBonusFly(int bonus, Action onComplete)
        {
            TMP_Text flyText = Instantiate(flyTextTemplate, flyTextTemplate.transform.parent);
            SetValueText(flyText, bonus);
            flyText.transform.position = comboText.transform.position;
            flyText.gameObject.SetActive(true);

            SetValueText(comboText, bonus);
            comboText.enabled = false;

            _flyTween = flyText.transform.DOMove(flyTarget.position, FlyDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    _flyTween = null;
                    Destroy(flyText.gameObject);
                    ShowNewValue();
                    onComplete?.Invoke();
                });
        }

        private void SetValueText(TMP_Text text, int value)
        {
            text.text = $"+{value}";
        }

        private void ShowNewValue()
        {
            comboText.enabled = true;
            comboText.transform.DOKill(true);

            DOTween.Sequence()
                .Append(comboText.transform.DOPunchScale(Vector3.one * 0.8f, PunchDuration, 1, 0.5f))
                .InsertCallback(PunchDuration / 2f, () => SetValueText(comboText, _value))
                .SetTarget(comboText.transform);
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
            combo.localScale = _isComboActive ? Vector3.one : Vector3.zero;
            _flyTween?.Kill();
            _flyTween = null;
            comboText.transform.DOKill();
            comboText.enabled = true;
            SetValueText(comboText, _value);
        }
    }
}
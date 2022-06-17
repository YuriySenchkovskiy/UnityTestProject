using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Text _label;
        [SerializeField] private Text _price;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _buy;

        private Weapon.Weapon _weapon;

        public event Action<Weapon.Weapon, WeaponView> SellButtonClicked;

        private void OnEnable()
        {
            _buy.onClick.AddListener(OnButtonClick);
            _buy.onClick.AddListener(TryLockItem);
        }

        private void OnDisable()
        {
            _buy.onClick.RemoveListener(OnButtonClick);
            _buy.onClick.RemoveListener(TryLockItem);
        }

        public void Render(Weapon.Weapon weapon)
        {
            _weapon = weapon;
            
            _label.text = _weapon.Label;
            _price.text = _weapon.Price.ToString();
            _icon.sprite = _weapon.Icon;
        }

        private void TryLockItem()
        {
            if (_weapon.IsBought)
            {
                _buy.gameObject.SetActive(false);
            }
        }

        private void OnButtonClick()
        {
            SellButtonClicked?.Invoke(_weapon, this);
        }
    }
}
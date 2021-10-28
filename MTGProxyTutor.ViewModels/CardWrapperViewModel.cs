using MTGProxyTutor.Contracts.Models.App;
using System.Collections.Generic;

namespace MTGProxyTutor.ViewModels
{
	public class CardWrapperViewModel : BaseViewModel
    {
        private bool _isSelected = true;
		public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                this.OnPropertyChanged(nameof(IsSelected));
            }
        }

        private Card _card;
        public Card Card
        {
            get
            {
                return _card;
            }
            set
            {
                _card = value;
                this.OnPropertyChanged(nameof(Card));
            }
        }

        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                this.OnPropertyChanged(nameof(Quantity));
            }
        }

        private List<CardImage> _images;
        public List<CardImage> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
                this.OnPropertyChanged(nameof(Images));
            }
        }
    }
}

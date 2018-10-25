using System;
using Xamarin.Forms;

namespace hairDresserApp.Models
{
	public class FormattedNumberEntry : Entry
	{
		private bool _shouldReactToTextChange = true;

		protected override void OnPropertyChanged(string propertyName = null)
		{
            _shouldReactToTextChange = true;
            if (nameof(this.Text).Equals(propertyName))
			{
				
				if (!_shouldReactToTextChange) return;
				string oldText;
				_shouldReactToTextChange = false;
				if(Text != "") 
				 oldText = this.Text;
				else  return;
                if (Text == null) return;
                long number = long.Parse(oldText, System.Globalization.NumberStyles.Number);
				string newText = number.ToString("N0");
              
				this.Text = newText;

				//
			}
			base.OnPropertyChanged(propertyName);
		}

	}
}

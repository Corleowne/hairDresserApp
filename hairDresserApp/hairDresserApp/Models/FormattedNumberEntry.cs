using System;
using Xamarin.Forms;

namespace hairDresserApp.Models
{
	public class FormattedNumberEntry : Entry
	{
		private bool _shouldReactToTextChange = true;
        string oldText ="";
        long number =0;
        string newText="";


        protected override void OnPropertyChanged(string propertyName = null)
		{
            if (nameof(this.Text).Equals(propertyName))
			{
				if (App.test == false)
					_shouldReactToTextChange = true;
				else _shouldReactToTextChange = false;

				if (!_shouldReactToTextChange) return;
				_shouldReactToTextChange = false;
				
				if (Text != "") 
				 oldText = this.Text;
				else  return;
				if (Text != null) { 
                 number = long.Parse(oldText, System.Globalization.NumberStyles.Number);
				 newText = number.ToString("N0");
				}
				else
				{
					newText = string.Empty;
				}
				this.Text = newText;
				if (App.test == false)
					_shouldReactToTextChange = true;
				else _shouldReactToTextChange = false;


                //
            }
            base.OnPropertyChanged(propertyName);
		}





	}
}

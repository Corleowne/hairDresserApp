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
                Console.WriteLine("{0},{1}",this.Text,propertyName);
				
				if (!_shouldReactToTextChange) return;
				_shouldReactToTextChange = false;
				if(Text != "") 
				 oldText = this.Text;
				else  return;
                if (Text == null) return;
                 number = long.Parse(oldText, System.Globalization.NumberStyles.Number);
				 newText = number.ToString("N0");
              
				this.Text = newText;
                _shouldReactToTextChange = true;


                //
            }
            base.OnPropertyChanged(propertyName);
		}



	}
}

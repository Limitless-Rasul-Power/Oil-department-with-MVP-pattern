using Best_Oil_with_MVP_pattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guna.UI2.WinForms;
using System.Windows.Forms;

namespace Best_Oil_with_MVP_pattern.Views
{
    public interface IMainView
    {
        EventHandler<EventArgs> LitrNumericUpDownValueChanged { get; set; }
        EventHandler<EventArgs> PriceNumericUpDownValueChanged { get; set; }
        EventHandler<EventArgs> PayButtonClicked { get; set; }
        EventHandler<EventArgs> PetrolComboBoxSelectedIndexChanged { get; set; }
        EventHandler<EventArgs> LitrRadioButtonCheckedChanged { get; set; }
        EventHandler<EventArgs> PriceRadioButtonCheckedChanged { get; set; }
        EventHandler<EventArgs> PayButtonMouseHover { get; set; }
        EventHandler<EventArgs> PayButtonMouseLeave { get; set; }        
        EventHandler<EventArgs> RemoveButtonClicked { get; set; }
        EventHandler<EventArgs> RemoveButtonMouseHover { get; set; }
        EventHandler<EventArgs> RemoveButtonMouseLeave { get; set; }
        EventHandler<EventArgs> ClearButtonClicked { get; set; }
        EventHandler<EventArgs> ClearButtonMouseHover { get; set; }
        EventHandler<EventArgs> ClearButtonMouseLeave { get; set; }


       
        Guna2RadioButton LitrRadioButton { get; set; }
        Guna2RadioButton PriceRadioButton { get; set; }
        Guna2ComboBox PetrolComboBox { get; set; }
        Guna2TextBox PriceTextBox { get; set; }
        Guna2NumericUpDown LitrNumericUpDown { get; set; }
        Guna2NumericUpDown PriceNumericUpDown { get; set; }
        Guna2HtmlLabel TotalPaymentLabel { get; set; }
        Guna2CircleButton PayButton{ get; set; }       
        Guna2Button RemoveButton { get; set; }
        Guna2Button ClearButton { get; set; }
        ListBox PaymentListBox { get; set; }
        Guna2HtmlLabel InfoLabel { get; set; }
        Guna2HtmlToolTip PictureToolTip { get; set; }
        Guna2PictureBox CarPictureBox { get; set; }

    }
}

using Best_Oil_with_MVP_pattern.Data;
using Best_Oil_with_MVP_pattern.Models;
using Best_Oil_with_MVP_pattern.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Best_Oil_with_MVP_pattern.Presenters
{
    public class MainViewPresenter
    {
        private readonly PetrolOperationContext _petrolOperationContext;
        private readonly IMainView _view;
        private readonly List<Petrol> _petrols;
        public MainViewPresenter(IMainView view)
        {
            _view = view;            

            SetAllEvents();

            _petrols = JsonFileHelper.JSONDeSerialization<Petrol>("Petrols");
            _view.PetrolComboBox.DisplayMember = "Name";
            _view.PetrolComboBox.DataSource = _petrols;
            _view.PriceTextBox.Text = _petrols[0].InitialPrice.ToString();



            _view.PictureToolTip.SetToolTip(_view.CarPictureBox, "Man refuelling his car.");


            _petrolOperationContext = new PetrolOperationContext();
             SetListBoxDataSource();

            try
            {
                if (!_petrolOperationContext.PetrolPaymentOperations.Any())
                {
                    _view.InfoLabel.Visible = true;
                }

            }
            catch (Exception)
            {
            }

        }

        private void SetAllEvents()
        {
            _view.LitrNumericUpDownValueChanged += ViewLitrNumericUpDownValueChanged;
            _view.PriceNumericUpDownValueChanged += ViewPriceNumericUpDownValueChanged;
            _view.PayButtonClicked += ViewPayButtonClicked;
            _view.PetrolComboBoxSelectedIndexChanged += ViewPetrolComboBoxSelectedIndexChanged;
            _view.LitrRadioButtonCheckedChanged += ViewLitrRadioButtonCheckedChanged;
            _view.PriceRadioButtonCheckedChanged += ViewPriceRadioButtonCheckedChanged;
            _view.PayButtonMouseHover += ViewPayButtonMouseHover;
            _view.PayButtonMouseLeave += ViewPayButtonMouseLeave;

            _view.RemoveButtonClicked += ViewRemoveButtonClicked;
            _view.RemoveButtonMouseHover += ViewRemoveButtonMouseHover;
            _view.RemoveButtonMouseLeave += ViewRemoveButtonMouseLeave;

            _view.ClearButtonClicked += ViewClearButtonClicked;
            _view.ClearButtonMouseHover += ViewClearButtonMouseHover;
            _view.ClearButtonMouseLeave += ViewClearButtonMouseLeave;
        }
        private void ViewLitrNumericUpDownValueChanged(object sender, EventArgs e)
        {
            _view.TotalPaymentLabel.Text = (_view.LitrNumericUpDown.Value * decimal.Parse(_view.PriceTextBox.Text)).ToString();
        }

        private void ViewPriceNumericUpDownValueChanged(object sender, EventArgs e)
        {
            _view.TotalPaymentLabel.Text = _view.PriceNumericUpDown.Value.ToString();
        }

        private void ViewPayButtonClicked(object sender, EventArgs e)
        {
            decimal totalPrice = decimal.Parse(_view.TotalPaymentLabel.Text);

            if (totalPrice == 0)
            {
                System.Windows.Forms.MessageBox.Show("You didn't buy anything.");
            }
            else
            {
                Petrol petrol = _view.PetrolComboBox.SelectedItem as Petrol;


                _petrolOperationContext.PetrolPaymentOperations.Add
                    (
                        new PetrolPaymentOperation { Petrol = petrol, IsBoughtLitr = _view.LitrRadioButton.Checked, BoughtPrice = totalPrice }
                    );


                _petrolOperationContext.SaveChanges();

                SetDefaultValuesForBuyingProcess();
                SetListBoxDataSource();

            }

        }

        private void SetDefaultValuesForBuyingProcess()
        {
            _view.LitrRadioButton.Checked = false;
            _view.PriceRadioButton.Checked = false;

            _view.LitrNumericUpDown.Enabled = false;
            _view.PriceNumericUpDown.Enabled = false;

            _view.LitrNumericUpDown.Value = 1M;
            _view.PriceNumericUpDown.Value = 1M;

            _view.TotalPaymentLabel.Text = "0";

            _view.InfoLabel.Visible = false;

        }
        private void ViewPetrolComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            _view.PriceTextBox.Text = _petrols.Find(p => p.Name == (_view.PetrolComboBox.SelectedItem as Petrol).Name).InitialPrice.ToString();

            if (_view.LitrRadioButton.Checked)
            {
                ViewLitrRadioButtonCheckedChanged(sender, e);
            }
            else if (_view.PriceRadioButton.Checked)
            {
                ViewPriceRadioButtonCheckedChanged(sender, e);
            }
        }


        private void ViewLitrRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            if (_view.LitrRadioButton.Checked)
            {
                SetDefaultValuesForNumericUpDownWhenRadioButtonChecked(_view.PriceNumericUpDown);

                _view.LitrNumericUpDown.Enabled = true;

                ViewLitrNumericUpDownValueChanged(sender, e);
            }
            else
            {
                ViewPriceNumericUpDownValueChanged(sender, e);
            }

        }

        private void SetDefaultValuesForNumericUpDownWhenRadioButtonChecked(Guna.UI2.WinForms.Guna2NumericUpDown numericUpDown)
        {
            numericUpDown.Value = 1M;
            numericUpDown.Enabled = false;
        }
        private void ViewPriceRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            if (_view.PriceRadioButton.Checked)
            {
                SetDefaultValuesForNumericUpDownWhenRadioButtonChecked(_view.LitrNumericUpDown);

                _view.PriceNumericUpDown.Enabled = true;

                ViewPriceNumericUpDownValueChanged(sender, e);
            }
            else
            {
                ViewLitrRadioButtonCheckedChanged(sender, e);
            }
        }

        private void ViewPayButtonMouseHover(object sender, EventArgs e)
        {
            _view.PayButton.FillColor = ColorTranslator.FromHtml("#3bb300");
        }

        private void ViewPayButtonMouseLeave(object sender, EventArgs e)
        {
            _view.PayButton.FillColor = Color.FromArgb(23, 163, 152);
        }

        private void ViewClearButtonClicked(object sender, EventArgs e)
        {
            _view.InfoLabel.Visible = true;

            _petrolOperationContext.PetrolPaymentOperations.RemoveRange(_petrolOperationContext.PetrolPaymentOperations);
            _petrolOperationContext.SaveChanges();

            SetListBoxDataSource();
        }

        private void ViewClearButtonMouseHover(object sender, EventArgs e)
        {
            _view.ClearButton.BorderThickness = 0;
            _view.ClearButton.ForeColor = Color.White;
            _view.ClearButton.FillColor = Color.FromArgb(244, 0, 0);
        }


        private void ViewClearButtonMouseLeave(object sender, EventArgs e)
        {
            _view.ClearButton.BorderThickness = 1;
            _view.ClearButton.ForeColor = Color.FromArgb(244, 0, 0);
            _view.ClearButton.FillColor = Color.White;
        }

        private void ViewRemoveButtonClicked(object sender, EventArgs e)
        {
            if (!(_view.PaymentListBox.SelectedItem is PetrolPaymentOperation removed))
            {
                return;
            }            

            _petrolOperationContext.PetrolPaymentOperations.Remove(removed);
            _petrolOperationContext.SaveChanges();

            SetListBoxDataSource();

            if (!_petrolOperationContext.PetrolPaymentOperations.Any())
            {
                _view.InfoLabel.Visible = true;
            }

        }

        private void SetListBoxDataSource()
        {
            _view.PaymentListBox.DataSource = null;
            _view.PaymentListBox.DataSource = _petrolOperationContext.PetrolPaymentOperations.ToList();
        }
        private void ViewRemoveButtonMouseHover(object sender, EventArgs e)
        {
            _view.RemoveButton.BorderThickness = 0;
            _view.RemoveButton.ForeColor = Color.White;
            _view.RemoveButton.FillColor = ColorTranslator.FromHtml("#fb5607");
        }

        private void ViewRemoveButtonMouseLeave(object sender, EventArgs e)
        {
            _view.RemoveButton.BorderThickness = 1;
            _view.RemoveButton.ForeColor = ColorTranslator.FromHtml("#fb5607");
            _view.RemoveButton.FillColor = Color.White;
        }


    }
}

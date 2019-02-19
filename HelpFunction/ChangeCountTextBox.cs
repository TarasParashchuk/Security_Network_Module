using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Security_network_module.HelpFunction
{
    class ChangeCountTextBox
    {
        public static void Gap(WrapPanel wrapPanel, int left_lim, int right_lim, string name, int margin_left, bool flag)
        {
            TextBox txtBox;
            for (int j = left_lim; j <= right_lim; j++)
            {
                if (flag)
                {
                    txtBox = new TextBox() { Name = name + j, Margin = new Thickness(margin_left, 0, 0, 0), FontSize = 14, FontFamily = new FontFamily("Segoe UI") };
                    if (margin_left != 0)
                        txtBox.PreviewTextInput += TxtBox_PreviewTextInput;
                    wrapPanel.Children.Add(txtBox);
                    wrapPanel.RegisterName(txtBox.Name, txtBox);
                }
                else
                {
                    if (wrapPanel.FindName(name + j) != null)
                    {
                        txtBox = (TextBox)wrapPanel.FindName(name + j);
                        wrapPanel.Children.Remove(txtBox);
                        wrapPanel.UnregisterName(txtBox.Name);
                    }
                }
            }
        }

        public static void ExpertCreate(WrapPanel wrapPanel, int right_lim)
        {
            TextBox txtBox;
            var text = string.Empty;
            var name = "txtBoxExpertAssessments";
            var pocket = string.Empty;

            for (var j = 1; j <= right_lim * right_lim; j++)
            {
                txtBox = new TextBox() { Name = name + j, FontSize = 14, FontFamily = new FontFamily("Segoe UI") };
                txtBox.PreviewTextInput += TxtBox_PreviewTextInput;
                wrapPanel.Children.Add(txtBox);
                wrapPanel.RegisterName(txtBox.Name, txtBox);

                if (j % right_lim == 0)
                {
                    wrapPanel.Width = wrapPanel.ItemWidth * right_lim;
                }
            }
        }

        public static void Remove(WrapPanel wrapPanel, string name)
        {
            TextBox txtBox;
            var i = 1;
            while (wrapPanel.FindName(name + i) != null)
            {
                txtBox = (TextBox)wrapPanel.FindName(name + i);
                wrapPanel.Children.Remove(txtBox);
                wrapPanel.UnregisterName(txtBox.Name);
                i++;
            }
        }

        private static void TxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

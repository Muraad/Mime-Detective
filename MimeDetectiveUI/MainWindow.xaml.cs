using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;

namespace MN.Mime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bLoad_Click(object sender, RoutedEventArgs e)
        {
            if(File.Exists("MimeTypes.xml"))
                MimeTypes.LoadFromXmlFile("MimeTypes.xml");
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            MimeTypes.SaveToXmlFile("MimeTypes.xml");
        }

        private async Task animateLearnSuccess()
        {
            Brush original = bLearn.Background;
            bLearn.Background = new SolidColorBrush(Colors.Green);
            await Task.Run(() => System.Threading.Thread.Sleep(750));
            bLearn.Background = original;
        }

        private async Task animateLearnFailure()
        {
            Brush original = bLearn.Background;
            bLearn.Background = new SolidColorBrush(Colors.Red);
            await Task.Run(() => System.Threading.Thread.Sleep(750));
            bLearn.Background = original;
        }

        private async void bLearn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dialog.FileNames.Length >= 2)
                {
                    string mimeType = getMimeType();
                    var infoOne = new FileInfo(dialog.FileNames[0]);
                    var infoTwo = new FileInfo(dialog.FileNames[1]);
                    var fileType = MimeDetective.LearnMimeType(infoOne, infoTwo, mimeType);
                    if (fileType != null)
                    {
                        //Detective.types.Add(fileType);
                        await animateLearnSuccess();
                    }
                    else
                        await animateLearnFailure();
                }
            }
            else
                await animateLearnFailure();
        }

        private string getMimeType()
        {
            string mimeType = tbMimeType.Text;
            if (mimeType == null || mimeType == String.Empty)
            {
                InputDialog dialog = new InputDialog();
                dialog.Owner = this;
                if ((bool)dialog.ShowDialog(TypeCode.String, "Please enter a mime type: "))
                    mimeType = dialog.Value<string>();
            }
            return mimeType;
        }
    }
}

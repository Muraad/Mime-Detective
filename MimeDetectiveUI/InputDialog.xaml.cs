/*
    Copyright (C) 2014  Muraad Nofal
    Contact: muraad.nofal@gmail.com
 
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MN.Mime
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        private object _value;
        private TypeCode _typeCode;

        public InputDialog()
        {
            InitializeComponent();
        }

        public InputDialog(WindowState state = System.Windows.WindowState.Normal)
        {
            InitializeComponent();
            this.WindowState = state;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool result = true;
            try
            {
                _value = System.Convert.ChangeType(tbInput.Text, _typeCode);
            }
            catch (Exception exc)
            {
                result = false;
            }
            finally
            {
                DialogResult = result;
            }
        }

        public void Show(TypeCode typeCode, string header)
        {
            _typeCode = typeCode;
            lHeader.Content = header;
            this.Show();
        }

        public bool? ShowDialog(TypeCode typeCode, string header)
        {
            _typeCode = typeCode;
            lHeader.Content = header;
            return this.ShowDialog();
        }

        public TypeCode TypeCode
        {
            get { return _typeCode; }
        }

        public T Value<T>()
        {
            return (T)_value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbInput.Focus();
        }
    }
}

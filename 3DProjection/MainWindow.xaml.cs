﻿using _3DProjection.Helpers;
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

namespace _3DProjection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double currentDrawingAngleX = 0;
        private double currentDrawingAngleY = 0;
        private double currentDrawingAngleZ = 0;
        public MainWindow()
        {
            InitializeComponent();
            DrawManager.Instance.SetUp(this.Canvas);
        }

        private void AddNode_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = null;
            this.XTextPopup.Text = string.Empty;
            this.YTextPopup.Text = string.Empty;
            this.ZTextPopup.Text = string.Empty;
            this.AddNodePopup.IsOpen = true;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = null;
            DrawManager.Instance.Clear();
            this.currentDrawingAngleX = this.currentDrawingAngleY = this.currentDrawingAngleZ = 0;
            this.SliderAboutX.Value = this.SliderAboutY.Value = this.SliderAboutZ.Value = 0;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = null;
            Application.Current.Shutdown();
        }

        private void CancelAddNodePopup_Click(object sender, RoutedEventArgs e)
        {
            this.AddNodePopup.IsOpen = false;
        }

        private void AddNodePopup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = Convert.ToDouble(this.XTextPopup.Text);
                double y = Convert.ToDouble(this.YTextPopup.Text);
                double z = Convert.ToDouble(this.ZTextPopup.Text);

                DrawManager.Instance.AddNode(x, y, z);
            }
            catch
            {

            }

            this.AddNodePopup.IsOpen = false;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            DrawManager.Instance.RemoveModeOn();
            this.RemoveBtn.Visibility = Visibility.Hidden;
            this.CancelRemoveBtn.Visibility = Visibility.Visible;
        }

        private void CancelRemove_Click(object sender, RoutedEventArgs e)
        {
            DrawManager.Instance.RemoveModeOff();
            this.RemoveBtn.Visibility = Visibility.Visible;
            this.CancelRemoveBtn.Visibility = Visibility.Hidden;
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = null;
            DrawManager.Instance.DrawSmth();
        }

        private void SliderAboutX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Mouse.OverrideCursor = null;

            Slider slider = (Slider)sender;
            DrawManager.Instance.Rotate(slider.Value - currentDrawingAngleX, Models.RotationLineEnum.AboutOX);
            currentDrawingAngleX = slider.Value;
        }

        private void SliderAboutY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Mouse.OverrideCursor = null;

            Slider slider = (Slider)sender;
            DrawManager.Instance.Rotate(slider.Value - currentDrawingAngleY, Models.RotationLineEnum.AboutOY);
            currentDrawingAngleY = slider.Value;
        }

        private void SliderAboutZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Mouse.OverrideCursor = null;

            Slider slider = (Slider)sender;
            DrawManager.Instance.Rotate(slider.Value - currentDrawingAngleZ, Models.RotationLineEnum.AboutOZ);
            currentDrawingAngleZ = slider.Value;
        }
    }
}

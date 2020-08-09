using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawingVisuals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            stopwatch.Start();
            this.drawingContainer1.CreateUIElements(1000, true);
            this.drawingContainer2.CreateUIElements(1000, false);
            this.drawingContainer3.CreateUIElements(1000, true);
            stopwatch.Stop();            
            MessageBox.Show("Time elapsed in milisekonds: " + stopwatch.Elapsed.Milliseconds.ToString());
            stopwatch.Reset();
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            this.drawingContainer1.ClearAll();
            this.drawingContainer2.ClearAll();
            this.drawingContainer3.ClearAll();
        }
    }
}

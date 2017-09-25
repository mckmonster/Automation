using GraphX.Controls;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using QuickGraph.Serialization;
using YAXLib;
using System.Xml;
using QuickGraph.Algorithms;
using Automation.Core;
using Automation.Test.Plugin;
using Microsoft.Win32;

namespace Automation.App
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //CreateRandomGraph();
            //myArea.GenerateGraph();
            //zoomCtrl.ZoomToFill(); // Zoome au mieux

            KeyDown += MainWindow_KeyDown;
            KeyUp += MainWindow_KeyUp;
            MouseDown += MainWindow_MouseDown;
            myArea.VertexSelected += MyArea_VertexSelected;
            myArea.EdgeSelected += MyArea_EdgeSelected;

            GraphExecute.OnFinished += GraphExecute_OnFinished;
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && edit)
            {
                edit = false;

                var chooses = new ChooseJobType();
                //TODO do a better stuf to open the window under mouse
                var pos = Mouse.GetPosition(this);
                chooses.Left = pos.X; 
                chooses.Top = pos.Y;

                var result = chooses.ShowDialog();
                if (result.HasValue & result.Value)
                {
                    var vertex = chooses.SelectedJob;
                    myArea.AddJob(vertex, pos);
                }
            }
        }

        private void MyArea_EdgeSelected(object sender, GraphX.Controls.Models.EdgeSelectedEventArgs args)
        {
            if (args.MouseArgs.RightButton == MouseButtonState.Pressed)
            {
                var edge = args.EdgeControl.Edge as MyEdge;
                myArea.LogicCore.Graph.RemoveEdge(edge);
                myArea.RemoveEdge(edge);
            }
        }

        private bool edit = false;
        private VertexControl control = null;
        private void MyArea_VertexSelected(object sender, GraphX.Controls.Models.VertexSelectedEventArgs args)
        {
           if (edit)
           {
                if (args.MouseArgs.LeftButton == MouseButtonState.Pressed)
                {
                    if (control == null)
                    {
                        control = args.VertexControl;
                    }
                    else
                    {
                        myArea.AddLink(control.Vertex as Job, args.VertexControl.Vertex as Job);
                    }
                }
           }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                edit = false;
                control = null;
            }
        }

        
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                edit = true;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = "All files|*.*", Title = "Select layout file name", FileName = "laytest.xml" };
            if (dlg.ShowDialog() == true)
            {
                GraphSerializer.Serialize(dlg.OpenFile(), myArea.LogicCore.Graph);
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "All files|*.*", Title = "Select layout file", FileName = "laytest.xml" };
            if (dlg.ShowDialog() != true) return;
            try
            {
                myArea.LogicCore.Graph = GraphSerializer.DeSerialize(dlg.OpenFile());
                myArea.GenerateGraph();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to load layout file:\n {0}", ex));
            }
        }       

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            myArea.LogicCore.Graph.Execute();

        }

        private void GraphExecute_OnFinished()
        {
            MessageBox.Show("Execution finished");
        }

        private void Retry_Click(object sender, RoutedEventArgs e)
        {
            myArea.LogicCore.Graph.Execute(true);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            myArea.LogicCore.Graph.Cancel();
        }
    }
}

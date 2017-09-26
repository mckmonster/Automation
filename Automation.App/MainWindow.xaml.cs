using GraphX.Controls;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using GraphX.Controls.Models;
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
            zoomCtrl.IsAnimationEnabled = false;

            if (zoomCtrl.ContextMenu != null)
            {
                foreach (var type in JobFactory.AvailableTypes())
                {
                    var item = new MenuItem()
                    {
                        Header = type.Name,
                        Tag = type
                    };
                    item.Click += MenuItem_OnClick;
                    zoomCtrl.ContextMenu.Items.Add(item);
                }
            }

            myArea.VertexClicked += MyAreaOnVertexClicked;
            myArea.EdgeSelected += MyArea_EdgeSelected;

            GraphExecute.OnFinished += GraphExecute_OnFinished;
        }

        private VertexControl control = null;
        private void MyAreaOnVertexClicked(object sender, VertexClickedEventArgs args)
        {
            if (args.MouseArgs.ChangedButton == MouseButton.Left)
            {
                if (args.Modifiers == ModifierKeys.Control)
                {
                    if (control == null)
                    {
                        control = args.Control;
                    }
                    else if (!control.Equals(args.Control))
                    {
                        myArea.AddLink(control.Vertex as Job, args.Control.Vertex as Job);
                        control = null;
                    }
                }
            }
            else if (args.MouseArgs.ChangedButton == MouseButton.Right)
            {
                if (args.Modifiers == ModifierKeys.Control)
                {
                    myArea.LogicCore.Graph.PropagateNodeProperty(args.Control.Vertex as Job);
                }
            }

            //MessageBox.Show($"{(args.Control.Vertex as Job).Name} {args.MouseArgs.ChangedButton} {args.Modifiers}");
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

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var pos = Mouse.GetPosition(this);
            var menuitem = sender as MenuItem;
            if (menuitem != null)
            {
                var job = JobFactory.CreateJob((menuitem.Tag as Type)?.FullName);
                myArea.AddJob(job, pos);
            }
        }

        private void ZoomCtrl_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (zoomCtrl.ContextMenu != null) zoomCtrl.ContextMenu.IsOpen = true;
        }
    }
}

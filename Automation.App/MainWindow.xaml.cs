using GraphX.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Automation.Core;
using GraphX.Controls.Models;
using Microsoft.Win32;
using Automation.App.Controls;
using Automation.App.Helper;
using Automation.Core.Helpers;

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

            myArea.VertexSelected += MyArea_VertexSelected;
            myArea.EdgeSelected += MyArea_EdgeSelected;

            GraphExecute.OnFinished += GraphExecute_OnFinished;
        }

        private List<Job> selectedVertices = new List<Job>();

        private void MyArea_VertexSelected(object sender, VertexSelectedEventArgs args)
        {
            if (args.MouseArgs.ChangedButton == MouseButton.Right)
            {
                if (args.VertexControl.ContextMenu == null)
                {
                    args.VertexControl.ContextMenu = new ContextMenu();
                    var propagate = new MenuItem()
                    {
                        Header = "Propagate",
                        DataContext = args.VertexControl.DataContext
                    };
                    propagate.Click += Propagate_Click;
                    args.VertexControl.ContextMenu.Items.Add(propagate);
                }
                args.VertexControl.ContextMenu.IsOpen = true;
            }
            else if (args.MouseArgs.ChangedButton == MouseButton.Left)
            {
                if (args.Modifiers == ModifierKeys.Control)
                {
                    var job = args.VertexControl.Vertex as Job;
                    var jobselected = selectedVertices.Contains(job);

                    if (jobselected)
                    {
                        job.Selected = false;
                        job.RaisePropertyChanged("Selected");
                        selectedVertices.Remove(job);
                    }
                    else
                    {
                        job.Selected = true;
                        job.RaisePropertyChanged("Selected");
                        selectedVertices.Add(job);
                    }
                }
            }
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
        }

        private void Propagate_Click(object sender, RoutedEventArgs e)
        {
            myArea.LogicCore.Graph.PropagateNodeProperty((sender as MenuItem).DataContext as Job);
        }

        private void MyArea_EdgeSelected(object sender, EdgeSelectedEventArgs args)
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

        private void Duplicate_Click(object sender, RoutedEventArgs e)
        {
            IntDialogBox dialog = new IntDialogBox();
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var selectedjob = myArea.LogicCore.Graph.Vertices.ToList().FindAll(job => job.Selected);
                myArea.DuplicateNodes(selectedjob,dialog.SelectedValue);
            }
        }

        private void Extended_Click(object sender, RoutedEventArgs e)
        {
            foreach(var vertexControl in myArea.GetAllVertexControls())
            {
                (vertexControl.Vertex as Job).IsExtended = !(vertexControl.Vertex as Job).IsExtended;
            }
        }
    }
}

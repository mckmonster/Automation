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

namespace GraphX_test
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
            CreateRandomGraph();
            myArea.GenerateGraph();
            zoomCtrl.ZoomToFill(); // Zoome au mieux

            KeyDown += MainWindow_KeyDown;
            KeyUp += MainWindow_KeyUp;
            MouseDown += MainWindow_MouseDown;
            myArea.VertexSelected += MyArea_VertexSelected;
            myArea.EdgeSelected += MyArea_EdgeSelected;
                
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && edit)
            {
                edit = false;

                var pos = zoomCtrl.TranslatePoint(e.GetPosition(zoomCtrl), myArea);
                pos.Offset(-22.5, -22.5);

                var chooses = new ChooseJobType();
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

        private void CreateRandomGraph()
        {
            //Create data graph object
            var graph = myArea.LogicCore.Graph;

            var v1 = new HoudiniJob("RockGeneration");
            var v2 = new HoudiniJob("RockClean");
            var v3 = new HoudiniJob("TreeGeneration");
            var v4 = new HoudiniJob("Test");
            var v5 = new HoudiniJob("Finish");

            myArea.AddJob(v1);
            myArea.AddJob(v2);
            myArea.AddJob(v3);
            myArea.AddJob(v4);
            myArea.AddJob(v5);

            myArea.AddLink(v1, v2);
            myArea.AddLink(v1, v3);
            myArea.AddLink(v2, v3);
            myArea.AddLink(v2, v4);
            myArea.AddLink(v3, v5);
            myArea.AddLink(v4, v5);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //TODO make function to save graph
            FileStream stream = File.Open("test.xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            var serializer = new YAXSerializer(typeof(List<GraphSerializationData>));
            using (var textWriter = new StreamWriter(stream))
            {
                serializer.Serialize(myArea.ExtractSerializationData(), textWriter);
                textWriter.Flush();
            }

            var settings = new XmlWriterSettings()
            {
                Indent = true,
            };
            using (var writer = XmlWriter.Create("test2.xml", settings))
            {
                var graph = myArea.LogicCore.Graph;
                graph.SerializeToXml(writer, v => v.Name.ToString(), AlgorithmExtensions.GetEdgeIdentity(graph), "Graph", "Job", "Edge", "");
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            //TODO make function to load graph
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            GraphExecute.Execute(myArea.LogicCore.Graph);
        }

        private void Retry_Click(object sender, RoutedEventArgs e)
        {
            GraphExecute.Execute(myArea.LogicCore.Graph, true);
        }
    }
}

﻿using System.Windows;
using GraphX.Controls;
using Automation.Core;

namespace Automation.App.Gx
{
    public class MyArea : GraphArea<MyVertex, MyEdge, MyGraph>
    {
        public MyArea()
        {
            LogicCore = new MyGXLogicCore();

            SetVerticesDrag(true);
        }

        internal void AddJob(MyVertex job)
        {
            LogicCore.Graph.AddVertex(job);
            AddVertex(job, new VertexControl(job));
        }

        internal void AddJob(MyVertex job, Point pos)
        {
            LogicCore.Graph.AddVertex(job);
            var vc = new VertexControl(job);
            vc.SetPosition(pos);
            AddVertex(job, vc);
        }

        internal void AddLink(MyVertex v1, MyVertex v2)
        {
            var e = new MyEdge(v1, v2, 1);
            LogicCore.Graph.AddEdge(e);

            //VC DataContext will be bound to v1 by default. You can control this by specifing additional property in the constructor
            var vc1 = VertexList[v1];
            var vc2 = VertexList[v2];

            var ec = new EdgeControl(vc1, vc2, e);
            InsertEdge(e, ec); //inserts edge into the start of the children list to draw it below vertices
        }
    }
}


using GraphX.PCL.Logic.Models;
using Automation.Core;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;

namespace Automation.App
{
    public class MyGXLogicCore : GXLogicCore<Job, MyEdge, MyGraph>
    {
        public MyGXLogicCore()
        {
            Graph = new MyGraph();
            DefaultLayoutAlgorithm = GraphX.PCL.Common.Enums.LayoutAlgorithmTypeEnum.Sugiyama;
            DefaultOverlapRemovalAlgorithm = GraphX.PCL.Common.Enums.OverlapRemovalAlgorithmTypeEnum.OneWayFSA;


            DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
            EdgeCurvingEnabled = true;
            AsyncAlgorithmCompute = false;

            DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 20;
            DefaultOverlapRemovalAlgorithmParams.VerticalGap = 20;
        }
    }
}

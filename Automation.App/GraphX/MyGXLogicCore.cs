using GraphX.PCL.Logic.Models;
using Automation.Core;
using GraphX.PCL.Common.Enums;

namespace Automation.App.Gx
{
    public class MyGXLogicCore : GXLogicCore<MyVertex, MyEdge, MyGraph>
    {
        public MyGXLogicCore()
        {
            Graph = new MyGraph();
            DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Sugiyama;
            DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.OneWayFSA;


            DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
            EdgeCurvingEnabled = true;
            AsyncAlgorithmCompute = false;

            DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 20;
            DefaultOverlapRemovalAlgorithmParams.VerticalGap = 20;
        }
    }
}

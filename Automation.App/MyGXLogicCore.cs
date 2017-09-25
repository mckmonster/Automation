using GraphX.PCL.Logic.Models;
using Automation.Core;

namespace Automation.App
{
    public class MyGXLogicCore : GXLogicCore<Job, MyEdge, MyGraph>
    {
        public MyGXLogicCore()
        {
            Graph = new MyGraph();
            DefaultLayoutAlgorithm = GraphX.PCL.Common.Enums.LayoutAlgorithmTypeEnum.Sugiyama;
            DefaultOverlapRemovalAlgorithm = GraphX.PCL.Common.Enums.OverlapRemovalAlgorithmTypeEnum.OneWayFSA;
            EdgeCurvingEnabled = true;
        }
    }
}

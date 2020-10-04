using SolarEnergy.SolarLib.Classes.Collections;
using System.Drawing;
using ZedGraph;

namespace SolarEnergy.UI.Ext
{
    public class ZedGraphControlExt : ZedGraphControl
    {
        public DataRange Range { get { return range; } set { range = value; drawGraphs(); } }
        private DataRange range;

        public ZedGraphControlExt(DataRange range)
        {
            this.range = range;
            drawGraphs();
        }

        /// <summary>
        /// обновление графиков
        /// </summary>
        private void drawGraphs()
        {
            if (range == null)
                return;
            //Прямая радиация
            GraphPane.Title.Text = "";
            GraphPane.XAxis.Title.Text = "t, час";
            GraphPane.YAxis.Title.Text = "Э, кВт*ч/м2";
            GraphPane.GraphObjList.Clear();
            GraphPane.CurveList.Clear();
            PointPairList clist = new PointPairList();
            for (int i = 0; i < range.Count; i++)
                clist.Add(i, range[i].ClearSkyInsolation);
            _ = GraphPane.AddCurve("Прямая", clist, Color.Red);

            PointPairList alist = new PointPairList();
            for (int i = 0; i < range.Count; i++)
                alist.Add(i, range[i].AllSkyInsolation);
            _ = GraphPane.AddCurve("Суммарная", alist, Color.DarkBlue);

            AxisChange();
            Invalidate();
        }
    }
}

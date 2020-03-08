using SolarEnergy.SolarLib.Classes.Structures;
using SolarEnergy.SolarLib.Models.Interfaces;
using SolarEnergy.SolarLib.Models.MonthTransformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEnergy.SolarLib.Classes.Collections
{
    public class DataRange : RawRange
    {
        public DataRange(DataItem item, MonthTransformationModels modelKind) : base()
        {
            if (item == null || modelKind == MonthTransformationModels.None)
                return;

            IMonthTransformerModel model;
            switch (modelKind)
            {
                case MonthTransformationModels.Constant:
                    model = new ConstantModel();
                    break;
                case MonthTransformationModels.LinearInterpolation:
                    model = new LinearInterpolationModel();
                    break;
                default: throw new Exception("Эта модель не реализована");
            }

            this.AddRange(model.GenerateRange(item));
        }

        public DataRange() : this(null, MonthTransformationModels.None)
        { }
    }
}

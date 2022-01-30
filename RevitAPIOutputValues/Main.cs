using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIOutputValues
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            string wallInfo = string.Empty;

            var walls = new FilteredElementCollector(doc)
                .OfClass(typeof(Wall))
                .Cast<Wall>()
                .ToList();

            foreach (Wall wall in walls)
            {
                string wallName = wall.Name;
                string wallVolume = wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsValueString();
                wallInfo += $"{wallName};{wallVolume}{Environment.NewLine}";
            }

            string path = @"G:\Рабочий стол\Повышение квалификации\BIM проектирование\Revit API\Задание 4.1 Вывод значений\wallInfo.txt";
            File.WriteAllText(path, wallInfo);

            return Result.Succeeded;
        }
    }
}


using System;
using System.Runtime.InteropServices;
using InvAddIn;
using Inventor;
using PartsBox.Models;

namespace StressTest
{
    class Program
    {


        private static Application InventorApplication;

        static void Main(string[] args)
        {
            TestAddIn();
        }

        private static void TestAddIn()
        {
			try
			{
                InventorApplication = (Application)Marshal.GetActiveObject("Inventor.Application");
            }
            catch (COMException)
            {
                try
                {
                    //Если не получилось перехватить приложение - выкинется исключение на то,
                    //что такого активного приложения нет. Попробуем создать приложение вручную.
                    var invAppType = Type.GetTypeFromProgID("Inventor.Application");

                    InventorApplication = (Application)Activator.CreateInstance(invAppType);
                    InventorApplication.Visible = true;
                }
                catch (Exception)
                {
                    throw new ApplicationException(@"Не получилось запустить Inventor.");
                }
            }

            // В открытом приложении создаем документ
            PartDoc = (PartDocument)InventorApplication.Documents.Add
            (DocumentTypeEnum.kPartDocumentObject,
                InventorApplication.FileManager.GetTemplateFile
                (DocumentTypeEnum.kPartDocumentObject,
                    SystemOfMeasureEnum.kMetricSystemOfMeasure));

            // Описание документа
            PartDefinition = PartDoc.ComponentDefinition;
            // Инициализация метода геометрии
            TransientGeometry = InventorApplication.TransientGeometry;
            var builder = new PartsBoxBuilder();
            builder.Application = InventorApplication;
            var boxParameters = new PartsBoxParameters();
            builder.BuildPartsBox(boxParameters);
        }

        public static PartComponentDefinition PartDefinition { get; set; }
        public static TransientGeometry TransientGeometry { get; private set; }
        public static PartDocument PartDoc { get; set; }
    }
}

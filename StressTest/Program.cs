
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using InvAddIn;
using Inventor;
using Microsoft.VisualBasic.Devices;
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
            var builder = new PartsBoxBuilder
            {
                Application = InventorApplication
            };
            var boxParameters = new PartsBoxParameters
            {
                CellsInLength = 5,
                CellsInWidth = 5
            };
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var streamWriter = new StreamWriter($"log.txt", true);
            Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var count = 0;
            while (true)
            {
                builder.BuildPartsBox(boxParameters);
                var computerInfo = new ComputerInfo();
                var usedMemory = (computerInfo.TotalPhysicalMemory - computerInfo.AvailablePhysicalMemory) *
                                 0.000000000931322574615478515625;
                streamWriter.WriteLine(
                    $"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");
                streamWriter.Flush();
            }
        }

        public static PartComponentDefinition PartDefinition { get; set; }
        public static TransientGeometry TransientGeometry { get; private set; }
        public static PartDocument PartDoc { get; set; }
    }
}

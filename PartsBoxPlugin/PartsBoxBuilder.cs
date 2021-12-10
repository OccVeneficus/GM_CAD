using Inventor;
using PartsBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvAddIn
{
    /// <summary>
    /// Класс для построения модели ящика для деталей.
    /// </summary>
    public class PartsBoxBuilder
    {
        /// <summary>
        /// Свойство для доступа к объекту Inventor.
        /// </summary>
        public Inventor.Application Application { get; set; }

        /// <summary>
        /// Открытый документ (деталь).
        /// </summary>
        private PartDocument _partDocument;

        /// <summary>
        /// Определение детали.
        /// </summary>
        private PartComponentDefinition _partDefinition;

        /// <summary>
        /// Объект геометрии.
        /// </summary>
        private TransientGeometry _transGeometry;

        /// <summary>
        /// Текущий скетч.
        /// </summary>
        private PlanarSketch _currentSketch;

        /// <summary>
        /// Параметры ящика.
        /// </summary>
        private PartsBoxParameters _partsBoxParameters;

        /// <summary>
        /// Полоска прогресса построения модели.
        /// </summary>
        private ProgressBar progressBar;

        private int _index = 0;

        public int Index 
        {
            get
            {
                _index++;
                return _index;
            }
        }

        /// <summary>
        /// Строит модель ящика для деталей по заданным параметрам.
        /// </summary>
        /// <param name="partsBoxParameters">Параметры ящика.</param>
        public void BuildPartsBox(PartsBoxParameters partsBoxParameters)
        {
            _partsBoxParameters = partsBoxParameters;
            _partDocument = Application.ActiveDocument as PartDocument;
            progressBar = Application.CreateProgressBar(false, 4, "Building parts box.");
            _partDefinition = _partDocument.ComponentDefinition;
            _transGeometry = Application.TransientGeometry;
            progressBar.Message = @"Building box block";
            progressBar.UpdateProgress();
            BuildBoxBlock();
            progressBar.Message = @"Building box cells";
            progressBar.UpdateProgress();
            BuildBoxCells();
            progressBar.Close();
        }

        /// <summary>
        /// Строит блок в котором будут выдавливаться ячейки ящика.
        /// </summary>
        private void BuildBoxBlock()
        {
            CreateSketch(2, "Main block");

            var halfWidth = _partsBoxParameters.Width / 2;
            var halfLength = _partsBoxParameters.Length / 2;
            var leftCornerX = 0 - halfLength;
            var leftCornerY = halfWidth;
            var rightCornerX = halfLength;
            var rightCornerY = 0 - halfWidth;
            DrawRectangle(leftCornerX, leftCornerY, rightCornerX, rightCornerY);

            Extrude(_partsBoxParameters.BoxBottomWidth + _partsBoxParameters.Height, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
        }

        /// <summary>
        /// Строит ячейки ящика.
        /// </summary>
        private void BuildBoxCells()
        {
            CreateSketch(2, "Cells");
            var halfWidth = _partsBoxParameters.Width / 2;
            var halfLength = _partsBoxParameters.Length / 2;
            var leftCornerX = 0 - halfLength;
            var leftCornerY = halfWidth;
            leftCornerX += _partsBoxParameters.OuterWallWidth;
            leftCornerY -= _partsBoxParameters.OuterWallWidth;

            progressBar.Message = @"Drawing box cells";
            progressBar.UpdateProgress();
            DrawRectangularMatrix(leftCornerX, leftCornerY);

            Extrude(_partsBoxParameters.Height - _partsBoxParameters.BoxBottomWidth, PartFeatureExtentDirectionEnum.kNegativeExtentDirection, PartFeatureOperationEnum.kCutOperation);
        }

        /// <summary>
        /// Выдавливание.
        /// </summary>
        /// <param name="distance">Длинна выдавливания.</param>
        /// <param name="extrudeDirection">Направление выдавливания.</param>
        /// <param name="operationType">Тип операции.</param>
        public void Extrude(double distance, PartFeatureExtentDirectionEnum extrudeDirection =
            PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum operationType = PartFeatureOperationEnum.kJoinOperation)
        {
            var extrudeDef = _partDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(_currentSketch.Profiles.AddForSolid(),
                operationType);
            //TODO: To const
            extrudeDef.SetDistanceExtent(distance / 10.0, extrudeDirection);
            _partDefinition.Features.ExtrudeFeatures.Add(extrudeDef);
        }

        /// <summary>
        /// Нарисовать прямоугольник по двум точкам.
        /// </summary>
        /// <param name="pointOneX">X координата верхнего угла.</param>
        /// <param name="pointOneY">Y координата верхнего угла.</param>
        /// <param name="pointTwoX">X координата нижнего угла.</param>
        /// <param name="pointTwoY">Y координата нижнего угла.</param>
        public void DrawRectangle(double pointOneX, double pointOneY, double pointTwoX, double pointTwoY)
        {
            pointOneX /= 10.0;
            pointOneY /= 10.0;
            pointTwoX /= 10.0;
            pointTwoY /= 10.0;
            var cornerPointOne = _transGeometry.CreatePoint2d(pointOneX, pointOneY);
            var cornerPointTwo = _transGeometry.CreatePoint2d(pointTwoX, pointTwoY);
            _currentSketch.SketchLines.AddAsTwoPointRectangle(cornerPointOne, cornerPointTwo);
        }

        /// <summary>
        /// Рисует прямоугольный массив прямоугольников.
        /// </summary>
        /// <param name="startPointX">X координата начала.</param>
        /// <param name="startPointY">Y координата начала.</param>
        private void DrawRectangularMatrix(double startPointX, double startPointY)
        {
            for(var cellsInLength = 0; cellsInLength < _partsBoxParameters.CellsInLength; cellsInLength++)
            {
                var firstStartPointY = startPointY;
                for (var cellsInWidth = 0; cellsInWidth < _partsBoxParameters.CellsInWidth; cellsInWidth++)
                {
                    var endPointX = startPointX + _partsBoxParameters.GetOneCellLength;
                    var endPointY = startPointY - _partsBoxParameters.GetOneCellWidth;
                    DrawRectangle(startPointX, startPointY, endPointX, endPointY);
                    startPointY -= _partsBoxParameters.GetOneCellWidth + _partsBoxParameters.InnerWallWidth;
                }
                startPointY = firstStartPointY;
                startPointX += _partsBoxParameters.GetOneCellLength + _partsBoxParameters.InnerWallWidth;
            }
        }

        /// <summary>
        /// Создать новый скетч.
        /// </summary>
        /// <param name="planeIndex">Индекс оси 1 - ZY, 2 - ZX, 3 - XY.</param>
        /// <param name="name">Имя скетча.</param>
        /// <returns>Новый скетч.</returns>
        private PlanarSketch CreateSketch(int planeIndex, string name = "")
        {
            var plane = _partDefinition.WorkPlanes[planeIndex];
            _currentSketch = _partDefinition.Sketches.Add(plane);
            if(string.IsNullOrEmpty(name))
            {
                name = $"Sketch {Index}";
            }
            _currentSketch.Name = name;
            return _currentSketch;
            
        }
    }
}

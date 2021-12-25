using Inventor;
using PartsBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private ProgressBar _progressBar;

        /// <summary>
        /// Поле для индекса скетчей.
        /// </summary>
        private int _index = 0;

        /// <summary>
        /// Миллиметров в сантиметрах. Константа для перевода значений.
        /// </summary>
        private const double MmInCm = 10.0;

        /// <summary>
        /// Свойство индекса скетчей.
        /// </summary>
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
            _progressBar = Application.CreateProgressBar(false, 4, "Building parts box.");
            _partDefinition = _partDocument.ComponentDefinition;
            var a = _partDefinition.Features;
            _transGeometry = Application.TransientGeometry;
            _progressBar.Message = @"Building box block";
            _progressBar.UpdateProgress();
            BuildBoxBlock();
            _progressBar.Message = @"Building box cells";
            _progressBar.UpdateProgress();
            BuildBoxCells();
            _progressBar.Close();
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

            Extrude(_partsBoxParameters.BoxBottomWidth + _partsBoxParameters.Height,
                PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
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

            _progressBar.Message = @"Drawing box cells";
            _progressBar.UpdateProgress();
            DrawRectangularMatrix(leftCornerX, leftCornerY);

            Extrude(_partsBoxParameters.Height - _partsBoxParameters.BoxBottomWidth,
                PartFeatureExtentDirectionEnum.kNegativeExtentDirection, PartFeatureOperationEnum.kCutOperation);

            CreateSketch(2, "Merge");

            MergeCells(leftCornerX,leftCornerY);
        }

        /// <summary>
        /// Функция слияния ячеек.
        /// </summary>
        /// <param name="startPointX">Начальная точка по X.</param>
        /// <param name="startPointY">Начальная точка по Y.</param>
        private void MergeCells(double startPointX, double startPointY)
        {
            var cellsToMerge = _partsBoxParameters.Cells.Where(x => x.IsMerge || x.HasNeighbor).ToList();
            if (cellsToMerge.Count() <= 1)
            {
                return;
            }
            foreach (var cellInfo in cellsToMerge)
            {
                var startX = (_partsBoxParameters.GetOneCellLength +
                              _partsBoxParameters.InnerWallWidth) * cellInfo.Index.Item1;
                var startY = (_partsBoxParameters.GetOneCellWidth +
                              _partsBoxParameters.InnerWallWidth) * cellInfo.Index.Item2;
                startX = startPointX + startX;
                startY = startPointY - startY;
                var endX = startX + _partsBoxParameters.GetOneCellLength;
                var endY = startY - _partsBoxParameters.GetOneCellWidth;
                
                DrawMergedSketch(cellInfo, endX, endY, startX, startY);
            }
            Extrude(_partsBoxParameters.Height - _partsBoxParameters.BoxBottomWidth,
                PartFeatureExtentDirectionEnum.kNegativeExtentDirection, PartFeatureOperationEnum.kCutOperation);
        }

        /// <summary>
        /// Нарисовать эскиз для слияния ячеек.
        /// </summary>
        /// <param name="cellInfo">Информация о ячейке для слияния.</param>
        /// <param name="endX">Конец квадрата слияния по длине.</param>
        /// <param name="endY">Конец квадрата слияния по ширине.</param>
        /// <param name="startX">Начало квадрата слияния по длине.</param>
        /// <param name="startY">Начало квадрата слияния по ширине.</param>
        private void DrawMergedSketch(CellInfo cellInfo, double endX, double endY,
            double startX, double startY)
        {
            var neighbors = GetNeighbors(cellInfo);
            var isAllNotNull = neighbors[0] != null && neighbors[1] != null && neighbors[2] != null;

            if (isAllNotNull && neighbors[0].IsMerge &&
                neighbors[1].IsMerge && neighbors[2].IsMerge)
            {
                endX += _partsBoxParameters.GetOneCellLength + _partsBoxParameters.InnerWallWidth;
                endY -= _partsBoxParameters.GetOneCellWidth + _partsBoxParameters.InnerWallWidth;
                foreach (var neighbor in neighbors)
                {
                    neighbor.IsMerge = false;
                    var anyNeighbour = GetNeighbors(neighbors[0]).FirstOrDefault(x => x != null);
                    neighbor.HasNeighbor = anyNeighbour != null;
                }
            }
            else if (neighbors[0] != null && neighbors[1] != null && neighbors[0].IsMerge && neighbors[1].IsMerge)
            {
                var oldX = endX;
                endX += _partsBoxParameters.GetOneCellLength + _partsBoxParameters.InnerWallWidth;
                DrawRectangle(startX, startY, endX, endY);
                endX = oldX;
                endY -= _partsBoxParameters.GetOneCellWidth + _partsBoxParameters.InnerWallWidth;
            }
            else if (neighbors[0] != null && neighbors[0].IsMerge)
            {
                endX += _partsBoxParameters.GetOneCellLength + _partsBoxParameters.InnerWallWidth;
            }
            else if (neighbors[1] != null && neighbors[1].IsMerge)
            {
                endY -= _partsBoxParameters.GetOneCellWidth + _partsBoxParameters.InnerWallWidth;
            }

            DrawRectangle(startX, startY, endX, endY);
        }

        /// <summary>
        /// Получить соседей справа, снизу и угловую.
        /// </summary>
        /// <param name="cell">Ячейка, соседей которой надо получить.</param>
        /// <returns>Соседи указанной ячейки.</returns>
        private List<CellInfo> GetNeighbors(CellInfo cell)
        {
            var (widthIndex, lengthIndex) = cell.Index;
            return new List<CellInfo>
            {
                GetCellByIndex(widthIndex + 1, lengthIndex),
                GetCellByIndex(widthIndex, lengthIndex + 1),
                GetCellByIndex(widthIndex + 1, lengthIndex + 1)
            };
        }

        /// <summary>
        /// Получить информацию о ячейке по индексу.
        /// </summary>
        /// <param name="widthIndex">Индекс по ширине.</param>
        /// <param name="lengthIndex">Индекс по высоте.</param>
        /// <returns>Информация о ячейке с соотвествующим индексом.</returns>
        private CellInfo GetCellByIndex(int widthIndex, int lengthIndex)
        {
            return _partsBoxParameters.Cells.FirstOrDefault(x =>
                x.Index == (widthIndex, lengthIndex));
        }

        /// <summary>
        /// Выдавливание.
        /// </summary>
        /// <param name="distance">Длинна выдавливания.</param>
        /// <param name="extrudeDirection">Направление выдавливания.</param>
        /// <param name="operationType">Тип операции.</param>
        public void Extrude(double distance, PartFeatureExtentDirectionEnum extrudeDirection =
            PartFeatureExtentDirectionEnum.kPositiveExtentDirection,
            PartFeatureOperationEnum operationType = PartFeatureOperationEnum.kJoinOperation)
        {
            var extrudeDef =
                _partDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(
                    _currentSketch.Profiles.AddForSolid(), operationType);
            extrudeDef.SetDistanceExtent(distance / MmInCm, extrudeDirection);
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
            pointOneX /= MmInCm;
            pointOneY /= MmInCm;
            pointTwoX /= MmInCm;
            pointTwoY /= MmInCm;
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
                    //TODO: RSDN
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
            //_currentSketch.Name = name;
            return _currentSketch;
            
        }
    }
}

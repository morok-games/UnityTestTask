using _Bludoku.Scripts.Boards;

namespace _Bludoku.Scripts.Score
{
    public class ScoreComboSystem
    {
        private ClearShape _shape;
        private int _counter;

        private const int StartCounter = 5;

        public bool IsActive => _shape != ClearShape.None;
        public ClearShape Shape => _shape;
        public int Counter => _counter;

        public int FigurePlaced(ClearResult result)
        {
            ClearShape shapes = result.ClearedShapes;

            if (shapes == ClearShape.None)
            {
                if (IsActive && --_counter <= 0)
                {
                    Reset();
                }

                return 0;
            }

            if ((shapes & _shape) != ClearShape.None)
            {
                int bonus = _counter;
                _counter++;
                return bonus;
            }

            _shape = shapes;
            _counter = StartCounter;
            return 0;
        }

        public void Reset()
        {
            _shape = ClearShape.None;
            _counter = 0;
        }

        public void Restore(ClearShape shape, int counter)
        {
            if (shape == ClearShape.None || counter <= 0)
            {
                Reset();
                return;
            }

            _shape = shape;
            _counter = counter;
        }
    }
}

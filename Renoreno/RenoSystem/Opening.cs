using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RenoSystem
{
    public class Opening
    {

        //constants for minimun values 
        private const int MIN_WIDTH = 50;
        private const int MIN_HEIGHT = 120;
        private const int MIN_EDGING = 10;

        private int _width;
        private int _height;
        private int _edging;

        public OpeningType Type { get; set; } //get or set type no validation here (enums)

        public int Width
        {
            get { return _width; } 
            set // validations for public mutators using is not !
            {
                if (!Utilities.IsNonZeroPositive(value))
                {
                    throw new ArgumentException($"Width must be a positive non-zero value, but was {value}.");
                }
                if (!Utilities.MeetsMinimumCriteria(value, MIN_WIDTH))
                {
                    throw new ArgumentException($"Width must be at least {MIN_WIDTH} cm, but was {value}.");
                }
                _width = value;
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if (!Utilities.IsNonZeroPositive(value))
                {
                    throw new ArgumentException($"Height must be a positive non-zero value, but was {value}.");
                }
                if (!Utilities.MeetsMinimumCriteria(value, MIN_HEIGHT))
                {
                    throw new ArgumentException($"Height must be at least {MIN_HEIGHT} cm, but was {value}.");
                }
                _height = value;
            }
        }

        public int Edging
        {
            get { return _edging; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Edging must be zero or a positive value, but was {value}.");
                }
                if (value > 0 && !Utilities.MeetsMinimumCriteria(value, MIN_EDGING))
                {
                    throw new ArgumentException($"Edging, if present, must be at least {MIN_EDGING} cm, but was {value}.");
                }
                _edging = value;
            }
        }
        //Area and perimeter

        public int Area // read only 
        {
            get { return Width * Height; }
        }

        public int Perimeter // read only 
        {
            get { return (Width + Height) * 2; }
        }

        //greedy / GROOOVY constructor 
        public Opening(OpeningType type, int width, int height, int edging = 0)
        {
            Type = type;
            Width = width;
            Height = height;
            Edging = edging;
        }

        public override string ToString() // ovearloaded tostring to separate string value
        {
            return $"{Type},{Width},{Height},{Edging}";
        }
    }






}
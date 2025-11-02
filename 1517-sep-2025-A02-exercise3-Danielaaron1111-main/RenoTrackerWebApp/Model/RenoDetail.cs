
namespace RenoSystem
{
    public class RenoDetail
    {
        //data members

        private string _PlanId;
        private int _WallHeight;
        private int _WallWidth;
        private int _OpeningHeight;
        private int _OpeningWidth;
        private string _Description;

        //constants

        private const int WALLMINIMUMHEIGHT = 100;
        private const int WALLMINIMUMWIDTH = 26;
        public const int OPENINGMINIMUMWIDTH = 50;
        public const int OPENINGMINIMUMHEIGHT = 72;
        public const int OPENINGMINIMUMEDGING = 10;

        //properties

        public string PlanId
        {
            get
            {
                return _PlanId;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Wall PlanId is missing! Please enter a PlanId to proceed.");
                }

                _PlanId = value.Trim();
            }
        }

        public int WallHeight
        {
            get
            {
                return _WallHeight;
            }
            private set
            {
                if (!Utilities.IsNonZeroPositive(value))
                {
                    throw new ArgumentException($"The Wall height value: {value} is invalid. Must be a positive value.");
                }

                else if (!Utilities.MeetsMinimumCriteria(value, WALLMINIMUMHEIGHT))
                {
                    throw new ArgumentException($"The Wall height of {value} in too short, should be a minimum of {WALLMINIMUMHEIGHT} cm");
                }

                _WallHeight = value;
            }
        }

        public int WallWidth
        {
            get
            {
                return _WallWidth;
            }
            private set
            {
                if (!Utilities.IsNonZeroPositive(value))
                {
                    throw new ArgumentException($"The Wall width value: {value} is invalid. Must be a positive value.");
                }

                else if (!Utilities.MeetsMinimumCriteria(value, WALLMINIMUMWIDTH))
                {
                    throw new ArgumentException($"The Wall width of {value} in too short, should be a minimum of {WALLMINIMUMWIDTH} cm");
                }

                _WallWidth = value;
            }
        }

        public OpeningType WallOpening
        {
            get;
            private set;
        }

        public int OpeningWidth
        {
            get
            {
                return _OpeningWidth;
            }
            private set
            {
                if (WallOpening == OpeningType.None)
                {
                    _OpeningWidth = 0;
                }
                else
                {
                    if (!Utilities.IsNonZeroPositive(value))
                    {
                        throw new ArgumentException($"The Opening width value: {value} is invalid. Must be a positive value.");
                    }

                    if (!Utilities.MeetsMinimumCriteria(value, OPENINGMINIMUMWIDTH))
                    {
                        throw new ArgumentException($"The Opening width of {value} in too narrow, should be a minimum of {OPENINGMINIMUMWIDTH} cm"); // CORRECTED THE message TO USE OPENINGMINIMUMWIDTH
                    }

                    if (value > _WallWidth * .9)
                    {
                        throw new ArgumentException($"The Opening width of {value} in too large for wall width {WallWidth} cm");
                    }

                    _OpeningWidth = value;
                }
               
            }
        }

        public int OpeningHeight
        {
            get
            {
                return _OpeningHeight;
            }
            private set
            {
                if (WallOpening == OpeningType.None)
                {
                    _OpeningHeight = 0;
                }
                else
                {
                    if (!Utilities.IsNonZeroPositive(value))
                    {
                        throw new ArgumentException($"The Opening height value: {value} is invalid. Must be a positive value.");
                    }

                    if (!Utilities.MeetsMinimumCriteria(value, OPENINGMINIMUMHEIGHT))
                    {
                        throw new ArgumentException($"The Opening height of {value} in too narrow, should be a minimum of {WALLMINIMUMHEIGHT} cm");
                    }

                    if (value > WallHeight * .9)
                    {
                        throw new ArgumentException($"The Opening height of {value} in too large for wall height {WallHeight} cm");
                    }

                    _OpeningHeight = value;
                }

            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Plan description is missing! Please enter a description to proceed.");
                }

                _Description = value.Trim();
            }
        }


        //behaviours

        public RenoDetail(string planid, string descripiton, int wallwidth, int wallheight, OpeningType wallopening, int openingwidth = 0, int openingheight = 0)
        {
            PlanId = planid;
            Description = descripiton;
            WallWidth = wallwidth;
            WallHeight = wallheight;
            WallOpening = wallopening;
            OpeningWidth = openingwidth;
            OpeningHeight = openingheight;


            if (WallOpening != OpeningType.None)
            {
                ValidateOpeningToWallSurfaceRatio();
            }
        }

        private void ValidateOpeningToWallSurfaceRatio()
        {
            int WallArea = _WallWidth * _WallHeight;
            int OpeningArea = _OpeningWidth * _OpeningHeight;
            double MinWallArea = 0.90 * WallArea;
            if (OpeningArea >= MinWallArea)
            {
                throw new ArgumentException($"Opening limit exceeded: The area for the current opening is {OpeningArea}cm. It should be less than {MinWallArea}cm that is 90% of the wall area.");
            }
        }

        public override string ToString()
        {
            return $"{PlanId},{Description},{WallWidth},{WallHeight},{WallOpening.ToString()},{OpeningWidth},{OpeningHeight}";
        }

        public static RenoDetail Parse(string value)
        {

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("No data supplied");
            }
            string[] data = value.Split(',');
            if (data.Length != 7)
            {
                throw new FormatException($"Reno Detail input {value} incorrect format");
            }

            return new RenoDetail(data[0].Trim(), 
                                  data[1].Trim(),
                                  int.Parse(data[2]),
                                  int.Parse(data[3]),
                                  (OpeningType)Enum.Parse(typeof(OpeningType), data[4]), 
                                  int.Parse(data[5]),
                                  int.Parse(data[6]));
        }

    }
}

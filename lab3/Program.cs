using System;

namespace d0gge
{
    abstract class Plane : IComparable<Plane>
    {

        public Plane() {}
        public Plane(string name, int race, double mass, int seatNumber, string[] crew)
        {
            _mass = mass;
            _seatNumber = seatNumber;
            _crew = crew;
            _name = name;
            _race = race;
        }
        protected abstract double CalculateTakeOffMass();
        public int CompareTo(Plane other)
        {
            if(!Convert.ToBoolean(TakeOffMass.CompareTo(other.TakeOffMass)))
            {
                return Race.CompareTo(other.Race);
            }
            else
            {
                return TakeOffMass.CompareTo(other.TakeOffMass);
            }
        }
        protected int Race { get { return _race; } private set { _race = value; }}
        protected string? Name { get { return _name; } private set { _name = value; }}
        protected double Mass { get { return _mass; } private set { _mass = value; }}
        protected double TakeOffMass { get { return _takeOffMass; } set { _takeOffMass = value; }}
        protected string? Type { get { return _type; } set { _type = value; } }
        protected string[]? Crew {get { return _crew; } private set { _crew = value; }}
        protected int SeatNumber { get { return _seatNumber; } private set { _seatNumber = value; }}
        public override string ToString() => $"Type: {Type}. {Name} "
            + $"race #{Race} take off mass: {TakeOffMass}";

        protected double _mass;
        protected double _takeOffMass;
        protected string? _type;
        protected string[]? _crew;
        protected int _seatNumber;
        protected string? _name;
        protected int _race;
    }

    class PassengerPlane : Plane
    {
        private const double AveragePersonMass = 62;

        public PassengerPlane(string name, int race, double mass, int seatNumber, string[] crew)
            : base(name, race, mass, seatNumber, crew)
        { 
            _type = "PASSENGER";
            _takeOffMass = CalculateTakeOffMass();
        }

        protected override double CalculateTakeOffMass()
            => SeatNumber * AveragePersonMass;
    }

    class CargoPlane : Plane
    {
        public CargoPlane(string name, int race, double mass, string[] crew)
            : base(name, race, mass, 0, crew)
        {
            _type = "CARGO";
            _takeOffMass = CalculateTakeOffMass();
        }

        protected override double CalculateTakeOffMass()
            => _cargoMass + _mass;
        
        public double CargoMass { get { return _cargoMass; } private set { _cargoMass = value; }}

        private double _cargoMass;
    }

    class Airline
    {
        static int s_raceNumber = 0;

        public Airline() {}

        public void AddPlane(Plane plane)
        {
            _planes.Add(plane);
            s_raceNumber++;
        }
        public void SortFlights()
        {
            _planes.Sort((lhs, rhs) => lhs.CompareTo(rhs));
        } 

        public void PrintFilgths()
        {
            for (int i = 0; i < 6; ++i)
            {
                Console.WriteLine(_planes[i]);
            }
        }

        public int AverageTakeOffMass { get { return _averageTakeOffMass; } private set { _averageTakeOffMass = value; }}
        private List<Plane> _planes = new List<Plane>();
        private int _averageTakeOffMass;
        public List<Plane> Planes { get { return _planes; } }
    }

    class Program
    {
        static void Main()
        {
        } 
    }
}
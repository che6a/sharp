namespace d0gge
{
    public class CargoPlane : Plane
    {
        public CargoPlane() {}
        public CargoPlane(string name, int race, double mass, double cargoMass, string[] crew)
            : base(name, race, mass, 0, crew)
        {
            _type = "CARGO";
            _cargoMass = cargoMass;
            _takeOffMass = CalculateTakeOffMass();
        }

        protected override double CalculateTakeOffMass()
            => _cargoMass + _mass;
        
        public double CargoMass { get { return _cargoMass; } set { _cargoMass = value; } }

        private double _cargoMass;
    }
}
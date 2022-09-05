namespace d0gge
{
    public class PassengerPlane : Plane
    {
        private const double AveragePersonMass = 62;

        public PassengerPlane() {}
        public PassengerPlane(string name, int race, double mass, int seatNumber, string[] crew)
            : base(name, race, mass, seatNumber, crew)
        { 
            _type = "PASSENGER";
            _takeOffMass = CalculateTakeOffMass();
        }

        protected override double CalculateTakeOffMass()
            => SeatNumber * AveragePersonMass;
    }

}
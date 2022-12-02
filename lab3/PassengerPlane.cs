namespace d0gge
{
  public class PassengerPlane : Plane
  {
    private const double AveragePersonMass = 62;

    public PassengerPlane() { }
    // конструктор создания самолета
    // string name -- имя самолета 
    // int race -- Номер рейса
    // double mass -- масса
    // int seatNumber -- количество мест
    // string[] crew -- экипаж самолета 
    public PassengerPlane(string name, int race, double mass, int seatNumber, string[] crew)
        : base(name, race, mass, seatNumber, crew)
    {
      _type = "PASSENGER";
      _takeOffMass = CalculateTakeOffMass();
    }

    // Метод расчёта взлётной массы
    protected override double CalculateTakeOffMass()
        => SeatNumber * AveragePersonMass;
  }

}
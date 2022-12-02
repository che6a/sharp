using System.Xml.Serialization;

namespace d0gge
{
  [XmlInclude(typeof(PassengerPlane))]
  [XmlInclude(typeof(CargoPlane))]
  public abstract class Plane : IComparable<Plane>
  {

    public Plane() { }
    public Plane(string name, int race, double mass, int seatNumber, string[] crew)
    {
      _mass = mass;
      _seatNumber = seatNumber;
      _crew = crew;
      _name = name;
      _race = race;
    }

    // Абстрактный метод расчета взлетной массы
    protected abstract double CalculateTakeOffMass();
    // Метод для сравнения самолетов
    public int CompareTo(Plane other)
    {
      if (!Convert.ToBoolean(_takeOffMass.CompareTo(other.TakeOffMass)))
      {
        return _race.CompareTo(other.Race);
      }
      else
      {
        return _takeOffMass.CompareTo(other.TakeOffMass);
      }
    }
    public int Race { get { return _race; } set { _race = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public double Mass { get { return _mass; } set { _mass = value; } }
    public double TakeOffMass { get { return _takeOffMass; } set { _takeOffMass = value; } }
    public string Type { get { return _type; } set { _type = value; } }
    public string[] Crew { get { return _crew; } set { _crew = value; } }
    public int SeatNumber { get { return _seatNumber; } }
    public override string ToString() => $"Type: {_type}. {_name} "
        + $"race #{_race} take off mass: {_takeOffMass}"
        + $" Crew: [{_crew[0]}, {_crew[1]}, {_crew[2]}, {_crew[3]}]";

    protected double _mass;
    protected double _takeOffMass;
    protected string _type = "";
    protected string[] _crew = new string[0];
    protected int _seatNumber;
    protected string _name = "";
    protected int _race;
  }

}
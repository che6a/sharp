using System.Xml.Serialization;

namespace d0gge
{
    public class Airline
    {
        public static int s_raceNumber = 0;
        static double s_averageTakeOffMass = 0;

        public Airline() {}

        public int RaceNumber { get { return s_raceNumber; } }
        public void AddPlane(Plane plane)
        {
            _planes.Add(plane);
            _takeOffMassSum += plane.TakeOffMass;
            s_averageTakeOffMass = _takeOffMassSum / _planes.Count;
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

        public void PrintLastFlights()
        {
            Console.WriteLine(_planes[_planes.Count - 1]);
            Console.WriteLine(_planes[_planes.Count - 2]);
        }

        public void ToXml(string fileName)
        {
            var formatter = new XmlSerializer(typeof(List<Plane>));

            using (var stream = File.OpenWrite(fileName))
            {
                formatter.Serialize(stream, _planes);
                stream.Flush();
            }
        }

        public static Airline FromXml(string fileName)
        {
            Airline airline = new Airline();
            var formatter = new XmlSerializer(typeof(List<Plane>));
            using (var stream = File.OpenRead(fileName))
            {
                var planes = formatter.Deserialize(stream) as IEnumerable<Plane>; 
                if (planes != null) 
                {
                    airline.Planes.AddRange(planes);
                }
            }
            return airline;
        }


        public double AverageTakeOffMass { get { return s_averageTakeOffMass; } }
        private List<Plane> _planes = new List<Plane>();
        private int _averageTakeOffMass;
        private double _takeOffMassSum;
        public List<Plane> Planes { get { return _planes; } }
    }
}
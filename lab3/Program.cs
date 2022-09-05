namespace d0gge
{
    public class Program
    {
        static void Main()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Variant #2. Gavrilov E.A. KE-219");
                Console.WriteLine("1. Sort test.");
                Console.WriteLine("2. Print test.");
                Console.WriteLine("3. Print last test.");
                Console.WriteLine("4. Average take off mass test.");
                Console.WriteLine("5. Serialization test.");
                Console.WriteLine("6. Wrong format test.");
                Console.WriteLine("0. Exit.");

                Console.WriteLine();
                Console.WriteLine("Choose test: ");
                Test.Type number = (Test.Type) Int32.Parse(Console.ReadLine());
                if (number is Test.Type.Exit)
                {
                    break;
                }
                switch (number)
                {
                    case Test.Type.Sort:
                        Test.SortTest();
                        break;
                    case Test.Type.Print:
                        Test.PrintTest();
                        break;
                    case Test.Type.PrintLast:
                        Test.PrintLastTest();
                        break;
                    case Test.Type.AverageTakeOffMass:
                        Test.AverageTakeOffMassTest();
                        break;
                    case Test.Type.Serialization:
                        Test.SerializationTest();
                        break;
                    case Test.Type.WrongFormat:
                        Test.WrongFormatTest();
                        break;
                    default:
                        break;
                }
            }
            Console.Clear();
        } 
    }
}
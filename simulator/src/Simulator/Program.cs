using Simulator.Instructions;
using Simulator.Instructions.Commands;
using Simulator.Instructions.Queries;
using Simulator.Robots;

var simulator = new RobotSimulator();
var parser = new InputParser();

Console.WriteLine("Toy Robot Simulator");
Console.WriteLine("==================");
Console.WriteLine("Commands: PLACE X,Y,DIRECTION | PLACE X,Y | MOVE | LEFT | RIGHT | REPORT | EXIT");
Console.WriteLine();

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        continue;

    if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
        break;

    var instruction = parser.Parse(input);
    if (instruction != null)
    {
        switch (instruction)
        {
            case ICommand command:
                simulator.Execute(command);
                break;
            
            case IQuery query:
                var result = simulator.Query(query);
                Console.WriteLine(result);
                break;
        }
    }
}

Console.WriteLine("Goodbye!");
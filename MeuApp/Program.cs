namespace MeuApp;

enum Choice { Rock, Paper, Scissors }
enum Result { Draw, Win, Lose }

static class Program
{
    static Result CheckResult(Choice player, Choice computer) =>
        // A ternary operator checks if the player's choice is equal to the computer's choice.
        // Followed by a switch expression that determines the outcome based on the player's choice.
        player == computer ? Result.Draw : player switch
        {
            Choice.Rock when computer == Choice.Scissors => Result.Win,
            Choice.Paper when computer == Choice.Rock => Result.Win,
            Choice.Scissors when computer == Choice.Paper => Result.Win,
            _ => Result.Lose
        };

    static void Main()
    {
        // The Main method initializes the game, prompting the user for input and displaying the result.
        // It uses a random number generator to simulate the computer's choice.
        var rand = new Random();

        Console.Write("Choose Rock, Paper or Scissors: ");
        if (!Enum.TryParse<Choice>(Console.ReadLine(), true, out var player))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        // Generate a random choice for the computer.
        var computer = (Choice)rand.Next(0, 3);

        Console.WriteLine($"You: {player}, CPU: {computer}");
        Console.WriteLine(CheckResult(player, computer));
    }
}


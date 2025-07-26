# .Net Jokenpô

I got curious after watching the [video](https://www.youtube.com/watch?v=r_SRTbbtlD4&ab_channel=JovemTranquil%C3%A3o) by `Jovem Tranquilão` about the challenge for a Senior Java position. So, I decided to develop my own implementation in .Net.

🟡 **ATTENTION**: Under no circumstances am I equating my code with what was developed during the video. I developed it calmly, without the pressure of having people evaluating me while I was coding.

For performance reasons, [Jovem Tranquilão](https://www.youtube.com/@JovemTranquil%C3%A3o) chose to use Enums for comparisons to generate results. As we know, this translates to high-performance integer comparisons. So, I decided to follow the same idea.

## Breaking Down the Code

Below, I present the code in parts and the motivations for each decision made.

### Enums

Enums used for comparison and presenting results:

```csharp
enum Choice { Rock, Paper, Scissors }
enum Result { Draw, Win, Lose }
```

### Result Checking

To define results, I decided to use a static `expression-bodied` function (equivalent to arrow functions in JS), obviously with a single responsibility. This allows the complexity to remain O(1), since regardless of input size, the function executes a fixed number of operations.

```csharp
static Result CheckResult(Choice player, Choice computer) =>
    player == computer ? Result.Draw : player switch
    {
        Choice.Rock when computer == Choice.Scissors => Result.Win,
        Choice.Paper when computer == Choice.Rock => Result.Win,
        Choice.Scissors when computer == Choice.Paper => Result.Win,
        _ => Result.Lose
    };
```

#### Ternary

To define the result, I used a ternary:

```csharp
condition ? value_if_true : value_if_false
```

Where, if the condition is true, it immediately returns `Result.Draw`, indicating that both Player and Computer made the same choice.

#### Pattern Matching

For the negative case, I used `Pattern Matching`:

```csharp
player switch
{
    Choice.Rock when computer == Choice.Scissors => Result.Win,
    Choice.Paper when computer == Choice.Rock => Result.Win,
    Choice.Scissors when computer == Choice.Paper => Result.Win,
    _ => Result.Lose
};
```

This way, you can establish the conditions in which the Player will be the winner, in the lines using `when`.

And also, a default value for the other cases, where the Player will be the loser, in the line starting with `_`.

Pattern matching in .NET is a feature that allows you to compare values with specific patterns and execute actions based on those patterns, making the code more expressive and safe.

In C#, pattern matching can be used in `switch`, `is`, and `switch expressions`.
You can combine types, values, properties, and conditions (`when`) to decide the program flow.

The C# compiler can optimize the switch in different ways, depending on the type and number of cases:

- For enums or integers with many cases, the compiler can generate a jump table, allowing direct access to the correct case, making execution very fast.

  - A jump table is a structure used by the compiler to optimize multi-choice instructions, like switch. It works like an array of code addresses (or indexes), where each position corresponds to a case in the switch.

    When the program executes the switch, it calculates the table index based on the tested value and "jumps" directly to the corresponding code block, without needing to compare each case sequentially.

    Advantage:

    Makes execution very fast, especially when there are many cases, as it avoids multiple comparisons.

- For a few cases, like in the code where there are only three options, the compiler usually translates the `switch` into a sequence of `if-else` comparisons, since there is no significant gain with a jump table. In this case, the main advantage of the switch is code clarity and organization.

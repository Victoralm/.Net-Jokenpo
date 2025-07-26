# .Net Jokenpô

Eu fiquei curioso com o [vídeo](https://www.youtube.com/watch?v=r_SRTbbtlD4&ab_channel=JovemTranquil%C3%A3o) do `Jovem Tranquilão`, sobre o desafio para a posição de Java Sênior. E acabei por desenvolver a minha própria implementação em .Net.

🟡 ATENÇÃO: Não estou, sob nenhuma circunstancia, fazendo a equivalencia entre o meu código e o que foi desenvolvido durante o vídeo. Eu desenvolvi com calma, sem a pressão de ter pessoas me avaliando enquanto eu desenvolvia o código em questão.

Para fins de performance, o [Jovem Tranquilão](https://www.youtube.com/@JovemTranquil%C3%A3o) fez a escolha de fazer as comparações para gerar os resultados usando Enum. QUê, como sabemos, se traduz em uma comparação de alta performance entre inteiros. Dessa forma, resolvi seguir a mesma ideia.

## Destrinchando o código

A seguir, apresento o código por partes e as motivações para cada decisão tomaada.

### Enums

Enums utilizados para comparação e apresentação dos resultados:

```csharp
enum Choice { Rock, Paper, Scissors }
enum Result { Draw, Win, Lose }
```

### Checagem de resultado

Para a definição de resultados, decidi usar uma função `expression-bodied` (equivalente às arrow functions em JS) estática, obviamente de responsabilidade única. Na qual é possível manter a complexidade em O(1) uma vez que, independentemente do tamanho da entrada, a função executa um número fixo de operações.

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

#### Ternário

Para a definição do resultado, usei um ternário:

```csharp
condição ? valor_se_verdadeiro : valor_se_falso
```

Onde, caso a condição seja verdadeira, retorna imediatamente `Result.Draw`. Indicando que ambos, Player e Computer, fizeram a mesma escolha.

#### Pattern Matching

Para o caso negativo, usei um `Pattern Matching`:

```csharp
player switch
{
    Choice.Rock when computer == Choice.Scissors => Result.Win,
    Choice.Paper when computer == Choice.Rock => Result.Win,
    Choice.Scissors when computer == Choice.Paper => Result.Win,
    _ => Result.Lose
};
```

Dessa forma, é possível estabelecer as condições em que o Player será vencedor. Nas linhas usando `when`.

E, ainda, um valor padrão para os demais casos, onde o Player será o perdedor. Na linha iniciada com `_`.

Pattern matching em .NET é um recurso que permite comparar valores com padrões específicos e executar ações baseadas nesses padrões, tornando o código mais expressivo e seguro.

No C#, pattern matching pode ser usado em `switch`, `is`, e `expressões switch`.
Você pode combinar tipos, valores, propriedades e condições (`when`) para decidir o fluxo do programa.

O compilador C# pode otimizar o switch de diferentes formas, dependendo do tipo e do número de casos:

- Para enums ou inteiros com muitos casos, o compilador pode gerar uma tabela de saltos (jump table), permitindo acesso direto ao caso correto, tornando a execução muito rápida.

  - Uma tabela de saltos (jump table) é uma estrutura usada pelo compilador para otimizar instruções de múltipla escolha, como o switch. Ela funciona como um array de endereços de código (ou índices), onde cada posição corresponde a um caso do switch.

    Quando o programa executa o switch, ele calcula o índice da tabela com base no valor testado e "salta" diretamente para o bloco de código correspondente, sem precisar comparar cada caso sequencialmente.

    Vantagem:

    Torna a execução muito rápida, especialmente quando há muitos casos, pois evita múltiplas comparações.

- Para poucos casos, como no código onde são apenas três opções, o compilador geralmente traduz o `switch` para uma sequência de comparações `if-else`, pois não há ganho significativo com uma tabela de saltos. Neste caso, a principal vantagem do switch aqui é a clareza e organização do código.

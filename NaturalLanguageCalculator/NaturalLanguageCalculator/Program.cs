using NaturalLanguageCalculator.Domain;
using NaturalLanguageCalculator.Domain.Interface;
using NaturalLanguageCalculator.Domain.Operator;
using NaturalLanguageCalculator.Domain.Parser;
using Ninject;
using System;

namespace NaturalLanguageCalculator
{
    public class Program
    {
        private static IKernel _kernel;

        public static void Main(string[] args)
        {
            const string helpCommand = "help";
            const string exitCommand = "exit";

            _kernel = SetupIoc();
            var calculator = _kernel.Get<ICalculator>();

            Console.WriteLine("Natural Language Calculator");
            Console.WriteLine("Copyright © 2016 Anthony Fung");
            Console.WriteLine();
            Console.WriteLine($"type \"{helpCommand}\" for syntax, \"{exitCommand}\" to quit");
            Console.WriteLine();

            var input = string.Empty;

            while (input != exitCommand)
            {
                try
                {
                    Console.Write("> ");
                    input = Console.ReadLine();

                    if (input == helpCommand)
                    {
                        OutputHelp();
                    }
                    else
                    {
                        var output = calculator.Evaluate(input);
                        Console.WriteLine(output);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Did not understand \"{input}\"");
                }
            }
        }

        private static IKernel SetupIoc()
        {
            var kernel = new StandardKernel();

            kernel.Bind<ICalculator>().To<Calculator>().InSingletonScope();
            kernel.Bind<INumberNamer>().To<NumberNamer>().InSingletonScope();
            kernel.Bind<INumberParser>().To<NumberParser>().InSingletonScope();
            kernel.Bind<IOperator>().To<SumOperator>().InSingletonScope();
            kernel.Bind<IOperator>().To<DifferenceOperator>().InSingletonScope();
            kernel.Bind<IOperatorParser>().To<OperatorParser>().InSingletonScope();

            return kernel;
        }

        private static void OutputHelp()
        {
            Console.WriteLine("Syntax: <number> <operator> <number>");
            Console.WriteLine("Example: \"three plus six\"");
            Console.WriteLine();
            Console.WriteLine("Vocabulary list:");

            var numberParser = _kernel.Get<INumberParser>();
            var operationParser = _kernel.Get<IOperatorParser>();

            OutputParserVocabulary(numberParser);
            OutputParserVocabulary(operationParser);

            Console.WriteLine();
        }

        private static void OutputParserVocabulary(IParser parser)
        {
            var vocabulary = parser.GetVocabularyList();
            foreach (var item in vocabulary)
            {
                Console.WriteLine(item);
            }
        }
    }
}

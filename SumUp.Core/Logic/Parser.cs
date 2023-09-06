namespace SumUp.Core.Logic
{
    using SumUp.Core.Options;

    using Commander.NET;
    using Commander.NET.Exceptions;

    internal static class Parser
    {
        public static SumUpOptions? Parse(string[] args)
        {
            CommanderParser<SumUpOptions> parser = new CommanderParser<SumUpOptions>();

            if (args.Length == 0)
            {
                Console.WriteLine(parser.Usage());
                return null;
            }

            try
            {
                return parser.Parse(args);
            }
            catch (ParameterMissingException ex)
            {
                Console.WriteLine($"Missing parameter: {ex.ParameterName}");
            }
            catch (Exception ex) when (ex.InnerException?.GetType() == typeof(ParameterMissingException))
            {
                Console.WriteLine($"Missing parameter: ${((ParameterMissingException)ex.InnerException).ParameterName}");
            }
            catch (ParameterMatchException ex)
            {
                Console.WriteLine($"Invalid format for argument: {ex.ParameterName}");
            }
            catch (Exception)
            {
                Console.WriteLine(parser.Usage());
            }

            return null;
        }
    }
}

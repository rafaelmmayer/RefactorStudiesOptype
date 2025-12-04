using System.Reflection;
using FluentValidation;
using FluentValidation.Results;

namespace Core.Domain.Studies.Validator;

public static class StudyValidatorRegistry
{
    private static readonly Dictionary<Type, Func<object, ValidationResult>> Validators = new();

    static StudyValidatorRegistry()
    {
        var validators = typeof(ICoreMarker)
            .Assembly.GetTypes()
            .Where(t =>
                t is { IsAbstract: false, IsGenericTypeDefinition: false }
                && typeof(IValidator).IsAssignableFrom(t)
            );

        foreach (var validatorType in validators)
        {
            var iface = validatorType
                .GetInterfaces()
                .First(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)
                );

            var inputType = iface.GetGenericArguments()[0];

            var validatorInstance = (IValidator)Activator.CreateInstance(validatorType)!;

            var validateMethod = typeof(StudyValidatorRegistry)
                .GetMethod(
                    nameof(BuildValidatorDelegate),
                    BindingFlags.NonPublic | BindingFlags.Static
                )!
                .MakeGenericMethod(inputType);

            var del = 
                (Func<object, ValidationResult>)validateMethod.Invoke(null, [validatorInstance])!;

            Validators[inputType] = del;
        }
    }

    private static Func<object, ValidationResult> BuildValidatorDelegate<T>(IValidator validator)
    {
        var typedValidator = (IValidator<T>)validator;

        return input =>
        {
            var ctx = new ValidationContext<T>((T)input);
            return typedValidator.Validate(ctx);
        };
    }

    public static Func<object, ValidationResult> GetValidator(Type inputType)
    {
        if (!Validators.TryGetValue(inputType, out var fn))
        {
            throw new Exception($"Validador não encontrado para {inputType.Name}");
        }

        return fn;
    }
}

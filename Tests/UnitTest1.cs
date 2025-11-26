using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using SourceGenarator;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public async Task ShouldValidate()
    {
        var context = new CSharpSourceGeneratorTest<StudyTypesGenerator, DefaultVerifier>
        {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestCode = "class Dummy { }"
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(StudyTypesGenerator), "Sample.g.cs", """
            internal static class Sample
            {
                public const string AssemblyName = "TestProject";
            }
            """));

        await context.RunAsync();
    }
}
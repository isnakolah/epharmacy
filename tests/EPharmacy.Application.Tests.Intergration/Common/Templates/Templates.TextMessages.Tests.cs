using EPharmacy.Application.Common.Templates;

namespace EPharmacy.Application.Tests.Intergration.Common.TemplatesTests;

public class TextMessagesTemplatesTests
{
    [Fact]
    public void NewPrescription_ShouldReturnMessageWithName_WhenNameProvided()
    {
        // Arrange
        var name = "Daniel";

        // Act
        var template = Templates.TextMessages.NewPrescription(name);

        // Assert
        template.Should().Contain(name);
    }
}

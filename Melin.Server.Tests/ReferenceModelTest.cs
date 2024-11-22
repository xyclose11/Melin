using System.ComponentModel.DataAnnotations;
using Melin.Server.Models;


namespace Melin.Server.Tests;

public class ReferenceModelTest
{
    [Fact]
    public void CreateNewReference_NotNull()
    {
        var reference = new Reference();

        Assert.NotNull(reference);
    }

    [Fact]
    public void SetTitleTooLong_Return_Error()
    {
        var reference = new Reference
        {
            Title = "This is an example of a title that is too long and should not be allowed to be set in the" +
                    "applicatio. The limit should be around 256 characters at most, although there is a change that" +
                    "there is a reference with a longer title. If this is the case we can take a more thorough look" +
                    "and allow special cases to occur. This is an example of a title that is too long and should not be allowed to be set in the" +
                    "applicatio. The limit should be around 256 characters at most, although there is a change that" +
                    "there is a reference with a longer title. If this is the case we can take a more thorough look" +
                    "and allow special cases to occur. This is an example of a body of words"
        };
        
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(reference);
        bool isValid = Validator.TryValidateObject(reference, context, validationResults, true);
        
        Assert.Single(validationResults);
        Assert.False(isValid, "Validation should fail due to exceeding MaxLength");
    }
    
    [Fact]
    public void Create_Reference_Email_Validation_Returns_Validation_Error()
    {
        var reference = new Reference
        {
            Title = "Test",
            OwnerEmail = "thisIsAnInvalidEmail;[]"
        };

        var results = new List<ValidationResult>();
        var validationContext = new ValidationContext(reference);
        bool isValid = Validator.TryValidateObject(reference, validationContext, results, true);

        Assert.Single(results);
        Assert.False(isValid);
    }

    [Fact]
    public void CreateNewReference_IdRequired()
    {
        var reference = new Reference();

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(reference);
        var isValid = Validator.TryValidateObject(reference, validationContext, validationResults, true);

        Assert.NotNull(reference);
        Assert.True(isValid);
        Assert.IsType<int>(reference.Id);
    }
}
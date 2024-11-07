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
                    "and allow special cases to occur. This is an example of a body of words t"
        };

        var validation = new List<ValidationResult>();
        var context = new ValidationContext(reference, null, null);
        bool isValid = Validator.TryValidateObject(reference, context, validation, true);
        
        Assert.False(isValid, "Validation should fail due to exceeding MaxLength");
        // Assert.Contains(validation,
        //     validation =>
        //         validation.ErrorMessage.Contains(
        //             "The field Title must be a string or array type with a max length of '256' characters."));
    }

    [Fact]
    public void CreateNewReference_IdRequired()
    {
        var reference = new Reference();

        Assert.NotNull(reference.Id);
    }
}
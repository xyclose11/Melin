using System.Globalization;
using Melin.Server.Models;
using Melin.Server.Models.Binders;
using Melin.Server.Models.References;
using Newtonsoft.Json;

namespace Melin.Server.Tests.JSONConverterTests;

public class ReferenceJSONConverterTest
{
    private readonly ReferenceConverter _referenceConverter = new ReferenceConverter();
    
    [Fact]
    public void ValidJSON_SuccessfullyParsesReference_Returns_BookObject()
    {
        // Arrange
         var jsonInput =
            "{\"URL\":\"https://books.google.com/books/about/1984.html?hl=&id=Dd9N0AEACAAJ\"," +
            "\"abstractNote\":\"Written more than 70 years ago, 1984 was George Orwell’s chilling prophecy about the future. And while 1984 has come and gone, his dystopian vision of a government that will do anything to control the narrative is timelier than ever... • Nominated as one of America’s best-loved novels by PBS’s The Great American Read • “The Party told you to reject the evidence of your eyes and ears. It was their final, most essential command.” Winston Smith toes the Party line, rewriting history to satisfy the demands of the Ministry of Truth. With each lie he writes, Winston grows to hate the Party that seeks power for its own sake and persecutes those who dare to commit thoughtcrimes. But as he starts to think for himself, Winston can’t escape the fact that Big Brother is always watching... A startling and haunting novel, 1984 creates an imaginary world that is completely convincing from start to finish. No one can deny the novel’s hold on the imaginations of whole generations, or the power of its admonitions—a power that seems to grow, not lessen, with the passage of time.\"," +
            "\"applicationNumber\":\"\",\"assignee\":\"\",\"audioRecordingFormat\":\"\",\"billNumber\":\"\",\"blogTitle\":\"\",\"bookTitle\":\"\",\"caseName\":\"\",\"code\":\"\",\"codePages\":\"\",\"codeVolume\":\"\",\"committee\":\"\",\"company\":\"\",\"conferenceName\":\"\",\"country\":\"\",\"court\":\"\",\"creators\":[{\"type\":[0],\"firstName\":\"George\",\"lastName\":\"Orwell\"}],\"dataType\":\"\",\"date\":\"\",\"dateDecided\":\"\",\"datePublished\":\"1950/7/1\",\"distributor\":\"\",\"docketNumber\":\"\",\"documentNumber\":\"\",\"episodeNumber\":\"\",\"extraFields\":[],\"fileFormat\":\"\",\"filingDate\":\"\",\"firstPage\":\"\",\"format\":\"\",\"history\":\"\",\"identifier\":\"\",\"isbn\":\"9780451524935\",\"issueDate\":\"\",\"language\":\"en\",\"legislativeBody\":\"\",\"libraryCatalog\":\"\",\"locationStored\":\"\",\"manuscriptType\":\"\",\"mapType\":\"\",\"numberOfVolumes\":0,\"pageAmount\":0,\"patentNumber\":\"\",\"place\":\"\",\"postType\":\"\",\"priorityNumber\":\"\",\"proceedingsTitle\":\"\",\"programTitle\":\"\",\"programmingLanguage\":\"\",\"publication\":\"Signet\",\"publisher\":\"\",\"references\":[],\"reporter\":\"\",\"reporterVolume\":\"\",\"repository\":\"\",\"repositoryLocation\":\"\",\"rights\":[],\"runningTime\":\"\",\"scale\":\"\",\"section\":\"\",\"series\":\"\",\"seriesNumber\":0,\"seriesTitle\":\"\",\"session\":\"\",\"subject\":\"\",\"system\":\"\",\"versionNumber\":\"\",\"videoRecordingFormat\":\"\",\"volumeAmount\":0,\"websiteType\":\"\",\"type\":\"book\",\"title\":\"1984\"}";
         var reader = new JsonTextReader(new StringReader(jsonInput));

         
         
        // Act
        var bookObject = _referenceConverter.ReadJson(reader, typeof(Reference), null, false, new JsonSerializer());
        
        // Assert
        Assert.NotNull(bookObject);
        Assert.IsType<Book>(bookObject);
    }

    [Fact]
    public void ValidJSON_SuccessfullyParsesDatePublished_GivenOnlyYear_Returns_Book()
    {
         var jsonInput =
            "{\"URL\":\"https://books.google.com/books/about/1984.html?hl=&id=Dd9N0AEACAAJ\"," +
            "\"applicationNumber\":\"\"," +
            "\"assignee\":\"\"," +
            "\"audioRecordingFormat\":\"\"," +
            "\"billNumber\":\"\"," +
            "\"blogTitle\":\"\"," +
            "\"bookTitle\":\"\"," +
            "\"caseName\":\"\"," +
            "\"code\":\"\"," +
            "\"codePages\":\"\",\"codeVolume\":\"\",\"committee\":\"\",\"company\":\"\",\"conferenceName\":\"\",\"country\":\"\",\"court\":\"\",\"creators\":[{\"type\":[0],\"firstName\":\"George\",\"lastName\":\"Orwell\"}],\"dataType\":\"\",\"date\":\"\",\"dateDecided\":\"\",\"datePublished\":\"1950\",\"distributor\":\"\",\"docketNumber\":\"\",\"documentNumber\":\"\",\"episodeNumber\":\"\",\"extraFields\":[],\"fileFormat\":\"\",\"filingDate\":\"\",\"firstPage\":\"\",\"format\":\"\",\"history\":\"\",\"identifier\":\"\",\"isbn\":\"9780451524935\",\"issueDate\":\"\",\"language\":\"en\",\"legislativeBody\":\"\",\"libraryCatalog\":\"\",\"locationStored\":\"\",\"manuscriptType\":\"\",\"mapType\":\"\",\"numberOfVolumes\":0,\"pageAmount\":0,\"patentNumber\":\"\",\"place\":\"\",\"postType\":\"\",\"priorityNumber\":\"\",\"proceedingsTitle\":\"\",\"programTitle\":\"\",\"programmingLanguage\":\"\",\"publication\":\"Signet\",\"publisher\":\"\",\"references\":[],\"reporter\":\"\",\"reporterVolume\":\"\",\"repository\":\"\",\"repositoryLocation\":\"\",\"rights\":[],\"runningTime\":\"\",\"scale\":\"\",\"section\":\"\",\"series\":\"\",\"seriesNumber\":0,\"seriesTitle\":\"\",\"session\":\"\",\"subject\":\"\",\"system\":\"\",\"versionNumber\":\"\",\"videoRecordingFormat\":\"\",\"volumeAmount\":0,\"websiteType\":\"\",\"type\":\"book\",\"title\":\"1984\"}";

         var reader = new JsonTextReader(new StringReader(jsonInput));

         var bookObject = _referenceConverter.ReadJson(reader, typeof(Reference), null, false, new JsonSerializer());

         Assert.NotNull(bookObject?.DatePublished);
         Assert.IsType<Book>(bookObject);
         Assert.Equal("1950", bookObject.DatePublished);
    }
}
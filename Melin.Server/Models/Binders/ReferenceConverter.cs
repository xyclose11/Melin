using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Melin.Server.Models.References;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Melin.Server.Models.Binders;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

/// <summary>
/// Responsible for converting Reference related data (JSON) into a more "detailed" Reference Type <see cref="ReferenceType"/>
/// </summary>
public class ReferenceConverter : JsonConverter<Reference>
{
    public override Reference? ReadJson(JsonReader reader, Type objectType, Reference? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);
        var referenceType = obj["type"]?.ToString();  // Get the Type field (e.g., Book or Artwork)

        if (referenceType == null)
        {
            throw new NullReferenceException();
        }
        
        // Parse Dates

        if (obj["datePublished"] != null)
        {
            var t = obj["datePublished"]?.ToString();
            DateTime.TryParseExact(t, "yyyy", null, DateTimeStyles.None, out DateTime result);
            obj["datePublished"] = result;
        }

        if (obj["language"] != null)
        {
            var l = obj["language"]?.ToString();
            Enum.TryParse(l, out Language result);

            if (result != null)
            {
                obj["language"] = result.ToString();
            }
            else
            {
                obj["language"] = "";
            }
        }

        if (referenceType.Equals(ReferenceType.Artwork.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Artwork));
            return o as Artwork;
        }
        if (referenceType.Equals(ReferenceType.AudioRecording.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(AudioRecording));
            if (o != null)
            {
                return o as AudioRecording;
            }
        }
        if (referenceType.Equals(ReferenceType.Bill.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Bill));
            if (o != null)
            {
                return o as Bill;
            }
        }
        if (referenceType.Equals(ReferenceType.BlogPost.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(BlogPost));
            if (o != null)
            {
                return o as BlogPost;
            }
        }
        if (referenceType.Equals(ReferenceType.Book.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var t = obj["datePublished"]?.ToString();
            DateTime.TryParseExact(t, "yyyy", null, DateTimeStyles.None, out DateTime result);
            Console.WriteLine(result);
            var o = obj.ToObject(typeof(Book));
            if (o != null)
            {
                return o as Book;
            }
        }
        if (referenceType.Equals(ReferenceType.BookSection.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(BookSection));
            if (o != null)
            {
                return o as BookSection;
            }
        }
        if (referenceType.Equals(ReferenceType.Case.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Case));
            if (o != null)
            {
                return o as Case;
            }
        }
        if (referenceType.Equals(ReferenceType.ConferencePaper.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(ConferencePaper));
            if (o != null)
            {
                return o as ConferencePaper;
            }
        }
        if (referenceType.Equals(ReferenceType.DictionaryEntry.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(DictionaryEntry));
            if (o != null)
            {
                return o as DictionaryEntry;
            }
        }
        if (referenceType.Equals(ReferenceType.Document.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Document));
            if (o != null)
            {
                return o as Document;
            }
        }
        if (referenceType.Equals(ReferenceType.Email.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Email));
            if (o != null)
            {
                return o as Email;
            }
        }
        if (referenceType.Equals(ReferenceType.EncyclopediaArticle.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(EncyclopediaArticle));
            if (o != null)
            {
                return o as EncyclopediaArticle;
            }
        }
        if (referenceType.Equals(ReferenceType.Film.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Film));
            if (o != null)
            {
                return o as Film;
            }
        }
        if (referenceType.Equals(ReferenceType.ForumPost.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(ForumPost));
            if (o != null)
            {
                return o as ForumPost;
            }
        }
        if (referenceType.Equals(ReferenceType.Hearing.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Hearing));
            if (o != null)
            {
                return o as Hearing;
            }
        }
        if (referenceType.Equals(ReferenceType.InstantMessage.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(InstantMessage));
            if (o != null)
            {
                return o as InstantMessage;
            }
        }
        if (referenceType.Equals(ReferenceType.Interview.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Interview));
            if (o != null)
            {
                return o as Interview;
            }
        }
        if (referenceType.Equals(ReferenceType.JournalArticle.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(JournalArticle));
            if (o != null)
            {
                return o as JournalArticle;
            }
        }
        if (referenceType.Equals(ReferenceType.Letter.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Letter));
            if (o != null)
            {
                return o as Letter;
            }
        }
        if (referenceType.Equals(ReferenceType.MagazineArticle.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(MagazineArticle));
            if (o != null)
            {
                return o as MagazineArticle;
            }
        }
        if (referenceType.Equals(ReferenceType.Manuscript.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Manuscript));
            if (o != null)
            {
                return o as Manuscript;
            }
        }
        if (referenceType.Equals(ReferenceType.Map.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Map));
            if (o != null)
            {
                return o as Map;
            }
        }
        if (referenceType.Equals(ReferenceType.NewspaperArticle.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(NewspaperArticle));
            if (o != null)
            {
                return o as NewspaperArticle;
            }
        }
        if (referenceType.Equals(ReferenceType.Patent.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Patent));
            if (o != null)
            {
                return o as Patent;
            }
        }
        if (referenceType.Equals(ReferenceType.Podcast.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Podcast));
            if (o != null)
            {
                return o as Podcast;
            }
        }
        if (referenceType.Equals(ReferenceType.Presentation.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Presentation));
            if (o != null)
            {
                return o as Presentation;
            }
        }
        if (referenceType.Equals(ReferenceType.RadioBroadcast.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(RadioBroadcast));
            if (o != null)
            {
                return o as RadioBroadcast;
            }
        }
        if (referenceType.Equals(ReferenceType.Report.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Report));
            if (o != null)
            {
                return o as Report;
            }
        }
        if (referenceType.Equals(ReferenceType.Software.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Software));
            if (o != null)
            {
                return o as Software;
            }
        }
        if (referenceType.Equals(ReferenceType.Statute.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Statute));
            if (o != null)
            {
                return o as Statute;
            }
        }
        if (referenceType.Equals(ReferenceType.Thesis.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Thesis));
            if (o != null)
            {
                return o as Thesis;
            }
        }
        if (referenceType.Equals(ReferenceType.TVBroadcast.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(TVBroadcast));
            if (o != null)
            {
                return o as TVBroadcast;
            }
        }
        if (referenceType.Equals(ReferenceType.VideoRecording.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(VideoRecording));
            if (o != null)
            {
                return o as VideoRecording;
            }
        }
        if (referenceType.Equals(ReferenceType.Website.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Website));
            if (o != null)
            {
                return o as Website;
            }
        }
        if (referenceType.Equals(ReferenceType.Attachment.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Attachment));
            if (o != null)
            {
                return o as Attachment;
            }
        }
        if (referenceType.Equals(ReferenceType.Note.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            var o = obj.ToObject(typeof(Note));
            if (o != null)
            {
                return o as Note;
            }
        }

        throw new JsonSerializationException($"Unknown reference type: {referenceType}");
    }

    public override void WriteJson(JsonWriter writer, Reference? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }


        var contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Converters = new List<JsonConverter> { new StringEnumConverter() }, // THIS IS REQUIRED FOR CONVERTING ENUMS -> STRINGS FOR JSON OUTPUT
            ContractResolver = contractResolver
        };
        var specializedSerializer = JsonSerializer.Create(settings);
        specializedSerializer.Serialize(writer, value);
    }
}

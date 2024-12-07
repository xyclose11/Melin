﻿using System.Text.Json;
using System.Text.Json.Serialization;
using Melin.Server.Models.References;

namespace Melin.Server.Models.Binders;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class ReferenceConverter : JsonConverter<Reference>
{
    public override Reference ReadJson(JsonReader reader, Type objectType, Reference existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        string referenceType = obj["type"]?.ToString();  // Get the Type field (e.g., Book or Artwork)

        if (referenceType == ReferenceType.Artwork.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Artwork));
            if (o != null)
            {
                return o as Artwork;
            }
        }
        if (referenceType == ReferenceType.AudioRecording.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(AudioRecording));
            if (o != null)
            {
                return o as AudioRecording;
            }
        }
        if (referenceType == ReferenceType.Bill.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Bill));
            if (o != null)
            {
                return o as Bill;
            }
        }
        if (referenceType == ReferenceType.BlogPost.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(BlogPost));
            if (o != null)
            {
                return o as BlogPost;
            }
        }
        if (referenceType == ReferenceType.Book.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Book));
            if (o != null)
            {
                return o as Book;
            }
        }
        if (referenceType == ReferenceType.BookSection.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(BookSection));
            if (o != null)
            {
                return o as BookSection;
            }
        }
        if (referenceType == ReferenceType.Case.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Case));
            if (o != null)
            {
                return o as Case;
            }
        }
        if (referenceType == ReferenceType.ConferencePaper.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(ConferencePaper));
            if (o != null)
            {
                return o as ConferencePaper;
            }
        }
        if (referenceType == ReferenceType.DictionaryEntry.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(DictionaryEntry));
            if (o != null)
            {
                return o as DictionaryEntry;
            }
        }
        if (referenceType == ReferenceType.Document.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Document));
            if (o != null)
            {
                return o as Document;
            }
        }
        if (referenceType == ReferenceType.Email.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Email));
            if (o != null)
            {
                return o as Email;
            }
        }
        if (referenceType == ReferenceType.EncyclopediaArticle.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(EncyclopediaArticle));
            if (o != null)
            {
                return o as EncyclopediaArticle;
            }
        }
        if (referenceType == ReferenceType.Film.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Film));
            if (o != null)
            {
                return o as Film;
            }
        }
        if (referenceType == ReferenceType.ForumPost.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(ForumPost));
            if (o != null)
            {
                return o as ForumPost;
            }
        }
        if (referenceType == ReferenceType.Hearing.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Hearing));
            if (o != null)
            {
                return o as Hearing;
            }
        }
        if (referenceType == ReferenceType.InstantMessage.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(InstantMessage));
            if (o != null)
            {
                return o as InstantMessage;
            }
        }
        if (referenceType == ReferenceType.Interview.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Interview));
            if (o != null)
            {
                return o as Interview;
            }
        }
        if (referenceType == ReferenceType.JournalArticle.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(JournalArticle));
            if (o != null)
            {
                return o as JournalArticle;
            }
        }
        if (referenceType == ReferenceType.Letter.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Letter));
            if (o != null)
            {
                return o as Letter;
            }
        }
        if (referenceType == ReferenceType.MagazineArticle.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(MagazineArticle));
            if (o != null)
            {
                return o as MagazineArticle;
            }
        }
        if (referenceType == ReferenceType.Manuscript.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Manuscript));
            if (o != null)
            {
                return o as Manuscript;
            }
        }
        if (referenceType == ReferenceType.Map.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Map));
            if (o != null)
            {
                return o as Map;
            }
        }
        if (referenceType == ReferenceType.NewspaperArticle.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(NewspaperArticle));
            if (o != null)
            {
                return o as NewspaperArticle;
            }
        }
        if (referenceType == ReferenceType.Patent.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Patent));
            if (o != null)
            {
                return o as Patent;
            }
        }
        if (referenceType == ReferenceType.Podcast.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Podcast));
            if (o != null)
            {
                return o as Podcast;
            }
        }
        if (referenceType == ReferenceType.Presentation.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Presentation));
            if (o != null)
            {
                return o as Presentation;
            }
        }
        if (referenceType == ReferenceType.RadioBroadcast.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(RadioBroadcast));
            if (o != null)
            {
                return o as RadioBroadcast;
            }
        }
        if (referenceType == ReferenceType.Report.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Report));
            if (o != null)
            {
                return o as Report;
            }
        }
        if (referenceType == ReferenceType.Software.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Software));
            if (o != null)
            {
                return o as Software;
            }
        }
        if (referenceType == ReferenceType.Statute.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Statute));
            if (o != null)
            {
                return o as Statute;
            }
        }
        if (referenceType == ReferenceType.Thesis.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Thesis));
            if (o != null)
            {
                return o as Thesis;
            }
        }
        if (referenceType == ReferenceType.TVBroadcast.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(TVBroadcast));
            if (o != null)
            {
                return o as TVBroadcast;
            }
        }
        if (referenceType == ReferenceType.VideoRecording.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(VideoRecording));
            if (o != null)
            {
                return o as VideoRecording;
            }
        }
        if (referenceType == ReferenceType.Webpage.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Webpage));
            if (o != null)
            {
                return o as Webpage;
            }
        }
        if (referenceType == ReferenceType.Attachment.ToString().ToLower())
        {
            var o = obj.ToObject(typeof(Attachment));
            if (o != null)
            {
                return o as Attachment;
            }
        }
        if (referenceType == ReferenceType.Note.ToString().ToLower())
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
        if (value == null || serializer == null)
        {
            return;
        }

        // JObject jObject = new();
        // jObject[nameof(value.)]
        var jo = JObject.FromObject(value);
        // var jo = JObject.FromObject(value, serializer);
        jo.WriteTo(writer);
    }
}

import { z } from "zod";
import { creatorFormSchema } from "@/routes/CustomComponents/CreateRefComponents/CreatorInput.tsx";

export const baseReferenceSchema = z.object({
    title: z.string().min(2, {
        message: "Title must be at least 2 characters.",
    }),
    shortTitle: z.string().optional(),
    language: z.string().optional(),
    datePublished: z.date().optional().nullable(),
    // rights: z.string().array().optional(),
    // extraFields: z.string().array().optional(),
    creators: z.array(creatorFormSchema).optional(),
});

export const bookSchema = z.object({
    Publication: z.string().min(2).optional(),
    BookTitle: z.string().min(2).optional(),
    Volume: z.string().min(2).optional(),
    Issue: z.string().min(2).optional(),
    Pages: z.number().min(0).optional(),
    Edition: z.string().min(2).optional(),
    Series: z.string().min(2).optional(),
    SeriesNumber: z.number().min(0).optional(),
    SeriesTitle: z.number().min(0).optional(),
    VolumeAmount: z.number().min(0).optional(),
    PageAmount: z.number().min(0).optional(),
    Section: z.string().min(2).optional(),
    Place: z.string().min(2).optional(),
    Publisher: z.string().min(2).optional(),
    JournalAbbreviation: z.string().min(2).optional(),
    ISBN: z.string().min(2).optional(),
    ISSN: z.string().min(2).optional(),
});

export const artworkSchema = baseReferenceSchema.extend({
    medium: z.string().min(2).max(128).default("").optional(),
    dimensions: z.string().min(2).max(128).default("").optional(),
    scale: z.string().min(2).max(128).optional(),
    mapType: z.string().min(2).max(128).optional(),
});

export const legislationSchema = z.object({
    NameOfAct: z.string().min(2).max(256),
    BillNumber: z.string().min(2).max(256),
    Code: z.string().min(2).max(256),
    CodeVolume: z.string().min(2).max(256),
    CodeNumber: z.string().min(2).max(256),
    PublicLawNumber: z.string().min(2).max(256),
    DateEnacted: z.date().optional(),
    Section: z.string().min(2).max(256).optional(),
    Committee: z.string().min(2).max(256).optional(),
    DocumentNumber: z.string().min(2).max(256).optional(),
    CodePages: z.string().min(2).max(256).optional(),
    LegislativeBody: z.string().min(2).max(256).optional(),
    Session: z.string().min(2).max(256).optional(),
    History: z.string().min(2).max(256).optional(),
});

export const legalCaseSchema = z.object({
    History: z.string().min(2).max(256).optional(),
    CaseName: z.string().min(2).max(256).optional(),
    Court: z.string().min(2).max(256).optional(),
    DateDecided: z.date().optional(),
    DocketNumber: z.string().min(2).max(256).optional(),
    Reporter: z.string().min(2).max(256).optional(),
    ReporterVolume: z.string().min(2).max(256).optional(),
    FirstPage: z.string().min(2).max(256).optional(),
});

export const patentSchema = z.object({
    Country: z.string().min(2).max(256),
    Assignee: z.string().min(2).max(256),
    IssuingAuthority: z.string().min(2).max(256),
    PatentNumber: z.string().min(2).max(256),
    FilingDate: z.date(),
    IssueDate: z.date(),
    ApplicationNumber: z.string().min(2).max(256),
    PriorityNumber: z.string().min(2).max(256),
    References: z.string().min(2).max(256),
    LegalStatus: z.string().min(2).max(256),
});

export const presentationSchema = z.object({
    ProceedingTitle: z.string().min(2).max(256),
    ConferenceName: z.string().min(2).max(256),
    Place: z.string().min(2).max(256),
    PresentationType: z.string().min(2).max(256),
});

export const primarySourceSchema = z.object({
    Medium: z.string().min(2).max(256),
    PrimarySourceType: z.string().min(2).max(256),
    Subject: z.string().min(2).max(256),
});

export const recordingSchema = z.object({
    FileFormat: z.string().min(2).max(256),
    RunningTime: z.string().min(2).max(256),
    ProgramTitle: z.string().min(2).max(256),
    EpisodeNumber: z.number().min(0).max(256).optional(),
    Network: z.string().min(2).max(256),
    Label: z.string().min(2).max(256),
    Distributor: z.string().min(2).max(256),
    Genre: z.string().min(2).max(256),
    Studio: z.string().min(2).max(256),
});

export const reportSchema = z.object({
    ReportType: z.string().min(2).max(256),
    ReportNumber: z.number().min(0),
    Institution: z.string().min(2).max(256),
});

export const softwareSchema = z.object({
    Version: z.string().min(2).max(256),
    System: z.string().min(2).max(256),
    Company: z.string().min(2).max(256).optional(),
    ProgrammingLanguage: z.string().min(2).max(256).optional().default(""),
});

export const websiteSchema = z.object({
    WebsiteTitle: z.string().min(2).max(256),
    ForumTitle: z.string().min(2).max(256).optional(),
    WebsiteType: z.string().min(2).max(256).optional(),
    PostType: z.string().min(2).max(256).optional(),
});

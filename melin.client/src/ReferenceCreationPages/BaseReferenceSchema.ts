import { z } from "zod";
import { creatorFormSchema } from "@/CreateRefComponents/CreatorInput.tsx";

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
    type: z.string(),
});

export const bookSchema = baseReferenceSchema.extend({
    publication: z.string().min(2).optional().or(z.literal("")),
    bookTitle: z.string().min(2).optional().or(z.literal("")),
    volume: z.string().min(2).optional().or(z.literal("")),
    issue: z.string().min(2).optional().or(z.literal("")),
    pages: z.number().min(0).optional().or(z.literal("")),
    edition: z.string().min(2).optional().or(z.literal("")),
    series: z.string().min(2).optional().or(z.literal("")),
    seriesNumber: z.number().min(0).optional().or(z.literal("")),
    seriesTitle: z.number().min(0).optional().or(z.literal("")),
    volumeAmount: z.number().min(0).optional().or(z.literal("")),
    pageAmount: z.number().min(0).optional().or(z.literal("")),
    section: z.string().min(2).optional().or(z.literal("")),
    place: z.string().min(2).optional().or(z.literal("")),
    publisher: z.string().min(2).optional().or(z.literal("")),
    journalAbbreviation: z.string().min(2).optional().or(z.literal("")),
    ISBN: z.string().min(2).optional().or(z.literal("")),
    ISSN: z.string().min(2).optional().or(z.literal("")),
});

export const artworkSchema = baseReferenceSchema.extend({
    medium: z.string().min(2).max(128).default("").optional().or(z.literal("")),
    dimensions: z
        .string()
        .min(2)
        .max(128)
        .default("")
        .optional()
        .or(z.literal("")),
    scale: z.string().min(2).max(128).optional().or(z.literal("")),
    mapType: z.string().min(2).max(128).optional().or(z.literal("")),
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

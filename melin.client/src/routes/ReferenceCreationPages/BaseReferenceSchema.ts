import { z } from "zod";
import { creatorFormSchema } from "@/routes/CustomComponents/CreateRefComponents/CreatorInput.tsx";

export const baseReferenceSchema = z.object({
    title: z.string().min(2, {
        message: "Title must be at least 2 characters.",
    }),
    shortTitle: z.string().min(2).optional(),
    language: z.string().min(2).optional(),
    datePublished: z.date().optional(),
    rights: z.string().array().optional(),
    extraFields: z.string().array().optional(),
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

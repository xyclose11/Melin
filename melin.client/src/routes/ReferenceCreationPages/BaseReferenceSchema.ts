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

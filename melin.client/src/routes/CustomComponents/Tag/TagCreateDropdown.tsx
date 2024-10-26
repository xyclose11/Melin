import { FormField } from "@/components/ui/form.tsx";
import { useFormContext } from "react-hook-form";
import { Tag, TagInput } from "emblor";
import React from "react";
import { z } from "zod";

export const tagSchema = z.object({
    id: z.string(),
    text: z
        .string()
        .min(2, {
            message: "Name must be at least 2 characters.",
        })
        .optional(),
});

export function TagCreateDropdown() {
    const { control, setValue, watch, formState: {errors} } = useFormContext();

    // Use watch to get the current tags from the form state
    const currentTags = watch("tags") || []; // Default to empty array if undefined

    const [activeTagIndex, setActiveTagIndex] = React.useState<number | null>(null);


    const handleTagChange = (newTags: React.SetStateAction<Tag[]>) => {
        setValue("tags", newTags);
    };

    return (
        <div>
            <FormField
                control={control}
                name="tags" // Ensure this matches your schema
                render={({ field }) => (
                    <TagInput
                        {...field}
                        placeholder="Enter a topic"
                        tags={currentTags} // Use watched tags here
                        setTags={handleTagChange} // Update the tags in form state
                        activeTagIndex={activeTagIndex}
                        setActiveTagIndex={setActiveTagIndex}
                    />
                )}
            />

            {errors.root && <div> {errors.root.message}</div>}
        </div>
    );
}

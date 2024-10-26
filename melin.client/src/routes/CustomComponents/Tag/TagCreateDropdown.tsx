import { FormControl, FormField, FormItem } from "@/components/ui/form.tsx";
import { useFormContext } from "react-hook-form";
import { Tag, TagInput } from "emblor";
import React from "react";
import { z } from "zod";

export const tagSchema = z.object({
    name: z
        .string()
        .min(2, {
            message: "Name must be at least 2 characters.",
        })
        .optional(),
});

export function TagCreateDropdown() {
    const { control, setValue, watch } = useFormContext();
    const [tags, setTags] = React.useState<Tag[]>([]);
    const [activeTagIndex, setActiveTagIndex] = React.useState<number | null>(
        null,
    );

    const currentTags = watch("tags");

    const handleTagChange = (newTags: React.SetStateAction<Tag[]>) => {
        setTags(newTags);
        setValue("tags", newTags);
        console.log(tags);
    };

    return (
        <div>
            <FormField
                control={control}
                render={({ field }) => (
                    <TagInput
                        {...field}
                        placeholder="Enter a topic"
                        tags={tags}
                        setTags={handleTagChange}
                        activeTagIndex={activeTagIndex}
                        setActiveTagIndex={setActiveTagIndex}
                    />
                )}
                name={"tagInput"}
            ></FormField>
        </div>
    );
}

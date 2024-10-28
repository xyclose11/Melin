import {
    FormControl,
    FormField,
    FormItem,
    FormLabel,
} from "@/components/ui/form.tsx";
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
    const {
        control,
        setValue,
        watch,
        formState: { errors },
    } = useFormContext();

    const currentTags = watch("tags") || [];

    const [activeTagIndex, setActiveTagIndex] = React.useState<number | null>(
        null,
    );

    const handleTagChange = (newTags: React.SetStateAction<Tag[]>) => {
        setValue("tags", newTags);
    };

    return (
        <div className={"space-y-2"}>
            <FormField
                control={control}
                name="tags"
                render={({ field }) => (
                    <FormItem>
                        <FormLabel> Tags</FormLabel>
                        <FormControl>
                            <TagInput
                                {...field}
                                placeholder="Enter a tag"
                                tags={currentTags}
                                setTags={handleTagChange}
                                activeTagIndex={activeTagIndex}
                                setActiveTagIndex={setActiveTagIndex}
                            />
                        </FormControl>
                    </FormItem>
                )}
            />

            {errors.root && <div> {errors.root.message}</div>}
        </div>
    );
}

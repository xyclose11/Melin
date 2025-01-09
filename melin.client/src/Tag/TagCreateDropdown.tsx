import {
    FormControl,
    FormField,
    FormItem,
    FormLabel,
} from "@/components/ui/form.tsx";
import { useFormContext } from "react-hook-form";
import { Tag, TagInput } from "emblor";
import React, { useEffect, useState } from "react";
import { z } from "zod";
import { instance } from "@/utils/axiosInstance.ts";
import { useToast } from "@/hooks/use-toast.ts";

export const tagSchema = z.object({
    id: z.string(),
    text: z
        .string()
        .min(2, {
            message: "Name must be at least 2 characters.",
        })
        .optional(),
    createdBy: z.string().optional(),
});

export function TagCreateDropdown() {
    const {
        control,
        setValue,
        watch,
        formState: { errors },
    } = useFormContext();

    const { toast } = useToast();

    const currentTags = watch("tags") || [];

    const [activeTagIndex, setActiveTagIndex] = React.useState<number | null>(
        null,
    );

    const [tagAutoFill, setTagAutoFill] = useState<Tag[]>();

    const getUserTags = async () => {
        try {
            // figure out which reference type is being used
            const response = await instance.get(`get-owned-tags`, {
                withCredentials: true,
            });

            if (response.status === 200) {
                setTagAutoFill(response.data);
            } else {
                toast({
                    variant: "destructive",
                    title: "Tag Retrieval Not Successful",
                    description: ``,
                });
            }
        } catch (error) {
            toast({
                variant: "destructive",
                title: "Tag Retrieval Not Successful",
                description: ``,
            });
        }
    };

    // load all user tags
    useEffect(() => {
        getUserTags();
    }, []);

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
                        <FormLabel>Tags</FormLabel>
                        <FormControl>
                            <TagInput
                                {...field}
                                placeholder="Enter a tag"
                                tags={currentTags}
                                setTags={handleTagChange}
                                activeTagIndex={activeTagIndex}
                                setActiveTagIndex={setActiveTagIndex}
                                enableAutocomplete={true}
                                autocompleteOptions={tagAutoFill}
                                maxTags={99}
                                maxLength={128}
                                showCount={true}
                                truncate={12}
                                clearAll={true}
                                size={"sm"}
                                shape={"rounded"}
                                inlineTags={true}
                                textCase={"capitalize"}
                                className="font-black"
                                styleClasses={{
                                    input: "bg-black border bg-background text-foreground sm:max-w-[350px]",
                                    inlineTagsContainer:
                                        "bg-gray-200 p-2 rounded",
                                    tag: {
                                        body: "flex items-center p-1 gap-2",
                                        closeButton:
                                            "text-red-500 hover:text-red-600",
                                    },
                                }}
                                variant={"default"}
                            />
                        </FormControl>
                    </FormItem>
                )}
            />

            {errors.root && <div> {errors.root.message}</div>}
        </div>
    );
}

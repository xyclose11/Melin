import {
    Form,
    FormControl,
    FormField,
    FormItem,
} from "@/components/ui/form.tsx";
import { useForm } from "react-hook-form";
import { Tag, TagInput } from "emblor";
import React, { useEffect, useState } from "react";
import { z } from "zod";
import { instance } from "@/utils/axiosInstance.ts";
import { useToast } from "@/hooks/use-toast.ts";
import { tagSchema } from "@/routes/CustomComponents/Tag/TagCreateDropdown.tsx";
import { zodResolver } from "@hookform/resolvers/zod";

export const addTagSchema = z.object({
    tags: z.array(tagSchema),
    refId: z.string(),
});

export function AddTagToReference({ refId }: { refId: string }) {
    const form = useForm<z.infer<typeof addTagSchema>>({
        resolver: zodResolver(addTagSchema),
        defaultValues: {
            tags: [],
            refId: "",
        },
    });

    const { toast } = useToast();

    const [currentTags, setCurrentTags] = useState<Tag[]>([]);
    const [activeTagIndex, setActiveTagIndex] = React.useState<number | null>(
        null,
    );

    const [tagAutoFill, setTagAutoFill] = useState<Tag[]>([]);

    const getUserTags = async () => {
        try {
            // figure out which reference type is being used
            const response = await instance.get(`get-owned-tags`, {
                withCredentials: true,
            });

            console.log(response);
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
        setCurrentTags(newTags);
        form.setValue("tags", newTags as [Tag, ...Tag[]]);
    };

    const addTag = async (data: z.infer<typeof addTagSchema>) => {
        try {
            data.refId = refId;
            const res = await instance.post(`add-tags-to-reference`, data, {
                withCredentials: true,
            });

            if (res.status === 200) {
                console.log("SUCCESS");
            } else {
                console.log("ERROR");
            }
        } catch (e) {
            console.error(e);
        }
    };

    return (
        <div className={"space-y-2"}>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(addTag)}>
                    <FormField
                        control={form.control}
                        name="tags"
                        render={({ field }) => (
                            <FormItem>
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
                                        styleClasses={{
                                            input: "w-full sm:max-w-[350px]",
                                            tag: {
                                                body: "flex items-center gap-2",
                                                closeButton:
                                                    "text-red-500 hover:text-red-600",
                                            },
                                        }}
                                        variant={"primary"}
                                    />
                                </FormControl>
                                <button type="submit">Submit</button>
                            </FormItem>
                        )}
                    />
                </form>
            </Form>

            {/*{form.errors.root && <div> {form.errors.root.message}</div>}*/}
        </div>
    );
}

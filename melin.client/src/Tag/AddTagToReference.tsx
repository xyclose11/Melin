﻿import {
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
import { tagSchema } from "@/Tag/TagCreateDropdown.tsx";
import { zodResolver } from "@hookform/resolvers/zod";
import { Button } from "@/components/ui/button.tsx";
import { ReferenceTag } from "@/Library.tsx";

export const addTagSchema = z.object({
    tags: z.array(tagSchema),
    refId: z.number(),
});

export function AddTagToReference({
    refId,
    stateChanger,
}: {
    refId: number;
    stateChanger: React.Dispatch<React.SetStateAction<ReferenceTag[]>>;
}) {
    const form = useForm<z.infer<typeof addTagSchema>>({
        resolver: zodResolver(addTagSchema),
        defaultValues: {
            tags: [],
            refId: -1,
        },
    });

    const errors = form.formState.errors;
    const { toast } = useToast();

    const [currentTags, setCurrentTags] = useState<Tag[]>([]);
    const [activeTagIndex, setActiveTagIndex] = React.useState<number | null>(
        null,
    );

    const [tagAutoFill, setTagAutoFill] = useState<Tag[]>([]);

    const getAllUserTags = async () => {
        try {
            // figure out which reference type is being used
            const response = await instance.get(
                `get-owned-tags?pageNumber=${0}&pageSize=${100}`,
                {
                    withCredentials: true,
                },
            );

            response.data.map(
                (t: {
                    id: number | string;
                    text: string;
                    description: string;
                }) => {
                    t.id = String(t.id);
                },
            );

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

    const getCurrentAppliedTags = async () => {
        try {
            // figure out which reference type is being used
            const response = await instance.get(
                `get-owned-tags-for-reference?pageNumber=${0}&pageSize=${100}&refId=${refId}`,
                {
                    withCredentials: true,
                },
            );

            if (response.status === 200) {
                response.data.map(
                    (t: {
                        id: number | string;
                        text: string;
                        description: string;
                    }) => {
                        t.id = String(t.id);
                    },
                );
                setCurrentTags(response.data);
            }
        } catch (error) {
            setCurrentTags([]);
            // toast({
            //     variant: "destructive",
            //     title: "Tag Retrieval Not Successful",
            //     description: ``,
            // });
        }
    };

    // load all user tags
    useEffect(() => {
        getAllUserTags();
        getCurrentAppliedTags();
    }, []);

    const handleTagChange = (newTags: Tag[]) => {
        setCurrentTags(newTags);

        const convertedReferenceTags: ReferenceTag[] = newTags.map((tag) => ({
            ...tag,
            createdBy: "",
        }));

        stateChanger(convertedReferenceTags);

        form.setValue("tags", newTags as [Tag, ...Tag[]]);
    };

    function generateRandom32BitInteger() {
        const max = 500000;
        return Math.floor(Math.random() * (max + 1));
    }

    const addTag = async (data: z.infer<typeof addTagSchema>) => {
        const convertedTags = data.tags?.map((tag) => ({
            ...tag,
            id: generateRandom32BitInteger(),
        }));

        const newData = {
            ...data,
            refId: refId,
            tags: convertedTags,
        };

        try {
            const res = await instance.post(`add-tags-to-reference`, newData, {
                withCredentials: true,
            });

            if (res.status === 200) {
                toast({
                    variant: "default",
                    title: "Tag Successfully Added!",
                });
            } else {
                toast({
                    variant: "destructive",
                    title: "Tag Unable to be Added!",
                    description: `Please Try Again`,
                });
                console.error(res);
            }
        } catch (e) {
            console.error(e);
        }
    };

    const onInvalid = (errors: any) => console.error(errors);

    return (
        <div className={"space-y-2"}>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(addTag, onInvalid)}>
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
                                        /*@ts-ignore*/
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
                                <Button type="submit">Submit</Button>
                            </FormItem>
                        )}
                    />
                    {errors.refId && <div>{errors.refId.message}</div>}
                    {errors.root && <div>{errors.root.message}</div>}
                    {errors.tags && <div>{errors.tags.message}</div>}
                </form>
            </Form>

            {/*{form.errors.root && <div> {form.errors.root.message}</div>}*/}
        </div>
    );
}

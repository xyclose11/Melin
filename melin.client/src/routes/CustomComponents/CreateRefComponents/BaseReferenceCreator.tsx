"use client";
import { z, ZodObject } from "zod";

import { zodResolver } from "@hookform/resolvers/zod";
import { Controller, useForm } from "react-hook-form";
import { format } from "date-fns";

import { Button } from "@/components/ui/button";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
    creatorFormSchema,
    CreatorInput,
} from "@/routes/CustomComponents/CreateRefComponents/CreatorInput.tsx";
import React, { useState } from "react";
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover.tsx";
import { cn } from "@/lib/utils.ts";
import { CalendarIcon, SquareX } from "lucide-react";
import { Calendar } from "@/components/ui/calendar";
import { instance } from "@/utils/axiosInstance.ts";
import { CREATOR_TYPES } from "@/routes/Library.tsx";

const formSchema = z.object({
    refType: z.string(),
    title: z.string().min(2, {
        message: "Title must be at least 2 characters.",
    }),
    shortTitle: z.string().optional(),
    language: z.string().optional(),
    datePublished: z.date().optional(),
    rights: z.string().optional(),
    extraFields: z.string().optional(),
    creators: z.array(creatorFormSchema).optional(),
});

let nextId = 0;
export function BaseReferenceCreator({
    refSchema,
    schemaName,
}: {
    refSchema: ZodObject<any>;
    schemaName: string;
}) {
    const [creatorArray, setCreatorArray] = useState<React.ReactNode[]>([]);
    const [datePublished, setDatePublished] = React.useState<Date>();

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            refType: "book",
            title: "",
            shortTitle: undefined,
            datePublished: undefined,
            language: undefined,
            extraFields: undefined,
            rights: undefined,
            creators: [
                {
                    id: 0,
                    creatorType: "Author",
                    firstName: "",
                    lastName: "",
                },
            ],
        },
    });

    const onSubmit = async (data: z.infer<typeof formSchema>) => {
        console.log("ASD");
        try {
            // figure out which reference type is being used
            await instance.post(`Reference/create-${schemaName}`, data, {
                withCredentials: true,
            });
            console.log("SUCCESS");
        } catch (error) {
            console.error("Create reference failed:", error);
        }
    };

    const {
        control,
        formState: { errors },
    } = form;

    function onClickAddCreator() {
        setCreatorArray([
            ...creatorArray,
            <CreatorInput name={`creators.${nextId}`} key={nextId} />,
        ]);
        nextId++;
    }

    // function onClickRemoveCreator(removeId: number) {
    //     const newArray = creatorArray.filter((creator: React.ReactNode) => {
    //         return creator.id !== removeId;
    //     });
    // }

    return (
        <div>
            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(onSubmit)}
                    className="space-y-2 gap-2 justify-items-start grid grid-cols-2 grid-flow-row-dense"
                >
                    <FormField
                        control={form.control}
                        name="title"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Title *</FormLabel>
                                <FormControl>
                                    <Input placeholder="Title" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="shortTitle"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Short Title</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="Short Title"
                                        {...field}
                                    />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="language"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Language</FormLabel>
                                <FormControl>
                                    <Input placeholder="Language" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="datePublished"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Date Published</FormLabel>
                                <FormControl>
                                    <Popover>
                                        <PopoverTrigger asChild>
                                            <Button
                                                variant={"outline"}
                                                className={cn(
                                                    "w-[280px] justify-start text-left font-normal",
                                                    !datePublished &&
                                                        "text-muted-foreground",
                                                )}
                                            >
                                                <CalendarIcon />
                                                {datePublished ? (
                                                    format(datePublished, "PPP")
                                                ) : (
                                                    <span> Click Here! </span>
                                                )}
                                            </Button>
                                        </PopoverTrigger>
                                        <PopoverContent className="w-auto p-0">
                                            <Calendar
                                                mode="single"
                                                selected={datePublished}
                                                onSelect={(date) => {
                                                    setDatePublished(date);
                                                    field.onChange(date);
                                                }}
                                                initialFocus
                                                {...field}
                                            />
                                        </PopoverContent>
                                    </Popover>
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="rights"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Rights</FormLabel>
                                <FormControl>
                                    <Input placeholder="Rights" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="extraFields"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Extra Information</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="Extra Fields"
                                        {...field}
                                    />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <div className={"col-span-2"}>
                        <ul>
                            {creatorArray.map((creator) => (
                                <li key={(creator as React.ReactElement).key}>
                                    {creator}
                                    <Button
                                        className={"h-8"}
                                        variant="destructive"
                                        type="button"
                                        size="icon"
                                    >
                                        <SquareX className="h-4 w-4" />
                                    </Button>
                                </li>
                            ))}
                        </ul>
                        <Button
                            className="m-2"
                            type="button"
                            onClick={onClickAddCreator}
                        >
                            + Add Another
                        </Button>
                    </div>

                    {Object.keys(refSchema.shape).map((key) => (
                        <Controller
                            key={key}
                            control={control}
                            name={
                                `bookSchema.${key}` as keyof z.infer<
                                    typeof formSchema
                                >
                            }
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>
                                        {key.replace(/([A-Z])/g, " $1")}:
                                    </FormLabel>
                                    <FormControl>
                                        {/*@ts-ignore*/}
                                        <Input
                                            placeholder={key.replace(
                                                /([A-Z])/g,
                                                " $1",
                                            )}
                                            {...field}
                                        />
                                    </FormControl>
                                </FormItem>
                            )}
                        />
                    ))}

                    {errors.creators && <div>{errors.creators.message}</div>}
                    {errors.title && <div>{errors.title.message}</div>}
                    {errors.shortTitle && (
                        <div>{errors.shortTitle.message}</div>
                    )}
                    {errors.rights && <div>{errors.rights.message}</div>}
                    {errors.datePublished && (
                        <div>{errors.datePublished.message}</div>
                    )}
                    {errors.extraFields && (
                        <div>{errors.extraFields.message}</div>
                    )}
                    {errors.language && <div>{errors.language.message}</div>}

                    {errors.root && <div> {errors.root.message}</div>}

                    <Button className="col-end-2 m-2" type="submit">
                        Submit
                    </Button>
                </form>
            </Form>
        </div>
    );
}

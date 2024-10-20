"use client";
import { z } from "zod";

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
import { CreatorInput } from "@/routes/CustomComponents/CreateRefComponents/CreatorInput.tsx";
import React, { useState } from "react";
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover.tsx";
import { cn } from "@/lib/utils.ts";
import { CalendarIcon } from "lucide-react";
import { Calendar } from "@/components/ui/calendar";
import { instance } from "@/utils/axiosInstance.ts";
import { baseReferenceSchema } from "@/routes/ReferenceCreationPages/BaseReferenceSchema.ts";

const bookSchema = z.object({
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

const formSchema = baseReferenceSchema.extend({ bookSchema });

let nextId = 0;

export function CreateReferenceBook() {
    const [creatorArray, setCreatorArray] = useState<React.ReactNode[]>([]);
    const [datePublished, setDatePublished] = React.useState<Date>();

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            title: "",
            creators: [
                {
                    creatorType: "author",
                    firstName: "",
                    lastName: "",
                },
            ],
        },
    });

    const {
        control,
        formState: { errors },
    } = form;

    const onSubmit = async (data: z.infer<typeof formSchema>) => {
        try {
            // figure out which reference type is being used
            await instance.post(`Reference/create-book`, data);
        } catch (error) {
            console.error("Create reference failed:", error);
        }
    };

    function onClickAddCreator() {
        setCreatorArray([
            ...creatorArray,
            <CreatorInput name={`creators.${nextId}`} key={nextId} />,
        ]);
        nextId++;
    }

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
                    <ul>
                        {creatorArray.map((creator) => (
                            <li key={(creator as React.ReactElement).key}>
                                {creator}
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

                    <FormField
                        control={form.control}
                        name="bookSchema.Publication"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Publication</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="Publication"
                                        {...field}
                                    />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    {Object.keys(bookSchema.shape).map((key) => (
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

                    <Button className="col-end-2 m-2" type="submit">
                        Submit
                    </Button>
                </form>
            </Form>
        </div>
    );
}

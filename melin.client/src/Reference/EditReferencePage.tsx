﻿import React, { useEffect, useState } from "react";
import { instance } from "@/utils/axiosInstance.ts";
import { getRouteApi, useNavigate } from "@tanstack/react-router";
import {
    artworkSchema,
    baseReferenceSchema,
    bookSchema,
    reportSchema,
    websiteSchema,
} from "@/ReferenceCreationPages/BaseReferenceSchema.ts";
import { z, ZodObject } from "zod";
import {
    Controller,
    FormProvider,
    useFieldArray,
    useForm,
} from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form.tsx";
import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import { TagCreateDropdown } from "@/Tag/TagCreateDropdown.tsx";
import { Input } from "@/components/ui/input.tsx";
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover.tsx";
import { Button } from "@/components/ui/button.tsx";
import { cn } from "@/lib/utils.ts";
import { CalendarIcon, SquareX } from "lucide-react";
import { Calendar } from "@/components/ui/calendar.tsx";
import {
    CREATOR_TYPES,
    CreatorInput,
} from "@/CreateRefComponents/CreatorInput.tsx";
import { useToast } from "@/hooks/use-toast.ts";
import { Tag } from "emblor";
import { useMutation, useQuery } from "@tanstack/react-query";
import { format } from "date-fns";
import { isValidDate } from "@/utils/isValidDate.ts";
import { DevTool } from "@hookform/devtools";

const route = getRouteApi("/(reference)/edit-reference/$refId");

export function EditReferencePage({ reference }: { reference: any }) {
    const [datePublished, setDatePublished] = React.useState<Date>();
    const navigate = useNavigate();
    const { toast } = useToast();
    const { refId } = route.useParams();
    const [refSchema, setRefSchema] =
        useState<ZodObject<any>>(baseReferenceSchema);

    const { isPending, isError, refetch, error } = useQuery({
        queryKey: ["single-reference", refId],
    });

    const mutation = useMutation({
        mutationFn: (data) => {
            return instance.put(`Reference/update/${refId}`, data, {
                withCredentials: true,
            });
        },
        onSuccess: () => {
            toast({
                variant: "default",
                title: "Reference Successfully Updated!",
                description: ``,
            });
            refetch();
            navigate({ to: "/library" });
        },
        onError: () => {
            toast({
                variant: "destructive",
                title: "Reference Not Updated Successfully",
                description: `Query Error: ${mutation?.error?.message}`,
            });
        },
    });

    const form = useForm<z.infer<typeof refSchema>>({
        resolver: refSchema ? zodResolver(refSchema) : undefined,
        defaultValues: reference,
    });

    const {
        control,
        formState: { errors, isLoading },
        handleSubmit,
    } = form;

    const { fields, append, remove } = useFieldArray({
        control,
        name: "creators",
    });
    function onClickAddCreator() {
        append({
            firstName: "",
            lastName: "",
            types: CREATOR_TYPES[0].value,
        });
    }

    function generateRandom32BitInteger() {
        const max = 500000;
        return Math.floor(Math.random() * (max + 1));
    }
    const onSubmit = async (data: any) => {
        const convertedTags = data.tags?.map((tag: Tag) => ({
            ...tag,
            id: generateRandom32BitInteger(),
        }));

        const newData = {
            ...data,
            tags: convertedTags,
        };

        mutation.mutate(newData);
    };

    if (isLoading || !refSchema) {
        return <div>Loading...</div>;
    }

    useEffect(() => {
        let newSchema: ZodObject<any>;

        setDatePublished(new Date(reference.datePublished));
        switch (reference.type) {
            case "Artwork":
                newSchema = artworkSchema;
                break;
            case "Book":
                newSchema = bookSchema;
                break;
            case "Report":
                newSchema = reportSchema;
                break;
            case "Website":
                newSchema = websiteSchema;
                break;
            default:
                newSchema = baseReferenceSchema;
                break;
        }

        setRefSchema(newSchema);
    }, []);

    console.log(errors);

    if (isPending) {
        return <div>LOADING... QUERY</div>;
    }

    if (isError) {
        return <div>ERROR: {error.message}</div>;
    }

    return (
        <>
            <FormProvider {...form}>
                <Form {...form}>
                    <form
                        onSubmit={handleSubmit(onSubmit)}
                        className="space-y-2 gap-2 justify-items-start grid grid-cols-3"
                    >
                        <Card>
                            <CardHeader className={"text-center"}>
                                <CardTitle>Tags</CardTitle>
                            </CardHeader>
                            <CardContent>
                                <TagCreateDropdown />
                            </CardContent>
                        </Card>
                        <Card className={"col-span-2 m-0"}>
                            <CardHeader className={"m-0"}>
                                <CardTitle>General Fields*</CardTitle>
                            </CardHeader>
                            <CardContent>
                                <FormField
                                    control={form.control}
                                    name="title"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Title *</FormLabel>
                                            <FormControl>
                                                <Input
                                                    placeholder="Title"
                                                    {...field}
                                                />
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
                                                <Input
                                                    placeholder="Language"
                                                    {...field}
                                                />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />
                                <FormField
                                    control={form.control}
                                    name="datePublished"
                                    render={({ field }) => (
                                        <FormItem className={""}>
                                            <FormLabel>
                                                Date Published
                                            </FormLabel>
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
                                                            {isValidDate(
                                                                datePublished,
                                                            ) &&
                                                            datePublished !==
                                                                undefined ? (
                                                                format(
                                                                    datePublished,
                                                                    "PPP",
                                                                )
                                                            ) : (
                                                                <span>
                                                                    {" "}
                                                                    Click Here!{" "}
                                                                </span>
                                                            )}
                                                        </Button>
                                                    </PopoverTrigger>
                                                    <PopoverContent className="w-auto p-0">
                                                        <Calendar
                                                            mode="single"
                                                            selected={
                                                                datePublished
                                                            }
                                                            onSelect={(
                                                                date,
                                                            ) => {
                                                                setDatePublished(
                                                                    date,
                                                                );
                                                                field.onChange(
                                                                    date,
                                                                );
                                                            }}
                                                            disabled={(date) =>
                                                                date >
                                                                    new Date() ||
                                                                date <
                                                                    new Date(
                                                                        "1900-01-01",
                                                                    )
                                                            }
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
                                {/*<FormField*/}
                                {/*    control={form.control}*/}
                                {/*    name="rights"*/}
                                {/*    render={() => (*/}
                                {/*        <FormItem>*/}
                                {/*            <FormLabel>Rights</FormLabel>*/}
                                {/*            <FormControl>*/}
                                {/*                /!*<Input placeholder="Rights" {...field} />*!/*/}
                                {/*            </FormControl>*/}
                                {/*            <FormMessage />*/}
                                {/*        </FormItem>*/}
                                {/*    )}*/}
                                {/*/>*/}
                                {/*<FormField*/}
                                {/*    control={form.control}*/}
                                {/*    name="extraFields"*/}
                                {/*    render={({ field }) => (*/}
                                {/*        <FormItem>*/}
                                {/*            <FormLabel>*/}
                                {/*                Extra Information*/}
                                {/*            </FormLabel>*/}
                                {/*            <FormControl>*/}
                                {/*                <Input*/}
                                {/*                    placeholder="Extra Fields"*/}
                                {/*                    {...(field ?? "")}*/}
                                {/*                />*/}
                                {/*            </FormControl>*/}
                                {/*            <FormMessage />*/}
                                {/*        </FormItem>*/}
                                {/*    )}*/}
                                {/*/>*/}
                            </CardContent>

                            <CardFooter>
                                *Applicable to all reference types
                            </CardFooter>
                        </Card>
                        <div className={"col-span-3 w-full"}>
                            <Card>
                                <CardHeader
                                    className={
                                        "text-center content-center justify-center"
                                    }
                                >
                                    <CardTitle>Creators</CardTitle>
                                </CardHeader>
                                <CardContent>
                                    <ul className="grid grid-cols-2 grid-rows-1 place-items-center">
                                        {fields.map((item, index) => {
                                            return (
                                                <li key={item.id}>
                                                    <CreatorInput
                                                        name={`creators.${index}`}
                                                    />
                                                    <button
                                                        type="button"
                                                        onClick={() =>
                                                            remove(index)
                                                        }
                                                    >
                                                        <SquareX />
                                                    </button>
                                                </li>
                                            );
                                        })}
                                    </ul>
                                    <Button
                                        className="m-2"
                                        type="button"
                                        onClick={onClickAddCreator}
                                    >
                                        + Add Another
                                    </Button>
                                </CardContent>
                            </Card>
                        </div>

                        <Card className={"col-span-3 w-full"}>
                            <CardHeader>
                                <CardTitle>Reference Specific</CardTitle>
                            </CardHeader>

                            <CardContent className={"grid grid-cols-2 gap-4"}>
                                {Object.keys(refSchema.shape)
                                    .filter((key) => !key.includes("title"))
                                    .filter(
                                        (key) => !key.includes("shortTitle"),
                                    )
                                    .filter((key) => !key.includes("language"))
                                    .filter(
                                        (key) => !key.includes("datePublished"),
                                    )
                                    .filter((key) => !key.includes("creators"))
                                    .map((key) => (
                                        <Controller
                                            key={key}
                                            control={control}
                                            name={`${key}` || ""}
                                            defaultValue={""}
                                            render={({ field }) => (
                                                <FormItem>
                                                    <FormLabel>
                                                        {key.replace(
                                                            /([A-Z])/g,
                                                            " $1",
                                                        )}
                                                        :
                                                    </FormLabel>
                                                    <FormControl className={""}>
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
                            </CardContent>
                        </Card>

                        {errors.root && <p>{errors.root?.message}</p>}

                        <Button className="col-end-2 m-2" type="submit">
                            Submit
                        </Button>
                    </form>
                </Form>
            </FormProvider>
            <DevTool control={control} />
        </>
    );
}

import React, { useEffect, useState } from "react";
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
import { Controller, FormProvider, useForm } from "react-hook-form";
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
import { CreatorInput } from "@/CreateRefComponents/CreatorInput.tsx";
import { useToast } from "@/hooks/use-toast.ts";
import { Tag } from "emblor";

let nextId = 0;

const route = getRouteApi("/(reference)/edit-reference/$refId");

export function EditReferencePage({ reference }) {
    const [creatorArray, setCreatorArray] = useState<React.ReactNode[]>([]);
    const [datePublished, setDatePublished] = React.useState<Date>();
    const navigate = useNavigate();
    const { toast } = useToast();
    const { refId } = route.useParams();
    const [refSchema, setRefSchema] =
        useState<ZodObject<any>>(baseReferenceSchema);
    const [schemaName, setSchemaName] = useState("");

    const form = useForm<z.infer<typeof refSchema>>({
        resolver: refSchema ? zodResolver(refSchema) : undefined,
        // defaultValues: async () => {
        //     return await getReferenceData();
        // },
    });

    const {
        control,
        formState: { errors, isLoading },
        handleSubmit,
    } = form;
    // const getReferenceData = async () => {
    //     try {
    //         const res = await instance.get(
    //             `Reference/get-single-reference?refId=${refId}`,
    //             {
    //                 withCredentials: true,
    //             },
    //         );
    //         console.log(res);
    //
    //         if (res.status == 200) {
    //             let newSchema: ZodObject<any>;
    //             let name = "";
    //
    //             setDatePublished(new Date(res.data.datePublished));
    //             switch (res.data.type) {
    //                 case "Artwork":
    //                     newSchema = artworkSchema;
    //                     name = "artwork";
    //                     break;
    //                 case "Book":
    //                     newSchema = bookSchema;
    //                     name = "book";
    //                     break;
    //                 case "Report":
    //                     newSchema = reportSchema;
    //                     name = "report";
    //                     break;
    //                 case "Website":
    //                     newSchema = websiteSchema;
    //                     name = "website";
    //                     break;
    //                 default:
    //                     newSchema = baseReferenceSchema;
    //                     break;
    //             }
    //
    //             console.log(res.data);
    //
    //             if (res.data.creators.length !== null) {
    //                 console.log(res.data.creators);
    //                 console.log(creatorArray);
    //                 res.data.creators.map(
    //                     (creator: {
    //                         id: number;
    //                         firstName: string;
    //                         lastName: string;
    //                         types: string;
    //                     }) => {
    //                         console.log(creator);
    //                         creatorArray.push(
    //                             <CreatorInput
    //                                 firstName={creator.firstName}
    //                                 lastName={creator.lastName}
    //                                 types={creator.types}
    //                                 name={`creators.${nextId}`}
    //                                 key={creator.id}
    //                             />,
    //                         );
    //                     },
    //                 );
    //                 nextId++;
    //             }
    //
    //             setRefSchema(newSchema);
    //             setSchemaName(name);
    //             return res.data;
    //         } else {
    //             console.error(res);
    //         }
    //     } catch (e) {
    //         console.error(e);
    //     }
    // };
    function onClickAddCreator() {
        setCreatorArray([
            ...creatorArray,
            <CreatorInput
                firstName={""}
                lastName={""}
                types={""}
                name={`creators.${nextId}`}
                key={nextId}
            />,
        ]);
        nextId++;
    }

    function onClickRemoveCreator(removeId: string | null) {
        if (removeId === null) {
            toast({
                variant: "destructive",
                title: "Cannot remove creator",
                description: `Unable to remove creator!`,
            });
        } else if (creatorArray.length <= 0) {
            toast({
                variant: "default",
                title: "Creator's is empty",
                description: ``,
            });
        } else {
            setCreatorArray(
                creatorArray.filter(
                    (item: React.ReactNode) =>
                        (item as React.ReactElement).key !== removeId,
                ),
            );
        }
    }

    function generateRandom32BitInteger() {
        const max = 500000;
        return Math.floor(Math.random() * (max + 1));
    }
    const onSubmit = async (data: any) => {
        console.log(data);
        const convertedTags = data.tags?.map((tag: Tag) => ({
            ...tag,
            id: generateRandom32BitInteger(),
        }));

        const newData = {
            ...data,
            tags: convertedTags,
        };

        try {
            // figure out which reference type is being used
            console.log(newData);
            const response = await instance.put(
                `Reference/update-${refId}`,
                newData,
                {
                    withCredentials: true,
                },
            );
            if (response.status === 200) {
                toast({
                    variant: "default",
                    title: "Reference Successfully Updated!",
                    description: ``,
                });
            } else {
                toast({
                    variant: "destructive",
                    title: "Reference Not Created Successfully",
                    description: ``,
                });
            }

            await navigate({ to: "/library" });
            console.log("SUCCESS");
        } catch (error) {
            console.error("Update reference failed:", error);
        }
    };

    if (isLoading || !refSchema) {
        return <div>Loading...</div>;
    }

    useEffect(() => {
        console.log(reference);
    }, []);

    console.log("REFERENCE");
    console.log(reference.data);

    console.log(errors);

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
                                                            {/*{datePublished ? (*/}
                                                            {/*    format(*/}
                                                            {/*        // datePublished,*/}
                                                            {/*        "PPP",*/}
                                                            {/*    )*/}
                                                            {/*) : (*/}
                                                            {/*    <span>*/}
                                                            {/*        {" "}*/}
                                                            {/*        Click Here!{" "}*/}
                                                            {/*    </span>*/}
                                                            {/*)}*/}
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
                                        {creatorArray.map((creator) => (
                                            <li
                                                key={
                                                    (
                                                        creator as React.ReactElement
                                                    ).key
                                                }
                                                className={"col-span-3 w-full"}
                                            >
                                                <div
                                                    className={
                                                        "flex justify-between items-center p-1"
                                                    }
                                                >
                                                    {creator}
                                                    <div className={""}>
                                                        <SquareX
                                                            className="h-5 w-5"
                                                            onClick={() => {
                                                                onClickRemoveCreator(
                                                                    (
                                                                        creator as React.ReactElement
                                                                    ).key,
                                                                );
                                                            }}
                                                        />
                                                    </div>
                                                </div>
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
        </>
    );
}

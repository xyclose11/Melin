import React, { Suspense, useEffect, useState } from "react";
import { BaseReferenceCreator } from "@/routes/CustomComponents/CreateRefComponents/BaseReferenceCreator.tsx";
import { instance } from "@/utils/axiosInstance.ts";
import { useNavigate, useParams } from "react-router-dom";
import {
    artworkSchema,
    baseReferenceSchema,
} from "@/routes/ReferenceCreationPages/BaseReferenceSchema.ts";
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
import { TagCreateDropdown } from "@/routes/CustomComponents/Tag/TagCreateDropdown.tsx";
import { Input } from "@/components/ui/input.tsx";
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover.tsx";
import { Button } from "@/components/ui/button.tsx";
import { cn } from "@/lib/utils.ts";
import { CalendarIcon, SquareX } from "lucide-react";
import { format } from "date-fns";
import { Calendar } from "@/components/ui/calendar.tsx";
import { CreatorInput } from "@/routes/CustomComponents/CreateRefComponents/CreatorInput.tsx";
import { useToast } from "@/hooks/use-toast.ts";

let nextId = 0;

export function EditReferencePage() {
    const [creatorArray, setCreatorArray] = useState<React.ReactNode[]>([]);
    const [datePublished, setDatePublished] = React.useState<Date>();
    const navigate = useNavigate();
    const { toast } = useToast();
    const { refId } = useParams();
    let refSchema: ZodObject<any> = baseReferenceSchema;

    const form = useForm<z.infer<typeof refSchema>>({
        resolver: zodResolver(refSchema),
        defaultValues: async () => {
            return await getReferenceData();
        },
    });

    const {
        control,
        formState: { errors },
    } = form;
    const getReferenceData = async () => {
        try {
            const res = await instance.get(
                `Reference/get-single-reference?refId=${refId}`,
                {
                    withCredentials: true,
                },
            );

            if (res.status == 200) {
                console.log(res.data);
                // refName = res.data.type;

                switch (res.data.type) {
                    case "Artwork":
                        refSchema = artworkSchema;
                        return res.data;
                }
            } else {
                console.error(res);
            }
        } catch (e) {
            console.error(e);
        }
    };
    function onClickAddCreator() {
        setCreatorArray([
            ...creatorArray,
            <CreatorInput name={`creators.${nextId}`} key={nextId} />,
        ]);
        nextId++;
    }

    function onClickRemoveCreator(removeId: string | null) {
        if (removeId === null) {
            toast("", {
                variant: "destructive",
                title: "Cannot remove creator",
                description: `Unable to remove creator!`,
            });
        } else if (creatorArray.length <= 0) {
            toast("", {
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
    const onSubmit = async (data: z.infer<typeof refSchema>) => {
        const convertedTags = data.tags?.map((tag) => ({
            ...tag,
            id: generateRandom32BitInteger(),
        }));

        const newData = {
            ...data,
            tags: convertedTags,
        };

        try {
            // figure out which reference type is being used
            const response = await instance.post(
                `Reference/create-${refSchema}`,
                newData,
                {
                    withCredentials: true,
                },
            );
            if (response.status === 200) {
                toast({
                    variant: "default",
                    title: "Reference Successfully Created!",
                    description: ``,
                });
            } else {
                toast({
                    variant: "destructive",
                    title: "Reference Not Created Successfully",
                    description: ``,
                });
            }

            navigate("/library");
            console.log("SUCCESS");
        } catch (error) {
            console.error("Create reference failed:", error);
        }
    };
    // useEffect(() => {
    //     getReferenceData();
    // }, []);

    return (
        <>
            <Suspense fallback={<div> LOADING...</div>}>
                <FormProvider {...form}>
                    <Form {...form}>
                        <form
                            onSubmit={form.handleSubmit(onSubmit)}
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
                                                <FormLabel>
                                                    Short Title
                                                </FormLabel>
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
                                                                variant={
                                                                    "outline"
                                                                }
                                                                className={cn(
                                                                    "w-[280px] justify-start text-left font-normal",
                                                                    !datePublished &&
                                                                        "text-muted-foreground",
                                                                )}
                                                            >
                                                                <CalendarIcon />
                                                                {datePublished ? (
                                                                    format(
                                                                        datePublished,
                                                                        "PPP",
                                                                    )
                                                                ) : (
                                                                    <span>
                                                                        {" "}
                                                                        Click
                                                                        Here!{" "}
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
                                        render={() => (
                                            <FormItem>
                                                <FormLabel>Rights</FormLabel>
                                                <FormControl>
                                                    {/*<Input placeholder="Rights" {...field} />*/}
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
                                                <FormLabel>
                                                    Extra Information
                                                </FormLabel>
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
                                                    className={
                                                        "col-span-3 w-full"
                                                    }
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

                                <CardContent
                                    className={"grid grid-cols-2 gap-4"}
                                >
                                    {Object.keys(refSchema.shape).map((key) => (
                                        <Controller
                                            key={key}
                                            control={control}
                                            name={
                                                `bookSchema.${key}` as keyof z.infer<
                                                    typeof refSchema
                                                >
                                            }
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
                                </CardContent>
                            </Card>

                            {errors.creators && (
                                <div>{errors.creators.message}</div>
                            )}
                            {errors.title && <div>{errors.title.message}</div>}
                            {errors.shortTitle && (
                                <div>{errors.shortTitle.message}</div>
                            )}
                            {errors.rights && (
                                <div>{errors.rights.message}</div>
                            )}
                            {errors.datePublished && (
                                <div>{errors.datePublished.message}</div>
                            )}
                            {errors.extraFields && (
                                <div>{errors.extraFields.message}</div>
                            )}
                            {errors.language && (
                                <div>{errors.language.message}</div>
                            )}
                            {errors.tags && <div> {errors.tags.message}</div>}

                            {errors.root && <div> {errors.root.message}</div>}

                            <Button className="col-end-2 m-2" type="submit">
                                Submit
                            </Button>
                        </form>
                    </Form>
                </FormProvider>
            </Suspense>
        </>
    );
}

"use client";
import { z, ZodObject } from "zod";

import { zodResolver } from "@hookform/resolvers/zod";
import {
    Controller,
    FormProvider,
    useFieldArray,
    useForm,
} from "react-hook-form";

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
    CREATOR_TYPES,
    creatorFormSchema,
    CreatorInput,
} from "@/CreateRefComponents/CreatorInput.tsx";
import { SquareX } from "lucide-react";
import { instance } from "@/utils/axiosInstance.ts";
import { TagCreateDropdown, tagSchema } from "@/Tag/TagCreateDropdown.tsx";
import { useToast } from "@/hooks/use-toast.ts";
import { useNavigate } from "@tanstack/react-router";
import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import { DevTool } from "@hookform/devtools";

const rightsSchema = z.object({
    name: z.string().optional(),
});

const formSchema = z.object({
    type: z.string(),
    title: z.string().min(2, {
        message: "Title must be at least 2 characters.",
    }),
    shortTitle: z.string().optional(),
    language: z.string().optional(),
    datePublished: z.string().optional(),
    locationStored: z.string().optional(),
    rights: z.array(rightsSchema).optional(),
    extraFields: z.string().optional(),
    creators: z.array(creatorFormSchema).optional(),
    tags: z.array(tagSchema).optional(),
});

export function BaseReferenceCreator({
    refSchema,
    schemaName,
}: {
    refSchema: ZodObject<any>;
    schemaName: string;
}) {
    const navigate = useNavigate();
    const { toast } = useToast();

    const form = useForm<z.infer<typeof refSchema>>({
        resolver: zodResolver(refSchema),
        defaultValues: {
            type: "0",
            title: "",
            shortTitle: "",
            datePublished: "",
            language: "English",
            locationStored: "",
            extraFields: undefined,
            rights: undefined,
            creators: [{}],
            tags: [],
        },
    });

    function generateRandom32BitInteger() {
        const max = 500000;
        return Math.floor(Math.random() * (max + 1));
    }

    const onSubmit = async (data: any) => {
        const convertedTags = data.tags?.map((tag: any) => ({
            ...tag,
            id: generateRandom32BitInteger(),
        }));
        console.log(data);

        const newData = {
            ...data,
            tags: convertedTags,
        };

        newData.type = schemaName;

        try {
            // figure out which reference type is being used
            const response = await instance.post(
                `Reference/create-reference`,
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

            navigate({ to: "/library" });
        } catch (error) {
            console.error("Create reference failed:", error);
        }
    };

    const {
        control,
        formState: { errors },
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

    return (
        <div className="justify-center">
            <FormProvider {...form}>
                <Form {...form}>
                    <form
                        onSubmit={form.handleSubmit(onSubmit)}
                        className="gap-2 justify-items-start grid grid-cols-1"
                    >
                        <Card className={"w-full"}>
                            <CardHeader className={"justify-center"}>
                                <CardTitle className="justify-center">
                                    Shared Fields
                                </CardTitle>
                            </CardHeader>
                            <CardContent className={"w-full"}>
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
                                                <Input
                                                    placeholder="datePublished"
                                                    {...field}
                                                />
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
                                <FormField
                                    control={form.control}
                                    name="locationStored"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>
                                                Location Stored
                                            </FormLabel>
                                            <FormControl>
                                                <Input
                                                    placeholder="Bookshelf, Coffee Table,..."
                                                    {...field}
                                                />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />
                                <TagCreateDropdown />
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

                        <Card className={"col-span-2 w-full"}>
                            <CardHeader>
                                <CardTitle>Reference Specific</CardTitle>
                            </CardHeader>

                            <CardContent className={"grid grid-cols-2 gap-4"}>
                                {Object.keys(refSchema.shape)
                                    .filter((schemaField) => {
                                        const excludedFields = [
                                            "title",
                                            "shortTitle",
                                            "language",
                                            "datePublished",
                                            "creators",
                                            "type",
                                        ];
                                        return !excludedFields.includes(
                                            schemaField,
                                        );
                                    })
                                    .map((key) => (
                                        <Controller
                                            key={key}
                                            control={form.control}
                                            name={
                                                `${key}` as keyof z.infer<
                                                    typeof formSchema
                                                >
                                            }
                                            defaultValue={""}
                                            render={({ field }) => (
                                                <FormItem>
                                                    <FormLabel>
                                                        {key
                                                            .replace(
                                                                /([A-Z])/g,
                                                                " $1",
                                                            )
                                                            .charAt(0)
                                                            .toUpperCase() +
                                                            key.slice(1)}
                                                        :
                                                    </FormLabel>
                                                    <FormControl className={""}>
                                                        {/*@ts-ignore*/}
                                                        <Input
                                                            placeholder={
                                                                key
                                                                    .replace(
                                                                        /([A-Z])/g,
                                                                        " $1",
                                                                    )
                                                                    .charAt(0)
                                                                    .toUpperCase() +
                                                                key.slice(1)
                                                            }
                                                            {...form.register(
                                                                field.name,
                                                            )}
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
                        {errors.rights && <div>{errors.rights.message}</div>}
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
            <DevTool control={control} />
        </div>
    );
}

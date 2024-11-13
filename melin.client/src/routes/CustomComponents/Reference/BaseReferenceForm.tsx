import { Controller, FormProvider } from "react-hook-form";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from "@/components/ui/card.tsx";
import { TagCreateDropdown } from "@/routes/CustomComponents/Tag/TagCreateDropdown.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover.tsx";
import { Button } from "@/components/ui/button.tsx";
import { cn } from "@/lib/utils.ts";
import { CalendarIcon, SquareX } from "lucide-react";
import { format } from "date-fns";
import { Calendar } from "@/components/ui/calendar.tsx";
import React, { useEffect } from "react";
import { z } from "zod";
import { CreatorInput } from "@/routes/CustomComponents/CreateRefComponents/CreatorInput.tsx";
import { toast } from "@/hooks/use-toast.ts";

export function BaseReferenceForm() {

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

    function onClickRemoveCreator(removeId: string | null) {
        if (removeId === null) {
            toast("",{
                variant: "destructive",
                title: "Cannot remove creator",
                description: `Unable to remove creator!`,
            });
        } else if (creatorArray.length <= 0) {
            toast("",{
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

    useEffect(() => {
        onClickAddCreator();
    }, []);
    return (
        <div>
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
                                                            {datePublished ? (
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
        </div>
    )
}
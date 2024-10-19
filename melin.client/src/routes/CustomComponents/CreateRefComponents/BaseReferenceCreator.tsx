"use client";
import { z } from "zod";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

import { Button } from "@/components/ui/button";
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { CreatorInput } from "@/routes/CustomComponents/CreateRefComponents/CreatorInput.tsx";
import { useState } from "react";

const formSchema = z.object({
    title: z.string().min(2, {
        message: "Title must be at least 2 characters.",
    }),
});

let nextId = 0;

export function BaseReferenceCreator() {
    const [creatorArray, setCreatorArray] = useState<React.ReactNode[]>([]);

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            title: "",
        },
    });

    function onSubmit(values: z.infer<typeof formSchema>) {
        console.log(values);
    }

    function onClickAddCreator() {
        setCreatorArray([...creatorArray, <CreatorInput key={nextId} />]);
        nextId++;
    }

    return (
        <div>
            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(onSubmit)}
                    className="space-y-8"
                >
                    <FormField
                        control={form.control}
                        name="title"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Title</FormLabel>
                                <FormControl>
                                    <Input placeholder="Title" {...field} />
                                </FormControl>
                                <FormDescription></FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <Button type="submit">Submit</Button>
                </form>
            </Form>

            <ul>
                {creatorArray.map((creator) => (
                    <li key={(creator as React.ReactElement).key}>{creator}</li>
                ))}
            </ul>

            <Button type="button" onClick={onClickAddCreator}>
                + Add Another
            </Button>
        </div>
    );
}

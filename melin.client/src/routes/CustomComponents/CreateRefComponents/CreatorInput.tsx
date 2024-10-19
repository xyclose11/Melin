﻿import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { Check, ChevronsUpDown } from "lucide-react";
import { cn } from "@/lib/utils.ts";
import {
    Command,
    CommandEmpty,
    CommandGroup,
    CommandInput,
    CommandItem,
    CommandList,
} from "@/components/ui/command";
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover";
import { Button } from "@/components/ui/button";
const CREATOR_TYPES = [
    { label: "Author", value: "author" },
    { label: "Editor", value: "editor" },
    { label: "Series Editor", value: "series-editor" },
    { label: "Translator", value: "translator" },
    { label: "Reviewed Author", value: "reviewed-author" },
    { label: "Artist", value: "artist" },
    { label: "Performer", value: "performer" },
    { label: "Composer", value: "composer" },
    { label: "Director", value: "director" },
    { label: "Podcaster", value: "podcaster" },
    { label: "Cartographer", value: "cartographer" },
    { label: "Programmer", value: "programmer" },
    { label: "Presenter", value: "presenter" },
    { label: "Interview With", value: "interview-with" },
    { label: "Interviewer", value: "interviewer" },
    { label: "Recipient", value: "recipient" },
    { label: "Sponsor", value: "sponsor" },
    { label: "Inventor", value: "inventor" },
] as const;

const formSchema = z.object({
    creatorType: z.enum(
        CREATOR_TYPES.map((type) => type.value) as [string, ...string[]],
        { errorMap: () => ({ message: "Invalid Creator Type" }) },
    ),
    firstName: z.string().min(2, {
        message: "First Name must be at least 2 characters.",
    }),
    lastName: z.string().min(2, {
        message: "Last Name must be at least 2 characters.",
    }),
});

export function CreatorInput() {
    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            creatorType: "author",
            firstName: "",
            lastName: "",
        },
    });

    return (
        <div>
            <Form {...form}>
                <form className="space-y-8 columns-2">
                    <FormField
                        control={form.control}
                        name="creatorType"
                        render={({ field }) => (
                            <FormItem className="flex flex-col">
                                <FormLabel>creatorType</FormLabel>
                                <Popover>
                                    <PopoverTrigger asChild>
                                        <FormControl>
                                            <Button
                                                variant="outline"
                                                role="combobox"
                                                className={cn(
                                                    "w-[200px] justify-between",
                                                    !field.value &&
                                                        "text-muted-foreground",
                                                )}
                                            >
                                                {field.value
                                                    ? CREATOR_TYPES.find(
                                                          (creatorType) =>
                                                              creatorType.value ===
                                                              field.value,
                                                      )?.label
                                                    : "Select language"}
                                                <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                                            </Button>
                                        </FormControl>
                                    </PopoverTrigger>
                                    <PopoverContent className="w-[200px] p-0">
                                        <Command>
                                            <CommandInput placeholder="Search language..." />
                                            <CommandList>
                                                <CommandEmpty>
                                                    No language found.
                                                </CommandEmpty>
                                                <CommandGroup>
                                                    {CREATOR_TYPES.map(
                                                        (creatorType) => (
                                                            <CommandItem
                                                                value={
                                                                    creatorType.label
                                                                }
                                                                key={
                                                                    creatorType.value
                                                                }
                                                                onSelect={() => {
                                                                    form.setValue(
                                                                        "creatorType",
                                                                        creatorType.value,
                                                                    );
                                                                }}
                                                            >
                                                                <Check
                                                                    className={cn(
                                                                        "mr-2 h-4 w-4",
                                                                        creatorType.value ===
                                                                            field.value
                                                                            ? "opacity-100"
                                                                            : "opacity-0",
                                                                    )}
                                                                />
                                                                {
                                                                    creatorType.label
                                                                }
                                                            </CommandItem>
                                                        ),
                                                    )}
                                                </CommandGroup>
                                            </CommandList>
                                        </Command>
                                    </PopoverContent>
                                </Popover>
                                <FormDescription>
                                    This is the language that will be used in
                                    the dashboard.
                                </FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="firstName"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>First Name</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="First Name"
                                        {...field}
                                    />
                                </FormControl>
                                <FormDescription></FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="lastName"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Last Name</FormLabel>
                                <FormControl>
                                    <Input placeholder="Last Name" {...field} />
                                </FormControl>
                                <FormDescription></FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                </form>
            </Form>
        </div>
    );
}

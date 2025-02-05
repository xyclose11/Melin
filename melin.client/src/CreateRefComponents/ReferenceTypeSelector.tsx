﻿"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { Check, ChevronsUpDown } from "lucide-react";
import { useForm } from "react-hook-form";
import { z } from "zod";

import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import {
    Command,
    CommandEmpty,
    CommandGroup,
    CommandInput,
    CommandItem,
    CommandList,
} from "@/components/ui/command";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover";

const REFERENCE_TYPES = [
    { label: "Artwork", value: "artwork" },
    { label: "Audio Recording", value: "audio-recording" },
    // { label: "Bill", value: "bill" },
    // { label: "Blog Post", value: "blog-post" },
    { label: "Book", value: "book" },
    // { label: "Book Section", value: "book-section" },
    { label: "Case", value: "case" },
    // { label: "Conference Paper", value: "conference-paper" },
    // { label: "Dictionary Entry", value: "dictionary-entry" },
    // { label: "Document", value: "document" },
    // { label: "Email", value: "email" },
    // { label: "Encyclopedia Article", value: "encyclopedia-article" },
    // { label: "Film", value: "film" },
    // { label: "Forum Post", value: "forum-post" },
    // { label: "Hearing", value: "hearing" },
    // { label: "Instant Message", value: "instant-message" },
    // { label: "Interview", value: "interview" },
    // { label: "Journal Article", value: "journal-article" },
    // { label: "Magazine Article", value: "magazine-article" },
    // { label: "Manuscript", value: "manuscript" },
    // { label: "Map", value: "map" },
    // { label: "Newspaper Article", value: "newspaper-article" },
    { label: "Patent", value: "patent" },
    // { label: "Podcast", value: "podcast" },
    { label: "Presentation", value: "presentation" },
    // { label: "Radio Broadcast", value: "radio-broadcast" },
    { label: "Report", value: "report" },
    { label: "Software", value: "software" },
    // { label: "Statute", value: "statute" },
    // { label: "Thesis", value: "thesis" },
    // { label: "TV Broadcast", value: "tv-broadcast" },
    // { label: "Video Recording", value: "video-recording" },
    { label: "Website", value: "website" },
    // { label: "Attachment", value: "attachment" },
    // { label: "Note", value: "note" },
] as const;

const FormSchema = z.object({
    refType: z.string({
        required_error: "Please select a Reference Type.",
    }),
});

export function ReferenceTypeSelector({ handleState }: any) {
    const form = useForm<z.infer<typeof FormSchema>>({
        resolver: zodResolver(FormSchema),
    });

    function handleChange() {
        handleState(form.getValues().refType);
    }

    return (
        <Form {...form}>
            <form className="space-y-6">
                <FormField
                    control={form.control}
                    name="refType"
                    render={({ field }) => (
                        <FormItem className="flex flex-col">
                            <FormLabel>Type</FormLabel>
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
                                                ? REFERENCE_TYPES.find(
                                                      (types) =>
                                                          types.value ===
                                                          field.value,
                                                  )?.label
                                                : "Select Reference Type"}
                                            <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                                        </Button>
                                    </FormControl>
                                </PopoverTrigger>
                                <PopoverContent className="w-[200px] p-0">
                                    <Command>
                                        <CommandInput
                                            className="bg-background p-0 m-0"
                                            placeholder="Search Reference Types..."
                                        />
                                        <CommandList>
                                            <CommandEmpty>
                                                No reference type found.
                                            </CommandEmpty>
                                            <CommandGroup>
                                                {REFERENCE_TYPES.map(
                                                    (types) => (
                                                        <CommandItem
                                                            value={types.label}
                                                            key={types.value}
                                                            onSelect={() => {
                                                                form.setValue(
                                                                    "refType",
                                                                    types.value,
                                                                );
                                                                handleChange();
                                                            }}
                                                        >
                                                            <Check
                                                                className={cn(
                                                                    "mr-2 h-4 w-4",
                                                                    types.value ===
                                                                        field.value
                                                                        ? "opacity-100"
                                                                        : "opacity-0",
                                                                )}
                                                            />
                                                            {types.label}
                                                        </CommandItem>
                                                    ),
                                                )}
                                            </CommandGroup>
                                        </CommandList>
                                    </Command>
                                </PopoverContent>
                            </Popover>
                            <FormMessage />
                        </FormItem>
                    )}
                />
            </form>
        </Form>
    );
}

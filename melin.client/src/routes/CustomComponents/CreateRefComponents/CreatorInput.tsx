import {
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { useFormContext } from "react-hook-form";
import { z } from "zod";
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

export const CREATOR_TYPES = [
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

export const creatorFormSchema = z.object({
    id: z.number(),
    creatorType: z
        .enum(
            CREATOR_TYPES.map((type) => type.value) as [string, ...string[]],
            { errorMap: () => ({ message: "Invalid Creator Type" }) },
        )
        .optional(),
    firstName: z.string().optional(),
    lastName: z.string().optional(),
    reference: z.string().optional(),
});

export function CreatorInput({ name }: { name: string }) {
    const { control } = useFormContext();

    return (
        <div className={"grid grid-cols-4 grid-flow-col"}>
            <FormField
                control={control}
                name={`${name}.creatorType`}
                render={({ field }) => (
                    <FormItem className="flex flex-col">
                        <FormLabel>Creator Type</FormLabel>
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
                                            : "Select Creator Type"}
                                        <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                                    </Button>
                                </FormControl>
                            </PopoverTrigger>
                            <PopoverContent className="w-[200px] p-0">
                                <Command>
                                    <CommandInput placeholder="Search creator types..." />
                                    <CommandList>
                                        <CommandEmpty>
                                            No creator type found.
                                        </CommandEmpty>
                                        <CommandGroup>
                                            {CREATOR_TYPES.map(
                                                (creatorType) => (
                                                    <CommandItem
                                                        value={
                                                            creatorType.label
                                                        }
                                                        key={creatorType.value}
                                                        onSelect={() => {
                                                            field.onChange(
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
                                                        {creatorType.label}
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

            <FormField
                control={control}
                name={`${name}.firstName`}
                render={({ field }) => (
                    <FormItem>
                        <FormLabel>First Name</FormLabel>
                        <FormControl>
                            <Input placeholder="First Name" {...field} />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )}
            />
            <FormField
                control={control}
                name={`${name}.lastName`}
                render={({ field }) => (
                    <FormItem>
                        <FormLabel>Last Name</FormLabel>
                        <FormControl>
                            <Input
                                className={"h-8"}
                                placeholder="Last Name"
                                {...field}
                            />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )}
            />
        </div>
    );
}

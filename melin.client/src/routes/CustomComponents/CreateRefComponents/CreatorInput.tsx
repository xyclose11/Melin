import {
    FormControl,
    FormField,
    FormItem,
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
    id: z.number().optional(),
    creatorType: z
        .enum(
            CREATOR_TYPES.map((type) => type.value) as [string, ...string[]],
            { errorMap: () => ({ message: "Invalid Creator Type" }) },
        )
        .optional(),
    firstName: z.string().optional(),
    lastName: z.string().optional(),
});

export function CreatorInput({ name }: { name: string }) {
    const {
        control,
        setValue,
        register,
        formState: { errors },
    } = useFormContext();

    return (
        <span className={"grid grid-cols-3"}>
            <FormField
                control={control}
                name={`${name}.creatorType`}
                render={({ field }) => (
                    <FormItem className="flex flex-col">
                        <Popover>
                            <PopoverTrigger asChild>
                                <FormControl>
                                    <Button
                                        variant="secondary"
                                        role="combobox"
                                        className={cn(
                                            "w-[80px] h-[26px] justify-between",
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
                                            : "Type"}
                                        <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-100" />
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
                render={() => (
                    <FormItem className={"w-[120px] mr-1"}>
                        <FormControl>
                            <Input
                                className={"h-8"}
                                placeholder="First Name"
                                defaultValue=""
                                {...register(`${name}.firstName`)}
                                onChange={(e) => {
                                    setValue(
                                        `${name}.firstName`,
                                        e.target.value,
                                    );
                                }}
                            />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )}
            />
            <FormField
                control={control}
                name={`${name}.lastName`}
                render={() => (
                    <FormItem className={"w-[120px] ml-1"}>
                        <FormControl>
                            <Input
                                className={"h-8"}
                                placeholder="Last Name"
                                defaultValue={""}
                                {...register(`${name}.lastName`)}
                                onChange={(e) => {
                                    setValue(
                                        `${name}.lastName`,
                                        e.target.value,
                                    );
                                }}
                            />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )}
            />
            {errors.root && <div> {errors.root.message}</div>}
        </span>
    );
}

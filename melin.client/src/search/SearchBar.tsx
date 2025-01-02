import { Input } from "@/components/ui/input.tsx";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
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
import { debounce } from "@/utils/debounce.ts";

const searchBarSchema = z.object({
    searchQuery: z.string().min(1),
});

export function SearchBar({
    handleQueryChange,
}: {
    handleQueryChange: (query: string) => void;
}) {
    const form = useForm<z.infer<typeof searchBarSchema>>({
        resolver: zodResolver(searchBarSchema),
        defaultValues: {
            searchQuery: "",
        },
    });

    function onSubmit(values: z.infer<typeof searchBarSchema>) {
        console.log(values);
    }

    const debouncedSubmit = debounce(onSubmit, 5000);

    return (
        <div className="justify-center w-[400px]">
            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(debouncedSubmit)}
                    className="space-y-8"
                >
                    <FormField
                        control={form.control}
                        name="searchQuery"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Query</FormLabel>
                                <FormControl>
                                    <Input
                                        className="min-w-[60%] max-w-[90%]"
                                        placeholder="ISBN, ISSN, DOI, Title..."
                                        onChangeCapture={(event) =>
                                            handleQueryChange(
                                                event.currentTarget.value,
                                            )
                                        }
                                        {...field}
                                    />
                                </FormControl>
                                <FormDescription>
                                    This is your public display name.
                                </FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    ></FormField>
                    <Button type="submit">Submit</Button>
                </form>
            </Form>
        </div>
    );
}

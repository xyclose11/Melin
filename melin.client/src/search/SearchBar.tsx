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
import { useQuery } from "@tanstack/react-query";
import { Cite } from "@citation-js/core";
import "@citation-js/plugin-csl";
import "@citation-js/plugin-isbn";
import { useState } from "react";
import { isPending } from "@reduxjs/toolkit";

const searchBarSchema = z.object({
    searchQuery: z.string().min(1),
});

export function SearchBar({
    handleQueryChange,
}: {
    handleQueryChange: (query: string) => void;
}) {
    const [userSearch, setUserSearch] = useState("");

    const form = useForm<z.infer<typeof searchBarSchema>>({
        resolver: zodResolver(searchBarSchema),
        defaultValues: {
            searchQuery: "",
        },
    });

    const query = useQuery({
        queryKey: ["userSearch", userSearch],
        queryFn: () => searchWithUserQuery(userSearch),
        staleTime: 50000,
    });

    async function searchWithUserQuery(q: string) {
        try {
            const res = Cite.async(q)
                .then((json) => {
                    console.log(
                        json.format("bibliography", {
                            format: "text",
                            template: "apa",
                        }),
                    );

                    return json.format("bibliography", {
                        format: "text",
                        template: "apa",
                    });
                })
                .catch((error) => {
                    console.log("catch");
                    console.log(error);
                });

            return res;
        } catch (e) {
            console.error(e);
        }
    }

    function onSubmit(values: z.infer<typeof searchBarSchema>) {
        setUserSearch(values.searchQuery);
    }

    const debouncedSubmit = debounce(onSubmit, 5000);

    if (query.isPending) {
        return <span>Loading...</span>;
    }
    if (query.isError) {
        return <span>Error: {query.error.message}</span>;
    }

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
            {query.isFetching && <div>Fetching...</div>}
            {query.isSuccess && <div>Success</div>}
        </div>
    );
}

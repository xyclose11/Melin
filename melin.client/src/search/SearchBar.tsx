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
import { useQuery } from "@tanstack/react-query";
import { Cite } from "@citation-js/core";
import "@citation-js/plugin-csl";
import "@citation-js/plugin-isbn";
import { useEffect, useState } from "react";
import { Progress } from "@/components/ui/progress.tsx";

const searchBarSchema = z.object({
    searchQuery: z.string().min(1),
});

interface Author {
    given: string;
    family: string;
}

export interface CSLJSON {
    type: string;
    author: Author[];
    title: string;
}

export function SearchBar({
    handleQueryChange,
}: {
    handleQueryChange: (query: string) => void;
}) {
    const [userSearch, setUserSearch] = useState("");
    const [progress, setProgress] = useState(27);

    const form = useForm<z.infer<typeof searchBarSchema>>({
        resolver: zodResolver(searchBarSchema),
        defaultValues: {
            searchQuery: "",
        },
    });

    const { isSuccess, isFetching, isPending, isError } = useQuery({
        queryKey: ["userSearch", userSearch],
        queryFn: () => searchWithUserQuery(userSearch),
        staleTime: 50000,
    });

    async function searchWithUserQuery(q: string) {
        try {
            const res = Cite.async(q)
                .then((json: any) => {
                    console.log(json);
                    // console.log(
                    //     json.format("bibliography", {
                    //         format: "text",
                    //         template: "apa",
                    //     }),
                    // );
                    return json.data;
                })
                .catch((error: any) => {
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
        const timer = setTimeout(() => setProgress(99), 176);
        return () => clearTimeout(timer);
    }

    return (
        <div className="justify-center w-[400px]">
            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(onSubmit)}
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
                                        disabled={isFetching}
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
            {isPending && <Progress value={progress} />}

            {isSuccess && <div>Success</div>}
            {isError && <div>Error</div>}
        </div>
    );
}

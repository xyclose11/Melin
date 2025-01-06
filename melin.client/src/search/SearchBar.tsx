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
import "@citation-js/plugin-doi";
import "@citation-js/plugin-wikidata";
import "@citation-js/plugin-software-formats";
import { useState } from "react";
import { Progress } from "@/components/ui/progress.tsx";
import { SearchIcon } from "lucide-react";
import { instance } from "@/utils/axiosInstance.ts";
import { CSLJSON, NameVariable } from "@/utils/CSLJSON.ts";
import { IGoogleBookAPIResponse } from "@/utils/IGoogleBookAPIResponse.ts";

const searchBarSchema = z.object({
    searchQuery: z.string().min(1),
});

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

    const { isFetching, isPending, isError } = useQuery({
        queryKey: ["userSearch", userSearch],
        queryFn: () => searchWithUserQuery(userSearch),
        staleTime: Infinity, // Stale-time set to infinity since the queried data is relatively static
    });

    // Current supported search types: ISBN, DOI, Wikidata QID, GitHub Repo URL, NPM package URL
    async function searchWithUserQuery(q: string) {
        try {
            let citationResult;
            try {
                citationResult = await Cite.async(q);
                return citationResult.data;
            } catch (citationError) {
                console.log("Citation.js parsing failed:", citationError);
            }

            // Fetch normal text results as a fallback or complementary source
            const normalTextRes = await instance.get(
                `https://www.googleapis.com/books/v1/volumes?q=${q}`,
            );

            if (normalTextRes.status === 200) {
                const convertedItems: CSLJSON[] = [];

                normalTextRes.data.items.map((i: IGoogleBookAPIResponse) => {
                    const dateParts: (string | number)[][] = [];
                    if (i.volumeInfo.publishedDate) {
                        const parts = i.volumeInfo.publishedDate.split("-");
                        dateParts.push(
                            parts.map((part) =>
                                isNaN(Number(part)) ? part : Number(part),
                            ),
                        );
                    }

                    convertedItems.push({
                        id: i.id,
                        type: "book",
                        title: i.volumeInfo.title,
                        author: i.volumeInfo.authors.map((a): NameVariable => {
                            return {
                                given: a,
                            };
                        }),
                        // ISBN: i.volumeInfo.industryIdentifiers[0].identifier,
                        publisher: i.volumeInfo.publisher,
                        issued: {
                            "date-parts":
                                dateParts.length > 0 ? dateParts : undefined,
                        },
                        "number-of-pages": i.volumeInfo.pageCount,
                        language: i.volumeInfo.language,
                    });
                });

                return convertedItems;
            }
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
        <div className="justify-center w-full mt-16">
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
                                <FormLabel>Search</FormLabel>
                                <FormControl>
                                    <div className="flex gap-4">
                                        <Input
                                            disabled={isFetching}
                                            className="min-w-[70%] max-w-[100%]"
                                            placeholder="ISBN, ISSN, DOI, Title..."
                                            onChangeCapture={(event) =>
                                                handleQueryChange(
                                                    event.currentTarget.value,
                                                )
                                            }
                                            {...field}
                                        />
                                        <Button type="submit" className="p-2">
                                            <SearchIcon />
                                        </Button>
                                    </div>
                                </FormControl>

                                <FormDescription>
                                    Search by ISBN, DOI, Wikidata, Title, and
                                    more!
                                </FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    ></FormField>
                </form>
            </Form>
            {isPending && <Progress value={progress} />}

            {isError && <div className="text-red-400">No Items Found!</div>}
        </div>
    );
}

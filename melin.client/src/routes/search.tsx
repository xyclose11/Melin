import { createFileRoute } from "@tanstack/react-router";
import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormMessage,
} from "@/components/ui/form.tsx";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input.tsx";
import { Button } from "@/components/ui/button.tsx";
import { useQuery } from "@tanstack/react-query";
import { instance } from "@/utils/axiosInstance.ts";
import { useEffect, useState } from "react";

export const Route = createFileRoute("/search")({
    component: SearchComponent,
});

const searchSchema = z.object({
    searchQuery: z.string().min(1, {
        message: "Search Parameters cannot be empty",
    }),
});

function useDebounce(value, delay) {
    const [debouncedValue, setDebouncedValue] = useState(value);

    useEffect(() => {
        const handler = setTimeout(() => {
            setDebouncedValue(value);
        }, delay);

        return () => {
            clearTimeout(handler);
        };
    }, [value, delay]);

    return debouncedValue;
}

function SearchComponent() {
    const form = useForm<z.infer<typeof searchSchema>>({
        resolver: zodResolver(searchSchema),
        defaultValues: {
            searchQuery: "",
        },
    });

    const debouncedInputValue = useDebounce(form.getValues().searchQuery, 4000);

    const { isLoading, isError, data } = useQuery({
        queryKey: ["search", debouncedInputValue],
        queryFn: async () => {
            return await fetchSearchParams(form.getValues().searchQuery);
        },
        staleTime: 15000,
    });

    const fetchSearchParams = async (searchParams: string) => {
        const res = await instance.get(
            `https://openlibrary.org/search.json?q=${searchParams}`,
        );
        if (res.status === 200) {
            console.log(res.data);
            return res.data;
        } else {
            return "";
        }
    };

    return (
        <div className="w-full flex justify-center items-center">
            <Card className="lg:w-[600px]">
                <CardHeader>
                    <CardTitle>Search</CardTitle>
                    <CardDescription>
                        Search for References by ISBN, title
                    </CardDescription>
                </CardHeader>
                <CardContent>
                    <Form {...form}>
                        <form className="space-y-8">
                            <FormField
                                control={form.control}
                                name="searchQuery"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormControl>
                                            <Input
                                                placeholder="Search..."
                                                {...field}
                                            />
                                        </FormControl>
                                        <FormDescription></FormDescription>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <Button type="submit">Search</Button>
                        </form>
                    </Form>
                    {data && (
                        <ul>
                            {data.docs.map((doc) => {
                                return <li>{doc.title}</li>;
                            })}
                        </ul>
                    )}
                </CardContent>
                <CardFooter>
                    {isError && <p>Error fetching data</p>}

                    {isLoading && <p>Loading...</p>}
                </CardFooter>
            </Card>
        </div>
    );
}

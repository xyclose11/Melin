import { createFileRoute } from "@tanstack/react-router";
import {
    Card,
    CardHeader,
    CardTitle,
    CardContent,
    CardFooter,
    CardDescription,
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

export const Route = createFileRoute("/search")({
    component: SearchComponent,
});

const searchSchema = z.object({
    searchQuery: z.string().min(1, {
        message: "Search Parameters cannot be empty",
    }),
});

function SearchComponent() {
    const form = useForm<z.infer<typeof searchSchema>>({
        resolver: zodResolver(searchSchema),
        defaultValues: {
            searchQuery: "",
        },
    });

    function onSubmit(values: z.infer<typeof searchSchema>) {
        console.log(values);
    }

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
                        <form
                            onSubmit={form.handleSubmit(onSubmit)}
                            className="space-y-8"
                        >
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
                </CardContent>
                <CardFooter>Footer</CardFooter>
            </Card>
        </div>
    );
}

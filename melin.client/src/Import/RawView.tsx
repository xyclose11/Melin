import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
    CardDescription,
} from "@/components/ui/card.tsx";
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import { Textarea } from "@/components/ui/textarea";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

const FormSchema = z.object({
    data: z.string(),
});

export function RawView({ rawData }: { rawData: string }) {
    const form = useForm<z.infer<typeof FormSchema>>({
        resolver: zodResolver(FormSchema),
        defaultValues: { data: rawData },
    });

    return (
        <div>
            <Card>
                <CardHeader>
                    <CardTitle>Raw View</CardTitle>
                    <CardDescription>Here is the Raw view!</CardDescription>
                </CardHeader>
                <CardContent>
                    <Form {...form}>
                        <form className="w-2/3 space-y-6">
                            <FormField
                                control={form.control}
                                name="data"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>Data</FormLabel>
                                        <FormControl>
                                            <Textarea
                                                placeholder=""
                                                className="resize-none"
                                                {...field}
                                            />
                                        </FormControl>
                                        <FormDescription>
                                            You can manually edit the parsed
                                            data here before saving it.
                                        </FormDescription>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                        </form>
                    </Form>
                </CardContent>
                <CardFooter>{/*    Display error messages HERE*/}</CardFooter>
            </Card>
        </div>
    );
}

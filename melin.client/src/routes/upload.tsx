import { createFileRoute } from "@tanstack/react-router";
import { Input } from "@/components/ui/input.tsx";
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { Button } from "@/components/ui/button.tsx";
// users allowed to upload file of type TXT, CSV, CSL-JSON, BibTex
export const Route = createFileRoute("/upload")({
    component: UploadComponent,
});

const MAX_UPLOAD_SIZE = 1024 * 1024 * 3;

const fileFormSchema = z.object({
    file: z
        .instanceof(FileList)
        .refine((list) => list.length > 0, "No Files Selected")
        .refine((list) => list.length <= 5, "Maximum 5 files")
        .transform((list) => Array.from(list))
        .refine(
            (files) => {
                const allowedTypes: { [key: string]: boolean } = {
                    "text/csv": true,
                    "text/plain": true,
                    "application/json": true,
                    "application/x-bibtex": true,
                };
                return files.every((file) => allowedTypes[file.type]);
            },
            {
                message:
                    "Invalid File Type. Allowed Types: CSV, TXT, JSON, BibTex",
            },
        )
        .refine(
            (files) => {
                return files.every((file) => file.size <= MAX_UPLOAD_SIZE);
            },
            {
                message: "File size should not exceed 3MB.",
            },
        ),
});
function UploadComponent() {
    const form = useForm<z.infer<typeof fileFormSchema>>({
        resolver: zodResolver(fileFormSchema),
    });

    const fileRef = form.register("file");

    function onSubmit(values: z.infer<typeof fileFormSchema>) {
        console.log(values);
    }

    return (
        <>
            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(onSubmit)}
                    className="space-y-8"
                >
                    <FormField
                        control={form.control}
                        name="file"
                        render={({}) => (
                            <FormItem>
                                <FormLabel>File Upload</FormLabel>
                                <FormControl>
                                    <Input multiple type="file" {...fileRef} />
                                </FormControl>
                                <FormDescription>File Upload!</FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <Button type="submit">Submit</Button>
                </form>
            </Form>
        </>
    );
}

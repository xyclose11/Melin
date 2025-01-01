﻿import { z } from "zod";
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Button } from "@/components/ui/button.tsx";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
const MAX_UPLOAD_SIZE = 1024 * 1024 * 3;

const fileFormSchema = z.object({
    files: z
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
export type ReferenceType = {
    id: number;
    title: string;
    edition: string;
    language: string;
    type: string;
};
export function ImportFile({
    handleFileChange,
    handleRawDataChange,
}: {
    handleFileChange: (newFile: ReferenceType[]) => void;
    handleRawDataChange: (newRawData: string[]) => void;
}) {
    const form = useForm<z.infer<typeof fileFormSchema>>({
        resolver: zodResolver(fileFormSchema),
    });

    const fileRef = form.register("files");

    function parseJSON(file: File): Promise<ReferenceType> {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = function (e) {
                try {
                    if (e.target === null || e.target.result === null) {
                        reject("JSON Validation is null");
                        return;
                    }
                    if (typeof e.target.result === "string") {
                        const data = JSON.parse(e.target.result);
                        console.log(data);
                        resolve(data);
                    }
                } catch (error) {
                    reject("Invalid JSON format");
                }
            };
            reader.readAsText(file);
        });
    }
    function handleFileUpload(values: z.infer<typeof fileFormSchema>) {
        if (!values) {
            return;
        }

        values.files.forEach((file) => {
            const fileExtension = file.name.split(".").pop()?.toLowerCase();

            file.text().then((rawData) => {
                if (rawData.length > 0) {
                    handleRawDataChange([rawData]);
                }
            });

            switch (fileExtension) {
                case "json":
                    parseJSON(file)
                        .then((data) => {
                            const newFile: ReferenceType = {
                                id: Date.now(),
                                title: data.title,
                                edition: data.edition || "N/A",
                                language: data.language || "N/A",
                                type: data.type || "book",
                            };
                            handleFileChange([newFile]);
                        })
                        .catch((error) => alert(error));
                    break;
                default:
                    alert("Unsupported file type.");
            }
        });
    }
    return (
        <div className="">
            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(handleFileUpload)}
                    className="flex flex-col space-y-6 w-full max-w-md bg-white p-6 rounded-lg shadow-md"
                >
                    <FormField
                        control={form.control}
                        name="files"
                        render={({}) => (
                            <FormItem className="flex flex-col space-y-4">
                                <FormLabel className="text-lg font-medium">
                                    File Upload
                                </FormLabel>
                                <FormControl>
                                    <Input
                                        className="p-2 border rounded-md"
                                        multiple
                                        type="file"
                                        {...fileRef}
                                    />
                                </FormControl>
                                <FormDescription className="text-sm text-gray-500">
                                    File Upload!
                                </FormDescription>
                                <FormMessage className="text-red-500 text-sm" />
                            </FormItem>
                        )}
                    />
                    <Button
                        className="w-full py-2 text-white bg-blue-500 rounded-md hover:bg-blue-600"
                        type="submit"
                    >
                        Submit
                    </Button>
                </form>
            </Form>
        </div>
    );
}

import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form.tsx";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { toast } from "sonner";
import { Input } from "@/components/ui/input.tsx";
import { Button } from "@/components/ui/button.tsx";
import { instance } from "@/utils/axiosInstance.ts";
import { TextArea } from "@radix-ui/themes";

export const TagFormSchema = z.object({
    text: z.string().min(2, {
        message: "Tag Name must be at least 2 characters.",
    }),
    description: z.string().optional(),
});

export function CreateTagDropdown() {
    const form = useForm<z.infer<typeof TagFormSchema>>({
        resolver: zodResolver(TagFormSchema),
        defaultValues: {
            text: "",
            description: "",
        },
    });

    const onSubmit = async (data: z.infer<typeof TagFormSchema>) => {
        try {
            const res = await instance.post(`create-tag`, data, {
                withCredentials: true,
            });

            if (res.status === 200) {
                toast("Tag Created!", {
                    description: `Tag name:: ${data.text}`,
                    action: {
                        label: "Undo",
                        onClick: () => console.log("Undo"),
                    },
                });
            } else {
                toast("Tag Creation Failed!", {
                    description: `Tag name:: ${data.text}`,
                    action: {
                        label: "Try Again",
                        onClick: () => console.log("Try Agained"),
                    },
                });
            }
        } catch (e) {
            console.error(e);
        }
    };
    return (
        <>
            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(onSubmit)}
                    className="w-2/3 space-y-6"
                >
                    <FormField
                        control={form.control}
                        name="text"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Tag Name*</FormLabel>
                                <FormControl>
                                    <Input placeholder="name..." {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="description"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Description</FormLabel>
                                <FormControl>
                                    <TextArea
                                        placeholder="description..."
                                        {...field}
                                    />
                                </FormControl>
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
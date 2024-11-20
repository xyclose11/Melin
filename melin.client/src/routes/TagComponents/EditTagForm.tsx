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
import { Input } from "@/components/ui/input.tsx";
import { Button } from "@/components/ui/button.tsx";
import { instance } from "@/utils/axiosInstance.ts";
import { TagFormSchema } from "@/routes/CustomComponents/Tag/CreateTagDropdown.tsx";
import { tagSchema } from "@/routes/CustomComponents/Tag/TagCreateDropdown.tsx";
import { useToast } from "@/hooks/use-toast.ts";

export function EditTagForm({ tagText }: { tagText: string }) {
    const form = useForm<z.infer<typeof tagSchema>>({
        resolver: zodResolver(TagFormSchema),
        defaultValues: {
            text: tagText,
        },
    });
    const { toast } = useToast();

    const onSubmit = async (data: z.infer<typeof tagSchema>) => {
        try {
            const res = await instance.put(
                `update-tag?curTagName=${tagText}`,
                data,
                {
                    withCredentials: true,
                },
            );

            if (res.status === 200) {
                toast({
                    description: `New Tag name:: ${data.text}`,
                });
            } else {
                toast({
                    description: `Tag name:: ${data.text}`,
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
                                <FormLabel>Edit Tag Name</FormLabel>
                                <FormControl>
                                    <Input placeholder="name..." {...field} />
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

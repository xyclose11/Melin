import {
    Form,
    FormControl,
    FormDescription,
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
import { TextArea } from "@radix-ui/themes";
import { useToast } from "@/hooks/use-toast.ts";
import { ToastAction } from "@/components/ui/toast.tsx";
import { Group } from "@/LibraryViews/GroupLibrary.tsx";

export const GroupFormSchema = z.object({
    name: z.string().min(2, {
        message: "Group Name must be at least 2 characters.",
    }),
    description: z.string().optional(),
});

export function CreateGroupForm({
    handleAddToUserGroup,
}: {
    handleAddToUserGroup: (newGroup: Group) => void;
}) {
    const form = useForm<z.infer<typeof GroupFormSchema>>({
        resolver: zodResolver(GroupFormSchema),
        defaultValues: {
            name: "",
            description: "",
        },
    });

    const { toast } = useToast();
    const onSubmit = async (data: z.infer<typeof GroupFormSchema>) => {
        try {
            const res = await instance.post("create-group", data, {
                withCredentials: true,
            });

            if (res.status === 200) {
                toast({
                    variant: "default",
                    title: `Group: ${data.name} Created Successfully`,
                });
                const newGroup: Group = {
                    id: res.data,
                    name: data.name,
                    description:
                        data.description === undefined ? "" : data.description,
                    updatedAt: "",
                };
                handleAddToUserGroup(newGroup);
            } else {
                toast({
                    variant: "destructive",
                    title: "Unable to Create",
                    action: (
                        <ToastAction altText={"Try Again"}>
                            Try Again
                        </ToastAction>
                    ),
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
                        name="name"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Group Name*</FormLabel>
                                <FormControl>
                                    <Input placeholder="name..." {...field} />
                                </FormControl>
                                <FormDescription>
                                    Groups may contain multiple references, and
                                    other groups
                                </FormDescription>
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
                                <FormDescription>Optional</FormDescription>
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

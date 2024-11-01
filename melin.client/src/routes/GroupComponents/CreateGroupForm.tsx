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
import { toast } from "sonner";
import { Input } from "@/components/ui/input.tsx";
import { Button } from "@/components/ui/button.tsx";
import { instance } from "@/utils/axiosInstance.ts";

const GroupFormSchema = z.object({
    name: z.string().min(2, {
        message: "Group Name must be at least 2 characters.",
    }),
});

export function CreateGroupForm() {
    const form = useForm<z.infer<typeof GroupFormSchema>>({
        resolver: zodResolver(GroupFormSchema),
        defaultValues: {
            name: "",
        },
    });
    async function onSubmit(data: z.infer<typeof GroupFormSchema>) {
        try {
            const res = await instance.post(
                "create-group",
                { data },
                { withCredentials: true },
            );

            if (res.status === 200) {
                toast("Group Created!", {
                    description: `Group name:: ${data.name}`,
                    action: {
                        label: "Undo",
                        onClick: () => console.log("Undo"),
                    },
                });
            } else {
                toast("Group Creation Failed!", {
                    description: `Group name:: ${data.name}`,
                    action: {
                        label: "Try Again",
                        onClick: () => console.log("Try Agained"),
                    },
                });
            }
        } catch (e) {
            console.error(e);
        }
    }
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
                                <FormLabel>Group Name</FormLabel>
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
                    <Button type="submit">Submit</Button>
                </form>
            </Form>
        </>
    );
}

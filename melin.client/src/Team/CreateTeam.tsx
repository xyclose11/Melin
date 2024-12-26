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

const member = z.object({
    EmailAddress: z.string().email(),
    UserName: z.string().min(0).max(256),
});

const teamSchema = z.object({
    name: z.string().min(1).max(512),
    description: z.string().optional(),
    members: z.array(member),
});

export default function CreateTeam() {
    const form = useForm<z.infer<typeof teamSchema>>({
        resolver: zodResolver(teamSchema),
        defaultValues: {
            name: "",
            description: "",
            members: [],
        },
    });

    async function onSubmit(values: z.infer<typeof teamSchema>) {
        try {
            const res = await instance.post("api/Team/create", values, {
                withCredentials: true,
            });

            console.log(res);
            if (res.status === 200) {
                console.log("SUCCESS");
            }
        } catch (e) {
            console.error(e);
        }
    }

    return (
        <div>
            <h1>Create New Team</h1>

            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(onSubmit)}
                    className="space-y-8"
                >
                    <FormField
                        control={form.control}
                        name={"name"}
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Team Name</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="Team Name..."
                                        {...field}
                                    ></Input>
                                </FormControl>
                                <FormDescription>
                                    What will your team be called. * This can be
                                    changed later on *
                                </FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    ></FormField>
                    <FormField
                        control={form.control}
                        name={"description"}
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Description</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="Description"
                                        {...field}
                                    ></Input>
                                </FormControl>
                                <FormDescription></FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    ></FormField>
                    <Button type="submit">Create Team</Button>
                </form>
            </Form>
        </div>
    );
}

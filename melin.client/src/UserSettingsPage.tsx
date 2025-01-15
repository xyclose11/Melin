﻿import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import getUserDetails from "@/api/getUserDetails.ts";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form.tsx";
import { Button } from "@/components/ui/button.tsx";
import { zodResolver } from "@hookform/resolvers/zod";

export const description =
    "A sign up form with first name, last name, email and password inside a card. There's an option to sign up with GitHub and a link to login if you already have an account";

const userSettingsSchema = z
    .object({
        firstName: z.string().min(2).max(256),
        lastName: z
            .string()
            .min(2)
            .max(512, { message: "Max length of 512 Characters" }),
        email: z.string().email({ message: "Invalid Email Address." }).max(512),
        confirmEmail: z
            .string()
            .email({ message: "Invalid Email Address." })
            .max(512),
        password: z.string().min(8).max(128),
        confirmPassword: z.string().min(8).max(128),
    })
    .superRefine(({ password, confirmPassword, email, confirmEmail }, ctx) => {
        if (password !== confirmPassword) {
            ctx.addIssue({
                code: "custom",
                message: "The passwords do not match",
                path: ["confirmPassword"],
            });
        }

        if (email !== confirmEmail) {
            ctx.addIssue({
                code: "custom",
                message: "The provided emails do not match",
                path: ["confirmEmail"],
            });
        }
    });

export function UserSettings() {
    const [currentEmail, setCurrentEmail] = useState("");

    const form = useForm<z.infer<typeof userSettingsSchema>>({
        resolver: zodResolver(userSettingsSchema),
        defaultValues: {
            firstName: "",
            lastName: "",
            email: "",
            confirmEmail: "",
            password: "",
            confirmPassword: "",
        },
    });

    function onSubmit(values: z.infer<typeof userSettingsSchema>) {
        console.log(values);
    }

    useEffect(() => {
        getUserDetails().then((r) => setCurrentEmail(r.email));
    }, []);

    return (
        <Card className="mx-auto max-w-sm">
            <CardHeader>
                <CardTitle className="text-xl">User Settings</CardTitle>
                <CardDescription>Update Account Information</CardDescription>
            </CardHeader>
            <CardContent className="grid gap-4">
                <Form {...form}>
                    <form
                        className="grid grid-cols-2 gap-4"
                        onSubmit={form.handleSubmit(onSubmit)}
                    >
                        <FormField
                            control={form.control}
                            name="firstName"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>First Name</FormLabel>
                                    <FormControl>
                                        <Input {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <FormField
                            control={form.control}
                            name="lastName"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>Last Name</FormLabel>
                                    <FormControl>
                                        <Input {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <FormField
                            control={form.control}
                            name="email"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>Email</FormLabel>
                                    <FormControl>
                                        <Input
                                            placeholder={currentEmail}
                                            type="email"
                                            {...field}
                                        />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <FormField
                            control={form.control}
                            name="confirmEmail"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>Confirm Email</FormLabel>
                                    <FormControl>
                                        <Input type="email" {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <FormField
                            control={form.control}
                            name="password"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>New Password</FormLabel>
                                    <FormControl>
                                        <Input type="password" {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <FormField
                            control={form.control}
                            name="confirmPassword"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>Confirm New Password</FormLabel>
                                    <FormControl>
                                        <Input type="password" {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <Button type="submit">Submit</Button>
                    </form>
                </Form>
            </CardContent>
        </Card>

        // <Card className="mx-auto max-w-sm">
        //     <CardHeader>
        //         <CardTitle className="text-xl">User Settings</CardTitle>
        //         <CardDescription>Update account information</CardDescription>
        //     </CardHeader>
        //     <CardContent>
        //         <div className="grid gap-4">
        //             <div className="grid grid-cols-2 gap-4">
        //                 <div className="grid gap-2">
        //                     <Label htmlFor="first-name">First name</Label>
        //                     <Input id="first-name" placeholder="Max" required />
        //                 </div>
        //                 <div className="grid gap-2">
        //                     <Label htmlFor="last-name">Last name</Label>
        //                     <Input
        //                         id="last-name"
        //                         placeholder="Robinson"
        //                         required
        //                     />
        //                 </div>
        //             </div>
        //             <div className="grid gap-2">
        //                 <Label htmlFor="email">Email</Label>
        //                 <Input
        //                     id="email"
        //                     type="email"
        //                     placeholder="m@example.com"
        //                     required
        //                 />
        //             </div>
        //             <div className="grid gap-2">
        //                 <Label htmlFor="password">Password</Label>
        //                 <Input id="password" type="password" />
        //             </div>
        //             <Button type="submit" className="w-full">
        //                 Save Changes
        //             </Button>
        //         </div>
        //     </CardContent>
        // </Card>
    );
}

export default UserSettings;

import { Button } from "@/components/ui/button";
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Link, useNavigate } from "@tanstack/react-router";

import { useState } from "react";
import { instance } from "@/utils/axiosInstance";
import { useAuth } from "@/utils/AuthProvider.tsx";
import { ToastAction } from "@/components/ui/toast.tsx";
import { useToast } from "@/hooks/use-toast.ts";

export const description =
    "A login form with email and password. There's an option to login with Google and a link to sign up if you don't have an account.";

export function LoginForm() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const { setIsAuthenticated } = useAuth(); // Get the function to update auth state
    const { toast } = useToast();
    const navigate = useNavigate();

    const handleLogin = async (e: any) => {
        e.preventDefault();
        try {
            await instance
                .post(
                    "api/Auth/login",
                    { email, password },
                    { withCredentials: true },
                )
                .then(function (response) {
                    if (response.status === 200) {
                        // show Toast
                        toast({
                            title: "Successfully Logged In",
                            description: `User: ${email} Logged In`,
                        });
                        setIsAuthenticated(true);

                        // route user
                        navigate({ to: "/library" });
                    } else {
                        toast({
                            title: "Login Failed",
                            description: "Please try again later",
                            action: (
                                <ToastAction altText={"Try Again"}>
                                    Try Again
                                </ToastAction>
                            ),
                        });
                        setIsAuthenticated(false);
                    }
                });
        } catch (error) {
            console.error("Login failed:", error);
        }
    };

    return (
        <div className="h-screen mt-24">
            <Card className="mx-auto mb-8 max-w-sm">
                <CardHeader>
                    <CardTitle className="text-2xl">Login</CardTitle>
                    <CardDescription>
                        Enter your email below to login to your account
                    </CardDescription>
                </CardHeader>
                <CardContent>
                    <form onSubmit={handleLogin}>
                        <div className="grid gap-4">
                            <div className="grid gap-2">
                                <Label htmlFor="email">Email</Label>
                                <Input
                                    id="email"
                                    type="email"
                                    placeholder="m@example.com"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    required
                                />
                            </div>
                            <div className="grid gap-2">
                                <div className="flex items-center">
                                    <Label htmlFor="password">Password</Label>
                                    <Input
                                        id="password"
                                        type="password"
                                        placeholder="*******"
                                        value={password}
                                        onChange={(e) =>
                                            setPassword(e.target.value)
                                        }
                                        required
                                    />
                                    <Link
                                        to={"/reset-password"}
                                        className="ml-auto inline-block text-sm underline"
                                    >
                                        Forgot your password?
                                    </Link>
                                </div>
                            </div>
                            <Button type="submit" className="w-full">
                                Login
                            </Button>
                            <Button variant="outline" className="w-full">
                                Login with Google
                            </Button>
                        </div>
                    </form>

                    <div className="mt-4 text-center text-sm">
                        Don&apos;t have an account?{" "}
                        <Link to={"/signup"} className="underline">
                            Sign up
                        </Link>
                    </div>
                </CardContent>
            </Card>
        </div>
    );
}

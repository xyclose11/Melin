import React from "react";
import { NavBar } from "@/Layout.tsx";
import { Toaster } from "@/components/ui/toaster";
import { useAuth } from "@/utils/AuthProvider.tsx";
import { Outlet } from "@tanstack/react-router";

export default function Root({ children }: { children: React.ReactNode }) {
    const { isAuthenticated } = useAuth();

    return (
        <div>
            <NavBar />

            <div className={"flex flex-col"}>
                {!isAuthenticated ? (
                    <main className={"mt-16"}>{children}</main>
                ) : (
                    <main className={"mt-16 flex ml-2"}>
                        <Outlet />
                        <Toaster />
                    </main>
                )}
            </div>
        </div>
    );
}

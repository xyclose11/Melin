import { Workspace } from "@/Workspace.tsx";
import React from "react";
import { NavBar } from "@/Layout.tsx";
import { Toaster } from "@/components/ui/toaster";
import { useAuth } from "@/utils/AuthProvider.tsx";
import { Outlet } from "@tanstack/react-router";

export default function Root({ children }: { children: React.ReactNode }) {
    const { isAuthenticated } = useAuth();

    return (
        <div className="site-section">
            <NavBar />

            <div className={"flex flex-col"}>
                {!isAuthenticated ? (
                    <main className={"mt-16 p-2"}>
                        {children}
                        <Outlet />
                    </main>
                ) : (
                    <main className={"mt-16"}>
                        <Workspace />
                        <Toaster />
                    </main>
                )}
            </div>
        </div>
    );
}

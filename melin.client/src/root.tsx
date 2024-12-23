import { Workspace } from "@/Workspace.tsx";
import React from "react";
import { NavBar } from "@/Layout.tsx";
import { Toaster } from "@/components/ui/toaster";
import { useAuth } from "@/utils/AuthProvider.tsx";
import { Outlet } from "@tanstack/react-router";
import { Footer } from "@/Footer.tsx";

export default function Root({ children }: { children: React.ReactNode }) {
    const { isAuthenticated } = useAuth();

    return (
        <>
            <NavBar />

            <div className={"flex flex-col h-screen"}>
                {!isAuthenticated ? (
                    <main className={"mt-16 p-2"}>
                        {children}
                        <Outlet />
                    </main>
                ) : (
                    <main className={"mt-16 p-2"}>
                        <Workspace />
                        <Toaster />
                    </main>
                )}
                <Footer />
            </div>
        </>
    );
}

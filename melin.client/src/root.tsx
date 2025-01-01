import { Workspace } from "@/Workspace.tsx";
import { WorkspaceToolBar } from "@/CustomComponents/WorkspaceToolBar.tsx";
import { SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar.tsx";
import React from "react";
import { NavBar } from "@/Layout.tsx";
import { Toaster } from "@/components/ui/toaster";
import { useAuth } from "@/utils/AuthProvider.tsx";
import { Outlet } from "@tanstack/react-router";
import { useCookies } from "react-cookie";

export default function Root({ children }: { children: React.ReactNode }) {
    const { isAuthenticated } = useAuth();

    const [cookies] = useCookies(["sidebar:state"]);
    const defaultOpen = cookies["sidebar:state"];

    return (
        <>
            <NavBar />

            <div>
                {!isAuthenticated ? (
                    <main className={"mt-16 p-2 w-screen"}>
                        {children}
                        <Outlet />
                    </main>
                ) : (
                    <SidebarProvider
                        className="overflow-hidden"
                        defaultOpen={defaultOpen}
                    >
                        <WorkspaceToolBar />
                        <main className={"mt-16 p-2 w-screen"}>
                            <SidebarTrigger>
                                <Outlet />
                            </SidebarTrigger>
                            <Workspace />
                        </main>
                        <Toaster />
                    </SidebarProvider>
                )}
            </div>
        </>
    );
}

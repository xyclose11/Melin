import { Workspace } from "@/routes/Workspace.tsx";
import { WorkspaceToolBar } from "@/routes/CustomComponents/WorkspaceToolBar.tsx";
import { SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar.tsx";
import React from "react";
import { NavBar } from "@/routes/Layout.tsx";
import { Toaster } from "@/components/ui/toaster";
import { useAuth } from "@/utils/AuthProvider.tsx";
import { Outlet } from "react-router-dom";

export default function Root({ children }: { children: React.ReactNode }) {
    const { isAuthenticated } = useAuth();
    return (
        <>
            <NavBar />

            <div className={"w-screen min-h-dvh"}>
                {!isAuthenticated ? (
                    <main className={"w-screen mt-16 flex p-2"}>
                        {children}
                        <Outlet />
                    </main>
                ) : (
                    <SidebarProvider>
                        <WorkspaceToolBar />
                        <main className={"w-screen mt-16 flex p-2"}>
                            <SidebarTrigger>{children}</SidebarTrigger>
                            <Workspace />
                        </main>
                        <Toaster />
                    </SidebarProvider>
                )}
            </div>
        </>
    );
}

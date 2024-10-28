import { Workspace } from "@/routes/Workspace.tsx";
import { WorkspaceToolBar } from "@/routes/CustomComponents/WorkspaceToolBar.tsx";
import { SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar.tsx";
import React from "react";
import { NavBar } from "@/routes/Layout.tsx";
import { Toaster } from "@/components/ui/toaster";

export default function Root({ children }: { children: React.ReactNode }) {
    return (
        <>
            <NavBar />

            <div className={"w-screen mt-16 min-h-dvh"}>
                <SidebarProvider>
                    <WorkspaceToolBar />
                    <main className={"w-screen flex p-2 m-2"}>
                        <SidebarTrigger>{children}</SidebarTrigger>
                        <Workspace />
                    </main>
                    <Toaster />
                </SidebarProvider>
            </div>
        </>
    );
}

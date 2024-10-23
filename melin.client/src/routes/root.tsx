import { Workspace } from "@/routes/Workspace.tsx";
import { WorkspaceToolBar } from "@/routes/CustomComponents/WorkspaceToolBar.tsx";
import { SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar.tsx";
import React from "react";
import { NavBar } from "@/routes/Layout.tsx";

export default function Root({ children }: {children: React.ReactNode }) {
    return (
        <>
            <NavBar />

        <div className={"mt-16 min-h-dvh flex content-center"}>
            <SidebarProvider>
                <WorkspaceToolBar/>
                <main>
                    <SidebarTrigger>
                        {children}
                    </SidebarTrigger>
                    <Workspace />
                </main>
            </SidebarProvider>
        </div>
        </>
    );
}

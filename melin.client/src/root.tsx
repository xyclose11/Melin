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

    const [cookies, setCookie] = useCookies(["sidebar:state"]);
    const defaultOpen = cookies !== null ? cookies["sidebar:state"] : true;

    function onSidebarChange() {
        setCookie("sidebar:state", !defaultOpen);
    }
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
                    <SidebarProvider
                        onChange={onSidebarChange}
                        defaultOpen={defaultOpen}
                    >
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

import { Outlet } from "react-router-dom";
import { NavBar } from "@/routes/Layout.tsx";
import { Workspace } from "@/routes/Workspace.tsx";
import { ThemeProvider } from "@/components/theme-provider.tsx";

export default function Root() {
    return (
        <>
            <NavBar />
            <div
                className={
                    "flex mt-16 justify-center items-center h-screen w-screen"
                }
            >
                <Workspace />
                <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
                    <Outlet />
                </ThemeProvider>
            </div>
        </>
    );
}

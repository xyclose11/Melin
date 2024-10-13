import { Outlet } from "react-router-dom";
import { NavBar } from "@/routes/Layout.tsx";
import { Workspace } from "@/routes/Workspace.tsx";

export default function Root() {
    return (
        <>
            <NavBar />
            <div
                className={
                    "flex mt-16 justify-center items-center h-screen w-screen overflow-auto"
                }
            >
                <Workspace />
                <Outlet />
            </div>
        </>
    );
}

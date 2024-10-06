import { Outlet } from "react-router-dom";
import { NavBar } from "@/routes/Layout.tsx";

export default function Root() {
    return (
        <>
            <NavBar />
            <div
                className={"flex justify-center items-center h-screen w-screen"}
            >
                <Outlet />
            </div>
        </>
    );
}

import { Outlet } from "react-router-dom";
import { NavBar } from "@/routes/Layout.tsx";

export default function Root() {
    return (
        <>
            <NavBar />
            <div id="detail">
                <Outlet />
            </div>
        </>
    );
}

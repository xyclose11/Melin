import { Outlet } from "@tanstack/react-router";

export function Workspace() {
    return (
        <div className="ml-12 flex justify-center">
            <Outlet />
        </div>
    );
}

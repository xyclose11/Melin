import { Outlet } from "@tanstack/react-router";

export function Workspace() {
    return (
        <div className="flex-1 ml-12 mr-36">
            <div className="flex">
                <Outlet />
            </div>
        </div>
    );
}

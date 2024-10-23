import { Outlet } from "react-router-dom";

export function Workspace() {
    return (
            <div className="flex p-4 justify-center flex-wrap">
                <Outlet />
            </div>
    );
}

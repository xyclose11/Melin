import { Outlet } from "react-router-dom";

export function Workspace() {
    return (
            <div className="flex-grow ml-12 mr-48 justify-center">
                <Outlet />
            </div>
    );
}

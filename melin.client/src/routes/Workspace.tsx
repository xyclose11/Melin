import { Outlet } from "react-router-dom";

export function Workspace() {
    return (
        <div className="flex-grow ml-12 mr-36 justify-center">
            <div className="flex gap-3">
                <Outlet />
            </div>
        </div>
    );
}

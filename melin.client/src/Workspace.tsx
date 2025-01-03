﻿import { Outlet } from "@tanstack/react-router";

export function Workspace() {
    return (
        <div className="w-full ml-12 mr-36 justify-center">
            <div className="flex">
                <Outlet />
            </div>
        </div>
    );
}
